using Xunit;
using MyFitnessBud.Tests.Pages;

namespace MyFitnessBud.Tests.PageTests
{
    public class CaloriesTests : BaseTest
    {
        private CaloriesPage? _caloriesPage;

        public CaloriesTests() : base() { }

        public override async Task InitializeAsync()
        {
            await base.InitializeAsync();
            _caloriesPage = new CaloriesPage(page);
        }

        [Fact]
        public async Task CaloriesPage_ShouldDisplayValues()
        {
            // Arrange
            await _caloriesPage!.GoToCaloriesPage();

            // Act & Assert
            Assert.NotNull(await _caloriesPage.GetConsumedCalories());
            Assert.NotNull(await _caloriesPage.GetBurnedCalories());
            Assert.NotNull(await _caloriesPage.GetMaintenanceCalories());
            Assert.NotNull(await _caloriesPage.GetCalorieDeficit());
        }

        [Fact]
        public async Task CaloriesPage_WithIntakeData_ShouldCalculateConsumed()
        {
            // Arrange
            await _caloriesPage!.GoToCaloriesPage();
            await _caloriesPage.SetIntakeData("[{\"name\":\"Apple\",\"calories\":95},{\"name\":\"Banana\",\"calories\":105}]");
            await _caloriesPage.RefreshPage();

            // Act & Assert
            var consumed = await _caloriesPage.GetConsumedCalories();
            Assert.Equal("200 kcal", consumed);
        }

        [Fact]
        public async Task CaloriesPage_WithWorkoutsData_ShouldCalculateBurned()
        {
            // Arrange
            await _caloriesPage!.GoToCaloriesPage();
            await _caloriesPage.SetWorkoutsData("[{\"name\":\"Walking\",\"met\":3.5,\"duration\":60}]");
            await _caloriesPage.SetWeight("60");
            await _caloriesPage.RefreshPage();

            // Act & Assert
            var burned = await _caloriesPage.GetBurnedCalories();
            Assert.Equal("210 kcal", burned);
        }

        [Fact]
        public async Task CaloriesPage_WithWeight_ShouldCalculateMaintenance()
        {
            // Arrange
            await _caloriesPage!.GoToCaloriesPage();
            await _caloriesPage.SetWeight("70");
            await _caloriesPage.RefreshPage();

            // Act & Assert
            var maintenance = await _caloriesPage.GetMaintenanceCalories();
            Assert.Equal("1540 kcal", maintenance);
        }
    }
}
