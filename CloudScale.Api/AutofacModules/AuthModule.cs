using Autofac;
using CloudScale.Api.Providers;
using CloudScale.Api.Repositories;
using CloudScale.Movies.Data;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace AutofacModules
{
    public class AuthModule : Module
    {

        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder.RegisterType<CloudScaleUserStore>()
                    .AsSelf();
            
            builder.RegisterType<CloudScaleUserManager>()
                    .AsSelf();

            builder.RegisterType<AuthRepository>();

            builder.RegisterType<SimpleAuthorizationServerProvider>()
                .AsImplementedInterfaces()
                .AsSelf();

        }
    }
}
