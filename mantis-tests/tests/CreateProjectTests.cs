using System.Collections.Generic;
using mantis_tests.model;
using NUnit.Framework;

namespace mantis_tests.tests
{
    [TestFixture]
    public class CreateProjectTests : AuthTestBase
    {
        ProjectData _projectData = new() {Name = "Test Project", Description = "Description of the project"};
        AccountData _accountData = new() {UserName = "administrator", Password = "root"};
        private List<ProjectData> _oldProjects;

        [SetUp]
        public new void SetUp()
        {
            applicationManager.Project.RemoveIfExist(_projectData, _accountData);
            _oldProjects = ProjectData.GetProjectsFromMantisAPI(_accountData);
        }

        [Test]
        public void CreateProject()
        {
            //Act
            applicationManager.Project.Create(_projectData, _accountData);
            var newProject = ProjectData.GetProjectsFromMantisAPI(_accountData);
            _oldProjects.Add(_projectData);
            _oldProjects.Sort();
            newProject.Sort();

            //Assert
            Assert.AreEqual(_oldProjects.Count + 1, applicationManager.Project.GetProjectListCount());
            Assert.AreEqual(_oldProjects, newProject);
        }
    }
}