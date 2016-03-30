using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MSDiary.Startup))]
namespace MSDiary
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
