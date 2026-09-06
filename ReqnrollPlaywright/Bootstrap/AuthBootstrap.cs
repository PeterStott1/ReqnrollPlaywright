using Microsoft.Playwright;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReqnrollPlaywright.Bootstrap
{
    public static class AuthBootstrap
    {
        public static async Task Run()
        {
            using var pw = await Playwright.CreateAsync();
            var browser = await pw.Chromium.LaunchAsync(new() { Headless = true });

            var context = await browser.NewContextAsync();
            var page = await context.NewPageAsync();

            // Step 1: Hit your app entry point
            await page.GotoAsync("https://yourapp.com");

            // Step 2: Follow OAuth redirect
            await page.WaitForURLAsync(url =>
                url.Contains("login.microsoftonline.com") ||
                url.Contains("auth0.com") ||
                url.Contains("okta.com"));

            // Step 3: Identity provider login
            await page.GetByLabel("Email").FillAsync("1234567890@mailinator,com");
            await page.GetByRole(AriaRole.Button, new() { Name = "Next" }).ClickAsync();

            await page.GetByLabel("Password").FillAsync("Password#12345");
            await page.GetByRole(AriaRole.Button, new() { Name = "Sign in" }).ClickAsync();

            // Step 4: Wait for SPA token acquisition
            await page.WaitForURLAsync("https://yourapp.com/dashboard");

            // Step 5: Save authenticated state
            await context.StorageStateAsync(new() { Path = "playwright/.auth/state.json" });
        }
    }
}
