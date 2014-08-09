using Autofac;
using Nimbus;
using Nimbus.Configuration;
using Nimbus.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Nimbus.Logger.Serilog;
using CloudScale.Movies.DataService;
using FluentScheduler;
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
