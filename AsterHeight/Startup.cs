using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MBS.Startup))]
namespace AsterHeight
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
