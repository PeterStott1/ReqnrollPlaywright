using Microsoft.Playwright;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using ReqnrollPlaywright.Hooks;
using ReqnrollPlaywright.Models;

namespace ReqnrollPlaywright.Bootstrap
{
    public static class AuthBootstrap
    {
        private static PlaywrightConfig? _config;
        public static async Task Run()
        {
            _config = ReqnrollHooks.ServiceProvider
                .GetRequiredService<IOptions<PlaywrightConfig>>()
                .Value;
            await CreateState(
               _config.PlaywrightSettings.UsernameEmail,
            _config.PlaywrightSettings.UserPassword, 
            _config.PlaywrightSettings.AuthState);
            
            await CreateState(
                _config.PlaywrightSettings.OdhUserEmail,
                _config.PlaywrightSettings.OdhPassword, 
                _config.PlaywrightSettings.OdhAuthState);

        }

        private static async Task CreateState(
            string username,
            string password,
            string stateFile)
        {
            using var pw = await Playwright.CreateAsync();
            
            var browser = await pw.Chromium.LaunchAsync(new()
            {
                Headless = true, 
                SlowMo = _config!.Browser.SlowMo
            });

            var context = await browser.NewContextAsync();
            var page = await context.NewPageAsync();
            var baseUrl = _config.PlaywrightSettings.BaseUrl; 
            // Step 1: Hit your app entry point
            await page.GotoAsync(baseUrl);
           
            // Step 3: Identity provider login
            await page.Locator("#username").FillAsync(username);
            await page.Locator("#password").FillAsync(password);
            await page.Locator("#MemberLoginButton").ClickAsync();
            // Step 4: Wait for SPA token acquisition
            await page.WaitForURLAsync(baseUrl);
            // Step 5: Save authenticated state
            await context.StorageStateAsync(new() { Path = stateFile });
        }
    }
}
