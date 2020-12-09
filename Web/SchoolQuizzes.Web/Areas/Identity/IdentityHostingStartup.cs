using Microsoft.AspNetCore.Hosting;

[assembly: HostingStartup(typeof(SchoolQuizzes.Web.Areas.Identity.IdentityHostingStartup))]

namespace SchoolQuizzes.Web.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            _ = builder.ConfigureServices((context, services) =>
              {
              });
        }
    }
}
