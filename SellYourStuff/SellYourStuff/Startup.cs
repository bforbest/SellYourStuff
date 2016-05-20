using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SellYourStuff.Startup))]
namespace SellYourStuff
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
