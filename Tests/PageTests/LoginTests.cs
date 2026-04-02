using NUnit.Framework;
using SeleniumTests.Pages;

namespace SeleniumTests.PageTests
{
    [TestFixture]
    public class LoginTests : BaseTest
    {
        private LoginPage _loginPage;
        private HomePage _homePage;

        [SetUp]
        public void TestSetup()
        {
            _loginPage = new LoginPage(driver);
            _homePage = new HomePage(driver);
        }

        [Test]
        public void Login_WithValidCredentials_ShouldRedirectToHome()
        {
            // Arrange
            _loginPage.GoToLoginPage();
            _loginPage.SetUsersData("[{\"username\":\"testuser\",\"password\":\"testpass\",\"weight\":\"60\",\"height\":\"170\"}]");

            // Act
            _loginPage.Login("testuser", "testpass");

            // Accept alert
            var alert = driver.SwitchTo().Alert();
            alert.Accept();

            // Assert
            Assert.AreEqual("http://localhost:5161/index.html", driver.Url);
        }

        [Test]
        public void Login_WithInvalidCredentials_ShouldShowError()
        {
            // Arrange
            _loginPage.GoToLoginPage();
            _loginPage.SetUsersData("[{\"username\":\"testuser\",\"password\":\"testpass\",\"weight\":\"60\",\"height\":\"170\"}]");

            // Act
            _loginPage.Login("invalid", "invalid");

            // Assert
            var alert = driver.SwitchTo().Alert();
            Assert.AreEqual("Invalid username or password.", alert.Text);
            alert.Accept();
        }

        [Test]
        public void Login_WithEmptyFields_ShouldShowError()
        {
            // Arrange
            _loginPage.GoToLoginPage();

            // Act
            _loginPage.ClickLogin();

            // Assert
            var alert = driver.SwitchTo().Alert();
            Assert.AreEqual("Please enter both username and password.", alert.Text);
            alert.Accept();
        }

        [Test]
        public void SignUpButton_ShouldNavigateToSignUpPage()
        {
            // Arrange
            _loginPage.GoToLoginPage();

            // Act
            _loginPage.ClickSignUp();

            // Assert
            Assert.AreEqual("http://localhost:5161/signup.html", driver.Url);
        }
    }
}
