using NUnit.Framework;
using SeleniumTests.Pages;

namespace SeleniumTests.PageTests
{
    [TestFixture]
    public class CaloriesTests : BaseTest
    {
        private CaloriesPage _caloriesPage;

        [SetUp]
        public void TestSetup()
        {
            _caloriesPage = new CaloriesPage(driver);
        }

        [Test]
        public void CaloriesPage_ShouldDisplayValues()
        {
            // Arrange
            _caloriesPage.GoToCaloriesPage();

            // Act & Assert
            Assert.IsNotNull(_caloriesPage.GetConsumedCalories());
            Assert.IsNotNull(_caloriesPage.GetBurnedCalories());
            Assert.IsNotNull(_caloriesPage.GetMaintenanceCalories());
            Assert.IsNotNull(_caloriesPage.GetCalorieDeficit());
        }

        [Test]
        public void CaloriesPage_WithIntakeData_ShouldCalculateConsumed()
        {
            // Arrange
            _caloriesPage.GoToCaloriesPage();
            _caloriesPage.SetIntakeData("[{\"name\":\"Apple\",\"calories\":95},{\"name\":\"Banana\",\"calories\":105}]");
            _caloriesPage.RefreshPage();

            // Act & Assert
            Assert.AreEqual("200 kcal", _caloriesPage.GetConsumedCalories());
        }

        [Test]
        public void CaloriesPage_WithWorkoutsData_ShouldCalculateBurned()
        {
            // Arrange
            _caloriesPage.GoToCaloriesPage();
            _caloriesPage.SetWorkoutsData("[{\"name\":\"Walking\",\"met\":3.5,\"duration\":60}]");
            _caloriesPage.SetWeight("60");
            _caloriesPage.RefreshPage();

            // Act & Assert
            Assert.AreEqual("210 kcal", _caloriesPage.GetBurnedCalories());
        }

        [Test]
        public void CaloriesPage_WithWeight_ShouldCalculateMaintenance()
        {
            // Arrange
            _caloriesPage.GoToCaloriesPage();
            _caloriesPage.SetWeight("70");
            _caloriesPage.RefreshPage();

            // Act & Assert
            Assert.AreEqual("1540 kcal", _caloriesPage.GetMaintenanceCalories());
        }
    }
}
