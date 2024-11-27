using System.Text.RegularExpressions;
using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;

namespace PlaywrightDemo.Demos.Hooks;

[Parallelizable(ParallelScope.Self)]
[TestFixture]
public class HooksTest : PageTest
{
    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        Console.WriteLine("OneTimeSetUp");
    }


    [SetUp]
    public async Task OpenPage()
    {
        await Page.GotoAsync("https://commitquality.com");
        Console.WriteLine(Page.Url);
    }

    [Test]
    public async Task TestTitle()
    {
        await Expect(Page).ToHaveTitleAsync(
            new Regex("CommitQuality - Test Automation Demo"),
            new PageAssertionsToHaveTitleOptions { Timeout = 2000 });
    }

    [TearDown]
    public async Task RedirectToGoogle()
    {
        await Page.GotoAsync("https://google.com");
        Console.WriteLine(Page.Url);
    }

    [OneTimeTearDown]
    public void OneTimeTearDown()
    {
        Console.WriteLine("OneTimeTearDown");
    }
}