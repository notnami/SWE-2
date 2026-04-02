using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System.Collections.ObjectModel;

namespace Tests.Pages
{
    public class WorkoutsPage
    {
        private readonly IWebDriver _driver;

        public WorkoutsPage(IWebDriver driver)
        {
            _driver = driver;
        }

        // Elements
        private SelectElement WorkoutSelect => new SelectElement(_driver.FindElement(By.Id("workout-name")));
        private IWebElement DurationInput => _driver.FindElement(By.Id("duration"));
        private IWebElement AddWorkoutButton => _driver.FindElement(By.ClassName("addWorkoutBtn"));
        private IWebElement WorkoutList => _driver.FindElement(By.Id("workoutList"));
        private IWebElement TotalCalories => _driver.FindElement(By.Id("totalCalories"));

        // Actions
        public void SelectWorkout(string workoutName)
        {
            WorkoutSelect.SelectByText(workoutName);
        }

        public void EnterDuration(string duration)
        {
            DurationInput.Clear();
            DurationInput.SendKeys(duration);
        }

        public void ClickAddWorkout()
        {
            AddWorkoutButton.Click();
        }

        public void AddWorkout(string workoutName, string duration)
        {
            SelectWorkout(workoutName);
            EnterDuration(duration);
            ClickAddWorkout();
        }

        public ReadOnlyCollection<IWebElement> GetWorkoutItems()
        {
            return WorkoutList.FindElements(By.TagName("li"));
        }

        public string GetTotalCalories()
        {
            return TotalCalories.Text;
        }

        public void DeleteWorkout(int index)
        {
            var workoutItems = GetWorkoutItems();
            if (index < workoutItems.Count)
            {
                var deleteBtn = workoutItems[index].FindElement(By.ClassName("delete-btn"));
                deleteBtn.Click();
            }
        }

        // Navigation
        public void GoToWorkoutsPage()
        {
            _driver.Navigate().GoToUrl("http://localhost:5161/workouts.html");
        }
    }
}
