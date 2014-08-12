using CloudScale.Movies.Messages;
using Nimbus;
using Nimbus.Handlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudScale.Movies.LookupService.Handlers
{
    public class PingHandler : IHandleRequest<PingRequest, PingResponse>
    {
        public async Task<PingResponse> Handle(PingRequest request)
        {
            return await Task.Run(() =>
            {
                return new PingResponse()
                {
                    Details = this.GetType() + " : " + this.GetType().Assembly.GetName().Version.ToString()
                };
            });
        }
    }
}
