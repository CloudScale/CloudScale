using Nimbus.MessageContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudScale.Movies.Messages
{
    public class GetMoviesRequest : IBusRequest<GetMoviesRequest, GetMoviesResponse>
    {
        public string Search { get; set; }
        public GetMoviesRequest()
        {

        }
    }
}
