using CloudScale.Movies.Messages;
using Nimbus.Handlers;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CloudScale.Movies.Models;
using System.Data.Entity.SqlServer;

namespace CloudScale.Movies.DataService.Handlers
{
    public class GetMoviesHandler : IHandleRequest<GetMoviesRequest, GetMoviesResponse>
    {
        private readonly IMoviesDataContext db;

        public GetMoviesHandler(IMoviesDataContext db)
        {
            this.db = db;
        }

        private IEnumerable<Movie> GetRandomMovies()
        {
            Random random = new Random(DateTimeOffset.Now.Millisecond);

            List<Movie> movies = new List<Movie>();

            int movieCount = db.Movies.Count();
            if (movieCount > 10)
            {
                int randomSkip = random.Next(0, movieCount - 10);
                movies.AddRange(db.Movies.OrderBy(p => p.OriginalTitle).Skip(randomSkip).Take(10).ToList());
            }
            else
            {
                movies.AddRange(db.Movies.OrderBy(p => p.OriginalTitle).ToList());
            }
            return movies;
        }

        private IEnumerable<Movie> GetSearchedMovies(string searchTerm)
        {
            List<Movie> movies = new List<Movie>();

            movies.AddRange(db.Movies.Where(p => p.OriginalTitle.Contains(searchTerm) || SqlFunctions.SoundCode(p.OriginalTitle) == SqlFunctions.SoundCode(searchTerm)).ToList());

            return movies.Take(10);
        }

        public async Task<GetMoviesResponse> Handle(GetMoviesRequest request)
        {
            Log.Information("Movies List Requested");

            return await Task.Run(() =>
            {
                List<Movie> movies = new List<Movie>();

                if (string.IsNullOrWhiteSpace(request.Search))
                    movies.AddRange(GetRandomMovies());
                else
                    movies.AddRange(GetSearchedMovies(request.Search));

                return new GetMoviesResponse() { Movies = movies };
            });
        }
    }
}
