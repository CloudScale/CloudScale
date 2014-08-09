using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Threading;
using Microsoft.WindowsAzure.ServiceRuntime;
using Serilog;
using Autofac;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Common;
using System.Data.Entity.Infrastructure;
using CloudScale.Movies.Models;
using System.Threading.Tasks;

namespace CloudScale.Movies.DataService
{
    public interface IMoviesDataContext
    {
        IDbSet<MovieScore> MovieScores { get; set; }
        IDbSet<MovieLookupResults> MovieLookupResults { get; set; }
        IDbSet<Movie> Movies { get; set; }

        DbChangeTracker ChangeTracker {get;}

        int SaveChanges();

        Task<int> SaveChangesAsync();

    }
}
