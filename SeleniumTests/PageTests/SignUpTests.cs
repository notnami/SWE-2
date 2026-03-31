using NUnit.Framework;
using SeleniumTests.Pages;

namespace SeleniumTests.PageTests
{
    [TestFixture]
    public class SignUpTests : BaseTest
    {
        private SignUpPage _signUpPage;
        private HomePage _homePage;

        [SetUp]
        public void TestSetup()
        {
            _signUpPage = new SignUpPage(driver);
            _homePage = new HomePage(driver);
        }

        [Test]
        public void SignUp_WithValidData_ShouldRedirectToHome()
        {
            // Arrange
            _signUpPage.GoToSignUpPage();

            // Act
            _signUpPage.SignUp("newuser", "newpass", "150", "65");

            // Accept alert
            var alert = driver.SwitchTo().Alert();
            alert.Accept();

            // Assert
            Assert.AreEqual("http://localhost:5161/index.html", driver.Url);
        }

        [Test]
        public void SignUp_WithExistingUsername_ShouldShowError()
        {
            // Arrange
            _signUpPage.GoToSignUpPage();
            _signUpPage.SetUsersData("[{\"username\":\"existinguser\",\"password\":\"pass\",\"weight\":\"150\",\"height\":\"65\"}]");

            // Act
            _signUpPage.SignUp("existinguser", "pass", "150", "65");

            // Assert
            var alert = driver.SwitchTo().Alert();
            Assert.AreEqual("Username already exists. Please choose a different one.", alert.Text);
            alert.Accept();
        }

        [Test]
        public void SignUp_WithEmptyFields_ShouldShowError()
        {
            // Arrange
            _signUpPage.GoToSignUpPage();

            // Act
            _signUpPage.ClickRegister();

            // Assert
            var alert = driver.SwitchTo().Alert();
            Assert.AreEqual("Please complete all signup fields.", alert.Text);
            alert.Accept();
        }

        [Test]
        public void LoginLink_ShouldNavigateToLoginPage()
        {
            // Arrange
            _signUpPage.GoToSignUpPage();

            // Act
            _signUpPage.ClickLoginLink();

            // Assert
            Assert.AreEqual("http://localhost:5161/login.html", driver.Url);
        }
    }
}