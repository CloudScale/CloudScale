using Autofac;
using CloudScale.Api.Repositories;
using CloudScale.Movies.Data;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AutofacModules
{
    public class DbModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder.RegisterType<MoviesDataContext>()
                    .As<IMoviesDataContext>()
                    .AsSelf();
        }
    }
}
