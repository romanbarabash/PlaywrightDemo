using Microsoft.Playwright;

namespace PlaywrightDemo.POM.Fixtures;

public class AuthTestFixture : BaseTest
{
    private static readonly string StateFilePath = Path.Combine(Directory.GetCurrentDirectory(), "state.json");

    public override BrowserNewContextOptions ContextOptions()
    {
        var options = base.ContextOptions();
        options.StorageStatePath = StateFilePath; // Add the authenticated state
        return options;
    }
}


