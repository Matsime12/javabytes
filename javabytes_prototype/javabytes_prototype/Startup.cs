using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(javabytes_prototype.Startup))]
namespace javabytes_prototype
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
