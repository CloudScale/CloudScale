using System.Data.Entity;
using CloudScale.Movies.Data.Migrations;
using CloudScale.Movies.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.WindowsAzure;

namespace CloudScale.Movies.Data
{
    public class MoviesDataContext : IdentityDbContext, IMoviesDataContext
    {
        public MoviesDataContext()
            : base(CloudConfigurationManager.GetSetting("MoviesDataContext"))
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<MoviesDataContext, Configuration>());
        }

        public virtual DbSet<OAuthClient> OAuthClients { get; set; }
        public virtual DbSet<OAuthRefreshToken> OAuthRefreshTokens { get; set; }

        public virtual DbSet<Movie> Movies { get; set; }
        public virtual DbSet<MovieScore> MovieScores { get; set; }
        public virtual DbSet<MovieLookupResults> MovieLookupResults { get; set; }
    }
}