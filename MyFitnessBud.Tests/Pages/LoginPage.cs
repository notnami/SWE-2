using Microsoft.Playwright;

namespace MyFitnessBud.Tests.Pages
{
    public class LoginPage
    {
        private readonly IPage _page;

        public LoginPage(IPage page)
        {
            _page = page;
        }

        // Locators
        private ILocator UsernameInput => _page.Locator("#username");
        private ILocator PasswordInput => _page.Locator("#password");
        private ILocator LoginButton => _page.Locator(".login .btn-secondary");
        private ILocator SignUpButton => _page.Locator(".login .btn-primary");

        // Actions
        public async Task EnterUsername(string username)
        {
            await UsernameInput.FillAsync(username);
        }

        public async Task EnterPassword(string password)
        {
            await PasswordInput.FillAsync(password);
        }

        public async Task ClickLogin()
        {
            await LoginButton.ClickAsync();
        }

        public async Task ClickSignUp()
        {
            await SignUpButton.ClickAsync();
        }

        public async Task Login(string username, string password)
        {
            await EnterUsername(username);
            await EnterPassword(password);
            await ClickLogin();
        }

        public async Task SetUsersData(string usersJson)
        {
            await _page.EvaluateAsync($"localStorage.setItem('users', '{usersJson}');");
        }

        // Navigation
        public async Task GoToLoginPage()
        {
            await _page.GotoAsync("http://localhost:5161/login.html");
        }
    }
}
