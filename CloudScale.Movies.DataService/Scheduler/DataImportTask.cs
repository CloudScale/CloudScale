using System.Linq;
using CloudScale.Movies.Data;
using CloudScale.Movies.Models;
using FluentScheduler;
using Newtonsoft.Json;
using Serilog;

namespace CloudScale.Movies.DataService.Scheduler
{
    public class DataImportTask : ITask
    {
        /// <summary>
        ///     Initializes a new instance of the DataImportTask class.
        /// </summary>
        private readonly IMoviesDataContext db;

        /// <summary>
        ///     Initializes a new instance of the NewMovieHandler class.
        /// </summary>
        public DataImportTask(IMoviesDataContext db)
        {
            this.db = db;
        }

        public void Execute()
        {
            Log.Information("Executing Task");

            var updateQuery = from m in db.Movies
                join l in db.MovieLookupResults on m.Id equals l.Id
                where (m.OriginalTitle == null && l.Data != null) || m.TMDBId == 0
                select new {Movie = m, LookupData = l.Data};

            foreach (var item in updateQuery)
            {
                Log.Information("Transferring Lookup Data from Cache to Movie Object");

                var result = JsonConvert.DeserializeObject<TMDBLookupResult>(item.LookupData);
                item.Movie.TMDBId = result.Id;
                if (!string.IsNullOrEmpty(result.ReleaseDate))
                    item.Movie.Year = int.Parse(result.ReleaseDate.Split('-')[0]);
                item.Movie.OriginalTitle = result.OriginalTitle;
                item.Movie.Rating = result.VoteAverage;
                item.Movie.PosterPath = result.PosterPath;
                item.Movie.BackdropPath = result.BackdropPath;
            }

            if (db.ChangeTracker.HasChanges())
            {
                int changes = db.SaveChanges();
                Log.Information("Saved {Changes} Changes", changes);
            }
        }
    }
}