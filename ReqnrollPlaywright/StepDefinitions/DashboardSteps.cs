using Microsoft.Playwright;
using ReqnrollPlaywright.Pages;
using ReqnrollPlaywright.Support;

namespace ReqnrollPlaywright.StepDefinitions
{
    [Binding]
    public class DashboardSteps(ScenarioContext scenario)
    {
        private readonly IBrowserContext _context = (IBrowserContext)scenario["BrowserContext"];
        private IPage? _page;
        private HomePage _homePage = null!;

        [Given("I am logged in and navigate to the home page")]
        public async Task GivenIAmLoggedInAndNavigateToTheHomePage()
        {
            _page = await _context.NewPageAsync();
            _homePage = new HomePage(_page);
            await _homePage.NavigateAsync();
            await ModalHelper.HandleGlobalModals(_page);
        }

        [When("I have landed on the homepage")]
        public async Task WhenIHaveLandedOnTheHomepage()
        {
            await _homePage.WaitForSelection();
        }

        [Then("I should see at least one card")]
        public async Task ThenIShouldSeeAtLeastOneCard()
        {
            Assert.Equal(1, await _homePage.ReturnContentCountAsync());
        }
    }
}
