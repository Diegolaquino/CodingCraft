using Hangfire;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CodingCraftoHOMod1Ex1EF.Startup))]
namespace CodingCraftoHOMod1Ex1EF
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            GlobalConfiguration.Configuration
                .UseSqlServerStorage(@"Data Source=(LocalDb)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\aspnet-CodingCraftoHOMod1Ex1EF-20181028051533.mdf;Initial Catalog=aspnet-CodingCraftoHOMod1Ex1EF-20181028051533;Integrated Security=True; MultipleActiveResultSets=true");
            ConfigureAuth(app);

            app.UseHangfireServer();
            app.UseHangfireDashboard();
        }
    }
}
