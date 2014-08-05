using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Classifieds.Startup))]
namespace Classifieds
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
