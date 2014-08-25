using System.Linq;
using System.Threading.Tasks;
using CloudScale.Movies.Data;
using CloudScale.Movies.Messages;
using CloudScale.Movies.Models;
using Nimbus;
using Nimbus.Handlers;
using Serilog;

namespace CloudScale.Movies.DataService.Handlers
{
    public class MovieScoreHandler : IHandleMulticastEvent<NewScoreEvent>
    {
        private readonly IBus bus;
        private readonly IMoviesDataContext db;

        /// <summary>
        ///     Initializes a new instance of the NewMovieHandler class.
        /// </summary>
        public MovieScoreHandler(IBus bus, IMoviesDataContext db)
        {
            this.bus = bus;
            this.db = db;
        }

        public async Task Handle(NewScoreEvent busEvent)
        {
            Log.Information("{Type} New Score for {Movie}", GetType().FullName, busEvent.MovieId);

            MovieScore movieScore =
                db.MovieScores.FirstOrDefault(p => p.MovieId == busEvent.MovieId && p.UserId == busEvent.UserId);

            if (movieScore == null)
            {
                movieScore = new MovieScore();
                movieScore.MovieId = busEvent.MovieId;
                movieScore.UserId = busEvent.UserId;

                db.MovieScores.Add(movieScore);
            }

            movieScore.Score = busEvent.Score;

            if (db.ChangeTracker.HasChanges())
                await db.SaveChangesAsync();
        }
    }
}