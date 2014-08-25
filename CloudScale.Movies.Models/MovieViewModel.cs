using System;

namespace CloudScale.Movies.Models
{
    public class MovieViewModel
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public double Rating { get; set; }
        public double UserRating { get; set; }
        public string BackdropPath { get; set; }
        public string PosterPath { get; set; }
    }
}