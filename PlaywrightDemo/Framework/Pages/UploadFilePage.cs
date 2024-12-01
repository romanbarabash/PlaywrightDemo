using Microsoft.Playwright;

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
            await choseFile.SetInputFilesAsync(filePath);
        }

        public async Task ClickSubmit()
        {
            await submit.ClickAsync();
        }

        public async Task GoTo()
        {
            await _page.GotoAsync("https://commitquality.com/practice-file-upload");
        }
    }
}

