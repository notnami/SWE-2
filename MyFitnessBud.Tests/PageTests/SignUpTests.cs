using MyFitnessBud.Tests.Pages;

namespace MyFitnessBud.Tests.PageTests
{
    public class SignUpTests : BaseTest
    {
        private SignUpPage? _signUpPage;
        private HomePage? _homePage;

        public SignUpTests() : base() { }

        public override async Task InitializeAsync()
        {
            await base.InitializeAsync();
            _signUpPage = new SignUpPage(page);
            _homePage = new HomePage(page);
        }

        [Fact]
        public async Task SignUp_WithValidData_ShouldRedirectToHome()
        {
            // Arrange
            await _signUpPage!.GoToSignUpPage();

            // Act
            await WaitForDialog(async () => await _signUpPage.SignUp("newuser", "newpass", "150", "65"));

            // Assert
            Assert.Equal("http://localhost:5161/index.html", page.Url);
        }

        [Fact]
        public async Task SignUp_WithExistingUsername_ShouldShowError()
        {
            // Arrange
            await _signUpPage!.GoToSignUpPage();
            await _signUpPage.SetUsersData("[{\"username\":\"existinguser\",\"password\":\"pass\",\"weight\":\"150\",\"height\":\"65\"}]");

            // Act
            var alertText = await WaitForDialog(async () => await _signUpPage.SignUp("existinguser", "pass", "150", "65"));

            // Assert
            Assert.Equal("Username already exists. Please choose a different one.", alertText);
        }

        [Fact]
        public async Task SignUp_WithEmptyFields_ShouldShowError()
        {
            // Arrange
            await _signUpPage!.GoToSignUpPage();

            // Act & Assert
            var alertText = await WaitForDialog(async () => await _signUpPage.ClickRegister());
            Assert.Equal("Please complete all signup fields.", alertText);
        }

        [Fact]
        public async Task LoginLink_ShouldNavigateToLoginPage()
        {
            // Arrange
            await _signUpPage!.GoToSignUpPage();

            // Act
            await _signUpPage.ClickLoginLink();

            // Assert
            Assert.Equal("http://localhost:5161/login.html", page.Url);
        }
    }
}
