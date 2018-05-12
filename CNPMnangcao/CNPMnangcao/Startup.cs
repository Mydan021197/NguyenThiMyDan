using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CNPMnangcao.Startup))]
namespace CNPMnangcao
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
