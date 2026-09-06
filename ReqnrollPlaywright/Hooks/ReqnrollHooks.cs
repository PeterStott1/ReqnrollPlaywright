using Microsoft.Playwright;
using ReqnrollPlaywright.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReqnrollPlaywright.Bootstrap;

namespace ReqnrollPlaywright.Hooks
{
    [Binding]
    public class ReqnrollHooks
    {
        private static PlaywrightContextFactory _factory;

        [BeforeTestRun]
        public static async Task BeforeTestRun()
        {
            await AuthBootstrap.Run();
            _factory = new PlaywrightContextFactory();
        }

        [BeforeScenario]
        public async Task BeforeScenario(ScenarioContext scenario)
        {
            var context = await _factory.CreateAuthenticatedContext();
            scenario["BrowserContext"] = context;
        }

        [AfterScenario]
        public async Task AfterScenario(ScenarioContext scenario)
        {
            var context = (IBrowserContext)scenario["BrowserContext"];
            await context.CloseAsync();
        }
    }
}
