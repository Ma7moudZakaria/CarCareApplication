using FluentScheduler;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace CarCareApplication.WebApp.Server
{
    public class Program
    {
        public static void Main(string[] args)
        {
            JobManager.Initialize();
            JobManager.UseUtcTime();
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
