using System.Threading.Tasks;
using CloudScale.Movies.Messages;
using Nimbus;
using Nimbus.Handlers;
using Serilog;

namespace CloudScale.Movies.Simulator.Handlers
{
    public class NewMovieHandler : IHandleMulticastEvent<NewMovieEvent>
    {
        private readonly IBus bus;

        /// <summary>
        ///     Initializes a new instance of the NewMovieHandler class.
        /// </summary>
        public NewMovieHandler(IBus bus)
        {
            this.bus = bus;
        }

        public async Task Handle(NewMovieEvent busEvent)
        {
            Log.Information("I Noticed a New Movie {Movie}", busEvent.Movie.Name);

            await Task.Run(() => { });
        }
    }
}