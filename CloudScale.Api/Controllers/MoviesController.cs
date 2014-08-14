using Nimbus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using CloudScale.Movies.Messages;
using System.Web.Http.Cors;
using CloudScale.Movies.Models;
using CloudScale.Movies.Data;
using System.Data.Entity.SqlServer;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using System.Security.Claims;
using CloudScale.Api.Filters;
using System.Web.Http.ValueProviders;
using System.Web.Http.Controllers;

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
        public async Task<string> NewMovie([FromBody]string name)
        {
            await bus.Publish<NewMovieEvent>(new NewMovieEvent(name));

            return "New Movie '" + name + "' submitted for processing";
        }

        [Route("vote")]
        [HttpPost]
        public async Task Vote([FromBody]MovieScore score)
        {
            Guid userId = (Guid)ActionContext.Request.Properties["UserId"]; 

            await bus.Publish<NewScoreEvent>(new NewScoreEvent(score.MovieId.Value, userId, score.Score));
        }

        private IEnumerable<Movie> GetRandomMovies()
        {
            Random random = new Random(DateTimeOffset.Now.Millisecond);

            List<Movie> movies = new List<Movie>();

            int movieCount = db.Movies.Where(p => p.TMDBId != 0).Count();
            if (movieCount > 10)
            {
                int randomSkip = random.Next(0, movieCount - 10);
                movies.AddRange(db.Movies.Where(p => p.TMDBId != 0).OrderBy(p => p.OriginalTitle).Skip(randomSkip).Take(10).ToList());
            }
            else
            {
                movies.AddRange(db.Movies.Where(p => p.TMDBId != 0).OrderBy(p => p.OriginalTitle).ToList());
            }

            return movies;
        }

        private IEnumerable<Movie> GetSearchedMovies(string searchTerm)
        {
            List<Movie> movies = new List<Movie>();

            movies.AddRange(db.Movies.Where(p => p.OriginalTitle.Contains(searchTerm) || SqlFunctions.SoundCode(p.OriginalTitle) == SqlFunctions.SoundCode(searchTerm)).ToList());

            return movies.Take(10);
        }

        [Route("")]
        [HttpGet]
        public async Task<IEnumerable<Movie>> GetMovies()
        {
            return await Task.Run(() => { return GetRandomMovies(); });
        }

        [Route("search/{searchString}")]
        [HttpGet]
        public async Task<IEnumerable<Movie>> SearchMovies(string searchString)
        {
            return await Task.Run(() => { return GetSearchedMovies(searchString); });
        }

        [Route("random")]
        [HttpGet]
        public async Task<MovieViewModel> GetRandomMovie()
        {
            Guid userId = (Guid)ActionContext.Request.Properties["UserId"];
            Random random = new Random(DateTimeOffset.Now.Millisecond);

            return await Task.Run(() =>
            {
                IEnumerable<Movie> list = GetRandomMovies();

                int skip = random.Next(list.Count());
                Movie movie = list.Skip(skip).FirstOrDefault();

                MovieScore movieScore = db.MovieScores.FirstOrDefault(p => p.MovieId == movie.Id && p.UserId == userId);

                MovieViewModel viewModel = new MovieViewModel();
                
                viewModel.Id = movie.Id;
                viewModel.Title = movie.OriginalTitle;
                viewModel.Rating = movie.Rating;
                viewModel.PosterPath = movie.PosterPath;
                viewModel.BackdropPath = movie.BackdropPath;

                if (movieScore != null)
                    viewModel.UserRating = movieScore.Score; 

                return viewModel;
            });
        }
    }
}
