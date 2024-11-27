using Microsoft.Playwright;
using PlaywrightDemo.POM.Pages;

namespace PlaywrightDemo.POM.Fixtures;

[SetUpFixture]
public class LoginFixture
{
    public IPlaywright PW { get; set; }
    public IBrowser Browser { get; set; }

    [OneTimeSetUp]
    public async Task Login()
    {
        PW = await Playwright.CreateAsync();
        Browser = await PW.Chromium.LaunchAsync(new BrowserTypeLaunchOptions { Headless = false });

        var context = await Browser.NewContextAsync();
        var page = await context.NewPageAsync();
        var loginPage = new LoginPage(page);

        await loginPage.GoTo();
        await loginPage.Login("test", "test");


        // save state to be able to use it in other tests
        await context.StorageStateAsync(new()
        {
            Path = Path.Combine(Directory.GetCurrentDirectory(), "state.json")
        });

        await page.CloseAsync();
        await Browser.CloseAsync();
    }
}