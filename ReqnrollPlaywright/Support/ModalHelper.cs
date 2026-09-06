using Microsoft.Playwright;

namespace ReqnrollPlaywright.Support;

public static class ModalHelper
{
    public static async Task HandleGlobalModals(IPage page)
    {
        var modal = page.Locator("#app-overlay");
        var closeBtn = page.Locator("#close-btn");

        if (await modal.IsVisibleAsync(new() { Timeout = 2000 }))
        {
            await closeBtn.ClickAsync();
        }
    }
}