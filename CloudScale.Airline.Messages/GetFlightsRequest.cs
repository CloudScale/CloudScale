using System;
using System.Collections.Generic;
using System.Linq;
using Nimbus.MessageContracts;
using CloudScale.Airline.Models;

namespace CloudScale.Airline.Messages
{
    public class GetFlightsRequest : IBusRequest<GetFlightsRequest, GetFlightsResponse>
    {
        /// <summary>
        /// Initializes a new instance of the GetFlightsRequest class.
        /// </summary>
        public GetFlightsRequest()
        {

        }
    }
}
