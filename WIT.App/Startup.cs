using Microsoft.Owin;
using WIT.App;

[assembly: OwinStartup(typeof(Startup))]
namespace WIT.App
{
    using Owin;

    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            this.ConfigureAuth(app);
        }
    }
}
