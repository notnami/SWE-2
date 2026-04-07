namespace MyFitnessBud.Tests.Pages
{
    public class HomePage
    {
        private readonly IPage _page;

        public HomePage(IPage page)
        {
            _page = page;
        }

        // Locators
        private ILocator SignUpButton => _page.Locator(".main .btn-primary");
        private ILocator LoginButton => _page.Locator(".main .btn-secondary");
        private ILocator LogoutLink => _page.Locator(".logoutLink");
        private ILocator MainSection => _page.Locator(".main");
        private ILocator MainLoginSection => _page.Locator(".mainLogin");
        private ILocator Header => _page.Locator("header h1");
        private ILocator ProfileUsername => _page.Locator("#profileUsername");
        private ILocator ProfileWeight => _page.Locator("#profileWeight");
        private ILocator ProfileHeight => _page.Locator("#profileHeight");

        // Actions
        public async Task ClickSignUp()
        {
            await SignUpButton.ClickAsync();
        }

        public async Task ClickLogin()
        {
            await LoginButton.ClickAsync();
        }

        public async Task ClickLogout()
        {
            await LogoutLink.ClickAsync();
        }

        public async Task ClickNavigationLink(string linkText)
        {
            await _page.ClickAsync($"a:has-text('{linkText}')");
        }

        public async Task<string> GetHeaderText()
        {
            return await Header.TextContentAsync() ?? "";
        }

        public async Task<string> GetProfileUsername()
        {
            return await ProfileUsername.TextContentAsync() ?? "";
        }

        public async Task<string> GetProfileWeight()
        {
            return await ProfileWeight.TextContentAsync() ?? "";
        }

        public async Task<string> GetProfileHeight()
        {
            return await ProfileHeight.TextContentAsync() ?? "";
        }

        public async Task<bool> IsLoggedIn()
        {
            return await MainLoginSection.IsVisibleAsync();
        }

        public async Task<bool> IsLoggedOut()
        {
            return await MainSection.IsVisibleAsync();
        }

        // Navigation
        public async Task GoToHomePage()
        {
            await _page.GotoAsync("http://localhost:5161/index.html");
        }
    }
}
