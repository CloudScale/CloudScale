using Autofac;
using Nimbus;
using Nimbus.Configuration;
using Nimbus.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Nimbus.Logger.Serilog;
using CloudScale.Movies.DataService;
using System.Data.Entity;
using CloudScale.Movies.DataService.Migrations;

namespace AutofacModules
{
    public class DbModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            Database.SetInitializer<MoviesDataContext>(new MigrateDatabaseToLatestVersion<MoviesDataContext, Configuration>());

            builder.RegisterType<MoviesDataContext>()
                    .As<IMoviesDataContext>();
        }
    }
}
