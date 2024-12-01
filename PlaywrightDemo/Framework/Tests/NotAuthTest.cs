using Microsoft.Playwright;
using PlaywrightDemo.POM.Fixtures;
using PlaywrightDemo.POM.Pages;


namespace PlaywrightDemo.DemoFramework.Tests;

[TestFixture]
public class NotAuthTest : BaseTest
{
    private MainPage _mainPage;

    [SetUp]
    public void SetUp()
    {
        _mainPage = new MainPage(Page);
    }

    [Test]
    public async Task TestNotLoggined()
    {
        await _mainPage.GoTo();
        await Assertions.Expect(Page.GetByText("Login")).ToBeVisibleAsync();
    }
}