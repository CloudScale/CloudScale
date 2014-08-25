using System;
using System.Linq;
using System.Threading.Tasks;
using CloudScale.Movies.Messages;
using Newtonsoft.Json;
using Nimbus;
using Nimbus.Handlers;
using Serilog;
using TMDbLib.Client;
using TMDbLib.Objects.General;
using TMDbLib.Objects.Search;

namespace CloudScale.Movies.LookupService.Handlers
{
    public class NewMovieHandler : IHandleMulticastEvent<NewMovieEvent>
    {
        private readonly IBus bus;
        private readonly TMDbClient client;

        /// <summary>
        ///     Initializes a new instance of the NewMovieHandler class.
        /// </summary>
        public NewMovieHandler(IBus bus, TMDbClient client)
        {
            this.bus = bus;
            this.client = client;
        }

        public async Task Handle(NewMovieEvent busEvent)
        {
            Log.Information("{Type} New Movie {Movie}", GetType().FullName, busEvent.Movie);

            IsRegisteredResponse isRegistered =
                await bus.Request(new IsRegisteredRequest(busEvent.Movie.Name), TimeSpan.FromSeconds(5));

            if (!isRegistered.Registered)
            {
                SearchContainer<SearchMovie> search = client.SearchMovie(busEvent.Movie.Name);

                SearchMovie result = search.Results.FirstOrDefault();
                if (result != null)
                {
                    var newEvent = new LookupMovieEvent
                    {
                        Id = busEvent.Movie.Id,
                        Name = busEvent.Movie.Name,
                        Data = JsonConvert.SerializeObject(result)
                    };

                    await bus.Publish(newEvent);
                }
            }
        }
    }
}