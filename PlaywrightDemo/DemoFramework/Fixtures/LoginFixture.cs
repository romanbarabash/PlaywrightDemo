using Microsoft.Playwright;
using PlaywrightDemo.POM.Pages;

namespace PlaywrightDemo.POM.Fixtures;

[SetUpFixture]
public class LoginFixture
{
    public static async Task SaveStateAsync(string username, string password, string stateFilePath)
    {
        var playwright = await Playwright.CreateAsync();
        var browser = await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
        {
            Headless = false,
            Args = new[] { "--start-maximized" }
        });

        var context = await browser.NewContextAsync(new BrowserNewContextOptions
        {
            ViewportSize = null
        });

        var page = await context.NewPageAsync();

        await new LoginPage(page).Login(username, password);

        // Save state
        await context.StorageStateAsync(new BrowserContextStorageStateOptions
        {
            Path = stateFilePath
        });

        await browser.CloseAsync();
    }


    [OneTimeSetUp]
    public async Task GlobalSetup()
    {
        var statePath = Path.Combine(Directory.GetCurrentDirectory(), "state.json");
        if (!File.Exists(statePath))
        {
            await SaveStateAsync("test", "test", statePath);
        }
    }
}