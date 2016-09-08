using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TypeLiteExample.Startup))]
namespace TypeLiteExample
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
