using System;

namespace CloudScale.Movies.Models
{
    public class Movie
    {
        public Movie(string name)
        {
            Name = name;
        }

        public Movie(Guid id, string name)
        {
            Id = id;
            Name = name;
        }

        /// <summary>
        ///     Initializes a new instance of the Movie class.
        /// </summary>
        public Movie()
        {
            Id = Guid.NewGuid();
        }

        public Guid Id { get; set; }
        public int TMDBId { get; set; }
        public string Name { get; set; }
        public string OriginalTitle { get; set; }
        public double Rating { get; set; }
        public int Year { get; set; }
        public string BackdropPath { get; set; }
        public string PosterPath { get; set; }
    }
}