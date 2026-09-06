using Microsoft.Playwright;

namespace ReqnrollPlaywright.Pages;

public class HomePage(IPage page)
{
    public async Task NavigateAsync()
    {
        await page.GotoAsync("https://test.members.vitality.co.uk/");
    }

    public async Task WaitForSelection()
    {
        await page.WaitForSelectorAsync(".vds-nav-card__image-container");
    }
    
    public async Task<int> ReturnContentCountAsync()
    {
        return await page.Locator(".vds-wide-card").CountAsync();
    }
}