
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ReqnrollPlaywright.Models;

namespace ReqnrollPlaywright.DiRegestry
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceProvider AddPlaywrightConfiguration()
        {
            var services = new ServiceCollection();
            var environment =
            Environment.GetEnvironmentVariable("TEST_ENV") ?? "Development";
            
            var playwrightConfig = new ConfigurationBuilder()
                .SetBasePath(Path.Combine(AppContext.BaseDirectory, "Support"))
                .AddJsonFile($"playwright.config.{environment}.json", false)
                .Build();
            
            services.AddSingleton<IConfiguration>(playwrightConfig);
            services.Configure<PlaywrightConfig>(playwrightConfig);
       
            return services.BuildServiceProvider();
        }
    }
}