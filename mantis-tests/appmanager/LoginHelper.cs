using mantis_tests.model;
using OpenQA.Selenium;

namespace mantis_tests.appmanager
{
    public class LoginHelper : HelperBase
    {
        public LoginHelper(ApplicationManager manager) : base(manager)
        {
        }

        public void Login(AccountData account)
        {
            OpenLoginPage();
            FillUserName(account);
            SubmitLoginUserName();
            FillPassword(account);
            SubmitLoginPassword();
        }

        private void SubmitLoginUserName()
        {
            Driver.FindElement(By.XPath("//*[@id='login-form']/fieldset/input[2]")).Click();
        }

        private void SubmitLoginPassword()
        {
            Driver.FindElement(By.XPath("//*[@id='login-form']/fieldset/input[3]")).Click();
        }

        private void FillUserName(AccountData accountData)
        {
            Driver.FindElement(By.Id("username")).SendKeys(accountData.UserName);
        }

        private void FillPassword(AccountData accountData)
        {
            Driver.FindElement(By.Id("password")).SendKeys(accountData.Password);
        }

        private void OpenLoginPage()
        {
            Driver.Url = "http://localhost:8080/mantisbt-2.25.1/login_page.php";
        }

        public void LogOut()
        {
            Driver.Url = "http://localhost:8080/mantisbt-2.25.1/logout_page.php";
        }
    }
}