using Autofac;
using CloudScale.Movies.DataService;
using Microsoft.WindowsAzure;

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