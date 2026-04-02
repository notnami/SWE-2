using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using NUnit.Framework;
using System;

namespace Tests
{
    public class BaseTest
    {
        protected IWebDriver driver;
        private const string BaseUrl = "http://localhost:5161/"; // Adjust to your application URL

        [SetUp]
        public virtual void Setup()
        {
            var options = new ChromeOptions();
            driver = new ChromeDriver(options);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            driver.Navigate().GoToUrl(BaseUrl);
        }

        [TearDown]
        public virtual void Teardown()
        {
            driver?.Quit();
            driver?.Dispose();
        }

        protected void WaitForElement(By locator, int timeoutSeconds = 10)
        {
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeoutSeconds));
            wait.Until(d => d.FindElement(locator));
        }

        protected IWebElement FindElement(By locator)
        {
            return driver.FindElement(locator);
        }

        protected void Click(By locator)
        {
            WaitForElement(locator);
            FindElement(locator).Click();
        }

        protected void SendKeys(By locator, string text)
        {
            WaitForElement(locator);
            FindElement(locator).SendKeys(text);
        }

        protected string GetText(By locator)
        {
            WaitForElement(locator);
            return FindElement(locator).Text;
        }
    }
}