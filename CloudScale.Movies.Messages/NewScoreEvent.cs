using System;
using Nimbus.MessageContracts;

namespace CloudScale.Movies.Messages
{
    public class NewScoreEvent : IBusEvent
    {
        public NewScoreEvent(Guid movieId, Guid userId, double score)
        {
            MovieId = movieId;
            UserId = userId;
            Score = score;
        }

        public NewScoreEvent()
        {
        }

        public Guid MovieId { get; set; }
        public Guid UserId { get; set; }
        public double Score { get; set; }
    }
}