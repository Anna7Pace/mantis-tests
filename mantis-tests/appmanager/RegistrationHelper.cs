using mantis_tests.model;
using OpenQA.Selenium;

namespace mantis_tests.appmanager
{
    public class RegistrationHelper : HelperBase
    {
        public RegistrationHelper(ApplicationManager manager) : base(manager)
        {
            throw new System.NotImplementedException();
        }

        public void Register(AccountData account)
        {
            OpenRegistrationForm();
            FillRegistrationForm(account);
            SubmitRegistration();
        }

        private void OpenRegistrationForm()
        {
            Driver.Url = "http://localhost:8080/mantisbt-2.25.1/signup_page.php";
        }

        private void FillRegistrationForm(AccountData account)
        {
            Driver.FindElement(By.Name("username")).SendKeys(account.UserName);
            Driver.FindElement(By.Name("email")).SendKeys(account.Email);
        }

        private void SubmitRegistration()
        {
           Driver.FindElement(By.XPath("//*[@id='signup-form']/fieldset/input[2]")).Click();
        }
    }
}
