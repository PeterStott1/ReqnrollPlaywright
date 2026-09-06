using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.Playwright;
using ReqnrollPlaywright.Hooks;
using ReqnrollPlaywright.Models;

namespace ReqnrollPlaywright.Pages;

public class HomePage(IPage page)
{
   // private ILocator NavigationBar => page.Locator("nav[class='vds-sub-navigation']");
   // private ILocator UpdateDetailsLink => page.Locator("a[Name='\"Update your details\"']");
    private ILocator ImageContainer => page.Locator(".vds-wide-card");
    public async Task NavigateAsync()
    {
        var config = ReqnrollHooks.ServiceProvider
            .GetRequiredService<IOptions<PlaywrightConfig>>()
            .Value;
        await page.GotoAsync(config.PlaywrightSettings.BaseUrl);
    }

    public async Task WaitForSelection()
    {
        await page.WaitForSelectorAsync(".vds-nav-card__image-container");
    }
    
    public async Task<int> ReturnContentCountAsync()
    {
        return await ImageContainer.CountAsync();
    }
}