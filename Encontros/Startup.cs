using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Encontros.Startup))]
namespace Encontros
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
