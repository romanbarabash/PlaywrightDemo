using Microsoft.Playwright;
using PlaywrightDemo.POM.Fixtures;
using PlaywrightDemo.POM.Pages;

namespace PlaywrightDemo.DemoFramework.Tests;


[TestFixture]
public class MainBannerTest : BaseTest
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
        await _mainPage.GoTo();

        await Expect(_mainPage.BannerMessage).ToHaveTextAsync(
           "ADVERTISE YOUR PRODUCT / SERVICE HERE: Contact me on X @CommitQuality",
           new LocatorAssertionsToHaveTextOptions { Timeout = 2000 }); //ex. with custom timeout for Locator
    }

}
