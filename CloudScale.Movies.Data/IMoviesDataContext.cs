using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using CloudScale.Movies.Models;
using System.Threading.Tasks;

namespace CloudScale.Movies.Data
{
    public interface IMoviesDataContext
    {
        DbSet<MovieScore> MovieScores { get; set; }
        DbSet<MovieLookupResults> MovieLookupResults { get; set; }
        DbSet<Movie> Movies { get; set; }

        DbChangeTracker ChangeTracker { get; }

        int SaveChanges();

        Task<int> SaveChangesAsync();

    }
}
