using System;
using System.Collections.Generic;
using System.Linq;
using Nimbus.MessageContracts;
using CloudScale.Airline.Models;

namespace CloudScale.Airline.Messages
{
    public class NewFlightCommand : IBusCommand
    {
        /// <summary>
        /// Initializes a new instance of the NewFlightCommand class.
        /// </summary>
        public NewFlightCommand()
        {
        }

        /// <summary>
        /// Initializes a new instance of the NewFlightCommand class.
        /// </summary>
        /// <param name="prefix"></param>
        /// <param name="number"></param>
        public NewFlightCommand(string prefix, int number)
        {
            Prefix = prefix;
            Number = number;
        }

        public string Prefix { get; set; }
        public int Number { get; set; }
    }
}
