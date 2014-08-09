using Nimbus.MessageContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudScale.Movies.Messages
{
    public class GetMoviesResponse : IBusResponse
    {
        public virtual List<Models.Movie> Movies { get; set; }
        public GetMoviesResponse()
        {

        }
    }
}
