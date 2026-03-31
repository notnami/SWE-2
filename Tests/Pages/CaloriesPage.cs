using OpenQA.Selenium;

namespace SeleniumTests.Pages
{
    public class CaloriesPage
    {
        private readonly IWebDriver _driver;

        public CaloriesPage(IWebDriver driver)
        {
            _driver = driver;
        }

        // Elements
        private IWebElement Consumed => _driver.FindElement(By.Id("consumed"));
        private IWebElement Burned => _driver.FindElement(By.Id("burned"));
        private IWebElement Maintenance => _driver.FindElement(By.Id("maintenance"));
        private IWebElement Deficit => _driver.FindElement(By.Id("deficit"));

        // Getters
        public string GetConsumedCalories()
        {
            return Consumed.Text;
        }

        public string GetBurnedCalories()
        {
            return Burned.Text;
        }

        public string GetMaintenanceCalories()
        {
            return Maintenance.Text;
        }

        public string GetCalorieDeficit()
        {
            return Deficit.Text;
        }

        public void SetIntakeData(string intakeJson)
        {
            ((IJavaScriptExecutor)_driver).ExecuteScript($"localStorage.setItem('intake', '{intakeJson}');");
        }

        public void SetWorkoutsData(string workoutsJson)
        {
            ((IJavaScriptExecutor)_driver).ExecuteScript($"localStorage.setItem('workouts', '{workoutsJson}');");
        }

        public void SetWeight(string weight)
        {
            ((IJavaScriptExecutor)_driver).ExecuteScript($"localStorage.setItem('weight', '{weight}');");
        }

        public void RefreshPage()
        {
            _driver.Navigate().Refresh();
        }

        // Navigation
        public void GoToCaloriesPage()
        {
            _driver.Navigate().GoToUrl("http://localhost:5161/calories.html");
        }
    }
}
