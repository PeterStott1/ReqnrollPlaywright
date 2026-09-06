using Microsoft.Playwright;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReqnrollPlaywright.Drivers
{
    public class BrowserDriver
    {
        private readonly bool _headless;
        private readonly bool _enableTracing;

        public BrowserDriver(bool headless = true, bool enableTracing = true)
        {
            _headless = headless;
            _enableTracing = enableTracing;
        }

        public async Task<IBrowser> CreateBrowserAsync()
        {
            var pw = await Playwright.CreateAsync();

            return await pw.Chromium.LaunchAsync(new()
            {
                Headless = _headless,
                Args = new[] { "--disable-dev-shm-usage" }
            });
        }

        public async Task<IBrowserContext> CreateContextAsync(IBrowser browser, string storageStatePath)
        {
            var context = await browser.NewContextAsync(new()
            {
                StorageStatePath = storageStatePath,
                ViewportSize = new() { Width = 500, Height = 500 }
            });

            if (_enableTracing)
            {
                await context.Tracing.StartAsync(new()
                {
                    Screenshots = true,
                    Snapshots = true,
                    Sources = true
                });
            }

            return context;
        }

        public async Task StopTracingAsync(IBrowserContext context, string scenarioName)
        {
            if (_enableTracing)
            {
                var safeName = scenarioName.Replace(" ", "_");
                await context.Tracing.StopAsync(new()
                {
                    Path = $"playwright/traces/{safeName}.zip"
                });
            }
        }
    }
}
