using System.Collections;
using System.Collections.Generic;
using Domain_Model;

namespace Data_Access_Layer
{
    public interface IDataService
    {
        IList<Movie> GetMovies(int limit, int offset);
        Movie GetMovieById(int id);
        int GetNumberOfMovies();
        void AddNewMovie(Movie movie);
    }
}