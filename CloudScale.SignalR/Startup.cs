using Owin;

namespace CloudScale.SignalR
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            AutofacConfig.Register(app);
            SignalRConfig.Register(app);
        }
    }
}