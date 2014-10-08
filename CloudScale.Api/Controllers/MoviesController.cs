using System;
using System.Collections.Generic;
using System.Data.Entity.SqlServer;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;
using CloudScale.Movies.Data;
using CloudScale.Movies.Messages;
using CloudScale.Movies.Models;
using Nimbus;

namespace CloudScale.Api.Controllers
{
    [Authorize]
    [EnableCors("*", "*", "*")]
    [RoutePrefix("movies")]
    public class MoviesController : ApiController
    {
        private readonly IBus bus;
        private readonly IMoviesDataContext db;

        public MoviesController(IBus bus, IMoviesDataContext db)
        {
            if (bus == null) throw new ArgumentNullException("bus");
            if (db == null) throw new ArgumentNullException("db");

            this.bus = bus;
            this.db = db;
        }

        [Route("new")]
        [HttpPost]
        public async Task<string> NewMovie([FromBody] string name)
        {
            await bus.Publish(new NewMovieEvent(name));

            return "New Movie '" + name + "' submitted for processing";
        }

        [Route("vote")]
        [HttpPost]
        public async Task Vote([FromBody] MovieScore score)
        {
            var userId = (Guid) ActionContext.Request.Properties["UserId"];

            await bus.Publish(new NewScoreEvent(score.MovieId.Value, userId, score.Score));
        }

        private IEnumerable<Movie> GetRandomMovies()
        {
            var random = new Random(DateTimeOffset.Now.Millisecond);

            var movies = new List<Movie>();

            int movieCount = db.Movies.Count(p => p.TMDBId != 0);
            if (movieCount > 10)
            {
                int randomSkip = random.Next(0, movieCount - 10);
                movies.AddRange(
                    db.Movies.Where(p => p.TMDBId != 0).OrderBy(p => p.OriginalTitle).Skip(randomSkip).Take(10).ToList());
            }
            else
            {
                movies.AddRange(db.Movies.Where(p => p.TMDBId != 0).OrderBy(p => p.OriginalTitle).ToList());
            }

            return movies;
        }

        private IEnumerable<Movie> GetSearchedMovies(string searchTerm)
        {
            var movies = new List<Movie>();

            movies.AddRange(
                db.Movies.Where(
                    p =>
                        p.OriginalTitle.Contains(searchTerm) ||
                        SqlFunctions.SoundCode(p.OriginalTitle) == SqlFunctions.SoundCode(searchTerm)).ToList());

            return movies.Take(10);
        }

        [Route("")]
        [HttpGet]
        public async Task<IEnumerable<Movie>> GetMovies()
        {
            return await Task.Run(() => GetRandomMovies());
        }

        [Route("search/{searchString}")]
        [HttpGet]
        public async Task<IEnumerable<Movie>> SearchMovies(string searchString)
        {
            return await Task.Run(() => GetSearchedMovies(searchString));
        }

        [Route("random")]
        [HttpGet]
		[AllowAnonymous]
        public async Task<MovieViewModel> GetRandomMovie()
        {
            var userId = (Guid) ActionContext.Request.Properties["UserId"];
            var random = new Random(DateTimeOffset.Now.Millisecond);

            return await Task.Run(() =>
            {
                List<Movie> list = GetRandomMovies().ToList();

                int skip = random.Next(list.Count());
                Movie movie = list.Skip(skip).FirstOrDefault();

                MovieScore movieScore = db.MovieScores.FirstOrDefault(p => p.MovieId == movie.Id && p.UserId == userId);

                var viewModel = new MovieViewModel();

	            if (movie != null)
	            {
		            viewModel.Id = movie.Id;
		            viewModel.Title = movie.OriginalTitle;
		            viewModel.Rating = movie.Rating;
		            viewModel.PosterPath = movie.PosterPath;
		            viewModel.BackdropPath = movie.BackdropPath;
	            }

	            if (movieScore != null)
                    viewModel.UserRating = movieScore.Score;

                return viewModel;
            });
        }
    }
}