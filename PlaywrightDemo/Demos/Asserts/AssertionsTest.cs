using System.Text.RegularExpressions;
using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;

namespace PlaywrightDemo.Demos.Asserts;

[Parallelizable(ParallelScope.Self)]
[TestFixture]
public class AssertionsTest : PageTest
{
    [Test]
    public async Task Checker()
    {
        await Page.GotoAsync("https://commitquality.com");
        await Expect(Page).ToHaveTitleAsync(
            new Regex("CommitQuality - Test Automation Demo"),
            new PageAssertionsToHaveTitleOptions { Timeout = 2000 }); //ex. with custom timeout for Page

        var firstRowName = Page.GetByTestId("name").First;
        await Expect(firstRowName).Not.ToHaveTextAsync(
            "Product 21",
            new LocatorAssertionsToHaveTextOptions { Timeout = 2000 }); //ex. with custom timeout for Locator
    }
}