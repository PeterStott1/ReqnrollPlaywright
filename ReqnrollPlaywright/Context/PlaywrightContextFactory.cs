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
        private const string StorageStatePath = "state.json";
        public PlaywrightContextFactory()
        {
            _driver = new BrowserDriver(headless: false, enableTracing: true);
        }

        public async Task<IBrowserContext> CreateAuthenticatedContext()
        {
            var browser = await _driver.CreateBrowserAsync();

            return await _driver.CreateContextAsync(
                browser,
                storageStatePath : (File.Exists(StorageStatePath)
                    ? StorageStatePath
                    : null)!
            );
        }
    }
}
