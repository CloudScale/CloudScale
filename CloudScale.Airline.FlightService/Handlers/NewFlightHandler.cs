using CloudScale.Airline.Messages;
using Raven.Client;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nimbus.Handlers;
using CloudScale.Airline.Models;

namespace CloudScale.Airline.FlightService.Handlers
{
    public class NewFlightHandler : IHandleCommand<NewFlightCommand>
    {
        private readonly IDocumentSession session;

        public NewFlightHandler(IDocumentSession session)
        {
            this.session = session;
        }

        public async Task Handle(NewFlightCommand busCommand)
        {
            await Task.Run(() =>
            {
                Log.Information("NewFlightHandler();");

                IQueryable<Flight> newVariable = session.Query<Flight>().Where(p => p.Prefix == busCommand.Prefix && p.Number == busCommand.Number);
                if (newVariable.Any())
                {
                    Log.Error("Flight {Prefix}{Number} already exists", busCommand.Prefix, busCommand.Number);
                }
                else
                {
                    Log.Information("Creating new Flight {Prefix}{Number}", busCommand.Prefix, busCommand.Number);
                    session.Store(new Flight(busCommand.Prefix, busCommand.Number));
                }
            });
        }
    }
}
