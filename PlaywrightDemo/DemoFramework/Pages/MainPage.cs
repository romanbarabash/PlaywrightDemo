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

}