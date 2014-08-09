using CloudScale.Movies.Messages;
using Nimbus.Handlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudScale.Movies.DataService.Handlers
{
    public class IsRegisteredHandler : IHandleRequest<IsRegisteredRequest, IsRegisteredResponse>
    {
        private readonly IMoviesDataContext db;

        /// <summary>
        /// Initializes a new instance of the NewMovieHandler class.
        /// </summary>
        public IsRegisteredHandler(IMoviesDataContext db)
        {
            this.db = db;
        }

        public async Task<IsRegisteredResponse> Handle(IsRegisteredRequest request)
        {
            bool movieExists = db.Movies.Any(p => p.Name == request.Name);
            bool dataExists = db.MovieLookupResults.Any(p => p.Name == request.Name);

            return await Task.Run(() => new IsRegisteredResponse() { Registered = movieExists && dataExists });
        }
    }
}
