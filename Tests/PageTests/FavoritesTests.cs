using NUnit.Framework;
using SeleniumTests.Pages;

namespace SeleniumTests.PageTests
{
    [TestFixture]
    public class FavoritesTests : BaseTest
    {
        private FavoritesPage _favoritesPage;
        private SnacksPage _snacksPage;

        [SetUp]
        public void TestSetup()
        {
            _favoritesPage = new FavoritesPage(driver);
            _snacksPage = new SnacksPage(driver);
        }

        [Test]
        public void RemoveFavorite_ShouldRemoveFromList()
        {
            // Arrange
            _snacksPage.GoToSnacksPage();
            _snacksPage.ToggleFavorite("Apple");
            _favoritesPage.GoToFavoritesPage();

            // Act
            _favoritesPage.RemoveFavorite(0);

            // Assert
            var favoriteItems = _favoritesPage.GetFavoriteItems();
            Assert.AreEqual(0, favoriteItems.Count);
        }

        [Test]
        public void Logout_ShouldRedirectToLogin()
        {
            // Arrange
            _favoritesPage.GoToFavoritesPage();

            // Act
            _favoritesPage.ClickLogout();

            // Accept alert
            var alert = driver.SwitchTo().Alert();
            alert.Accept();

            // Assert
            Assert.AreEqual("http://localhost:5161/login.html", driver.Url);
        }

        [Test]
        public void MultipleFavorites_ShouldDisplayAll()
        {
            // Arrange
            _snacksPage.GoToSnacksPage();
            _snacksPage.ToggleFavorite("Apple");
            _snacksPage.ToggleFavorite("Banana");
            _favoritesPage.GoToFavoritesPage();

            // Act & Assert
            var favoriteItems = _favoritesPage.GetFavoriteItems();
            Assert.AreEqual(2, favoriteItems.Count);
            Assert.IsTrue(favoriteItems[0].Text.Contains("Apple") || favoriteItems[1].Text.Contains("Apple"));
            Assert.IsTrue(favoriteItems[0].Text.Contains("Banana") || favoriteItems[1].Text.Contains("Banana"));
        }

        [Test]
        public void NoFavorites_ShouldShowMessage()
        {
            // Arrange
            _favoritesPage.GoToFavoritesPage();

            // Act & Assert
            var favoriteItems = _favoritesPage.GetFavoriteItems();
            Assert.AreEqual(1, favoriteItems.Count); // The "No favorites" li
            Assert.AreEqual("No favorites added yet.", favoriteItems[0].Text);
        }
    }
}
