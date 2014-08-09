using Autofac;
using Nimbus;
using Nimbus.Configuration;
using Nimbus.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Nimbus.Logger.Serilog;
using TMDbLib.Client;

namespace AutofacModules
{
    public class TMDBModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            var apiKey = Microsoft.WindowsAzure.CloudConfigurationManager.GetSetting("TMDBApiKey");

            builder.RegisterType<TMDbClient>()
                .AsSelf()
                .WithParameter("apiKey", apiKey)
                .SingleInstance();
        }
    }
}
