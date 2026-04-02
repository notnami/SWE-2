using OpenQA.Selenium;

namespace Tests.Pages
{
    public class LoginPage
    {
        private readonly IWebDriver _driver;

        public LoginPage(IWebDriver driver)
        {
            _driver = driver;
        }

        // Elements
        private IWebElement UsernameInput => _driver.FindElement(By.Id("username"));
        private IWebElement PasswordInput => _driver.FindElement(By.Id("password"));
        private IWebElement LoginButton => _driver.FindElement(By.CssSelector(".login .btn-secondary"));
        private IWebElement SignUpButton => _driver.FindElement(By.CssSelector(".login .btn-primary"));

        // Actions
        public void EnterUsername(string username)
        {
            UsernameInput.Clear();
            UsernameInput.SendKeys(username);
        }

        public void EnterPassword(string password)
        {
            PasswordInput.Clear();
            PasswordInput.SendKeys(password);
        }

        public void ClickLogin()
        {
            LoginButton.Click();
        }

        public void ClickSignUp()
        {
            SignUpButton.Click();
        }

        public void Login(string username, string password)
        {
            EnterUsername(username);
            EnterPassword(password);
            ClickLogin();
        }

        public void SetUsersData(string usersJson)
        {
            ((IJavaScriptExecutor)_driver).ExecuteScript($"localStorage.setItem('users', '{usersJson}');");
        }

        // Navigation
        public void GoToLoginPage()
        {
            _driver.Navigate().GoToUrl("http://localhost:5161/login.html");
        }
    }
}
