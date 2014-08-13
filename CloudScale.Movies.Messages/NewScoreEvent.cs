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
        public Guid MovieId { get; set; }
        public Guid UserId { get; set; }
        public double Score { get; set; }

        public NewScoreEvent(Guid movieId, Guid userId, double score)
        {
            this.MovieId = movieId;
            this.UserId = userId;
            this.Score = score;
        }

        public NewScoreEvent()
        {

        }
    }
}
