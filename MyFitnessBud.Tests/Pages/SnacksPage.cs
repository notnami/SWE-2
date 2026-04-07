namespace MyFitnessBud.Tests.Pages
{
    public class SnacksPage
    {
        private readonly IPage _page;

        public SnacksPage(IPage page)
        {
            _page = page;
        }

        // Locators
        private ILocator SearchInput => _page.Locator("#searchInput");
        private ILocator SearchButton => _page.Locator(".search-btn");
        private ILocator SnackList => _page.Locator("#snackList");
        private ILocator FoodNameInput => _page.Locator("#food-name");
        private ILocator AddButton => _page.Locator(".add-btn");
        private ILocator IntakeList => _page.Locator("#intakeList");
        private ILocator TotalCalories => _page.Locator("#totalCalories");

        // Actions
        public async Task EnterSearchText(string text)
        {
            await SearchInput.FillAsync(text);
        }

        public async Task ClickSearch()
        {
            await SearchButton.ClickAsync();
        }

        public async Task SearchSnacks(string text)
        {
            await EnterSearchText(text);
            await ClickSearch();
        }

        public async Task EnterFoodName(string food)
        {
            await FoodNameInput.FillAsync(food);
        }

        public async Task ClickAdd()
        {
            await AddButton.ClickAsync();
        }

        public async Task AddIntake(string food)
        {
            await EnterFoodName(food);
            await ClickAdd();
        }

        public async Task<List<ILocator>> GetSnackItems()
        {
            var locator = SnackList.Locator("li");
            int count = await locator.CountAsync();
            var items = new List<ILocator>();
            for (int i = 0; i < count; i++)
            {
                items.Add(locator.Nth(i));
            }
            return items;
        }

        public async Task<List<ILocator>> GetIntakeItems()
        {
            var locator = IntakeList.Locator("li");
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

        public async Task ToggleFavorite(string snackName)
        {
            var snackItems = await GetSnackItems();
            foreach (var item in snackItems)
            {
                var nameSpan = item.Locator("span");
                var text = await nameSpan.TextContentAsync();
                if (text == snackName)
                {
                    var star = item.Locator(".star").First;
                    await star.ClickAsync();
                    break;
                }
            }
        }

        public async Task DeleteIntakeItem(int index)
        {
            var intakeItems = await GetIntakeItems();
            if (index < intakeItems.Count)
            {
                var deleteBtn = intakeItems[index].Locator(".delete-btn");
                await deleteBtn.ClickAsync();
            }
        }

        // Navigation
        public async Task GoToSnacksPage()
        {
            await _page.GotoAsync("http://localhost:5161/snacks.html");
        }
    }
}
