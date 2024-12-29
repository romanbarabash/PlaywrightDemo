using AventStack.ExtentReports;
using PlaywrightDemo.DemoFramework.Pages;
using PlaywrightDemo.Framework.Fixtures;
using PlaywrightDemo.Framework.Utils;

namespace PlaywrightDemo.DemoFramework.Tests;

[TestFixture]
public class ClockTest : NotAuthTestFixture

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
        Log.WriteLine(Status.Info, "Open Clock Page as not authenticeted user");
        await _clockPage.GoTo();

        Log.WriteLine(Status.Info, "Set current time for \"2024-01-01T00:00:00Z\" and resume timer");
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

        Log.WriteLine(Status.Info, "Open Clock Page as not authenticeted user");
        await _clockPage.GoTo();

        Log.WriteLine(Status.Info, "Fast-Forward timer for 05:00");
        await Page.Clock.FastForwardAsync("05:00");
        //await Page.Clock.FastForwardAsync(5000);

        Log.WriteLine(Status.Info, "Verify Winner's text is shown after countdown timer exides");
        await Expect(Page.GetByText("YOU WON... GO SUBSCRIBE TO COMMIT QUALITY")).ToBeVisibleAsync();
    }
}