using Hangfire;
using Microsoft.Owin;
using Owin;
using System.Configuration;

[assembly: OwinStartupAttribute(typeof(CodingCraftoHOMod1Ex1EF.Startup))]
namespace CodingCraftoHOMod1Ex1EF
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            GlobalConfiguration.Configuration
.UseSqlServerStorage(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
            ConfigureAuth(app);

            app.UseHangfireServer();
            app.UseHangfireDashboard();
        }
    }
}
