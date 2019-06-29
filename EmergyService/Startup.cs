using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(EmergyService.Startup))]

namespace EmergyService
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureMobileApp(app);
            app.MapSignalR();
        }
    }
}