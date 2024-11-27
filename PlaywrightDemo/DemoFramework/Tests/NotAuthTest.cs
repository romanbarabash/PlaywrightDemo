using Microsoft.Playwright;
using PlaywrightDemo.POM.Fixtures;


namespace PlaywrightDemo.POM.Tests;

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