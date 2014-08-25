using System;

namespace CloudScale.Movies.Models
{
    public class MovieLookupResults
    {
        /// <summary>
        ///     Initializes a new instance of the MovieLookupResults class.
        /// </summary>
        public MovieLookupResults()
        {
            Id = Guid.NewGuid();
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Data { get; set; }
    }
}