using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudScale.Airline.Models
{
    public class Flight
    {
        public Guid Id { get; set; }
        public string Prefix { get; set; }
        public int Number { get; set; }
        public DateTimeOffset Departure { get; set; }

        /// <summary>
        /// Initializes a new instance of the Flight class.
        /// </summary>
        public Flight()
        {
            Id = Guid.Empty;
            Prefix = String.Empty;
            Number = 0;
            Departure = DateTimeOffset.MinValue;
        }

        /// <summary>
        /// Initializes a new instance of the Flight class.
        /// </summary>
        /// <param name="prefix"></param>
        /// <param name="number"></param>
        public Flight(string prefix, int number)
        {
            Prefix = prefix;
            Number = number;
            Id = Guid.Empty;
            Departure = DateTimeOffset.MinValue;
        }
    }
}
