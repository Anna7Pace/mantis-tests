namespace mantis_tests.appmanager
{
    public class ManagementMenuHelper : HelperBase
    {
        public ManagementMenuHelper(ApplicationManager manager) : base(manager)
        {
        }

        public void GoToManagePage()
        {
            Driver.Url = "http://localhost:8080/mantisbt-2.25.1/manage_overview_page.php";
        }
    }
}