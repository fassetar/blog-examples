using LoginAutomationSolution;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LoginTests
{
    [TestClass]
    public class LoginTest
    {
        [TestInitialize]
        public void Init()
        {
            Driver.Initialize();
        }
        
        [TestCleanup]
        public void Cleanup()
        {
            Driver.Close();
        }
    }
}
