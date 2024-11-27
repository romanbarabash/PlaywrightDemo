﻿using Microsoft.Playwright;

namespace PlaywrightDemo.POM.Pages;

public class LoginPage
{
    private readonly IPage _page;

    public LoginPage(IPage page)
    {
        _page = page;
    }

    private ILocator UsernameInput => _page.GetByPlaceholder("Enter Username");
    private ILocator PasswordInput => _page.GetByPlaceholder("Enter Password");
    private ILocator LoginButton => _page.Locator("//button[@data-testid='login-button']");

    public async Task Login(string username, string password)
    {
        await UsernameInput.FillAsync(username);
        await PasswordInput.FillAsync(password);
        await LoginButton.ClickAsync();
    }

    public async Task GoTo()
    {
        await _page.GotoAsync("https://commitquality.com/login");
    }
}