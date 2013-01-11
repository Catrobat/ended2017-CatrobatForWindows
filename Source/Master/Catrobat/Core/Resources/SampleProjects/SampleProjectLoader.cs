using System;
using Catrobat.Core.Objects;
using Catrobat.Core.Storage;
using Catrobat.Core.ZIP;
using System.Collections.Generic;

namespace Catrobat.Core.Resources.SampleProjects
{
  public class SampleProjectLoader
  {
    private Dictionary<string, string> sampleProjectNames = new Dictionary<string, string>
    { 
      {"Piano.catrobat","Piano"},
      {"Pacman.catrobat","Pacman"},
      {"HAL9000.catrobat", "HAL 9000"},
      {"Aquarium_v2.catrobat","Aquarium v2"},      
      {"Fishy.catroid","Fishy"},
      {"Egg_timer.catrobat","Egg Timer"},
      {"speakBrick.catroid", "SpeakBrick"},
      {"Broadcast_test.catroid", "BroadCastBricks"}, 
      {"pointTo.catroid","PointBrick"},
      {"LookBricks.catroid","LookBrick"},
      {"MotionBrick.catroid","Motion"},
      {"SwitchBackground.catroid", "BackgroundSwitching"},
      {"glideTo.catroid", "GlideTo"},      
    };

    public void LoadSampleProjects()
    {
      foreach (KeyValuePair<string,string> pair in sampleProjectNames)
      {
        string projectFileName = pair.Key;
        string projectName = pair.Value;

        string path = "Resources/SampleProjects/";
        path += projectFileName;

        var resourceStream = ResourceLoader.GetResourceStream(path);
        CatrobatZip.UnzipCatrobatPackageIntoIsolatedStorage(resourceStream, CatrobatContext.ProjectsPath + "/" + projectName);

        using (IStorage storage = StorageSystem.GetStorage())
        {
          string xml = storage.ReadTextFile(CatrobatContext.ProjectsPath + "/" + projectName + "/" + Project.ProjectCodePath);

          var project = new Project(xml);
          project.SetProjectName(projectName);
          
          project.Save();
        }
      }
    }
  }
}
