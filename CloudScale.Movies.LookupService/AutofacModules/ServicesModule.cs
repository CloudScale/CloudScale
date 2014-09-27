using Autofac;
using CloudScale.Movies.LookupService;
using Microsoft.WindowsAzure;
using TMDbLib.Client;

namespace AutofacModules
{
    public class ServicesModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

	        builder.RegisterType<ServiceImplementation>()
		        .AsSelf();
        }
    }
}