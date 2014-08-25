using System;
using Nimbus.MessageContracts;

namespace CloudScale.Movies.Messages
{
    [Obsolete]
    public class GetMoviesRequest : IBusRequest<GetMoviesRequest, GetMoviesResponse>
    {
        public string Search { get; set; }
    }
}