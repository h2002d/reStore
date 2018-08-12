using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(PizzaDomino.Startup))]
namespace PizzaDomino
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
