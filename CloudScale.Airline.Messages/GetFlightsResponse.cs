using System;
using System.Collections.Generic;
using System.Linq;
using Nimbus.MessageContracts;
using CloudScale.Airline.Models;

namespace CloudScale.Airline.Messages
{
    public class GetFlightsResponse : IBusResponse
    {
        public virtual List<Flight> Flights { get; set; }

        public GetFlightsResponse()
        {
            Flights = new List<Flight>();
        }
    }
}
