using Nimbus.MessageContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudScale.Movies.Messages
{
    public class IsRegisteredRequest : IBusRequest<IsRegisteredRequest, IsRegisteredResponse>
    {
        public string Name { get; set; }
        /// <summary>
        /// Initializes a new instance of the IsRegisteredRequest class.
        /// </summary>
        /// <param name="name"></param>
        public IsRegisteredRequest(string name)
        {
            Name = name;
        }

        public IsRegisteredRequest()
        {

        }
    }
}
