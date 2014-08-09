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
                
                int randomSkip = random.Next(0, db.Movies.Count() - 10);
                List<Movie> movies = db.Movies.OrderBy(p => p.OriginalTitle).Skip(randomSkip).Take(10).ToList();

                return new GetMoviesResponse() { Movies = movies };
            });
        }
    }
}
