using MyFitnessBud.Tests.Pages;

namespace MyFitnessBud.Tests.PageTests
{
    public class FavoritesTests : BaseTest
    {
        private FavoritesPage? _favoritesPage;
        private SnacksPage? _snacksPage;

        public FavoritesTests() : base() { }

        public override async Task InitializeAsync()
        {
            await base.InitializeAsync();
            _favoritesPage = new FavoritesPage(page);
            _snacksPage = new SnacksPage(page);
        }

        [Fact]
        public async Task RemoveFavorite_ShouldRemoveFromList()
        {
            // Arrange
            await _snacksPage!.GoToSnacksPage();
            await _snacksPage.ToggleFavorite("Apple");
            await _favoritesPage!.GoToFavoritesPage();

            // Act
            await _favoritesPage.RemoveFavorite(0);

            // Assert
            var favoriteItems = await _favoritesPage.GetFavoriteItems();
            Assert.Empty(favoriteItems);
        }

        [Fact]
        public async Task Logout_ShouldRedirectToLogin()
        {
            // Arrange
            await _favoritesPage!.GoToFavoritesPage();

            // Act
            await WaitForDialog(async () => await _favoritesPage.ClickLogout());

            // Assert
            Assert.Equal("http://localhost:5161/login.html", page.Url);
        }

        [Fact]
        public async Task MultipleFavorites_ShouldDisplayAll()
        {
            // Arrange
            await _snacksPage!.GoToSnacksPage();
            await _snacksPage.ToggleFavorite("Apple");
            await _snacksPage.ToggleFavorite("Banana");
            await _favoritesPage!.GoToFavoritesPage();

            // Act & Assert
            var favoriteItems = await _favoritesPage.GetFavoriteItems();
            Assert.Equal(2, favoriteItems.Count);
            var text1 = await favoriteItems[0].TextContentAsync();
            var text2 = await favoriteItems[1].TextContentAsync();
            Assert.True(
                (text1?.Contains("Apple") ?? false) || (text2?.Contains("Apple") ?? false),
                "Apple not found in favorites"
            );
            Assert.True(
                (text1?.Contains("Banana") ?? false) || (text2?.Contains("Banana") ?? false),
                "Banana not found in favorites"
            );
        }

        [Fact]
        public async Task NoFavorites_ShouldShowMessage()
        {
            // Arrange
            await _favoritesPage!.GoToFavoritesPage();

            // Act & Assert
            var favoriteItems = await _favoritesPage.GetFavoriteItems();
            Assert.Single(favoriteItems); // The "No favorites" li
            var text = await favoriteItems[0].TextContentAsync();
            Assert.Equal("No favorites added yet.", text);
        }
    }
}
