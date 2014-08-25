using FluentScheduler;

namespace CloudScale.Movies.DataService.Scheduler
{
    public class DataImportRegistry : Registry
    {
        /// <summary>
        ///     Initializes a new instance of the DataImportRegistry class.
        /// </summary>
        public DataImportRegistry()
        {
            Schedule<DataImportTask>().NonReentrant().ToRunNow().AndEvery(30).Seconds();
        }
    }
}