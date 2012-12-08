using System;
using System.Collections.Generic;
using Catrobat.Core.Objects;
using Catrobat.Core.Storage;

namespace Catrobat.Core
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

                var uri = new Uri("/MetroCatData;component/" + path, UriKind.Relative);
#if SILVERLIGHT
          var resourceStream = Application.GetResourceStream(uri).Stream;
          CatrobatZip.UnzipCatrobatPackageIntoIsolatedStorage(resourceStream, CatrobatContext.ProjectsPath + "/" + projectName);
        #else
                // TODO: implement me
#endif

                using (IStorage storage = StorageSystem.GetStorage())
                {
                    string xml =
                        storage.ReadTestFile(CatrobatContext.ProjectsPath + "/" + projectName + "/" +
                                             Project.ProjectCodePath);

                    var project = new Project(xml);
                    project.SetProjectName(projectName);

                    project.Save();
                }
            }
        }
    }
}