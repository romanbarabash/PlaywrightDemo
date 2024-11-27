using Microsoft.Playwright;

namespace PlaywrightDemo.POM.Fixtures;

public class AuthTestFixture
{
    public IBrowser Browser { get; set; }
    public IPage Page { get; set; }

    [SetUp]
    public async Task Setup()
    {
        var PW = await Playwright.CreateAsync();
        Browser = await PW.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
        {
            Headless = false,
            Args = ["--start-maximized"]
        });

        // create new context from saved state
        var context = await Browser.NewContextAsync(new()
        {
            StorageStatePath = Path.Combine(Directory.GetCurrentDirectory(), "state.json"),
            ViewportSize = ViewportSize.NoViewport
        });

        Page = await context.NewPageAsync();

    }

    [TearDown]
    public async Task TearDown()
    {
        await Page.CloseAsync();
        await Browser.CloseAsync();
    }
}
