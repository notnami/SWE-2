using OpenQA.Selenium;

namespace Tests.Pages
{
    public class SignUpPage
    {
        private readonly IWebDriver _driver;

        public SignUpPage(IWebDriver driver)
        {
            _driver = driver;
        }

        // Elements
        private IWebElement UsernameInput => _driver.FindElement(By.Id("username"));
        private IWebElement PasswordInput => _driver.FindElement(By.Id("password"));
        private IWebElement WeightInput => _driver.FindElement(By.Id("weight"));
        private IWebElement HeightInput => _driver.FindElement(By.Id("height"));
        private IWebElement RegisterButton => _driver.FindElement(By.CssSelector(".signup .btn-primary"));
        private IWebElement LoginLink => _driver.FindElement(By.LinkText("Already have an account? Log in here."));

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

        public void EnterWeight(string weight)
        {
            WeightInput.Clear();
            WeightInput.SendKeys(weight);
        }

        public void EnterHeight(string height)
        {
            HeightInput.Clear();
            HeightInput.SendKeys(height);
        }

        public void ClickRegister()
        {
            RegisterButton.Click();
        }

        public void ClickLoginLink()
        {
            LoginLink.Click();
        }

        public void SignUp(string username, string password, string weight, string height)
        {
            EnterUsername(username);
            EnterPassword(password);
            EnterWeight(weight);
            EnterHeight(height);
            ClickRegister();
        }

        public void SetUsersData(string usersJson)
        {
            ((IJavaScriptExecutor)_driver).ExecuteScript($"localStorage.setItem('users', '{usersJson}');");
        }

        // Navigation
        public void GoToSignUpPage()
        {
            _driver.Navigate().GoToUrl("http://localhost:5161/signup.html");
        }
    }
}