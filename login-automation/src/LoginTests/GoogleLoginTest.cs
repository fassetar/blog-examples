using LoginAutomationSolution;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LoginTests
{
    class GoogleLoginTest : LoginTest
    {
        [TestMethod]
        public void UserCanLogin()
        {
            //TODO: Thinking about adding this.
            LoginPage.GoTo("https://accounts.google.com/");
            LoginPage.LoginAs("test@test.com").WithPassword("test").Login();
            Assert.IsTrue(Dashboard.IsAt, "Failed to login.");
        }
    }
}