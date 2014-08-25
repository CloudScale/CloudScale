using System.Linq;
using System.Threading.Tasks;
using CloudScale.Movies.Data;
using CloudScale.Movies.Messages;
using Nimbus;
using Nimbus.Handlers;
using Serilog;

namespace CloudScale.Movies.DataService.Handlers
{
    public class NewMovieHandler : IHandleMulticastEvent<NewMovieEvent>
    {
        private readonly IBus bus;
        private readonly IMoviesDataContext db;

        /// <summary>
        ///     Initializes a new instance of the NewMovieHandler class.
        /// </summary>
        public NewMovieHandler(IBus bus, IMoviesDataContext db)
        {
            this.bus = bus;
            this.db = db;
        }

        public async Task Handle(NewMovieEvent busEvent)
        {
            Log.Information("{Type} New Movie {Movie}", GetType().FullName, busEvent.Movie);

            if (!db.Movies.Any(p => p.Name == busEvent.Movie.Name))
            {
                db.Movies.Add(busEvent.Movie);

                if (db.ChangeTracker.HasChanges())
                    await db.SaveChangesAsync();
            }
        }
    }
}