using OpenQA.Selenium;

namespace mantis_tests.appmanager
{
    public class HelperBase
    {
        protected static IWebDriver Driver;

        protected ApplicationManager manager;

        public HelperBase(ApplicationManager manager)
        {
            this.manager = manager;
            Driver = manager.Driver;
        }
    }
}