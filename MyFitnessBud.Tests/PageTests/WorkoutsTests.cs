using MyFitnessBud.Tests.Pages;

namespace MyFitnessBud.Tests.PageTests
{
    public class WorkoutsTests : BaseTest
    {
        private WorkoutsPage? _workoutsPage;

        public WorkoutsTests() : base() { }

        public override async Task InitializeAsync()
        {
            await base.InitializeAsync();
            _workoutsPage = new WorkoutsPage(page);
        }

        [Fact]
        public async Task AddWorkout_ShouldAddToList()
        {
            // Arrange
            await _workoutsPage!.GoToWorkoutsPage();

            // Act
            await _workoutsPage.AddWorkout("Walking", "30");

            // Assert
            var workoutItems = await _workoutsPage.GetWorkoutItems();
            Assert.Single(workoutItems);
            var itemText = await workoutItems[0].TextContentAsync();
            Assert.Contains("Walking", itemText, System.StringComparison.Ordinal);
        }

        [Fact]
        public async Task DeleteWorkout_ShouldRemoveFromList()
        {
            // Arrange
            await _workoutsPage!.GoToWorkoutsPage();
            await _workoutsPage.AddWorkout("Running", "20");

            // Act
            await _workoutsPage.DeleteWorkout(0);

            // Assert
            var workoutItems = await _workoutsPage.GetWorkoutItems();
            Assert.Empty(workoutItems);
        }

        [Fact]
        public async Task TotalCalories_ShouldUpdateAfterAddingWorkout()
        {
            // Arrange
            await _workoutsPage!.GoToWorkoutsPage();

            // Act
            await _workoutsPage.AddWorkout("Walking", "60");

            // Assert
            var totalCalories = await _workoutsPage.GetTotalCalories();
            // Assuming weight 60kg, MET 3.5, 1 hour: 3.5 * 60 * 1 = 210
            Assert.Equal("210 kcal", totalCalories);
        }

        [Fact]
        public async Task AddMultipleWorkouts_ShouldAccumulateCalories()
        {
            // Arrange
            await _workoutsPage!.GoToWorkoutsPage();

            // Act
            await _workoutsPage.AddWorkout("Walking", "30");
            await _workoutsPage.AddWorkout("Running", "30");

            // Assert
            var workoutItems = await _workoutsPage.GetWorkoutItems();
            Assert.Equal(2, workoutItems.Count);
            var totalCalories = await _workoutsPage.GetTotalCalories();
            // Walking: 3.5 * 60 * 0.5 = 105, Running: 8.3 * 60 * 0.5 = 249, Total: 354
            Assert.Equal("354 kcal", totalCalories);
        }

        [Fact]
        public async Task DeleteSpecificWorkout_ShouldRemoveCorrectOne()
        {
            // Arrange
            await _workoutsPage!.GoToWorkoutsPage();
            await _workoutsPage.AddWorkout("Walking", "30");
            await _workoutsPage.AddWorkout("Running", "30");

            // Act
            await _workoutsPage.DeleteWorkout(0); // Delete first (Walking)

            // Assert
            var workoutItems = await _workoutsPage.GetWorkoutItems();
            Assert.Single(workoutItems);
            var itemText = await workoutItems[0].TextContentAsync();
            Assert.Contains("Running", itemText, System.StringComparison.Ordinal);
        }
    }
}
