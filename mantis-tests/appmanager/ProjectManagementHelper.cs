using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text.RegularExpressions;
using mantis_tests.model;
using NUnit.Framework;
using OpenQA.Selenium;

namespace mantis_tests.appmanager
{
    public class ProjectManagementHelper : HelperBase
    {
        private ApplicationManager _applicationManager;

        public ProjectManagementHelper(ApplicationManager manager) : base(manager)
        {
            _applicationManager = manager;
        }

        public void Create(ProjectData project, AccountData accountData)
        {
            if (ProjectData.GetProjectsFromMantisAPI(accountData).Count < 1)
            {
                var isManageProjPageOpen = Driver.Url.Contains("manage_proj_page");
                if (isManageProjPageOpen)
                {
                    CreateNewProject();
                    FillProjectData(project);
                    SubmitProjectCreation();
                    Proceed();
                }
                else
                {
                    FillProjectData(project);
                    SubmitProjectCreation();
                    Proceed();
                }
            }
            else
            {
                _applicationManager.ManagementMenu.GoToManagePage();
                _applicationManager.Project.TransferToProjectPage();
                CreateNewProject();
                FillProjectData(project);
                SubmitProjectCreation();
                Proceed();
            }
        }

        private void Proceed()
        {
            Driver.FindElement(By.LinkText("Proceed")).Click();
        }

        private void SubmitProjectCreation()
        {
            Driver.FindElement(By.XPath("//*[@id='manage-project-create-form']/div/div[3]/input")).Click();
        }

        private void FillProjectData(ProjectData project)
        {
            Driver.FindElement(By.Id("project-name")).SendKeys(project.Name);
            Driver.FindElement(By.Id("project-description")).SendKeys(project.Description);
        }

        private void CreateNewProject()
        {
            Driver.FindElement(
                    By.XPath("//*[@id='main-container']/div[2]/div[2]/div/div/div[2]/div[2]/div/div[1]/form/button"))
                .Click();
        }

        public void TransferToProjectPage()
        {
            Driver.FindElement(By.LinkText("Manage Projects")).Click();
        }

        public void Remove(int index)
        {
            _applicationManager.ManagementMenu.GoToManagePage();
            _applicationManager.Project.TransferToProjectPage();
            SelectProjectToRemove(index);
            DeleteProject();
            ConfirmRemoveProject();
        }

        private void ConfirmRemoveProject()
        {
            Driver.FindElement(By.XPath("//*[@id='main-container']/div[2]/div[2]/div/div/div[2]/form/input[4]"))
                .Click();
        }

        private void DeleteProject()
        {
            Driver.FindElement(
                By.XPath("//*[@id='project-delete-form']/fieldset/input[3]")).Click();
        }

        private void SelectProjectToRemove(int index)
        {
            var listProject = Driver.FindElements(By.CssSelector(" td > a"));
            listProject[index].Click();
        }

        private void SelectProjectToRemove(string projectName)
        {
            _applicationManager.ManagementMenu.GoToManagePage();
            _applicationManager.Project.TransferToProjectPage();
            Driver.FindElement(By.LinkText(projectName)).Click();
        }

        public int GetProjectListCount()
        {
            return Driver.FindElements(By.XPath("//*[@id='main-container']/div//div[2]/table/tbody/tr")).Count;
        }

        public List<ProjectData> GetAllGroups()
        {
            _applicationManager.ManagementMenu.GoToManagePage();
            _applicationManager.Project.TransferToProjectPage();
            var rows =
                Driver.FindElements(By.XPath("//*[@id='main-container']/div//div[2]/table/tbody/tr"));

            return (from row in rows
                select row.FindElement(By.TagName("a"))
                into link
                let name = link.Text
                let href = link.GetAttribute("href")
                let m = Regex.Match(href, @"\d+$")
                let id = m.Value
                select new ProjectData {Name = name, Id = id}).ToList();
        }

        public void CreateIfNotExist(ProjectData projectData, AccountData accountData)
        {
            if (ProjectData.GetProjectsFromMantisAPI(accountData).Count >= 1) return;
            FillProjectData(projectData);
            SubmitProjectCreation();
            Proceed();
        }

        public void RemoveIfExist(ProjectData project, AccountData accountData)
        {
            foreach (var projectData in ProjectData.GetProjectsFromMantisAPI(accountData)
                .Where(projectData => projectData.Name == project.Name))
            {
                SelectProjectToRemove(projectData.Name);
                DeleteProject();
                ConfirmRemoveProject();
            }
        }
    }
}