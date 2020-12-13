using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Borrowee.WebMVC.Startup))]
namespace Borrowee.WebMVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
