using OpenQA.Selenium;

namespace Tests.Pages
{
    public class HomePage
    {
        private readonly IWebDriver _driver;

        public HomePage(IWebDriver driver)
        {
            _driver = driver;
        }

        // Elements
        private IWebElement SignUpButton => _driver.FindElement(By.CssSelector(".main .btn-primary"));
        private IWebElement LoginButton => _driver.FindElement(By.CssSelector(".main .btn-secondary"));
        private IWebElement LogoutLink => _driver.FindElement(By.ClassName("logoutLink"));
        private IWebElement MainSection => _driver.FindElement(By.ClassName("main"));
        private IWebElement MainLoginSection => _driver.FindElement(By.ClassName("mainLogin"));
        private IWebElement Header => _driver.FindElement(By.CssSelector("header h1"));
        private IWebElement ProfileUsername => _driver.FindElement(By.Id("profileUsername"));
        private IWebElement ProfileWeight => _driver.FindElement(By.Id("profileWeight"));
        private IWebElement ProfileHeight => _driver.FindElement(By.Id("profileHeight"));

        // Actions
        public void ClickSignUp()
        {
            SignUpButton.Click();
        }

        public void ClickLogin()
        {
            LoginButton.Click();
        }

        public void ClickLogout()
        {
            LogoutLink.Click();
        }

        public void ClickNavigationLink(string linkText)
        {
            var link = _driver.FindElement(By.LinkText(linkText));
            link.Click();
        }

        public string GetHeaderText()
        {
            return Header.Text;
        }

        public string GetProfileUsername()
        {
            return ProfileUsername.Text;
        }

        public string GetProfileWeight()
        {
            return ProfileWeight.Text;
        }

        public string GetProfileHeight()
        {
            return ProfileHeight.Text;
        }

        public bool IsLoggedIn()
        {
            return MainLoginSection.Displayed;
        }

        public bool IsLoggedOut()
        {
            return MainSection.Displayed;
        }

        // Navigation
        public void GoToHomePage()
        {
            _driver.Navigate().GoToUrl("http://localhost:5161/index.html");
        }
    }
}