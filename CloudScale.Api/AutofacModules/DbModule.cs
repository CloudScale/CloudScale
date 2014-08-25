using Autofac;
using CloudScale.Movies.Data;

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