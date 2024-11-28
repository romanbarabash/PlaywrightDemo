using Microsoft.Playwright;

namespace PlaywrightDemo.DemoFramework.Pages
{
    public class ClockPage
    {
        private readonly IPage _page;

        public ClockPage(IPage page)
        {
            _page = page;
        }

        public async Task GoTo()
        {
            await _page.GotoAsync("https://commitquality.com/practice-clock");
        }
    }
}
