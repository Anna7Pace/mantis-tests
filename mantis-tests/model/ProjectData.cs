using System;
using System.Collections.Generic;

namespace mantis_tests.model
{
    public class ProjectData : IEquatable<ProjectData>, IComparable<ProjectData>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Id { get; set; }

        //Get the list of projects that are accessible to the logged in user.
        public static List<ProjectData> GetProjectsFromMantisAPI(AccountData accountData)
        {
            var client = new Mantis.MantisConnectPortTypeClient();
            return client.mc_projects_get_user_accessible(accountData.UserName, accountData.Password);
        }
        
        public bool Equals(ProjectData other)
        {
            if (ReferenceEquals(other, null))
            {
                return false;
            }

            if (ReferenceEquals(this, other))
            {
                return true;
            }

            return Name == other.Name && Description == other.Description;
        }

        public int CompareTo(ProjectData other)
        {
            if (ReferenceEquals(other, null))
            {
                return 1;
            }

            if (Name.CompareTo(other.Name) == 0)
            {
                return Name.CompareTo(other.Name);
            }

            return Description.CompareTo(other.Description);
        }
    }
}