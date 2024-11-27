using Microsoft.Playwright.NUnit;

namespace PlaywrightDemo.Demos.TimeManagament;

[TestFixture]
public class ClockTest : PageTest
{
    [Test]
    public async Task TestClock()
    {
        await Page.GotoAsync("https://commitquality.com/practice-clock");
        await Page.Clock.PauseAtAsync("2024-01-01T00:00:00Z");
        await Page.Clock.ResumeAsync();
    }

    [Test]
    public async Task TestTimer()
    {
        await Page.Clock.InstallAsync(new()
        {
            TimeDate = DateTime.Now,
        });

        await Page.GotoAsync("https://commitquality.com/practice-clock");
        await Page.Clock.FastForwardAsync("05:00");
        //await Page.Clock.FastForwardAsync(5000);
        await Expect(Page.GetByText("YOU WON... GO SUBSCRIBE TO COMMIT QUALITY")).ToBeVisibleAsync();
    }
}