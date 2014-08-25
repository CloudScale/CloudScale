using System.Threading.Tasks;
using CloudScale.Movies.Data;
using CloudScale.Movies.Messages;
using CloudScale.Movies.Models;
using Nimbus;
using Nimbus.Handlers;
using Serilog;

namespace CloudScale.Movies.DataService.Handlers
{
    public class LookupMovieHandler : IHandleMulticastEvent<LookupMovieEvent>
    {
        private readonly IBus bus;
        private readonly IMoviesDataContext db;

        /// <summary>
        ///     Initializes a new instance of the NewMovieHandler class.
        /// </summary>
        public LookupMovieHandler(IBus bus, IMoviesDataContext db)
        {
            this.bus = bus;
            this.db = db;
        }

        public async Task Handle(LookupMovieEvent busEvent)
        {
            Log.Information("Processing Looked Up Movie {Movie}", busEvent.Id);

            db.MovieLookupResults.Add(new MovieLookupResults
            {
                Id = busEvent.Id,
                Name = busEvent.Name,
                Data = busEvent.Data
            });

            if (db.ChangeTracker.HasChanges())
                await db.SaveChangesAsync();
        }
    }
}