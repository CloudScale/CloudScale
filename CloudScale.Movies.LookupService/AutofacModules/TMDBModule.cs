using Autofac;
using Microsoft.WindowsAzure;
using TMDbLib.Client;

namespace AutofacModules
{
    public class TMDBModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            string apiKey = CloudConfigurationManager.GetSetting("TMDBApiKey");

            builder.RegisterType<TMDbClient>()
                .AsSelf()
                .WithParameter("apiKey", apiKey)
                .SingleInstance();
        }
    }
}