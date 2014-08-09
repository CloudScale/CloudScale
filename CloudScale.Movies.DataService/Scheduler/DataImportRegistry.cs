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
using FluentScheduler.Extensions;

namespace CloudScale.Movies.DataService.Scheduler
{
    public class DataImportRegistry : Registry
    {
        /// <summary>
        /// Initializes a new instance of the DataImportRegistry class.
        /// </summary>
        public DataImportRegistry()
        {
            Schedule<DataImportTask>().NonReentrant().ToRunNow().AndEvery(30).Seconds();
        }
    }
}
