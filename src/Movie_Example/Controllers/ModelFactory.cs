using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Domain_Model;
using Microsoft.AspNetCore.Mvc.Routing;
using Movie_Example.ViewModels;

namespace Movie_Example.Controllers
{
    public class ModelFactory
    {
        private static readonly IMapper MovieMapper;
        static ModelFactory()
        {
            var movieConfig = new MapperConfiguration(acfg => acfg.CreateMap<Movie, MovieViewModel>());
            MovieMapper = movieConfig.CreateMapper();
        }
        

        public static MovieViewModel Map(Movie movie, string url)
        {
            if (movie == null) return null;

            var annotationModel = MovieMapper.Map<MovieViewModel>(movie);
            annotationModel.Url = url;

            return annotationModel;
        }
    }
}
