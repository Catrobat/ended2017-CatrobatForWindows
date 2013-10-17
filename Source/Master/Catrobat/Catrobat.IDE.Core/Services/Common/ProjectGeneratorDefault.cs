using System.IO;
using Catrobat.IDE.Core.CatrobatObjects;
using Catrobat.IDE.Core.Resources.Localization;
using Catrobat.IDE.Core.Services.Data;
using Catrobat.IDE.Core.Services.Storage;
using SharpCompress.Archive.Zip;
using SharpCompress.Common;
using SharpCompress.Reader;

namespace Catrobat.IDE.Core.Services.Common
{
    public class ProjectGeneratorDefault : IProjectGenerator
    {
        public Project GenerateProject(string twoLetterIsoLanguageCode, bool writeToDisk)
        {
            var project = new Project();

            project.ProjectHeader = new ProjectHeader();
            project.SetProgramName(AppResources.Main_DefaultProjectName);
            project.ProjectScreenshot = new PortableImage(); // TODO: add real screenshot

            FillSprites(project, writeToDisk);

            if(writeToDisk)
                project.Save();

            return project;
        }

        private void FillSprites(Project project, bool writeToDisk)
        {
            project.SpriteList.Sprites.Add(new Sprite { Name = "Object1" }); // TODO: localize objects
            project.SpriteList.Sprites.Add(new Sprite { Name = "Object2" });
            project.SpriteList.Sprites.Add(new Sprite { Name = "Object3" });
        }
    }
}
