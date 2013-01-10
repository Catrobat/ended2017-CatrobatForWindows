using System.Collections.Generic;
using Catrobat.Core.Objects;
using Catrobat.Core.Storage;
using Catrobat.Core.ZIP;

namespace Catrobat.Core.Resources.SampleProjects
{
  public class SampleProjectLoader
  {
    private readonly Dictionary<string, string> sampleProjectNames = new Dictionary<string, string>
    {
      {"default.catroid", "Dafault"},
      {"speakBrick.catroid", "SpeakBrick"},
      {"Broadcast_test.catroid", "BroadCastBricks"},
      {"pointTo.catroid", "PointBrick"},
      {"LookBricks.catroid", "LookBrick"},
      {"MotionBrick.catroid", "Motion"},
      {"SwitchBackground.catroid", "BackgroundSwitching"},
      {"glideTo.catroid", "GlideTo"},
    };

    public void LoadSampleProjects()
    {
      foreach (var pair in sampleProjectNames)
      {
        string projectFileName = pair.Key;
        string projectName = pair.Value;

        string path = "Resources/SampleProjects/";
        path += projectFileName;

        var resourceStream = ResourceLoader.OpenResourceStream(path);
        CatrobatZip.UnzipCatrobatPackageIntoIsolatedStorage(resourceStream, CatrobatContext.ProjectsPath + "/" + projectName);


        using (IStorage storage = StorageSystem.GetStorage())
        {
          string xml = storage.ReadTextFile(CatrobatContext.ProjectsPath + "/" + projectName + "/" +
                                   Project.ProjectCodePath); // TODO: use ResourceLoader instead

          var project = new Project(xml);
          project.SetProjectName(projectName);

          project.Save();
        }
      }
    }
  }
}