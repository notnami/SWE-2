namespace MyFitnessBud.Tests.Pages
{
    public class FavoritesPage
    {
        private readonly IPage _page;

        public FavoritesPage(IPage page)
        {
            _page = page;
        }

        // Locators
        private ILocator FavoritesList => _page.Locator("#favoritesList");
        private ILocator LogoutLink => _page.Locator(".logoutLink");

        // Actions
        public async Task<List<ILocator>> GetFavoriteItems()
        {
            var locator = FavoritesList.Locator("li");
            int count = await locator.CountAsync();
            var items = new List<ILocator>();
            for (int i = 0; i < count; i++)
            {
                items.Add(locator.Nth(i));
            }
            return items;
        }

        public async Task RemoveFavorite(int index)
        {
            var favoriteItems = await GetFavoriteItems();
            if (index < favoriteItems.Count)
            {
                var removeBtn = favoriteItems[index].Locator(".remove-btn");
                await removeBtn.ClickAsync();
            }
        }

        public async Task ClickLogout()
        {
            await LogoutLink.ClickAsync();
        }

        // Navigation
        public async Task GoToFavoritesPage()
        {
            await _page.GotoAsync("http://localhost:5161/favorites.html");
        }
    }
}
