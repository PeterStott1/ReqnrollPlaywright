using Microsoft.Playwright;
using ReqnrollPlaywright.Context;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using ReqnrollPlaywright.Bootstrap;
using ReqnrollPlaywright.DiRegestry;
using ReqnrollPlaywright.Models;

namespace ReqnrollPlaywright.Hooks
{
    [Binding]
    public class ReqnrollHooks
    {
        private static PlaywrightContextFactory? _factory;
        public static IServiceProvider ServiceProvider { get; private set; } = null!;
        
        [BeforeTestRun]
        public static async Task BeforeTestRun()
        {
            ServiceProvider = ServiceCollectionExtensions.AddPlaywrightConfiguration();
            var config = ServiceProvider.GetRequiredService<IOptions<PlaywrightConfig>>();
            await AuthBootstrap.Run();
            _factory = new PlaywrightContextFactory(config.Value);
          
        }

        [BeforeScenario]
        public async Task BeforeScenario(ScenarioContext scenario)
        {
            var context = await (_factory?.CreateAuthenticatedContext(GetRole(scenario))
                ?? throw new InvalidOperationException("Factory is not initialized."));
            scenario["BrowserContext"] = context;
            
        }

        [AfterScenario]
        public async Task AfterScenario(ScenarioContext scenario)
        {
            var context = (IBrowserContext)scenario["BrowserContext"];
            await _factory?.StopTracing(context)!;
            await context.CloseAsync();
        }

        private static UserRole GetRole(ScenarioContext scenario)
        {
            var tags = scenario.ScenarioInfo.Tags;

            if (tags.Contains("Odh"))
                return UserRole.Odh;

            return UserRole.User;
        }

    }
}
