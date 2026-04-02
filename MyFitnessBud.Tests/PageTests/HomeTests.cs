using Xunit;
using MyFitnessBud.Tests.Pages;

namespace MyFitnessBud.Tests.PageTests
{
    public class HomeTests : BaseTest
    {
        private HomePage? _homePage;

        public HomeTests() : base() { }

        public override async Task InitializeAsync()
        {
            await base.InitializeAsync();
            _homePage = new HomePage(page);
        }

        [Fact]
        public async Task HomePage_WhenLoggedOut_ShouldShowSignUpAndLoginButtons()
        {
            // Arrange
            await _homePage!.GoToHomePage();

            // Act & Assert
            Assert.True(await _homePage.IsLoggedOut());
            Assert.Equal("Welcome to MyFitnessBud!", await _homePage.GetHeaderText());
        }

        [Fact]
        public async Task SignUpButton_ShouldNavigateToSignUpPage()
        {
            // Arrange
            await _homePage!.GoToHomePage();

            // Act
            await _homePage.ClickSignUp();

            // Assert
            Assert.Equal("http://localhost:5161/signup.html", page.Url);
        }

        [Fact]
        public async Task LoginButton_ShouldNavigateToLoginPage()
        {
            // Arrange
            await _homePage!.GoToHomePage();

            // Act
            await _homePage.ClickLogin();

            // Assert
            Assert.Equal("http://localhost:5161/login.html", page.Url);
        }

        [Fact]
        public async Task LogoutLink_WhenLoggedIn_ShouldLogout()
        {
            // Arrange - assume logged in
            await _homePage!.GoToHomePage();

            // Act & Assert
            await WaitForDialog(async () => await _homePage.ClickLogout());
            Assert.Equal("http://localhost:5161/login.html", page.Url);
        }

        [Fact]
        public async Task NavigationLinks_ShouldNavigateToCorrectPages()
        {
            // Arrange
            await _homePage!.GoToHomePage();

            // Act & Assert - Workouts
            await _homePage.ClickNavigationLink("Workouts");
            Assert.Equal("http://localhost:5161/workouts.html", page.Url);

            // Back to home
            await _homePage.GoToHomePage();

            // Calories
            await _homePage.ClickNavigationLink("Calories");
            Assert.Equal("http://localhost:5161/calories.html", page.Url);

            // Back to home
            await _homePage.GoToHomePage();

            // Snacks
            await _homePage.ClickNavigationLink("Snacks");
            Assert.Equal("http://localhost:5161/snacks.html", page.Url);

            // Back to home
            await _homePage.GoToHomePage();

            // Favorites
            await _homePage.ClickNavigationLink("Favorites");
            Assert.Equal("http://localhost:5161/favorites.html", page.Url);
        }
    }
}
