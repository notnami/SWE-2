namespace MyFitnessBud.Tests.Pages
{
    public class WorkoutsPage
    {
        private readonly IPage _page;

        public WorkoutsPage(IPage page)
        {
            _page = page;
        }

        // Locators
        private ILocator WorkoutSelect => _page.Locator("#workout-name");
        private ILocator DurationInput => _page.Locator("#duration");
        private ILocator AddWorkoutButton => _page.Locator(".addWorkoutBtn");
        private ILocator WorkoutList => _page.Locator("#workoutList");
        private ILocator TotalCalories => _page.Locator("#totalCalories");

        // Actions
        public async Task SelectWorkout(string workoutName)
        {
            await WorkoutSelect.SelectOptionAsync(workoutName);
        }

        public async Task EnterDuration(string duration)
        {
            await DurationInput.FillAsync(duration);
        }

        public async Task ClickAddWorkout()
        {
            await AddWorkoutButton.ClickAsync();
        }

        public async Task AddWorkout(string workoutName, string duration)
        {
            await SelectWorkout(workoutName);
            await EnterDuration(duration);
            await ClickAddWorkout();
        }

        public async Task<List<ILocator>> GetWorkoutItems()
        {
            var locator = WorkoutList.Locator("li");
            int count = await locator.CountAsync();
            var items = new List<ILocator>();
            for (int i = 0; i < count; i++)
            {
                items.Add(locator.Nth(i));
            }
            return items;
        }

        public async Task<string> GetTotalCalories()
        {
            return await TotalCalories.TextContentAsync() ?? "";
        }

        public async Task DeleteWorkout(int index)
        {
            var workoutItems = await GetWorkoutItems();
            if (index < workoutItems.Count)
            {
                var deleteBtn = workoutItems[index].Locator(".delete-btn");
                await deleteBtn.ClickAsync();
            }
        }

        // Navigation
        public async Task GoToWorkoutsPage()
        {
            await _page.GotoAsync("http://localhost:5161/workouts.html");
        }
    }
}
