using System.Collections.Generic;
using System.Linq;
using System;
using Nimbus;
using Serilog;

namespace CloudScale.Airline.FlightService
{
    public class FlightService
    {
        private readonly IBus bus;

        public FlightService(IBus bus)
        {
            this.bus = bus;
        }

        public void Start()
        {
            Log.Information("Starting Service");
        }

        public void Stop()
        {
            Log.Information("Stopping Service");
        }
    }
}
