using CloudScale.Movies.Data;
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
            Log.Information("{Type} New Score for {Movie}", GetType().FullName, busEvent.MovieId);

            var movieScore = db.MovieScores.FirstOrDefault(p => p.MovieId == busEvent.MovieId && p.UserId == busEvent.UserId);

            if (movieScore == null)
            {
                movieScore = new Models.MovieScore();
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
