using Autofac;
using Autofac.Integration.WebApi;
using Microsoft.WindowsAzure.ServiceRuntime;
using Nimbus;
using Nimbus.Configuration;
using Nimbus.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Hosting;
using Microsoft.WindowsAzure;
using StatsdClient;

namespace CloudScale.Api.AutofacModules
{
    public class StatsModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            string statsdHost = CloudConfigurationManager.GetSetting("statsd:host");
            string statsdPort = CloudConfigurationManager.GetSetting("statsd:port");

            if (!string.IsNullOrEmpty(statsdHost) && !string.IsNullOrEmpty(statsdPort))
            {
                builder.Register(c => new StatsdClient.Statsd(statsdHost, int.Parse(statsdPort)))
                    .As<IStatsd>()
                    .SingleInstance();
            }
        }
    }
}