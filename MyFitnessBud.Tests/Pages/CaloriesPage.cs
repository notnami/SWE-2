namespace MyFitnessBud.Tests.Pages
{
    public class CaloriesPage
    {
        private readonly IPage _page;

        public CaloriesPage(IPage page)
        {
            _page = page;
        }

        // Locators
        private ILocator Consumed => _page.Locator("#consumed");
        private ILocator Burned => _page.Locator("#burned");
        private ILocator Maintenance => _page.Locator("#maintenance");
        private ILocator Deficit => _page.Locator("#deficit");

        // Getters
        public async Task<string> GetConsumedCalories()
        {
            return await Consumed.TextContentAsync() ?? "";
        }

        public async Task<string> GetBurnedCalories()
        {
            return await Burned.TextContentAsync() ?? "";
        }

        public async Task<string> GetMaintenanceCalories()
        {
            return await Maintenance.TextContentAsync() ?? "";
        }

        public async Task<string> GetCalorieDeficit()
        {
            return await Deficit.TextContentAsync() ?? "";
        }

        public async Task SetIntakeData(string intakeJson)
        {
            await _page.EvaluateAsync($"localStorage.setItem('intake', '{intakeJson}');");
        }

        public async Task SetWorkoutsData(string workoutsJson)
        {
            await _page.EvaluateAsync($"localStorage.setItem('workouts', '{workoutsJson}');");
        }

        public async Task SetWeight(string weight)
        {
            await _page.EvaluateAsync($"localStorage.setItem('weight', '{weight}');");
        }

        public async Task RefreshPage()
        {
            await _page.ReloadAsync();
        }

        // Navigation
        public async Task GoToCaloriesPage()
        {
            await _page.GotoAsync("http://localhost:5161/calories.html");
        }
    }
}
