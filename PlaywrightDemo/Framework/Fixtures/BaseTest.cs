using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;

namespace PlaywrightDemo.POM.Fixtures;

public class BaseTest : PageTest
{
    private static ExtentReports _extent;
    private ExtentTest _test;


    [OneTimeSetUp]
    public void OneTimeSetup()
    {
        // Define the path for the report
        var reportPath = Path.Combine(TestContext.CurrentContext.WorkDirectory, "TestResults", "TestReport.html");

        // Create directories if not present
        var reportDir = Path.GetDirectoryName(reportPath);
        if (!Directory.Exists(reportDir))
        {
            Directory.CreateDirectory(reportDir);
        }

        // Initialize ExtentReports with a SparkReporter
        var htmlReporter = new ExtentSparkReporter(reportPath);
        htmlReporter.Config.DocumentTitle = "Playwright Test Report";
        htmlReporter.Config.ReportName = "Playwright Test Results";
        //htmlReporter.Config.Theme = AventStack.ExtentReports.Reporter.Configuration.Theme.Standard;

        _extent = new ExtentReports();
        _extent.AttachReporter(htmlReporter);
    }

    [SetUp]
    public async Task Setup()
    {
        // Start Playwright tracing
        await Context.Tracing.StartAsync(new()
        {
            Title = $"{TestContext.CurrentContext.Test.ClassName}.{TestContext.CurrentContext.Test.Name}",
            Screenshots = true,
            Snapshots = true,
            Sources = true
        });

        // Create a test node in ExtentReports
        _test = _extent.CreateTest(TestContext.CurrentContext.Test.FullName);
    }


    public override BrowserNewContextOptions ContextOptions()
    {
        return new BrowserNewContextOptions()
        {
            ColorScheme = ColorScheme.Light,
            ViewportSize = new() { Width = 1920, Height = 1080 },
            BaseURL = "https://commitquality.com",
        };
    }


    [TearDown]
    public async Task TearDown()
    {
        try
        {
            // Save trace files
            var tracePath = Path.Combine(
                TestContext.CurrentContext.WorkDirectory,
                "playwright-traces",
                $"{TestContext.CurrentContext.Test.ClassName}.{TestContext.CurrentContext.Test.Name}.zip"
            );
            await Context.Tracing.StopAsync(new() { Path = tracePath });

            _test.Log(Status.Info, $"Trace saved to: {tracePath}");
        }
        catch (Exception ex)
        {
            _test.Log(Status.Warning, $"Error during trace saving: {ex.Message}");
        }

        // Determine test outcome and log to ExtentReports
        var outcome = TestContext.CurrentContext.Result.Outcome.Status;
        var message = TestContext.CurrentContext.Result.Message ?? string.Empty;

        switch (outcome)
        {
            case NUnit.Framework.Interfaces.TestStatus.Passed:
                _test.Pass("Test passed.");
                break;
            case NUnit.Framework.Interfaces.TestStatus.Failed:
                _test.Fail($"Test failed: {message}");
                CaptureScreenshotForFailure();
                break;
            case NUnit.Framework.Interfaces.TestStatus.Skipped:
                _test.Skip("Test skipped.");
                break;
            default:
                _test.Warning("Test outcome unknown.");
                break;
        }
    }

    [OneTimeTearDown]
    public void OneTimeTearDown()
    {
        // Flush ExtentReports to ensure all logs are written
        _extent.Flush();
    }

    private void CaptureScreenshotForFailure()
    {
        try
        {
            // Save a screenshot for failed tests
            var screenshotPath = Path.Combine(
                TestContext.CurrentContext.WorkDirectory,
                "screenshots",
                $"{TestContext.CurrentContext.Test.ClassName}.{TestContext.CurrentContext.Test.Name}.png"
            );
            Directory.CreateDirectory(Path.GetDirectoryName(screenshotPath)!);
            Page.ScreenshotAsync(new PageScreenshotOptions { Path = screenshotPath }).Wait();
            _test.AddScreenCaptureFromPath(screenshotPath);
        }
        catch (Exception ex)
        {
            _test.Log(Status.Warning, $"Failed to capture screenshot: {ex.Message}");
        }
    }
}
