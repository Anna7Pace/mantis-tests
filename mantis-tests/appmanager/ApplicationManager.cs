using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace mantis_tests.appmanager
{
    public class ApplicationManager
    {
        public IWebDriver Driver { get; set; }
        private static string _baseUrl;
        private static readonly ThreadLocal<ApplicationManager> _applicationManager = new();
        public LoginHelper Login { get; set; }
        public APIHelper API { get; set; }
        public ProjectManagementHelper Project { get; set; }
        public RegistrationHelper Registration { get; set; }
        public FTPHelper FTP { get; set; }
        public ManagementMenuHelper ManagementMenu { get; set; }

        public ApplicationManager()
        {
            Driver = new ChromeDriver();
            _baseUrl = "http://localhost:8080/mantisbt-2.25.1/";
            Login = new LoginHelper(this);
            Registration = new RegistrationHelper(this);
            Project = new ProjectManagementHelper(this);
            ManagementMenu = new ManagementMenuHelper(this);
            FTP = new FTPHelper(this);
            API = new APIHelper(this);
        }

        ~ApplicationManager()
        {
            //Close browser
            Driver.Quit();
            Driver = null;

            //Clear helpers using   
            Login = null;
            Registration = null;
            Project = null;
            ManagementMenu = null;
            FTP = null;
            API = null;
        }

        public static ApplicationManager GetInstance()
        {
            if (_applicationManager.IsValueCreated) return _applicationManager.Value;
            var newInstance = new ApplicationManager();
            newInstance.Driver.Url = _baseUrl + "login_page.php";
            _applicationManager.Value = newInstance;

            return _applicationManager.Value;
        }
    }
}