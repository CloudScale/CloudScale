using Nimbus.MessageContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CloudScale.Movies.Models;

namespace CloudScale.Movies.Messages
{
    public class NewScoreEvent : IBusEvent
    {
        public string MovieName { get; set; }
        public string PersonName { get; set; }
        public double Score { get; set; }

        public NewScoreEvent(string movieName, string personName, double score)
        {
            this.MovieName = movieName;
            this.PersonName = personName; 
            this.Score = score;
        }

        public NewScoreEvent()
        {

        }
    }
}
