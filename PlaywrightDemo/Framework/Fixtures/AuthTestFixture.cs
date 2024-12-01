using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;

namespace PlaywrightDemo.POM.Fixtures;

public class AuthTestFixture : PageTest
{
    private static readonly string StateFilePath = Path.Combine(Directory.GetCurrentDirectory(), "state.json");

    public override BrowserNewContextOptions ContextOptions()
    {
        return new BrowserNewContextOptions
        {
            StorageStatePath = StateFilePath,
            ViewportSize = new ViewportSize { Width = 1920, Height = 1080 }
        };
    }

    [SetUp]
    public async Task StartTracingAsync()
    {
        await Context.Tracing.StartAsync(new TracingStartOptions
        {
            Title = $"{TestContext.CurrentContext.Test.ClassName}.{TestContext.CurrentContext.Test.Name}",
            Screenshots = true,
            Snapshots = true,
            Sources = true
        });
    }

    [TearDown]
    public async Task StopTracingAsync()
    {
        await Context.Tracing.StopAsync(new TracingStopOptions
        {
            Path = Path.Combine(
                TestContext.CurrentContext.WorkDirectory,
                "playwright-traces",
                $"{TestContext.CurrentContext.Test.ClassName}.{TestContext.CurrentContext.Test.Name}.zip"
            )
        });
    }
}