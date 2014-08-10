using CloudScale.Movies.Messages;
using Nimbus.Handlers;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CloudScale.Movies.Models;

namespace CloudScale.Movies.DataService.Handlers
{
    public class GetMoviesHandler : IHandleRequest<GetMoviesRequest, GetMoviesResponse>
    {
        private readonly IMoviesDataContext db;

        public GetMoviesHandler(IMoviesDataContext db)
        {
            this.db = db;
        }

        public async Task<GetMoviesResponse> Handle(GetMoviesRequest request)
        {
            Log.Information("Movies List Requested");

            return await Task.Run(() =>
            {
                Random random = new Random(DateTimeOffset.Now.Millisecond);

                List<Movie> movies = new List<Movie>();

                int movieCount = db.Movies.Count();
                if (movieCount > 10)
                {
                    int randomSkip = random.Next(0, movieCount - 10);
                    movies = db.Movies.OrderBy(p => p.OriginalTitle).Skip(randomSkip).Take(10).ToList();
                }
                else
                {
                    movies = db.Movies.OrderBy(p => p.OriginalTitle).ToList();
                }

                return new GetMoviesResponse() { Movies = movies };
            });
        }
    }
}
