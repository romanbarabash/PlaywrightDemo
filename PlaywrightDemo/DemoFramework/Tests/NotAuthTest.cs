using Microsoft.Playwright;
using PlaywrightDemo.POM.Fixtures;


namespace PlaywrightDemo.DemoFramework.Tests;

[TestFixture]
public class NotAuthTest : BaseTest
{
    [Test]
    public async Task TestNotLoggined()
    {
        await Page.GotoAsync("https://commitquality.com");
        await Assertions.Expect(Page.GetByText("Login")).ToBeVisibleAsync();
    }
}