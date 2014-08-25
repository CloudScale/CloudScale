using System;
using Nimbus.MessageContracts;

namespace CloudScale.Movies.Messages
{
    public class LookupMovieEvent : IBusEvent
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Data { get; set; }
    }
}