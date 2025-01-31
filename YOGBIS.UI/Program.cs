using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using OfficeOpenXml;

namespace YOGBIS.UI
{
    public class Program
    {
        public static void Main(string[] args)
        {
           // ExcelPackage.LicenseContext = LicenseContext.NonCommercial; // Lisans baðlamýný ayarla
            CreateHostBuilder(args).Build().Run();
        }
        
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
