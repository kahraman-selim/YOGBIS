using Microsoft.AspNetCore.Hosting;

[assembly: HostingStartup(typeof(YOGBIS.UI.Areas.Identity.IdentityHostingStartup))]
namespace YOGBIS.UI.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
            });
        }
    }
}