using Microsoft.Playwright;
using ReqnrollPlaywright.Models;

namespace ReqnrollPlaywright.Drivers
{
    public class BrowserDriver
    {
        private readonly bool _headless;
        private readonly bool _enableTracing;
        private readonly PlaywrightConfig _config;
        private IBrowserContext? Context { get; set; }
        private BrowserNewContextOptions? ContextOptions { get; set; }

        public BrowserDriver(PlaywrightConfig config)
        {
            _config = config;
            _headless = _config.Browser.Headless;
            _enableTracing = _config.Tracing.Enabled;
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

        public async Task<IBrowserContext> CreateContextAsync(IBrowser browser, UserRole role)
        {
            var height = _config.Context.ViewportHeight;
            var width = _config.Context.ViewportHeight;
         
            string storageState = role switch
            {
                UserRole.Odh => "odh-state.json",
                UserRole.User => "state.json",
                _ => throw new ArgumentOutOfRangeException(nameof(role), role, "Unknown role")
            };
            if (_config.Video.Enabled)
            {
                ContextOptions = new BrowserNewContextOptions
                {
                    RecordVideoDir = "videos",
                    RecordVideoSize = new RecordVideoSize
                    {
                        Width = width,
                        Height = height
                    },
                    StorageStatePath = storageState,
                    ViewportSize = new() { Width = width, Height = height }
                };
            }
            else
            {
                ContextOptions = new BrowserNewContextOptions
                {
                    StorageStatePath = storageState,
                    ViewportSize = new() { Width = width, Height = height }
                };
            }
            Context = await browser.NewContextAsync(ContextOptions);
            if (_enableTracing)
            {
                await Context.Tracing.StartAsync(new()
                {
                    Screenshots = _config.Tracing.Screenshots,
                    Snapshots = _config.Tracing.Snapshots,
                    Sources = _config.Tracing.Sources
                });
            }
            return Context;
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
