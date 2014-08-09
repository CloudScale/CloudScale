using Nimbus.MessageContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudScale.Movies.Messages
{
    public class PingRequest : IBusRequest<PingRequest, PingResponse>
    {
        /// <summary>
        /// Initializes a new instance of the PingRequest class.
        /// </summary>
        public PingRequest()
        {

        }
    }
}
