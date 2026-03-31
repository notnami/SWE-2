using NUnit.Framework;
using SeleniumTests.Pages;

namespace SeleniumTests.PageTests
{
    [TestFixture]
    public class HomeTests : BaseTest
    {
        private HomePage _homePage;

        [SetUp]
        public void TestSetup()
        {
            _homePage = new HomePage(driver);
        }

        [Test]
        public void HomePage_WhenLoggedOut_ShouldShowSignUpAndLoginButtons()
        {
            // Arrange
            _homePage.GoToHomePage();

            // Act & Assert
            Assert.IsTrue(_homePage.IsLoggedOut());
            Assert.AreEqual("Welcome to MyFitnessBud!", _homePage.GetHeaderText());
        }

        [Test]
        public void SignUpButton_ShouldNavigateToSignUpPage()
        {
            // Arrange
            _homePage.GoToHomePage();

            // Act
            _homePage.ClickSignUp();

            // Assert
            Assert.AreEqual("http://localhost:5161/signup.html", driver.Url);
        }

        [Test]
        public void LoginButton_ShouldNavigateToLoginPage()
        {
            // Arrange
            _homePage.GoToHomePage();

            // Act
            _homePage.ClickLogin();

            // Assert
            Assert.AreEqual("http://localhost:5161/login.html", driver.Url);
        }

        [Test]
        public void LogoutLink_WhenLoggedIn_ShouldLogout()
        {
            // Arrange - assume logged in
            _homePage.GoToHomePage();

            // Act
            _homePage.ClickLogout();

            // Accept alert
            var alert = driver.SwitchTo().Alert();
            alert.Accept();

            // Assert
            Assert.AreEqual("http://localhost:5161/login.html", driver.Url);
        }

        [Test]
        public void NavigationLinks_ShouldNavigateToCorrectPages()
        {
            // Arrange
            _homePage.GoToHomePage();

            // Act & Assert - Workouts
            _homePage.ClickNavigationLink("Workouts");
            Assert.AreEqual("http://localhost:5161/workouts.html", driver.Url);

            // Back to home
            _homePage.GoToHomePage();

            // Calories
            _homePage.ClickNavigationLink("Calories");
            Assert.AreEqual("http://localhost:5161/calories.html", driver.Url);

            // Back to home
            _homePage.GoToHomePage();

            // Snacks
            _homePage.ClickNavigationLink("Snacks");
            Assert.AreEqual("http://localhost:5161/snacks.html", driver.Url);

            // Back to home
            _homePage.GoToHomePage();

            // Favorites
            _homePage.ClickNavigationLink("Favorites");
            Assert.AreEqual("http://localhost:5161/favorites.html", driver.Url);
        }
    }
}