using mantis_tests.appmanager;
using NUnit.Framework;

namespace mantis_tests.tests
{
    [TestFixture]
    public class TestBase
    {
        public static bool PERFORM_LONG_UI_CHECKS = true;
        protected ApplicationManager applicationManager;

        [SetUp]
        public void SetUpTest()
        {
            applicationManager = ApplicationManager.GetInstance();
        }

        [TearDown]
        public void TearDownTest()
        {
            applicationManager.Login.LogOut();
        }
    }
}