using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudScale.Movies.Models
{
    public class MovieScore
    {
        public Guid Id { get; set; }
        public DateTimeOffset TimeLogged { get; set; }
        public virtual Movie Movie { get; set; }
        public Guid? MovieId { get; set; }
        public Guid? UserId { get; set; }
        public double Score { get; set; }

        public MovieScore()
        {
            Id = Guid.NewGuid();
            TimeLogged = DateTimeOffset.UtcNow;
        }
    }
}
