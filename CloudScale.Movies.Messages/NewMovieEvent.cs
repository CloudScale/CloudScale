using System;
using CloudScale.Movies.Models;
using Nimbus.MessageContracts;

namespace CloudScale.Movies.Messages
{
    public class NewMovieEvent : IBusEvent
    {
        public NewMovieEvent(string name)
        {
            Movie = new Movie(Guid.NewGuid(), name);
        }

        public NewMovieEvent()
        {
        }

        public Movie Movie { get; set; }
    }
}