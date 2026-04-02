using Xunit;
using MyFitnessBud.Tests.Pages;

namespace MyFitnessBud.Tests.PageTests
{
    public class LoginTests : BaseTest
    {
        private LoginPage? _loginPage;
        private HomePage? _homePage;

        public LoginTests() : base() { }

        public override async Task InitializeAsync()
        {
            await base.InitializeAsync();
            _loginPage = new LoginPage(page);
            _homePage = new HomePage(page);
        }

        [Fact]
        public async Task Login_WithValidCredentials_ShouldRedirectToHome()
        {
            // Arrange
            await _loginPage!.GoToLoginPage();
            await _loginPage.SetUsersData("[{\"username\":\"testuser\",\"password\":\"testpass\",\"weight\":\"60\",\"height\":\"170\"}]");

            // Act
            await WaitForDialog(async () => await _loginPage.Login("testuser", "testpass"));

            // Assert
            Assert.Equal("http://localhost:5161/index.html", page.Url);
        }

        [Fact]
        public async Task Login_WithInvalidCredentials_ShouldShowError()
        {
            // Arrange
            await _loginPage!.GoToLoginPage();
            await _loginPage.SetUsersData("[{\"username\":\"testuser\",\"password\":\"testpass\",\"weight\":\"60\",\"height\":\"170\"}]");

            // Act
            var alertText = await WaitForDialog(async () => await _loginPage.Login("invalid", "invalid"));

            // Assert
            Assert.Equal("Invalid username or password.", alertText);
        }

        [Fact]
        public async Task Login_WithEmptyFields_ShouldShowError()
        {
            // Arrange
            await _loginPage!.GoToLoginPage();

            // Act & Assert
            var alertText = await WaitForDialog(async () => await _loginPage.ClickLogin());
            Assert.Equal("Please enter both username and password.", alertText);
        }

        [Fact]
        public async Task SignUpButton_ShouldNavigateToSignUpPage()
        {
            // Arrange
            await _loginPage!.GoToLoginPage();

            // Act
            await _loginPage.ClickSignUp();

            // Assert
            Assert.Equal("http://localhost:5161/signup.html", page.Url);
        }
    }
}
