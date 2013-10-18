using System.IO;
using Catrobat.IDE.Core.CatrobatObjects;
using Catrobat.IDE.Core.Resources.Localization;
using Catrobat.IDE.Core.Services.Storage;
using Catrobat.IDE.Core.UI.PortableUI;
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
            var spriteBackground = new Sprite {Name = AppResources.DefaultProject_Background};
            var spriteMole1 = new Sprite {Name = AppResources.DefaultProject_Mole + " 1"};
            var spriteMole2 = new Sprite { Name = AppResources.DefaultProject_Mole + " 2" };
            var spriteMole3 = new Sprite { Name = AppResources.DefaultProject_Mole + " 3" };
            var spriteMole4 = new Sprite { Name = AppResources.DefaultProject_Mole + " 4" };

            project.SpriteList.Sprites.Add(spriteBackground);
            project.SpriteList.Sprites.Add(spriteMole1);
            project.SpriteList.Sprites.Add(spriteMole2);
            project.SpriteList.Sprites.Add(spriteMole3);
            project.SpriteList.Sprites.Add(spriteMole4);
        }
    }
}
