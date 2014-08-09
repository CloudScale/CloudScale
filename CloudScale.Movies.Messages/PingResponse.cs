using Nimbus.MessageContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudScale.Movies.Messages
{
    public class PingResponse : IBusResponse
    {
        public string Details { get; set; }

        /// <summary>
        /// Initializes a new instance of the PingResponse class.
        /// </summary>
        public PingResponse()
        {
            
        }        
    }
}