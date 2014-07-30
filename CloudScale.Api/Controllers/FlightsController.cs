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

namespace CloudScale.Api.Controllers
{
    [RoutePrefix("flights")]
    public class FlightsController : ApiController
    {
        private readonly IBus bus;

        public FlightsController(IBus bus)
        {
            if (bus == null) throw new ArgumentNullException("bus");
            
            this.bus = bus;
        }

        // GET api/values
        [Route("new/{prefix}/{number}")]
        [HttpGet]
        public async Task<string> Get(string prefix, int number)
        {
            await bus.Send<NewFlightCommand>(new NewFlightCommand(prefix, number));

            return "Thanks for the request";
        }

        [Route("")]
        [HttpGet]
        public async Task<GetFlightsResponse> GetFlights()
        {
            return await bus.Request<GetFlightsRequest, GetFlightsResponse>(new GetFlightsRequest());
        }
    }
}
