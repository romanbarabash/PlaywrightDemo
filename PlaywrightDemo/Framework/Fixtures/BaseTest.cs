using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using PlaywrightDemo.Framework.Utils;

namespace PlaywrightDemo.POM.Fixtures;

public class BaseTest : PageTest
{
    public static ExtentReports extent;
    public ExtentTest test;

    private static readonly string ReportDirectory = Path.Combine(
        TestContext.CurrentContext.WorkDirectory, "TestResults"
    );

    private static readonly string ReportFilePath = Path.Combine(
        ReportDirectory, "TestReport.html"
    );

    [OneTimeSetUp]
    public void OneTimeSetup()
    {
        if (extent == null)
        {
            if (!Directory.Exists(ReportDirectory))
            {
                Directory.CreateDirectory(ReportDirectory);
            }
            var htmlReporter = new ExtentSparkReporter(ReportFilePath)
            {
                Config =
                {
                    DocumentTitle = "Playwright Test Report",
                    ReportName = "Consolidated Test Results",
                }
            };

            extent = new ExtentReports();
            extent.AttachReporter(htmlReporter);
        }
    }

    [SetUp]
    public async Task Setup()
    {
        await Context.Tracing.StartAsync(new()
        {
            Title = $"{TestContext.CurrentContext.Test.ClassName}.{TestContext.CurrentContext.Test.Name}",
            Screenshots = true,
            Snapshots = true,
            Sources = true
        });

        test = extent.CreateTest(TestContext.CurrentContext.Test.FullName);
        Log.Instance.Initialize(test); // Initialize Log with the ExtentTest
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
            // Save Playwright trace files
            var tracePath = Path.Combine(
                ReportDirectory,
                "playwright-traces",
                $"{TestContext.CurrentContext.Test.ClassName}.{TestContext.CurrentContext.Test.Name}.zip"
            );

            await Context.Tracing.StopAsync(new() { Path = tracePath });
            Log.WriteLine(Status.Info, $"Setup: Trace saved to: {tracePath}");
        }
        catch (Exception ex)
        {
            Log.WriteLine(Status.Warning, $"Setup: Error saving trace: {ex.Message}");
        }

        var outcome = TestContext.CurrentContext.Result.Outcome.Status;
        var message = TestContext.CurrentContext.Result.Message ?? string.Empty;

        switch (outcome)
        {
            case NUnit.Framework.Interfaces.TestStatus.Passed:
                Log.WriteLine(Status.Pass, "Test passed.");
                break;

            case NUnit.Framework.Interfaces.TestStatus.Failed:
                Log.WriteLine(Status.Fail, $"Test failed: {message}");
                CaptureScreenshotForFailure();
                break;

            case NUnit.Framework.Interfaces.TestStatus.Skipped:
                Log.WriteLine(Status.Skip, "Test skipped.");
                break;

            default:
                Log.WriteLine(Status.Warning, "Test outcome neither Pass, Fail or Skip.");
                break;
        }
    }

    [OneTimeTearDown]
    public void OneTimeTearDown()
    {
        if (extent != null)
        {
            extent.Flush();
        }
    }

    private void CaptureScreenshotForFailure()
    {
        try
        {
            var screenshotPath = Path.Combine(
                ReportDirectory,
                "screenshots",
                $"{TestContext.CurrentContext.Test.ClassName}.{TestContext.CurrentContext.Test.Name}.png"
            );

            Directory.CreateDirectory(Path.GetDirectoryName(screenshotPath)!);
            Page.ScreenshotAsync(new PageScreenshotOptions { Path = screenshotPath }).Wait();

            Log.WriteLine(Status.Info, $"Setup: Screenshot saved to: {screenshotPath}");
            test.AddScreenCaptureFromPath(screenshotPath); // Optional, to attach to Extent report
        }
        catch (Exception ex)
        {
            Log.WriteLine(Status.Warning, $"Setup: Error capturing screenshot: {ex.Message}");
        }
    }
}