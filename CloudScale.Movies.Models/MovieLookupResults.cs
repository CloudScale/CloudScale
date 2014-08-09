using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudScale.Movies.Models
{
    public class MovieLookupResults
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Data { get; set; }
        /// <summary>
        /// Initializes a new instance of the MovieLookupResults class.
        /// </summary>
        public MovieLookupResults()
        {
        }
    }
}
