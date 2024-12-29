using AventStack.ExtentReports;
using PlaywrightDemo.DemoFramework.Pages;
using PlaywrightDemo.Framework.Fixtures;
using PlaywrightDemo.Framework.Utils;

namespace PlaywrightDemo.DemoFramework.Tests;


[TestFixture]
class UploadFilesTest : NotAuthTestFixture
{
    private UploadFilePage _uploadFilePage;

    [SetUp]
    public void SetUp()
    {
        _uploadFilePage = new UploadFilePage(Page);
    }

    [Test]
    public async Task UploadFile()
    {

        Log.WriteLine(Status.Info, "Open Upload File page as not authenticeted user");
        await _uploadFilePage.GoTo();

        var filePath = Path.Combine(AppContext.BaseDirectory, "..", "..", "..", "Framework!!!!", "TestArtifacts", "README.txt");

        Log.WriteLine(Status.Info, $"Upload file {filePath} and wait for upload dialog appears");
        await Page.PauseAsync();
        await _uploadFilePage.UploadFile(filePath);

        // Add event listener for the dialog box
        Page.Dialog += async (_, dialog) =>
        {
            await Page.PauseAsync();
            await dialog.AcceptAsync();
        };
        await Page.PauseAsync();
        await _uploadFilePage.ClickSubmit();
    }
}
