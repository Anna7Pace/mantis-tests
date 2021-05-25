using System.Collections.Generic;
using mantis_tests.model;
using NUnit.Framework;

namespace mantis_tests.tests
{
    [TestFixture]
    public class RemoveProjectTests : AuthTestBase
    {
        ProjectData _projectData = new() {Name = "Test Project", Description = "Description of the project"};
        AccountData _accountData = new() {UserName = "administrator", Password = "root"};
        private List<ProjectData> _oldProjects;

        [SetUp]
        public new void SetUp()
        {
            applicationManager.API.CreateIfNotExist(_projectData, _accountData);
            _oldProjects = ProjectData.GetProjectsFromMantisAPI(_accountData);
        }

        [Test]
        public void RemoveProject()
        {
            //Act
            applicationManager.Project.Remove(0);
            var newProjects = ProjectData.GetProjectsFromMantisAPI(_accountData);
            _oldProjects.RemoveAt(0);
            _oldProjects.Sort();
            newProjects.Sort();

            //Assert
            Assert.AreEqual(_oldProjects.Count - 1, applicationManager.Project.GetProjectListCount());
            Assert.AreEqual(_oldProjects, newProjects);
        }
    }
}