using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudScale.Movies.Models
{
    public class MovieScore
    {
        public string MovieName { get; set; }
        public string PersonName { get; set; }
        public double Score { get; set; }

        public MovieScore()
        {
        }
    }
}
