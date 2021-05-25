using System.IO;
using mantis_tests.model;
using NUnit.Framework;

namespace mantis_tests.tests
{
    [TestFixture]
    public class AccountCreationTests : TestBase
    {
        [OneTimeSetUp]
        public void SetUpConfig()
        {
            applicationManager.FTP.BackUpFile("/config_inc.php");
            using Stream localFile = File.Open("/config_inc.php", FileMode.Open);
            applicationManager.FTP.UploadFile("/config_inc.php", localFile);
        }

        [OneTimeTearDown]
        public void TearDown()
        {
            applicationManager.FTP.RestoreBackUpFile("/config_inc.php");
        }

        [Test]
        public void TestAccountRegistration()
        {
            var account = new AccountData
                {UserName = "testuser", Email = "testuser@localhost.localdomain"};
            applicationManager.Registration.Register(account);
        }
    }
}