using System.Collections.Generic;
using System.Linq;
using Data_Access_Layer;
using Domain_Model;

namespace MySql_Database
{
    public class DataServices: IDataService
    {
        public IList<Movie> GetMovies(int limit, int offset)
        {
            using (var db_movies = new MySqlDatabaseContext())
            {
                return db_movies.Movies
                    .OrderBy(m => m.Id)
                    .Skip(offset)
                    .Take(limit)
                    .ToList();
            }
        }

        public Movie GetMovieById(int id)
        {
            using (var db = new MySqlDatabaseContext())
            {
                return db.Movies.FirstOrDefault(c => c.Id == id);
            }
        }

        public int GetNumberOfMovies()
        {
            using (var db = new MySqlDatabaseContext())
            {
                return db.Movies.Count();
            }

        }
        public void AddNewMovie(Movie movie)
        {
            using (var db = new MySqlDatabaseContext())
            {
                movie.Id = db.Movies.Max(m => m.Id) + 1;
                db.Add(movie);
                db.SaveChanges();
            }
        }
    }
}