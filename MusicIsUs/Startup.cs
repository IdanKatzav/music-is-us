using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MusicIsUs.Startup))]
namespace MusicIsUs
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
