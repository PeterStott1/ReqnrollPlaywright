using Microsoft.Playwright;
using ReqnrollPlaywright.Drivers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReqnrollPlaywright.Context
{
    public class PlaywrightContextFactory
    {
        private readonly BrowserDriver _driver;

        public PlaywrightContextFactory()
        {
            _driver = new BrowserDriver(headless: true, enableTracing: true);
        }

        public async Task<IBrowserContext> CreateAuthenticatedContext()
        {
            var browser = await _driver.CreateBrowserAsync();

            return await _driver.CreateContextAsync(
                browser,
                storageStatePath: "playwright/.auth/state.json"
            );
        }
    }
}
