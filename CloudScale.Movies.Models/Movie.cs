using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudScale.Movies.Models
{
    public class Movie
    {
        public Guid Id { get; set; }
        public int TMDBId { get; set; }
        public string Name { get; set; }
        public string OriginalTitle { get; set; }
        public double Rating { get; set; }
        public int Year { get; set; }
        public string BackdropPath { get; set; }
        public string PosterPath { get; set; }

        public Movie(string name)
        {
            this.Name = name;
        }

        public Movie(Guid id, string name)
        {
            this.Id = id;
            this.Name = name;
        }

        /// <summary>
        /// Initializes a new instance of the Movie class.
        /// </summary>
        public Movie()
        {
        }
    }
}
