using Microsoft.Playwright;
using ReqnrollPlaywright.Drivers;
using ReqnrollPlaywright.Models;

namespace ReqnrollPlaywright.Context
{
    public class PlaywrightContextFactory
    {
        private readonly BrowserDriver _driver;

        public PlaywrightContextFactory(PlaywrightConfig options)
        {
            _driver = new BrowserDriver(options);
        }

        public async Task<IBrowserContext> CreateAuthenticatedContext(UserRole role)
        {
            var browser = await _driver.CreateBrowserAsync();
            return await _driver.CreateContextAsync(browser, role);
        }

        public async Task StopTracing(IBrowserContext context)
        {
            await _driver.StopTracingAsync(context, "Dashboard");
        }
    }
}
