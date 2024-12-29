using AventStack.ExtentReports;
using PlaywrightDemo.Framework.Fixtures;
using PlaywrightDemo.Framework.Utils;
using PlaywrightDemo.POM.Pages;

namespace PlaywrightDemo.DemoFramework.Tests;

[TestFixture]
public class LoginTest : NotAuthTestFixture
{
    private LoginPage _loginPage;
    private MainPage _mainPage;

    [SetUp]
    public void SetUp()
    {
        _loginPage = new LoginPage(Page);
        _mainPage = new MainPage(Page);
    }

    [Test]
    public async Task TestLoggined()
    {
        Log.WriteLine(Status.Info, "Open Login page as not authenticeted user and login");
        await _loginPage.GoTo();
        await _loginPage.Login("test", "test");

        Log.WriteLine(Status.Info, "Verify user authentificated and Logout button is shown");
        await Expect(_mainPage.NavigationBar.LogoutButton).ToBeVisibleAsync();
    }
}
