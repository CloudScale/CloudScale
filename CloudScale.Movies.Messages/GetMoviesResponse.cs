using System.Collections.Generic;
using CloudScale.Movies.Models;
using Nimbus.MessageContracts;

namespace CloudScale.Movies.Messages
{
    public class GetMoviesResponse : IBusResponse
    {
        public virtual List<Movie> Movies { get; set; }
    }
}