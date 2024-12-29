using AventStack.ExtentReports;
using Microsoft.Playwright;
using PlaywrightDemo.Framework.Utils;

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
            Log.WriteLine(Status.Info, "Go To Clock page");
            await _page.GotoAsync("https://commitquality.com/practice-clock");
        }
    }
}
