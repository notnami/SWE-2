using NUnit.Framework;
using SeleniumTests.Pages;

namespace SeleniumTests.PageTests
{
    [TestFixture]
    public class WorkoutsTests : BaseTest
    {
        private WorkoutsPage _workoutsPage;

        [SetUp]
        public void TestSetup()
        {
            _workoutsPage = new WorkoutsPage(driver);
        }

        [Test]
        public void AddWorkout_ShouldAddToList()
        {
            // Arrange
            _workoutsPage.GoToWorkoutsPage();

            // Act
            _workoutsPage.AddWorkout("Walking", "30");

            // Assert
            var workoutItems = _workoutsPage.GetWorkoutItems();
            Assert.AreEqual(1, workoutItems.Count);
            Assert.IsTrue(workoutItems[0].Text.Contains("Walking"));
        }

        [Test]
        public void DeleteWorkout_ShouldRemoveFromList()
        {
            // Arrange
            _workoutsPage.GoToWorkoutsPage();
            _workoutsPage.AddWorkout("Running", "20");

            // Act
            _workoutsPage.DeleteWorkout(0);

            // Assert
            var workoutItems = _workoutsPage.GetWorkoutItems();
            Assert.AreEqual(0, workoutItems.Count);
        }

        [Test]
        public void TotalCalories_ShouldUpdateAfterAddingWorkout()
        {
            // Arrange
            _workoutsPage.GoToWorkoutsPage();

            // Act
            _workoutsPage.AddWorkout("Walking", "60");

            // Assert
            var totalCalories = _workoutsPage.GetTotalCalories();
            // Assuming weight 60kg, MET 3.5, 1 hour: 3.5 * 60 * 1 = 210
            Assert.AreEqual("210 kcal", totalCalories);
        }

        [Test]
        public void AddMultipleWorkouts_ShouldAccumulateCalories()
        {
            // Arrange
            _workoutsPage.GoToWorkoutsPage();

            // Act
            _workoutsPage.AddWorkout("Walking", "30");
            _workoutsPage.AddWorkout("Running", "30");

            // Assert
            var workoutItems = _workoutsPage.GetWorkoutItems();
            Assert.AreEqual(2, workoutItems.Count);
            var totalCalories = _workoutsPage.GetTotalCalories();
            // Walking: 3.5 * 60 * 0.5 = 105, Running: 8.3 * 60 * 0.5 = 249, Total: 354
            Assert.AreEqual("354 kcal", totalCalories);
        }

        [Test]
        public void DeleteSpecificWorkout_ShouldRemoveCorrectOne()
        {
            // Arrange
            _workoutsPage.GoToWorkoutsPage();
            _workoutsPage.AddWorkout("Walking", "30");
            _workoutsPage.AddWorkout("Running", "30");

            // Act
            _workoutsPage.DeleteWorkout(0); // Delete first (Walking)

            // Assert
            var workoutItems = _workoutsPage.GetWorkoutItems();
            Assert.AreEqual(1, workoutItems.Count);
            Assert.IsTrue(workoutItems[0].Text.Contains("Running"));
        }
    }
}
