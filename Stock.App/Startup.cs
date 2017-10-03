using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Stock.App.Startup))]
namespace Stock.App
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
