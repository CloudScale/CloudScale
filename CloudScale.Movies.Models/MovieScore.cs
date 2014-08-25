using System;

namespace CloudScale.Movies.Models
{
    public class MovieScore
    {
        public MovieScore()
        {
            Id = Guid.NewGuid();
            TimeLogged = DateTimeOffset.UtcNow;
        }

        public Guid Id { get; set; }
        public DateTimeOffset TimeLogged { get; set; }
        public virtual Movie Movie { get; set; }
        public Guid? MovieId { get; set; }
        public Guid? UserId { get; set; }
        public double Score { get; set; }
    }
}