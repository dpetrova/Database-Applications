using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using _05.CodeFirst_Movies;
using Formatting = System.Xml.Formatting;

namespace _06.QueryTheDatabase
{
    class Program
    {
        static void Main()
        {
            var context = new MoviesEntities();

            //1.	Adult Movies
            //Export all adult movies. Order them by title and by ratings received as secondary criteria. 
            //Select each movie's title and the number of ratings received.

            var adultMovies = context.Movies
                .Where(m => m.AgeRestriction == AgeRestriction.Adult)
                .OrderBy(m => m.Title)
                .ThenByDescending(m => m.Ratings.Count)
                .Select(m => new
                {
                    title = m.Title,
                    ratingsGiven = m.Ratings.Count
                });

            string adultMoviesJson = JsonConvert.SerializeObject(adultMovies, Newtonsoft.Json.Formatting.Indented);
            File.WriteAllText("../../adult-movies.json", adultMoviesJson);


            // Query 2.Rated Movies by User:
            var ratedMoviesByUser = context.Users
                .Where(u => u.Username == "jmeyery")
                .Select(u => new
                {
                    username = u.Username,
                    ratedMovies = u.Ratings
                        .OrderBy(r => r.Movie.Title)
                        .Select(r => new
                        {
                            title = r.Movie.Title,
                            userRating = r.Stars,
                            averageRating = r.Movie.Ratings.Average(rStars => rStars.Stars)
                        })
                });
            string ratedMoviesByUserJson = JsonConvert.SerializeObject(ratedMoviesByUser, Newtonsoft.Json.Formatting.Indented);
            File.WriteAllText("../../rated-movies-by-jmeyery.json", ratedMoviesByUserJson);


            //// Query 3.Top 10 Favourite Movies:
            var favouriteMovies = context.Movies
                .Where(m => m.AgeRestriction == AgeRestriction.Teen)
                .OrderByDescending(m => m.UsersThatMovieIsFavourite.Count)
                .ThenBy(m => m.Title)
                .Take(10)
                .Select(m => new
                {
                    isbn = m.Isbn,
                    title = m.Title,
                    favouritedBy = m.UsersThatMovieIsFavourite.Select(u => u.Username)
                });

            string favouriteMoviesJson = JsonConvert.SerializeObject(favouriteMovies, Newtonsoft.Json.Formatting.Indented);
            File.WriteAllText("../../top-10-favourite-movies.json", favouriteMoviesJson);

        }
    }
}
