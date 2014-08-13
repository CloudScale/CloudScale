using CloudScale.Movies.Data.Migrations;
using CloudScale.Movies.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.WindowsAzure;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace CloudScale.Movies.Data
{
    public class MoviesDataContext : IdentityDbContext, IMoviesDataContext
    {
        public MoviesDataContext()
            : base(CloudConfigurationManager.GetSetting("MoviesDataContext"))
        {
            Database.SetInitializer<MoviesDataContext>(new MigrateDatabaseToLatestVersion<MoviesDataContext, Configuration>());
        }

        public virtual DbSet<Movie> Movies { get; set; }
        public virtual DbSet<MovieScore> MovieScores { get; set; }
        public virtual DbSet<MovieLookupResults> MovieLookupResults { get; set; }

        public virtual DbSet<OAuthClient> OAuthClients { get; set; }
        public virtual DbSet<OAuthRefreshToken> OAuthRefreshTokens { get; set; }
    }
}