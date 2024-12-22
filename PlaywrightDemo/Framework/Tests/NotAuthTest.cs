using AventStack.ExtentReports;
using Microsoft.Playwright;
using NUnit.Framework.Internal;
using PlaywrightDemo.Framework.Utils;
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

        Log.WriteLine(Status.Info, "Test: verify Login button is present");
        await Assertions.Expect(Page.GetByText("Login")).ToBeVisibleAsync();
    }
}
