﻿using System;
using Catrobat.Core.Objects;
using Catrobat.Core.Storage;
using Catrobat.Core.ZIP;
using System.Collections.Generic;
using System.Diagnostics;

namespace Catrobat.Core.Resources
{
  public class SampleProjectLoader
  {
    private readonly Dictionary<string, string> _sampleProjectNames = new Dictionary<string, string>
    { 
      {"Piano.catrobat","Piano"},
      {"Pacman.catrobat","Pacman"},
      {"HAL9000.catrobat", "HAL 9000"},
      {"Aquarium_v2.catrobat","Aquarium v2"},
      {"Fishy.catroid","Fishy"},
      {"Egg_timer.catrobat","Egg Timer"}
    };

    public void LoadSampleProjects()
    {
      using (var storage = StorageSystem.GetStorage())
      {
        storage.DeleteDirectory("");
      }

      foreach (KeyValuePair<string, string> pair in _sampleProjectNames)
      {
        string projectFileName = pair.Key;
        string projectName = pair.Value;
        string path = string.Format("SampleProjects/{0}", projectFileName);

        System.IO.Stream resourceStream = null;

        try
        {
          var resourceLoader = ResourceLoader.CreateResourceLoader();
          resourceStream = resourceLoader.OpenResourceStream(ResourceScope.Resources, path);

          try
          {
            var projectFolderPath = CatrobatContext.ProjectsPath + "/" + projectName;

            using (IStorage storage = StorageSystem.GetStorage())
            {
              if (!storage.DirectoryExists(projectFolderPath))
              {
                CatrobatZip.UnzipCatrobatPackageIntoIsolatedStorage(resourceStream, CatrobatContext.ProjectsPath + "/" + projectName);
              }
            }

            using (IStorage storage = StorageSystem.GetStorage())
            {
              string xml = storage.ReadTextFile(CatrobatContext.ProjectsPath + "/" + projectName + "/" + Project.ProjectCodePath);

              var project = new Project(xml);
              project.SetProjectName(projectName);

              project.Save();
            }
          }
          catch (Exception) // TODO: Implement catch exception body
          {

          }
        }
        catch (Exception)
        {
          Debugger.Break(); // sample project does not exist: please remove from _sampleProjectNames or add to Core/Resources/SampleProjects
          continue;
        }
        finally
        {
          resourceStream.Dispose();
        }
      }
    }
  }
}
