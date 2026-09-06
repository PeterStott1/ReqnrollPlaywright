using Microsoft.Playwright;
using ReqnrollPlaywright.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReqnrollPlaywright.Hooks
{
    [Binding]
    public class ReqnrollHooks
    {
        public static PlaywrightContextFactory? Factory;

        [BeforeTestRun]
        public static void BeforeTestRun()
        {
            Factory = new PlaywrightContextFactory();
        }

        [BeforeScenario]
        public async Task BeforeScenario(ScenarioContext scenario)
        {
            var context = await Factory!.CreateAuthenticatedContext();
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
