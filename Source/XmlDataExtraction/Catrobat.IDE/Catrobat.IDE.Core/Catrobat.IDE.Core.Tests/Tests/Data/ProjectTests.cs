using Catrobat.IDE.Core.Tests.Misc;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Catrobat.IDE.Core.Tests.Tests.Data
{
    [TestClass]
    public class ProjectTests
    {
        [ClassInitialize()]
        public static void TestClassInitialize(TestContext testContext)
        {
            TestHelper.InitializeTests();
        }

        //[TestMethod, TestCategory("GatedTests")]
        //public void CreateNewProjectTest()
        //{
        //    CatrobatContext.SetContextHolder(new ContextHolderTests(new CatrobatContext()));

        //    const string newProjectName = "TestProjectCreateNew";
        //    CatrobatContext.GetContext().CreateNewProject(newProjectName);

        //    using (IStorage storage = StorageSystem.GetStorage())
        //    {
        //        Assert.AreEqual(CatrobatContextBase.ProjectsPath + "/" + newProjectName, CatrobatContext.GetContext().CurrentProject.BasePath);

        //        Assert.IsTrue(storage.FileExists(CatrobatContext.GetContext().CurrentProject.BasePath + "/" + Project.ProjectCodePath));
        //        Assert.IsTrue(storage.DirectoryExists(CatrobatContext.GetContext().CurrentProject.BasePath + "/" + Project.ImagesPath));
        //        Assert.IsTrue(storage.DirectoryExists(CatrobatContext.GetContext().CurrentProject.BasePath + "/" + Project.SoundsPath));
        //    }

        //    CatrobatContext.SetContextHolder(null);
        //}

        //[TestMethod, TestCategory("GatedTests")]
        //public void DeleteProjectTest()
        //{
        //    TestHelper.InitializeAndClearCatrobatContext();
        //    CatrobatContext.SetContextHolder(new ContextHolderTests(new CatrobatContext()));

        //    const string newProjectName = "TestProjectDelete";
        //    CatrobatContext.GetContext().CreateNewProject(newProjectName);

        //    Assert.AreEqual(newProjectName, CatrobatContext.GetContext().CurrentProject.ProjectHeader.ProgramName);

        //    using (IStorage storage = StorageSystem.GetStorage())
        //    {
        //        Assert.IsTrue(storage.FileExists(CatrobatContext.GetContext().CurrentProject.BasePath + "/" + Project.ProjectCodePath));
        //        Assert.IsTrue(storage.DirectoryExists(CatrobatContext.GetContext().CurrentProject.BasePath + "/" + Project.ImagesPath));
        //        Assert.IsTrue(storage.DirectoryExists(CatrobatContext.GetContext().CurrentProject.BasePath + "/" + Project.SoundsPath));
        //    }

        //    CatrobatContext.GetContext().DeleteProject(newProjectName);

        //    using (IStorage storage = StorageSystem.GetStorage())
        //    {
        //        Assert.IsTrue(storage.DirectoryExists(CatrobatContext.GetContext().CurrentProject.BasePath));
        //        Assert.IsFalse(storage.DirectoryExists(CatrobatContextBase.ProjectsPath + "/" + newProjectName));
        //    }

        //    CatrobatContext.SetContextHolder(null);
        //}

        //[TestMethod, TestCategory("GatedTests")]
        //public void DeleteCurrentProjectTest()
        //{
        //    TestHelper.InitializeAndClearCatrobatContext();
        //    CatrobatContext.SetContextHolder(new ContextHolderTests(new CatrobatContext()));

        //    const string newProjectName1 = "TestProjectDelete1";
        //    const string newProjectName2 = "TestProjectDelete2";
        //    CatrobatContext.GetContext().CreateNewProject(newProjectName1);
        //    CatrobatContext.GetContext().CreateNewProject(newProjectName2);

        //    Assert.AreEqual(newProjectName2, CatrobatContext.GetContext().CurrentProject.ProjectHeader.ProgramName);

        //    using (IStorage storage = StorageSystem.GetStorage())
        //    {
        //        Assert.IsTrue(storage.FileExists(CatrobatContext.GetContext().CurrentProject.BasePath + "/" + Project.ProjectCodePath));
        //        Assert.IsTrue(storage.DirectoryExists(CatrobatContext.GetContext().CurrentProject.BasePath + "/" + Project.ImagesPath));
        //        Assert.IsTrue(storage.DirectoryExists(CatrobatContext.GetContext().CurrentProject.BasePath + "/" + Project.SoundsPath));
        //    }

        //    CatrobatContext.GetContext().DeleteProject(newProjectName2);

        //    using (IStorage storage = StorageSystem.GetStorage())
        //    {
        //        Assert.IsFalse(storage.DirectoryExists(CatrobatContextBase.ProjectsPath + "/" + newProjectName2));
        //    }

        //    Assert.IsTrue(CatrobatContext.GetContext().CurrentProject.ProjectHeader.ProgramName == CatrobatContextBase.DefaultProjectName);

        //    CatrobatContext.SetContextHolder(null);
        //}

        //[TestMethod, TestCategory("GatedTests")]
        //public void SetCurrentProjectTest()
        //{
        //    TestHelper.InitializeAndClearCatrobatContext();
        //    CatrobatContext.SetContextHolder(new ContextHolderTests(new CatrobatContext()));

        //    const string oldProjectName = "test";
        //    const string newProjectName = "SelectProjectTestProject";
        //    CatrobatContext.GetContext().CreateNewProject(oldProjectName);
        //    CatrobatContext.GetContext().CreateNewProject(newProjectName);

        //    CatrobatContext.GetContext().SetCurrentProject(oldProjectName);

        //    Assert.AreEqual(oldProjectName, CatrobatContext.GetContext().CurrentProject.ProjectHeader.ProgramName);
            
        //    CatrobatContext.SetContextHolder(null);
        //}

        //[TestMethod, TestCategory("GatedTests")]
        //public void RenameProjectTest()
        //{
        //    TestHelper.InitializeAndClearCatrobatContext();
        //    CatrobatContext.SetContextHolder(new ContextHolderTests(new CatrobatContext()));

        //    const string projectName = "RenameProjectTestProject";
        //    CatrobatContext.GetContext().CreateNewProject(projectName);

        //    CatrobatContext.GetContext().CurrentProject.ProjectHeader.ProgramName = "RenamedProject";

        //    using (IStorage storage = StorageSystem.GetStorage())
        //    {
        //        Assert.AreEqual("Projects/RenamedProject",CatrobatContext.GetContext().CurrentProject.BasePath);
        //        Assert.IsTrue(storage.FileExists(CatrobatContext.GetContext().CurrentProject.BasePath + "/" + Project.ProjectCodePath));
        //        Assert.IsTrue(storage.DirectoryExists(CatrobatContext.GetContext().CurrentProject.BasePath + "/" + Project.ImagesPath));
        //        Assert.IsTrue(storage.DirectoryExists(CatrobatContext.GetContext().CurrentProject.BasePath + "/" + Project.SoundsPath));
        //    }

        //    CatrobatContext.SetContextHolder(null);
        //}

        //[TestMethod, TestCategory("GatedTests")]
        //public void UpdateLocalProjectsTest()
        //{
        //    TestHelper.InitializeAndClearCatrobatContext();
        //    CatrobatContext.SetContextHolder(new ContextHolderTests(new CatrobatContext()));

        //    const string newProjectName1 = "SelectProjectTestProject1";
        //    const string newProjectName2 = "SelectProjectTestProject2";
        //    const string newProjectName3 = "SelectProjectTestProject3";

        //    CatrobatContext.GetContext().CreateNewProject(newProjectName1);
        //    CatrobatContext.GetContext().CreateNewProject(newProjectName2);
        //    CatrobatContext.GetContext().CreateNewProject(newProjectName3);

        //    var loaclProjects = CatrobatContext.GetContext().LocalProjects;

        //    bool found1 = false;
        //    bool found2 = false;
        //    foreach (ProjectDummyHeader header in loaclProjects)
        //    {
        //        if (header.ProjectName == newProjectName1)
        //            found1 = true;

        //        if (header.ProjectName == newProjectName2)
        //            found2 = true;
        //    }

        //    Assert.AreEqual(3, loaclProjects.Count);
        //    Assert.IsTrue(found1 && found2);

        //    CatrobatContext.SetContextHolder(null);
        //}

        //[TestMethod, TestCategory("GatedTests")]
        //public void CopyProjectTest()
        //{
        //    TestHelper.InitializeAndClearCatrobatContext();
        //    CatrobatContext.SetContextHolder(new ContextHolderTests(new CatrobatContext()));

        //    const string newProjectName1 = "SelectProjectTestProject1";
        //    const string copyToTest1 = "SelectProjectTestProject11";
        //    const string copyToTest2 = "SelectProjectTestProject12";

        //    CatrobatContext.GetContext().CreateNewProject(newProjectName1);

        //    CatrobatContext.GetContext().CopyProject(newProjectName1);
        //    CatrobatContext.GetContext().SetCurrentProject(copyToTest1);
        //    Assert.AreEqual(copyToTest1, CatrobatContext.GetContext().CurrentProject.ProjectHeader.ProgramName);

        //    CatrobatContext.GetContext().CopyProject(newProjectName1);
        //    CatrobatContext.GetContext().SetCurrentProject(copyToTest2);
        //    Assert.AreEqual(copyToTest2, CatrobatContext.GetContext().CurrentProject.ProjectHeader.ProgramName);

        //    CatrobatContext.SetContextHolder(null);
        //}
    }
}
