using MyFitnessBud.Tests.Pages;

namespace MyFitnessBud.Tests.PageTests
{
    public class SnacksTests : BaseTest
    {
        private SnacksPage? _snacksPage;

        public SnacksTests() : base() { }

        public override async Task InitializeAsync()
        {
            await base.InitializeAsync();
            _snacksPage = new SnacksPage(page);
        }

        [Fact]
        public async Task SearchSnacks_ShouldReturnResults()
        {
            // Arrange
            await _snacksPage!.MockSearchResults(new object[]
            {
                new
                {
                    productCode = "111",
                    name = "Apple",
                    imageUrl = "",
                    allergens = "",
                    ingredientsAvailable = true
                }
            });
            await _snacksPage.GoToSnacksPage();

            // Act
            await _snacksPage.SearchSnacks("apple");

            // Assert
            var snackItems = await _snacksPage.GetSnackItems();
            Assert.NotEmpty(snackItems);

            var hasApple = false;
            foreach (var item in snackItems)
            {
                var text = await item.Locator("span:not(.star)").TextContentAsync();
                if (text?.Contains("apple", System.StringComparison.OrdinalIgnoreCase) == true)
                {
                    hasApple = true;
                    break;
                }
            }

            Assert.True(hasApple, "Expected at least one search result item containing 'apple'.");
        }

        [Fact]
        public async Task ToggleFavorite_WhenNotLoggedIn_ShowsLoginPrompt()
        {
            // Arrange
            await _snacksPage!.MockSearchResults(new object[]
            {
                new
                {
                    productCode = "111",
                    name = "Apple",
                    imageUrl = "",
                    allergens = "",
                    ingredientsAvailable = true
                }
            });
            await _snacksPage.GoToSnacksPage();
            await _snacksPage.SearchSnacks("apple");

            // Act
            var alertText = await WaitForDialog(async () => await _snacksPage.ToggleFavorite("Apple"));

            // Assert
            Assert.Equal("Please log in first.", alertText);
        }
    }
}
