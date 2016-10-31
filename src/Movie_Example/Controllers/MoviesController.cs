using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data_Access_Layer;
using Domain_Model;
using Microsoft.AspNetCore.Mvc;
using Movie_Example.ViewModels;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace Movie_Example.Controllers
{ 
    [Route("api/movies")]
    public class MoviesController : Controller
    {
        public IDataService _iDataService;
        public MoviesController(IDataService iDataservice)
        {
            _iDataService = iDataservice;
        }
        //[HttpGet]
        //[Route("")]
        //public IActionResult Get(int page = 0, int pageSize = 5)
        //{
        //    int limit = pageSize;
        //    int offset = page * pageSize;
        //    var movieList = _iDataService.GetMovies(limit, offset).Select(m => new MovieViewModel { MovieId = m.Id, Title = m.Title, Year = m.Year });
        //    if (movieList == null) return NotFound();
            
        //    var result = new
        //    {
        //        Data = movieList
        //    };

        //    return Ok(result);
        //}
        

        [HttpGet]
        [Route("")]
        public IActionResult Get(int page = 0, int pageSize = 5)
        {
            int limit = pageSize;
            int offset = page * pageSize;
            var movieList = _iDataService.GetMovies(limit, offset).Select(m => new MovieViewModel
            {
                Url = Url.Action("Get", "Movies", new { m.Id }, Url.ActionContext.HttpContext.Request.Scheme),
                Title = m.Title,
                Year = m.Year
            });

            var totalMovieNumber = _iDataService.GetNumberOfMovies();
            var lastpage = totalMovieNumber / pageSize;

            var prev = page <= 0 ? null : Url.Action("Get","Movies", new { page = page - 1, pageSize },Url.ActionContext.HttpContext.Request.Scheme);
            var next = page >= lastpage ? null : Url.Action("Get", "Movies", new { page = page + 1, pageSize }, Url.ActionContext.HttpContext.Request.Scheme);

            var result = new
            {
                Total = totalMovieNumber,
                Prev = prev,
                Next = next,
                Data = movieList
            };

            return Ok(result);
        }

        [HttpGet]
        [Route("{id}")]
        public IActionResult Get(int id)
        {
            var data = _iDataService.GetMovieById(id);
            if (data == null) return NotFound();

            var url = Url.Action("Get", "Movies", new { data.Id }, Url.ActionContext.HttpContext.Request.Scheme);

            var model = ModelFactory.Map(data, url);

            return Ok(model);
        }
        


        [HttpPost]
        public IActionResult Post([FromBody] MovieViewModel model)
        {
            var movie = new Movie { Title = model.Title, Year = model.Year };
            _iDataService.AddNewMovie(movie);
            var url = Url.Action("Get", "Movies", new {movie.Id}, Url.ActionContext.HttpContext.Request.Scheme);
            var movieAdded = ModelFactory.Map(movie, url);
            return Ok(movieAdded);
        }
    }
}
