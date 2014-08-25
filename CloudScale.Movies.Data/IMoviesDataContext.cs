using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Threading.Tasks;
using CloudScale.Movies.Models;

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