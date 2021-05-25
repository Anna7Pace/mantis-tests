using mantis_tests.model;

namespace mantis_tests.appmanager
{
    public class APIHelper : HelperBase
    {
        public APIHelper(ApplicationManager manager) : base(manager)
        {
        }

        public void CreateIfNotExist(ProjectData projectData, AccountData accountData)
        {
            var client = new Mantis.MantisConnectPortTypeClient();
            if (ProjectData.GetProjectsFromMantisAPI(accountData).Count < 1)
            {
                client.mc_project_add(accountData.UserName, accountData.Password, projectData);
            }
        }
    }
}

