using Microsoft.Playwright;
using PlaywrightDemo.POM.Fixtures;

namespace PlaywrightDemo.POM.Tests;

[TestFixture]
public class AuthTest : AuthTestFixture
{
    [Test]
    public async Task TestLoggined()
    {
        await Page.GotoAsync("https://commitquality.com");
        await Assertions.Expect(Page.GetByText("Logout")).ToBeVisibleAsync();
    }
}