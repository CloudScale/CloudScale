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
using Microsoft.WindowsAzure;

namespace CloudScale.Movies.DataService
{
    public class MoviesDataContext : DbContext, IMoviesDataContext
    {
        public MoviesDataContext()
            : base(CloudConfigurationManager.GetSetting("MoviesDataContext"))
        {

        }

        public virtual IDbSet<Movie> Movies { get; set; }
        public virtual IDbSet<MovieScore> MovieScores { get; set; }
        public virtual IDbSet<MovieLookupResults> MovieLookupResults { get; set; }
    }
}