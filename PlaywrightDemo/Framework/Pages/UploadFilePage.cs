using AventStack.ExtentReports;
using Microsoft.Playwright;
using PlaywrightDemo.Framework.Utils;

namespace PlaywrightDemo.DemoFramework.Pages
{
    public class UploadFilePage
    {
        private readonly IPage _page;

        public UploadFilePage(IPage page)
        {
            _page = page;
        }

        private ILocator choseFile => _page.Locator("input[type=\"file\"]");
        private ILocator submit => _page.Locator("button[type=\"submit\"]");

        public async Task UploadFile(string filePath)
        {
            Log.WriteLine(Status.Info, $"Set input file {filePath} into file Load");
            await choseFile.SetInputFilesAsync(filePath);
        }

        public async Task ClickSubmit()
        {
            Log.WriteLine(Status.Info, "Click Submit button");
            await submit.ClickAsync();
        }

        public async Task GoTo()
        {
            Log.WriteLine(Status.Info, "Go to Upload File Page");
            await _page.GotoAsync("https://commitquality.com/practice-file-upload");
        }
    }
}

