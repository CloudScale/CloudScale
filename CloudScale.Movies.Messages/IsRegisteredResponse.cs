using Nimbus.MessageContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudScale.Movies.Messages
{
    public class IsRegisteredResponse : IBusResponse
    {
        public bool Registered { get; set; }

        public IsRegisteredResponse()
        {

        }
    }
}
