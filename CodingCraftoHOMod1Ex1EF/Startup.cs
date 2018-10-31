using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CodingCraftoHOMod1Ex1EF.Startup))]
namespace CodingCraftoHOMod1Ex1EF
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
