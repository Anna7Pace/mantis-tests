using mantis_tests.appmanager;
using mantis_tests.model;
using NUnit.Framework;

namespace mantis_tests.tests
{
    [TestFixture]
    public class AuthTestBase : TestBase
    {
       
        [SetUp]
        public void SetUp()
        {
           ApplicationManager.GetInstance().Login.Login(new AccountData("administrator", "root"));
        }
    }
}