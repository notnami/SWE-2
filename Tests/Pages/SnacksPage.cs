using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System.Collections.ObjectModel;

namespace SeleniumTests.Pages
{
    public class SnacksPage
    {
        private readonly IWebDriver _driver;

        public SnacksPage(IWebDriver driver)
        {
            _driver = driver;
        }

        // Elements
        private IWebElement SearchInput => _driver.FindElement(By.Id("searchInput"));
        private IWebElement SearchButton => _driver.FindElement(By.CssSelector(".search-btn"));
        private IWebElement SnackList => _driver.FindElement(By.Id("snackList"));
        private IWebElement FoodNameInput => _driver.FindElement(By.Id("food-name"));
        private IWebElement AddButton => _driver.FindElement(By.CssSelector(".add-btn"));
        private IWebElement IntakeList => _driver.FindElement(By.Id("intakeList"));
        private IWebElement TotalCalories => _driver.FindElement(By.Id("totalCalories"));

        // Actions
        public void EnterSearchText(string text)
        {
            SearchInput.Clear();
            SearchInput.SendKeys(text);
        }

        public void ClickSearch()
        {
            SearchButton.Click();
        }

        public void SearchSnacks(string text)
        {
            EnterSearchText(text);
            ClickSearch();
        }

        public void EnterFoodName(string food)
        {
            FoodNameInput.Clear();
            FoodNameInput.SendKeys(food);
        }

        public void ClickAdd()
        {
            AddButton.Click();
        }

        public void AddIntake(string food)
        {
            EnterFoodName(food);
            ClickAdd();
        }

        public ReadOnlyCollection<IWebElement> GetSnackItems()
        {
            return SnackList.FindElements(By.TagName("li"));
        }

        public ReadOnlyCollection<IWebElement> GetIntakeItems()
        {
            return IntakeList.FindElements(By.TagName("li"));
        }

        public string GetTotalCalories()
        {
            return TotalCalories.Text;
        }

        public void ToggleFavorite(string snackName)
        {
            var snackItems = GetSnackItems();
            foreach (var item in snackItems)
            {
                var nameSpan = item.FindElement(By.TagName("span"));
                if (nameSpan.Text == snackName)
                {
                    var star = item.FindElements(By.ClassName("star"))[0];
                    star.Click();
                    break;
                }
            }
        }

        public void DeleteIntakeItem(int index)
        {
            var intakeItems = GetIntakeItems();
            if (index < intakeItems.Count)
            {
                var deleteBtn = intakeItems[index].FindElement(By.ClassName("delete-btn"));
                deleteBtn.Click();
            }
        }

        // Navigation
        public void GoToSnacksPage()
        {
            _driver.Navigate().GoToUrl("http://localhost:5161/snacks.html");
        }
    }
}
