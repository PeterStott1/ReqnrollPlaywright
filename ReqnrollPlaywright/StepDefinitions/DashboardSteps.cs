using Microsoft.Playwright;
using Reqnroll;
using System;

namespace ReqnrollPlaywright.StepDefinitions
{
    [Binding]
    public class DashboardSteps(ScenarioContext scenario)
    {
        private readonly IBrowserContext _context = (IBrowserContext)scenario["BrowserContext"];
        private IPage? _page;

        [Given("I am on the dashboard")]
        public async Task GivenIAmOnTheDashboard()
        {
            _page = await _context.NewPageAsync();
            await _page.GotoAsync("https://yourapp.com/dashboard");
            await _page.WaitForSelectorAsync("data-test=dashboard-title");
        }

        [When("I open the notifications panel")]
        public async Task WhenIOpenTheNotificationsPanel()
        {
            await _page!.GetByTestId("notifications-button").ClickAsync();
            await _page.WaitForSelectorAsync("data-test=notifications-panel");
        }

        [Then("I should see at least one notification")]
        public async Task ThenIShouldSeeAtLeastOneNotification()
        {
            var count = await _page!.Locator("data-test=notification-item").CountAsync();
            if (count < 1)
                throw new Exception("Expected at least one notification.");
        }
    }
}
