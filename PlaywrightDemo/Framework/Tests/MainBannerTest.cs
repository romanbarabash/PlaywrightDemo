using AventStack.ExtentReports;
using Microsoft.Playwright;
using PlaywrightDemo.Framework.Fixtures;
using PlaywrightDemo.Framework.Utils;
using PlaywrightDemo.POM.Pages;

namespace PlaywrightDemo.DemoFramework.Tests;


[TestFixture]
public class MainBannerTest : NotAuthTestFixture
{
    private MainPage _mainPage;

    [SetUp]
    public void SetUp()
    {
        _mainPage = new MainPage(Page);
    }

    [Test]
    public async Task TestMainBannerText()
    {
        Log.WriteLine(Status.Info, "Open Main page as not authenticeted user");
        await _mainPage.GoTo();

        Log.WriteLine(Status.Info, "Verify exact text is present \"ADVERTISE YOUR PRODUCT / SERVICE HERE: Contact me on X @CommitQuality\" in Main Banner section");
        await Expect(_mainPage.BannerMessage).ToHaveTextAsync(
           "ADVERTISE YOUR PRODUCT / SERVICE HERE: Contact me on X @CommitQuality",
           new LocatorAssertionsToHaveTextOptions { Timeout = 2000 }); //ex. with custom timeout for Locator
    }

}
