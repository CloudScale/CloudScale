using CloudScale.Movies.Messages;
using Nimbus;
using Nimbus.Handlers;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudScale.Movies.DataService.Handlers
{
    public class MovieScoreHandler : IHandleMulticastEvent<NewScoreEvent>
    {
        private readonly IBus bus;
        private readonly IMoviesDataContext db;

        /// <summary>
        /// Initializes a new instance of the NewMovieHandler class.
        /// </summary>
        public MovieScoreHandler(IBus bus, IMoviesDataContext db)
        {
            this.bus = bus;
            this.db = db;
        }

        public async Task Handle(NewScoreEvent busEvent)
        {
            Log.Information("{Type} New Score for {Movie}", GetType().FullName, busEvent.MovieName);

            var movieScore = db.MovieScores.FirstOrDefault(p => p.MovieName == busEvent.MovieName && p.PersonName == busEvent.PersonName);

            if (movieScore == null)
            {
                movieScore = new Models.MovieScore();
                movieScore.MovieName = busEvent.MovieName;
                movieScore.PersonName = busEvent.PersonName;
                db.MovieScores.Add(movieScore);
            }

            movieScore.Score = busEvent.Score;

            if (db.ChangeTracker.HasChanges())
                await db.SaveChangesAsync();
        }
    }
}
