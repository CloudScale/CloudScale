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

namespace CloudScale.Api.Controllers
{
    [EnableCors("*", "*", "*")]
    [RoutePrefix("movies")]
    public class MoviesController : ApiController
    {
        private readonly IBus bus;

        public MoviesController(IBus bus)
        {
            if (bus == null) throw new ArgumentNullException("bus");

            this.bus = bus;
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
            await bus.Publish<NewScoreEvent>(new NewScoreEvent(score.MovieName, score.PersonName, score.Score));
        }

        [Route("")]
        [HttpGet]
        public async Task<IEnumerable<Movie>> GetMovies()
        {
            GetMoviesResponse movies = await bus.Request<GetMoviesRequest, GetMoviesResponse>(new GetMoviesRequest(), TimeSpan.FromSeconds(2));

            return movies.Movies;
        }

        [Route("search/{searchString}")]
        [HttpGet]
        public async Task<IEnumerable<Movie>> SearchMovies(string searchString)
        {
            GetMoviesResponse movies = await bus.Request<GetMoviesRequest, GetMoviesResponse>(new GetMoviesRequest() { Search = searchString }, TimeSpan.FromSeconds(2));

            return movies.Movies;
        }

        [Route("random")]
        [HttpGet]
        public async Task<Movie> GetRandomMovie()
        {
            GetMoviesResponse movies = await bus.Request<GetMoviesRequest, GetMoviesResponse>(new GetMoviesRequest(), TimeSpan.FromSeconds(2));

            return movies.Movies.FirstOrDefault();
        }
    }
}
