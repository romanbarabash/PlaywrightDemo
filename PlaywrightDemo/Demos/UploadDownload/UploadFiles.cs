using Microsoft.Playwright.NUnit;

namespace PlaywrightDemo.Demos.UploadDownload;


[TestFixture]
class UploadFiles : PageTest
{
    [Test]
    public async Task UploadFile()
    {
        await Page.GotoAsync("https://commitquality.com/practice-file-upload");
        await Page.PauseAsync();

        await Page.Locator("input[type=\"file\"]").SetInputFilesAsync("C:\\Users\\rbarabash\\RiderProjects\\PlaywrightFirstDemo\\PlaywrightDemo\\UploadDownload\\README.txt");

        // Add event listener for the dialog box
        Page.Dialog += async (_, dialog) =>
        {
            await Page.PauseAsync();
            await dialog.AcceptAsync();
        };
        await Page.PauseAsync();

        await Page.GetByText("Submit").ClickAsync();
    }
}
