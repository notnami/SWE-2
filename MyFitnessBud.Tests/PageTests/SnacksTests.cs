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
        public async Task SearchSnacks_ShouldFilterList()
        {
            // Arrange
            await _snacksPage!.GoToSnacksPage();

            // Act
            await _snacksPage.SearchSnacks("Apple");

            // Assert
            var snackItems = await _snacksPage.GetSnackItems();
            Assert.Single(snackItems);
            var itemText = await snackItems[0].Locator("span:not(.star)").TextContentAsync();
            Assert.Equal("Apple", itemText);
        }

        [Fact]
        public async Task AddIntake_ShouldAddToList()
        {
            // Arrange
            await _snacksPage!.GoToSnacksPage();

            // Act
            await _snacksPage.AddIntake("Apple");

            // Assert
            var intakeItems = await _snacksPage.GetIntakeItems();
            Assert.Single(intakeItems);
            var itemText = await intakeItems[0].TextContentAsync();
            Assert.Contains("apple", itemText, System.StringComparison.OrdinalIgnoreCase);
        }

        [Fact]
        public async Task ToggleFavorite_ShouldMarkAsFavorite()
        {
            // Arrange
            await _snacksPage!.GoToSnacksPage();

            // Act
            await _snacksPage.ToggleFavorite("Apple");

            // Assert
            var snackItems = await _snacksPage.GetSnackItems();
            ILocator? appleItem = null;
            foreach (var item in snackItems)
            {
                var text = await item.Locator("span:not(.star)").TextContentAsync();
                if (text == "Apple")
                {
                    appleItem = item;
                    break;
                }
            }
            Assert.NotNull(appleItem);
            var star = await appleItem!.Locator(".star").TextContentAsync();
            Assert.Equal("★", star);
        }

        [Fact]
        public async Task DeleteIntakeItem_ShouldRemoveFromList()
        {
            // Arrange
            await _snacksPage!.GoToSnacksPage();
            await _snacksPage.AddIntake("Apple");

            // Act
            await _snacksPage.DeleteIntakeItem(0);

            // Assert
            var intakeItems = await _snacksPage.GetIntakeItems();
            Assert.Empty(intakeItems);
        }

        [Fact]
        public async Task TotalCalories_ShouldUpdateAfterAddingIntake()
        {
            // Arrange
            await _snacksPage!.GoToSnacksPage();

            // Act
            await _snacksPage.AddIntake("Apple");

            // Assert
            var totalCalories = await _snacksPage.GetTotalCalories();
            Assert.Equal("95 kcal", totalCalories);
        }

        [Fact]
        public async Task AddMultipleIntake_ShouldAccumulateCalories()
        {
            // Arrange
            await _snacksPage!.GoToSnacksPage();

            // Act
            await _snacksPage.AddIntake("Apple");
            await _snacksPage.AddIntake("Banana");

            // Assert
            var intakeItems = await _snacksPage.GetIntakeItems();
            Assert.Equal(2, intakeItems.Count);
            var totalCalories = await _snacksPage.GetTotalCalories();
            // Apple (95) + Banana (105) = 200
            Assert.Equal("200 kcal", totalCalories);
        }
    }
}
