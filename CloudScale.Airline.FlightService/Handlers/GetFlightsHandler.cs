//using CloudScale.Airline.Messages;
//using Raven.Client;
//using Serilog;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using Nimbus.Handlers;
//using CloudScale.Airline.Models;

//namespace CloudScale.Airline.FlightService.Handlers
//{
//    public class GetFlightsHandler : IHandleRequest<GetFlightsRequest, GetFlightsResponse>
//    {
//        private readonly IDocumentSession session;
//        public GetFlightsHandler(IDocumentSession session)
//        {
//            this.session = session;
//        }

//        public async Task<GetFlightsResponse> Handle(GetFlightsRequest request)
//        {
//            Log.Information("GetFlightsHandler();");

//            return await Task.Run<GetFlightsResponse>(() =>
//            {
//                IQueryable<Flight> query = session.Query<Flight>();
//                GetFlightsResponse response = new GetFlightsResponse { Flights = query.ToList() };
//                Log.Information("There are {Resultscount} results", response.Flights.Count());
//                return response;
//            });
//        }
//    }
//}
