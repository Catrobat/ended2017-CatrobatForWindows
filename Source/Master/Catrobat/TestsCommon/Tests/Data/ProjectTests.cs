using Catrobat.Core;
using Catrobat.Core.Objects;
using Catrobat.Core.Storage;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Catrobat.TestsCommon.Misc;
using Catrobat.TestsCommon.SampleData;

namespace Catrobat.TestsCommon.Tests.Data
{
  [TestClass]
  public class ProjectTests
  {
    [ClassInitialize()]
    public static void TestClassInitialize(TestContext testContext)
    {
      TestHelper.InitializeTests();
    }

    [TestMethod]
    public void CreateNewProjectTest()
    {
      TestHelper.InitializeAndClearCatrobatContext();

      var catrobatContext = SampleLoader.LoadSampleProject("test.catroid", "DefaultProject");

      string newProjectName = "TestProjectCreateNew";

      catrobatContext.CreateNewProject(newProjectName);

      using (IStorage storage = StorageSystem.GetStorage())
      {
        Assert.AreEqual(catrobatContext.CurrentProject.BasePath, CatrobatContext.ProjectsPath + "/" + newProjectName);

        Assert.IsTrue(storage.FileExists(catrobatContext.CurrentProject.BasePath + "/" + Project.ProjectCodePath));
        Assert.IsTrue(storage.DirectoryExists(catrobatContext.CurrentProject.BasePath + "/" + Project.ImagesPath));
        Assert.IsTrue(storage.DirectoryExists(catrobatContext.CurrentProject.BasePath + "/" + Project.SoundsPath));
      }

    }

    [TestMethod]
    public void DeleteProjectTest()
    {
      TestHelper.InitializeAndClearCatrobatContext();

      SampleLoader.LoadSampleProject("test.catroid", "DefaultProject1");
      var catrobatContext = SampleLoader.LoadSampleProject("test.catroid", "DefaultProject2");

      Assert.AreEqual(catrobatContext.CurrentProject.ProjectName, "DefaultProject2");

      using (IStorage storage = StorageSystem.GetStorage())
      {
        Assert.IsTrue(storage.FileExists(catrobatContext.CurrentProject.BasePath + "/" + Project.ProjectCodePath));
        Assert.IsTrue(storage.DirectoryExists(catrobatContext.CurrentProject.BasePath + "/" + Project.ImagesPath));
        Assert.IsTrue(storage.DirectoryExists(catrobatContext.CurrentProject.BasePath + "/" + Project.SoundsPath));
      }

      catrobatContext.DeleteProject("DefaultProject1");

      using (IStorage storage = StorageSystem.GetStorage())
      {
        Assert.IsTrue(storage.DirectoryExists(catrobatContext.CurrentProject.BasePath));
        Assert.IsFalse(storage.DirectoryExists(CatrobatContext.ProjectsPath + "/" + "DefaultProject1"));
      }
    }

    [TestMethod]
    public void DeleteCurrentProjectTest()
    {
      TestHelper.InitializeAndClearCatrobatContext();

      SampleLoader.LoadSampleProject("test.catroid", "DefaultProject1");
      var catrobatContext = SampleLoader.LoadSampleProject("test.catroid", "DefaultProject2");

      Assert.AreEqual(catrobatContext.CurrentProject.ProjectName, "DefaultProject2");

      using (IStorage storage = StorageSystem.GetStorage())
      {
        Assert.IsTrue(storage.FileExists(catrobatContext.CurrentProject.BasePath + "/" + Project.ProjectCodePath));
        Assert.IsTrue(storage.DirectoryExists(catrobatContext.CurrentProject.BasePath + "/" + Project.ImagesPath));
        Assert.IsTrue(storage.DirectoryExists(catrobatContext.CurrentProject.BasePath + "/" + Project.SoundsPath));
      }

      catrobatContext.DeleteProject("DefaultProject2");

      using (IStorage storage = StorageSystem.GetStorage())
      {
        Assert.IsFalse(storage.DirectoryExists(CatrobatContext.ProjectsPath + "/" + "DefaultProject2"));
      }

      Assert.IsTrue(catrobatContext.CurrentProject.ProjectName == CatrobatContext.DefaultProjectName);
    }

    [TestMethod]
    public void SetCurrentProjectTest()
    {
      TestHelper.InitializeAndClearCatrobatContext();

      string newProjectName = "SelectProjectTestProject";
      string oldProjectName = "test";
      var catrobatContext = SampleLoader.LoadSampleProject("test.catroid", oldProjectName);

      catrobatContext.CreateNewProject(newProjectName);

      catrobatContext.SetCurrentProject(oldProjectName);

      Assert.AreEqual(catrobatContext.CurrentProject.ProjectName, oldProjectName);
    }

    [TestMethod]
    public void RenameProjectTest()
    {
      TestHelper.InitializeAndClearCatrobatContext();

      var catrobatContext = SampleLoader.LoadSampleProject("test.catroid", "test");

      catrobatContext.CurrentProject.ProjectName = "RenamedProject";

      using (IStorage storage = StorageSystem.GetStorage())
      {
        Assert.IsTrue(storage.FileExists(catrobatContext.CurrentProject.BasePath + "/" + Project.ProjectCodePath));
        Assert.IsTrue(storage.DirectoryExists(catrobatContext.CurrentProject.BasePath + "/" + Project.ImagesPath));
        Assert.IsTrue(storage.DirectoryExists(catrobatContext.CurrentProject.BasePath + "/" + Project.SoundsPath));
      }
    }

    [TestMethod]
    public void UpdateLocalProjectsTest()
    {
      TestHelper.InitializeAndClearCatrobatContext();

      string newProjectName1 = "SelectProjectTestProject1";
      string newProjectName2 = "SelectProjectTestProject2";
      string newProjectName3 = "SelectProjectTestProject3";

      var catrobatContext = new CatrobatContext();

      catrobatContext.CreateNewProject(newProjectName1);
      catrobatContext.CreateNewProject(newProjectName2);
      catrobatContext.CreateNewProject(newProjectName3);

      var loaclProjects = catrobatContext.LocalProjects;

      bool found1 = false;
      bool found2 = false;
      foreach (ProjectHeader header in loaclProjects)
      {
        if (header.ProjectName == newProjectName1)
          found1 = true;

        if (header.ProjectName == newProjectName2)
          found2 = true;
      }

      Assert.AreEqual(loaclProjects.Count, 3);
      Assert.IsTrue(found1 && found2);
    }

    [TestMethod]
    public void CopyProjectTest()
    {
      TestHelper.InitializeAndClearCatrobatContext();

      string newProjectName1 = "SelectProjectTestProject1";
      string newProjectName2 = "SelectProjectTestProject2";
      string copyToTest1 = "SelectProjectTestProject11";
      string copyToTest2 = "SelectProjectTestProject11";

      var catrobatContext = new CatrobatContext();

      catrobatContext.CreateNewProject(newProjectName1);
      catrobatContext.CreateNewProject(newProjectName2);

      catrobatContext.CopyProject(newProjectName1);
      catrobatContext.SetCurrentProject(copyToTest1);
      Assert.IsTrue(catrobatContext.CurrentProject.ProjectName == copyToTest1);

      catrobatContext.CopyProject(newProjectName1);
      catrobatContext.SetCurrentProject(copyToTest2);
      Assert.IsTrue(catrobatContext.CurrentProject.ProjectName == copyToTest2);
    }
  }
}
