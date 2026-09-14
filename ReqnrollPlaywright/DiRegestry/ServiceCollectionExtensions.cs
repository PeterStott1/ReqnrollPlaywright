using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ReqnrollPlaywright.Models;

namespace ReqnrollPlaywright.DiRegestry
{
    /// <summary>
    /// extension static class used to collect
    /// the configuration to use per environment
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Creates and configures a service provider for Playwright tests by
        /// loading environment-specific settings from the Support folder.
        /// Defaults to the Development configuration when TEST_ENV is not set.
        /// </summary>
        /// <returns>built service provider</returns>
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