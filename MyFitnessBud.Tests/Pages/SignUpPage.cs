using Microsoft.Playwright;

namespace MyFitnessBud.Tests.Pages
{
    public class SignUpPage
    {
        private readonly IPage _page;

        public SignUpPage(IPage page)
        {
            _page = page;
        }

        // Locators
        private ILocator UsernameInput => _page.Locator("#username");
        private ILocator PasswordInput => _page.Locator("#password");
        private ILocator WeightInput => _page.Locator("#weight");
        private ILocator HeightInput => _page.Locator("#height");
        private ILocator RegisterButton => _page.Locator(".signup .btn-primary");
        private ILocator LoginLink => _page.Locator("a:has-text('Already have an account? Log in here.')");

        // Actions
        public async Task EnterUsername(string username)
        {
            await UsernameInput.FillAsync(username);
        }

        public async Task EnterPassword(string password)
        {
            await PasswordInput.FillAsync(password);
        }

        public async Task EnterWeight(string weight)
        {
            await WeightInput.FillAsync(weight);
        }

        public async Task EnterHeight(string height)
        {
            await HeightInput.FillAsync(height);
        }

        public async Task ClickRegister()
        {
            await RegisterButton.ClickAsync();
        }

        public async Task ClickLoginLink()
        {
            await LoginLink.ClickAsync();
        }

        public async Task SignUp(string username, string password, string weight, string height)
        {
            await EnterUsername(username);
            await EnterPassword(password);
            await EnterWeight(weight);
            await EnterHeight(height);
            await ClickRegister();
        }

        public async Task SetUsersData(string usersJson)
        {
            await _page.EvaluateAsync($"localStorage.setItem('users', '{usersJson}');");
        }

        // Navigation
        public async Task GoToSignUpPage()
        {
            await _page.GotoAsync("http://localhost:5161/signup.html");
        }
    }
}
