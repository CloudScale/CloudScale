using Nimbus.MessageContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CloudScale.Movies.Models;

namespace CloudScale.Movies.Messages
{
    public class LookupMovieEvent : IBusEvent
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Data { get; set; }

        public LookupMovieEvent()
        {
            
        }
    }
}
