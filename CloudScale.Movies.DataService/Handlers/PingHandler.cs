using System.Threading.Tasks;
using CloudScale.Movies.Messages;
using Nimbus.Handlers;

namespace CloudScale.Movies.DataService.Handlers
{
    public class PingHandler : IHandleRequest<PingRequest, PingResponse>
    {
        public async Task<PingResponse> Handle(PingRequest request)
        {
            return await Task.Run(() =>
            {
                return new PingResponse
                {
                    Details = GetType() + " : " + GetType().Assembly.GetName().Version
                };
            });
        }
    }
}