using Microsoft.Playwright;

namespace PlaywrightDemo.POM.Fixtures;

public class AuthTestFixture
{
    public static IPlaywright Playwright { get; private set; }
    public static IBrowser Browser { get; private set; }
    public IBrowserContext Context { get; private set; }
    public IPage Page { get; private set; }

    [OneTimeSetUp]
    public async Task OneTimeSetup()
    {
        Playwright = await Microsoft.Playwright.Playwright.CreateAsync();
        Browser = await Playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
        {
            Headless = false,
            Args = new[] { "--start-maximized" }
        });
    }

    [SetUp]
    public async Task Setup()
    {
        // Create new context from saved state
        Context = await Browser.NewContextAsync(new BrowserNewContextOptions
        {
            StorageStatePath = Path.Combine(Directory.GetCurrentDirectory(), "state.json"),
            ViewportSize = new ViewportSize { Width = 1920, Height = 1080 }
        });

        Page = await Context.NewPageAsync();
        await StartTraceTests(Context);
    }

    [TearDown]
    public async Task TearDown()
    {
        await StopTraceTests(Context);
        await Page.CloseAsync();
        await Context.CloseAsync();
    }

    [OneTimeTearDown]
    public async Task OneTimeTearDown()
    {
        await Browser.CloseAsync();
        Playwright.Dispose();
    }

    public async Task StartTraceTests(IBrowserContext context)
    {
        await context.Tracing.StartAsync(new TracingStartOptions
        {
            Title = $"{TestContext.CurrentContext.Test.ClassName}.{TestContext.CurrentContext.Test.Name}",
            Screenshots = true,
            Snapshots = true,
            Sources = true
        });
    }

    public async Task StopTraceTests(IBrowserContext context)
    {
        await context.Tracing.StopAsync(new TracingStopOptions
        {
            Path = Path.Combine(
                TestContext.CurrentContext.WorkDirectory,
                "playwright-traces",
                $"{TestContext.CurrentContext.Test.ClassName}.{TestContext.CurrentContext.Test.Name}.zip"
            )
        });
    }
}
