using Nimbus.MessageContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CloudScale.Movies.Models;

namespace CloudScale.Movies.Messages
{
    public class NewMovieEvent : IBusEvent
    {
        public Movie Movie { get; set; }
        public NewMovieEvent(string name)
        {
            Movie = new Movie(Guid.NewGuid(), name);
        }

        public NewMovieEvent()
        {

        }
    }
}
