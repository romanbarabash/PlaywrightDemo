using PlaywrightDemo.DemoFramework.Pages;
using PlaywrightDemo.Framework.Fixtures;

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
        await _uploadFilePage.GoTo();
        await Page.PauseAsync();

        await _uploadFilePage.UploadFile(Path.Combine(AppContext.BaseDirectory, "..", "..", "..", "Framework!!!!", "TestArtifacts", "README.txt"));

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
