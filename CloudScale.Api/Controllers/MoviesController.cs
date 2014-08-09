using Nimbus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using CloudScale.Airline.Messages;
using CloudScale.Airline.Models;
using CloudScale.Movies.Messages;
using System.Web.Http.Cors;

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

        // GET api/values
        [Route("new/{name}")]
        [HttpGet]
        public async Task<string> Get(string name)
        {
            await bus.Publish<NewMovieEvent>(new NewMovieEvent(name));

            return "New Movie '" + name + "' submitted for processing";
        }

        [Route("")]
        [HttpGet]
        public async Task<IEnumerable<CloudScale.Movies.Models.Movie>> Get()
        {
            GetMoviesResponse movies = await bus.Request<GetMoviesRequest, GetMoviesResponse>(new GetMoviesRequest(), TimeSpan.FromSeconds(2));

            return movies.Movies;
        }
    }
}
