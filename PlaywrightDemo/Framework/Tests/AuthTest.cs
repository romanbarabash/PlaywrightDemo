using AventStack.ExtentReports;
using Microsoft.Playwright;
using PlaywrightDemo.Framework.Utils;
using PlaywrightDemo.POM.Fixtures;
using PlaywrightDemo.POM.Pages;

namespace PlaywrightDemo.DemoFramework.Tests;

[TestFixture]
public class AuthTest : AuthTestFixture
{
    private MainPage _mainPage;

    [SetUp]
    public void SetUp()
    {
        _mainPage = new MainPage(Page);
    }


    [Test]
    public async Task TestLoggined()
    {
        Log.WriteLine(Status.Info, "Open Main page as authenticeted user");
        await _mainPage.GoTo();

        Log.WriteLine(Status.Info, "Verify Logout button is shown");
        await Assertions.Expect(Page.GetByText("Logout")).ToBeVisibleAsync();
    }
}