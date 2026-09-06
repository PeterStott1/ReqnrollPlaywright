using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.Playwright;
using ReqnrollPlaywright.Hooks;
using ReqnrollPlaywright.Models;

namespace ReqnrollPlaywright.Bootstrap
{
    public static class AuthBootstrap
    {
        public static async Task Run()
        {
            var config = ReqnrollHooks.ServiceProvider
                .GetRequiredService<IOptions<PlaywrightConfig>>()
                .Value;
            using var pw = await Playwright.CreateAsync();
            var browser = await pw.Chromium.LaunchAsync(new()
            {
                Headless = true, 
                SlowMo = config.Browser.SlowMo
            });
            var context = await browser.NewContextAsync();
            var page = await context.NewPageAsync();
            var baseUrl = config.PlaywrightSettings.BaseUrl; 
            // Step 1: Hit your app entry point
            await page.GotoAsync(baseUrl);
            var user = config.PlaywrightSettings.UsernameEmail;
            var pass = config.PlaywrightSettings.UserPassword; 
            // Step 3: Identity provider login
            await page.Locator("#username").FillAsync(user);
            await page.Locator("#password").FillAsync(pass);
            await page.Locator("#MemberLoginButton").ClickAsync();
            // Step 4: Wait for SPA token acquisition
            await page.WaitForURLAsync(baseUrl);
            // Step 5: Save authenticated state
            await context.StorageStateAsync(new() { Path = config.PlaywrightSettings.AuthState });
        }
    }
}
