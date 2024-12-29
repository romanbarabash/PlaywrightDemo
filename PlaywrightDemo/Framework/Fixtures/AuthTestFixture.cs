using AventStack.ExtentReports;
using Microsoft.Playwright;
using PlaywrightDemo.Framework.Utils;

namespace PlaywrightDemo.POM.Fixtures;

public class AuthTestFixture : BaseTest
{
    private static readonly string StateFilePath = Path.Combine(Directory.GetCurrentDirectory(), "state.json");

    public override BrowserNewContextOptions ContextOptions()
    {
        var options = base.ContextOptions();
        options.StorageStatePath = StateFilePath;

        Log.WriteLine(Status.Info, $"Browser context options using saved state: {options}");

        return options;
    }
}


