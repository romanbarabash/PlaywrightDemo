using Microsoft.Playwright;

namespace PlaywrightDemo.POM.Pages.Sections;

public class NavigationBar
{
    private readonly IPage _page;

    public NavigationBar(IPage page)
    {
        _page = page;
    }

    public ILocator LogoutButton => _page.GetByText("Logout");

}