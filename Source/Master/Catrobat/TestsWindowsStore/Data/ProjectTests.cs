using System;
using Catrobat.Core;
using Catrobat.Core.Objects;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using TestsWindowsStore.Data.SampleData;
using Catrobat.Core.Storage;

namespace TestsWindowsStore.Data
{
  [TestClass]
  public class ProjectTests
  {
    [TestMethod]
    public void CreateNewProjectTest()
    {
      TestHelper.InitializeAndClearCatrobatContext();

      SampleLoader.LoadSampleProject("test.catroid", "DefaultProject");

      string newProjectName = "TestProjectCreateNew";

      CatrobatContext.Instance.CreateNewProject(newProjectName);

      using (IStorage storage = StorageSystem.GetStorage())
      {
        Assert.AreEqual(CatrobatContext.Instance.CurrentProject.BasePath, CatrobatContext.ProjectsPath + "/" + newProjectName);

        Assert.IsTrue(storage.FileExists(CatrobatContext.Instance.CurrentProject.BasePath + "/" + Project.ProjectCodePath));
        Assert.IsTrue(storage.DirectoryExists(CatrobatContext.Instance.CurrentProject.BasePath + "/" + Project.ImagesPath));
        Assert.IsTrue(storage.DirectoryExists(CatrobatContext.Instance.CurrentProject.BasePath + "/" + Project.SoundsPath));
      }

      TestHelper.InitializeAndClearCatrobatContext();
    }

    [TestMethod]
    public void DeleteProjectTest()
    {
      TestHelper.InitializeAndClearCatrobatContext();

      SampleLoader.LoadSampleProject("test.catroid", "DefaultProject1");
      SampleLoader.LoadSampleProject("test.catroid", "DefaultProject2");

      Assert.AreEqual(CatrobatContext.Instance.CurrentProject.ProjectName, "DefaultProject2");

      using (IStorage storage = StorageSystem.GetStorage())
      {
        Assert.IsTrue(storage.FileExists(CatrobatContext.Instance.CurrentProject.BasePath + "/" + Project.ProjectCodePath));
        Assert.IsTrue(storage.DirectoryExists(CatrobatContext.Instance.CurrentProject.BasePath + "/" + Project.ImagesPath));
        Assert.IsTrue(storage.DirectoryExists(CatrobatContext.Instance.CurrentProject.BasePath + "/" + Project.SoundsPath));
      }

      CatrobatContext.Instance.DeleteProject("DefaultProject1");

      using (IStorage storage = StorageSystem.GetStorage())
      {
        Assert.IsTrue(storage.DirectoryExists(CatrobatContext.Instance.CurrentProject.BasePath));
        Assert.IsFalse(storage.DirectoryExists(CatrobatContext.ProjectsPath + "/" + "DefaultProject1"));
      }

      TestHelper.InitializeAndClearCatrobatContext();
    }

    [TestMethod]
    public void DeleteCurrentProjectTest()
    {
      TestHelper.InitializeAndClearCatrobatContext();

      SampleLoader.LoadSampleProject("test.catroid", "DefaultProject1");
      SampleLoader.LoadSampleProject("test.catroid", "DefaultProject2");

      Assert.AreEqual(CatrobatContext.Instance.CurrentProject.ProjectName, "DefaultProject2");

      using (IStorage storage = StorageSystem.GetStorage())
      {
        Assert.IsTrue(storage.FileExists(CatrobatContext.Instance.CurrentProject.BasePath + "/" + Project.ProjectCodePath));
        Assert.IsTrue(storage.DirectoryExists(CatrobatContext.Instance.CurrentProject.BasePath + "/" + Project.ImagesPath));
        Assert.IsTrue(storage.DirectoryExists(CatrobatContext.Instance.CurrentProject.BasePath + "/" + Project.SoundsPath));
      }

      CatrobatContext.Instance.DeleteProject("DefaultProject2");

      using (IStorage storage = StorageSystem.GetStorage())
      {
        Assert.IsFalse(storage.DirectoryExists(CatrobatContext.ProjectsPath + "/" + "DefaultProject2"));
      }

      Assert.IsTrue(CatrobatContext.Instance.CurrentProject.ProjectName == CatrobatContext.DefaultProjectName);

      TestHelper.InitializeAndClearCatrobatContext();
    }

    [TestMethod]
    public void SetCurrentProjectTest()
    {
      TestHelper.InitializeAndClearCatrobatContext();

      string newProjectName = "SelectProjectTestProject";
      string oldProjectName = "test";
      SampleLoader.LoadSampleProject("test.catroid", oldProjectName);



      CatrobatContext.Instance.CreateNewProject(newProjectName);

      CatrobatContext.Instance.SetCurrentProject(oldProjectName);

      Assert.AreEqual(CatrobatContext.Instance.CurrentProject.ProjectName, oldProjectName);

      TestHelper.InitializeAndClearCatrobatContext();
    }

    [TestMethod]
    public void RenameProjectTest()
    {
      TestHelper.InitializeAndClearCatrobatContext();

      SampleLoader.LoadSampleProject("test.catroid", "test");

      CatrobatContext.Instance.CurrentProject.ProjectName = "RenamedProject";

      using (IStorage storage = StorageSystem.GetStorage())
      {
        Assert.IsTrue(storage.FileExists(CatrobatContext.Instance.CurrentProject.BasePath + "/" + Project.ProjectCodePath));
        Assert.IsTrue(storage.DirectoryExists(CatrobatContext.Instance.CurrentProject.BasePath + "/" + Project.ImagesPath));
        Assert.IsTrue(storage.DirectoryExists(CatrobatContext.Instance.CurrentProject.BasePath + "/" + Project.SoundsPath));
      }

      TestHelper.InitializeAndClearCatrobatContext();
    }

    [TestMethod]
    public void UpdateLocalProjectsTest()
    {
      TestHelper.InitializeAndClearCatrobatContext();

      string newProjectName1 = "SelectProjectTestProject1";
      string newProjectName2 = "SelectProjectTestProject2";
      string newProjectName3 = "SelectProjectTestProject3";

      CatrobatContext.Instance.CreateNewProject(newProjectName1);
      CatrobatContext.Instance.CreateNewProject(newProjectName2);
      CatrobatContext.Instance.CreateNewProject(newProjectName3);

      var loaclProjects = CatrobatContext.Instance.LocalProjects;

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

      TestHelper.InitializeAndClearCatrobatContext();
    }

    [TestMethod]
    public void CopyProjectTest()
    {
      TestHelper.InitializeAndClearCatrobatContext();

      string newProjectName1 = "SelectProjectTestProject1";
      string newProjectName2 = "SelectProjectTestProject2";
      string copyToTest1 = "SelectProjectTestProject11";
      string copyToTest2 = "SelectProjectTestProject11";

      CatrobatContext.Instance.CreateNewProject(newProjectName1);
      CatrobatContext.Instance.CreateNewProject(newProjectName2);

      CatrobatContext.Instance.CopyProject(newProjectName1);
      CatrobatContext.Instance.SetCurrentProject(copyToTest1);
      Assert.IsTrue(CatrobatContext.Instance.CurrentProject.ProjectName == copyToTest1);

      CatrobatContext.Instance.CopyProject(newProjectName1);
      CatrobatContext.Instance.SetCurrentProject(copyToTest2);
      Assert.IsTrue(CatrobatContext.Instance.CurrentProject.ProjectName == copyToTest2);

      TestHelper.InitializeAndClearCatrobatContext();
    }
  }
}
