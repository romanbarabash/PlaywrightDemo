using PlaywrightDemo.Framework.Fixtures;
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
        await _loginPage.GoTo();
        await _loginPage.Login("test", "test");

        // assert
        await Expect(_mainPage.NavigationBar.LogoutButton).ToBeVisibleAsync();
    }
}
