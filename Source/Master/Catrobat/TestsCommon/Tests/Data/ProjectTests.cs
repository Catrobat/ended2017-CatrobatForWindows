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

      SampleLoader.LoadSampleProject("test.catroid", "DefaultProject");

      string newProjectName = "TestProjectCreateNew";

      CatrobatContext.GetContext().CreateNewProject(newProjectName);

      using (IStorage storage = StorageSystem.GetStorage())
      {
        Assert.AreEqual(CatrobatContext.GetContext().CurrentProject.BasePath, CatrobatContext.ProjectsPath + "/" + newProjectName);

        Assert.IsTrue(storage.FileExists(CatrobatContext.GetContext().CurrentProject.BasePath + "/" + Project.ProjectCodePath));
        Assert.IsTrue(storage.DirectoryExists(CatrobatContext.GetContext().CurrentProject.BasePath + "/" + Project.ImagesPath));
        Assert.IsTrue(storage.DirectoryExists(CatrobatContext.GetContext().CurrentProject.BasePath + "/" + Project.SoundsPath));
      }

    }

    [TestMethod]
    public void DeleteProjectTest()
    {
      TestHelper.InitializeAndClearCatrobatContext();

      SampleLoader.LoadSampleProject("test.catroid", "DefaultProject1");
      SampleLoader.LoadSampleProject("test.catroid", "DefaultProject2");

      Assert.AreEqual(CatrobatContext.GetContext().CurrentProject.ProjectName, "DefaultProject2");

      using (IStorage storage = StorageSystem.GetStorage())
      {
        Assert.IsTrue(storage.FileExists(CatrobatContext.GetContext().CurrentProject.BasePath + "/" + Project.ProjectCodePath));
        Assert.IsTrue(storage.DirectoryExists(CatrobatContext.GetContext().CurrentProject.BasePath + "/" + Project.ImagesPath));
        Assert.IsTrue(storage.DirectoryExists(CatrobatContext.GetContext().CurrentProject.BasePath + "/" + Project.SoundsPath));
      }

      CatrobatContext.GetContext().DeleteProject("DefaultProject1");

      using (IStorage storage = StorageSystem.GetStorage())
      {
        Assert.IsTrue(storage.DirectoryExists(CatrobatContext.GetContext().CurrentProject.BasePath));
        Assert.IsFalse(storage.DirectoryExists(CatrobatContext.ProjectsPath + "/" + "DefaultProject1"));
      }
    }

    [TestMethod]
    public void DeleteCurrentProjectTest()
    {
      TestHelper.InitializeAndClearCatrobatContext();

      SampleLoader.LoadSampleProject("test.catroid", "DefaultProject1");
      SampleLoader.LoadSampleProject("test.catroid", "DefaultProject2");

      Assert.AreEqual(CatrobatContext.GetContext().CurrentProject.ProjectName, "DefaultProject2");

      using (IStorage storage = StorageSystem.GetStorage())
      {
        Assert.IsTrue(storage.FileExists(CatrobatContext.GetContext().CurrentProject.BasePath + "/" + Project.ProjectCodePath));
        Assert.IsTrue(storage.DirectoryExists(CatrobatContext.GetContext().CurrentProject.BasePath + "/" + Project.ImagesPath));
        Assert.IsTrue(storage.DirectoryExists(CatrobatContext.GetContext().CurrentProject.BasePath + "/" + Project.SoundsPath));
      }

      CatrobatContext.GetContext().DeleteProject("DefaultProject2");

      using (IStorage storage = StorageSystem.GetStorage())
      {
        Assert.IsFalse(storage.DirectoryExists(CatrobatContext.ProjectsPath + "/" + "DefaultProject2"));
      }

      Assert.IsTrue(CatrobatContext.GetContext().CurrentProject.ProjectName == CatrobatContext.DefaultProjectName);
    }

    [TestMethod]
    public void SetCurrentProjectTest()
    {
      TestHelper.InitializeAndClearCatrobatContext();

      string newProjectName = "SelectProjectTestProject";
      string oldProjectName = "test";
      SampleLoader.LoadSampleProject("test.catroid", oldProjectName);

      CatrobatContext.GetContext().CreateNewProject(newProjectName);

      CatrobatContext.GetContext().SetCurrentProject(oldProjectName);

      Assert.AreEqual(CatrobatContext.GetContext().CurrentProject.ProjectName, oldProjectName);
    }

    [TestMethod]
    public void RenameProjectTest()
    {
      TestHelper.InitializeAndClearCatrobatContext();

      SampleLoader.LoadSampleProject("test.catroid", "test");

      CatrobatContext.GetContext().CurrentProject.ProjectName = "RenamedProject";

      using (IStorage storage = StorageSystem.GetStorage())
      {
        Assert.IsTrue(storage.FileExists(CatrobatContext.GetContext().CurrentProject.BasePath + "/" + Project.ProjectCodePath));
        Assert.IsTrue(storage.DirectoryExists(CatrobatContext.GetContext().CurrentProject.BasePath + "/" + Project.ImagesPath));
        Assert.IsTrue(storage.DirectoryExists(CatrobatContext.GetContext().CurrentProject.BasePath + "/" + Project.SoundsPath));
      }
    }

    [TestMethod]
    public void UpdateLocalProjectsTest()
    {
      TestHelper.InitializeAndClearCatrobatContext();

      string newProjectName1 = "SelectProjectTestProject1";
      string newProjectName2 = "SelectProjectTestProject2";
      string newProjectName3 = "SelectProjectTestProject3";

      CatrobatContext.GetContext().CreateNewProject(newProjectName1);
      CatrobatContext.GetContext().CreateNewProject(newProjectName2);
      CatrobatContext.GetContext().CreateNewProject(newProjectName3);

      var loaclProjects = CatrobatContext.GetContext().LocalProjects;

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

      CatrobatContext.GetContext().CreateNewProject(newProjectName1);
      CatrobatContext.GetContext().CreateNewProject(newProjectName2);

      CatrobatContext.GetContext().CopyProject(newProjectName1);
      CatrobatContext.GetContext().SetCurrentProject(copyToTest1);
      Assert.IsTrue(CatrobatContext.GetContext().CurrentProject.ProjectName == copyToTest1);

      CatrobatContext.GetContext().CopyProject(newProjectName1);
      CatrobatContext.GetContext().SetCurrentProject(copyToTest2);
      Assert.IsTrue(CatrobatContext.GetContext().CurrentProject.ProjectName == copyToTest2);
    }
  }
}
