using Autofac;
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
                builder.Register(c => new Statsd(statsdHost, int.Parse(statsdPort)))
                    .As<IStatsd>()
                    .SingleInstance();
            }
        }
    }
}