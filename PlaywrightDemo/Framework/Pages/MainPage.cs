using Microsoft.Playwright;
using PlaywrightDemo.POM.Pages.Sections;

namespace PlaywrightDemo.POM.Pages;

public class MainPage
{
    private readonly IPage _page;

    public MainPage(IPage page)
    {
        _page = page;
    }

    public NavigationBar NavigationBar => new(_page);

    public ILocator BannerMessage => _page.Locator("//*[@class='banner-container']/p");

    internal async Task GoTo()
    {
        await _page.GotoAsync("https://commitquality.com"); ;
    }
}