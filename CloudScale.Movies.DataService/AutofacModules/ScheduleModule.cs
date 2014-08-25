using Autofac;
using CloudScale.Movies.DataService.Scheduler;

namespace AutofacModules
{
    public class ScheduleModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder.RegisterType<AutofacTaskFactory>()
                .SingleInstance()
                .AsImplementedInterfaces();

            builder.RegisterType<DataImportRegistry>()
                .AsSelf();

            builder.RegisterType<DataImportTask>()
                .AsSelf();
        }
    }
}