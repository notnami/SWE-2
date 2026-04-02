using OpenQA.Selenium;
using System.Collections.ObjectModel;

namespace Tests.Pages
{
    public class FavoritesPage
    {
        private readonly IWebDriver _driver;

        public FavoritesPage(IWebDriver driver)
        {
            _driver = driver;
        }

        // Elements
        private IWebElement FavoritesList => _driver.FindElement(By.Id("favoritesList"));
        private IWebElement LogoutLink => _driver.FindElement(By.ClassName("logoutLink"));

        // Actions
        public ReadOnlyCollection<IWebElement> GetFavoriteItems()
        {
            return FavoritesList.FindElements(By.TagName("li"));
        }

        public void RemoveFavorite(int index)
        {
            var favoriteItems = GetFavoriteItems();
            if (index < favoriteItems.Count)
            {
                var removeBtn = favoriteItems[index].FindElement(By.ClassName("remove-btn"));
                removeBtn.Click();
            }
        }

        public void ClickLogout()
        {
            LogoutLink.Click();
        }

        // Navigation
        public void GoToFavoritesPage()
        {
            _driver.Navigate().GoToUrl("http://localhost:5161/favorites.html");
        }
    }
}
