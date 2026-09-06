using Microsoft.Playwright;


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
            await page.GotoAsync("https://test.members.vitality.co.uk/identity/login/member/Wso2IdentityServer?returnUrl=%2F");
            
            // Step 3: Identity provider login
            await page.Locator("#username").FillAsync("1260643158@mailinator.com");
       
            await page.Locator("#password").FillAsync("Password#12345");
            await page.Locator("#MemberLoginButton").ClickAsync();
         

            // Step 4: Wait for SPA token acquisition
            await page.WaitForURLAsync("https://test.members.vitality.co.uk/");
            // Step 5: Save authenticated state
            await context.StorageStateAsync(new() { Path = "state.json" });
        }
    }
}
