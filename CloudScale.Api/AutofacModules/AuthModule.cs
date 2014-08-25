using Autofac;
using CloudScale.Api.Providers;
using CloudScale.Api.Repositories;

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