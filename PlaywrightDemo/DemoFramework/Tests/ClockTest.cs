using PlaywrightDemo.DemoFramework.Pages;
using PlaywrightDemo.POM.Fixtures;

namespace PlaywrightDemo.DemoFramework.Tests;

[TestFixture]
public class ClockTest : BaseTest

{
    private ClockPage _clockPage;

    [SetUp]
    public void SetUp()
    {
        _clockPage = new ClockPage(Page);
    }


    [Test]
    public async Task TestClock()
    {
        await _clockPage.GoTo();
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

        await _clockPage.GoTo();
        await Page.Clock.FastForwardAsync("05:00");
        //await Page.Clock.FastForwardAsync(5000);
        await Expect(Page.GetByText("YOU WON... GO SUBSCRIBE TO COMMIT QUALITY")).ToBeVisibleAsync();
    }
}