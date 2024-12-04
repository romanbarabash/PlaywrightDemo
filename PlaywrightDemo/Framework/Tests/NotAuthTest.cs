using AventStack.ExtentReports;
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
        test.Log(Status.Info, "Step: open main page");
        await _mainPage.GoTo();

        test.Log(Status.Info, "Assert: verify Login button is present");
        await Assertions.Expect(Page.GetByText("Login")).ToBeVisibleAsync();
    }
}