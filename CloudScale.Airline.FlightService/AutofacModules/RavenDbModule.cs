using Autofac;
using Raven.Client;
using Raven.Client.Document;
using Raven.Client.Embedded;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CloudScale.Airline.FlightService.AutofacModules
{
    public class RavenDbModule : Autofac.Module
    {
        protected override void Load(Autofac.ContainerBuilder builder)
        {
            base.Load(builder);

            builder.Register(x =>
                {
                    var store = new EmbeddableDocumentStore
                    {
                        DataDirectory = "Data",
                        UseEmbeddedHttpServer = true
                    };
                    store.Initialize();
                    return store;
                })
                .As<IDocumentStore>()
                .SingleInstance();

            builder.Register(x => x.Resolve<IDocumentStore>().OpenSession())
                 .As<IDocumentSession>()
                 .OnActivating(x =>
                 {
                     Serilog.Log.Information("OnActivating()");
                 })
                 .OnRelease(x =>
                 {
                     Serilog.Log.Information("OnRelease()");

                     // When the scope is released, save changes
                     //  before disposing the session.
                     x.SaveChanges();
                     x.Dispose();
                 })
                 .InstancePerLifetimeScope();
        }
    }
}
