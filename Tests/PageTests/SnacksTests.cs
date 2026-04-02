using NUnit.Framework;
using Tests.Pages;
using OpenQA.Selenium;
using System.Linq;

namespace Tests.PageTests
{
    [TestFixture]
    public class SnacksTests : BaseTest
    {
        private SnacksPage _snacksPage;

        [SetUp]
        public void TestSetup()
        {
            _snacksPage = new SnacksPage(driver);
        }

        [Test]
        public void SearchSnacks_ShouldFilterList()
        {
            // Arrange
            _snacksPage.GoToSnacksPage();

            // Act
            _snacksPage.SearchSnacks("Apple");

            // Assert
            var snackItems = _snacksPage.GetSnackItems();
            Assert.AreEqual(1, snackItems.Count);
            Assert.AreEqual("Apple", snackItems[0].FindElement(By.TagName("span")).Text);
        }

        [Test]
        public void AddIntake_ShouldAddToList()
        {
            // Arrange
            _snacksPage.GoToSnacksPage();

            // Act
            _snacksPage.AddIntake("Apple");

            // Assert
            var intakeItems = _snacksPage.GetIntakeItems();
            Assert.AreEqual(1, intakeItems.Count);
            Assert.IsTrue(intakeItems[0].Text.Contains("apple"));
        }

        [Test]
        public void ToggleFavorite_ShouldMarkAsFavorite()
        {
            // Arrange
            _snacksPage.GoToSnacksPage();

            // Act
            _snacksPage.ToggleFavorite("Apple");

            // Assert
            var snackItems = _snacksPage.GetSnackItems();
            var appleItem = snackItems.FirstOrDefault(i => i.FindElement(By.TagName("span")).Text == "Apple");
            Assert.IsNotNull(appleItem);
            var star = appleItem.FindElement(By.ClassName("star"));
            Assert.AreEqual("★", star.Text);
        }

        [Test]
        public void DeleteIntakeItem_ShouldRemoveFromList()
        {
            // Arrange
            _snacksPage.GoToSnacksPage();
            _snacksPage.AddIntake("Apple");

            // Act
            _snacksPage.DeleteIntakeItem(0);

            // Assert
            var intakeItems = _snacksPage.GetIntakeItems();
            Assert.AreEqual(0, intakeItems.Count);
        }

        [Test]
        public void TotalCalories_ShouldUpdateAfterAddingIntake()
        {
            // Arrange
            _snacksPage.GoToSnacksPage();

            // Act
            _snacksPage.AddIntake("Apple");

            // Assert
            var totalCalories = _snacksPage.GetTotalCalories();
            Assert.AreEqual("95 kcal", totalCalories);
        }

        [Test]
        public void AddMultipleIntake_ShouldAccumulateCalories()
        {
            // Arrange
            _snacksPage.GoToSnacksPage();

            // Act
            _snacksPage.AddIntake("Apple");
            _snacksPage.AddIntake("Banana");

            // Assert
            var intakeItems = _snacksPage.GetIntakeItems();
            Assert.AreEqual(2, intakeItems.Count);
            var totalCalories = _snacksPage.GetTotalCalories();
            Assert.AreEqual("190 kcal", totalCalories);
        }

        [Test]
        public void SearchWithNoResults_ShouldShowEmptyList()
        {
            // Arrange
            _snacksPage.GoToSnacksPage();

            // Act
            _snacksPage.SearchSnacks("NonExistent");

            // Assert
            var snackItems = _snacksPage.GetSnackItems();
            Assert.AreEqual(0, snackItems.Count);
        }

        [Test]
        public void SearchCaseInsensitive_ShouldFindSnacks()
        {
            // Arrange
            _snacksPage.GoToSnacksPage();

            // Act
            _snacksPage.SearchSnacks("apple");

            // Assert
            var snackItems = _snacksPage.GetSnackItems();
            Assert.AreEqual(1, snackItems.Count);
            Assert.AreEqual("Apple", snackItems[0].FindElement(By.TagName("span")).Text);
        }
    }
}
