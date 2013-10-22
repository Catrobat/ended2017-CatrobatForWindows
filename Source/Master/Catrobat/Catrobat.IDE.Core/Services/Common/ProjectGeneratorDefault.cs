using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Catrobat.IDE.Core.CatrobatObjects;
using Catrobat.IDE.Core.CatrobatObjects.Costumes;
using Catrobat.IDE.Core.Resources.Localization;
using Catrobat.IDE.Core.Services.Storage;
using Catrobat.IDE.Core.UI.Converters;
using Catrobat.IDE.Core.UI.PortableUI;
using Catrobat.IDE.Core.Utilities.Helpers;
using SharpCompress.Archive.Zip;
using SharpCompress.Common;
using SharpCompress.Reader;

namespace Catrobat.IDE.Core.Services.Common
{
    public class ProjectGeneratorDefault : IProjectGenerator
    {
        private const string ResourcePathToLookFiles = "Content/Programs/Default/Looks/";

        private const string LookFileNameBackground = "52fb8540d8d751880ab012e26c86e1f5_background.png";
        private const string LookFileNameCat = "cd8435e8bf34b6be6c0fdf700f03e01e_cat.png";
        private const string LookFileNameRain = "28226cf909b0e5deb2d275b73a96fbec_rain.png";
        private const string LookFileNameSun = "5c8b67f427443c1bb5c50a4aeb007949_sun.png";
        private const string LookFileNameWater = "7fe260ebb6c78fa3f0d6afaffb5a7759_water.png";
        private const string LookFileNameCloud1 = "e8b139ca83c443159b464c26d8483bae_cloud1.png";
        private const string LookFileNameCloud2 = "c96b5d3adc1d4b199fc76fc518deae86_cloud2.png";

        public Project GenerateProject(string twoLetterIsoLanguageCode, bool writeToDisk)
        {
            var project = new Project();

            project.ProjectHeader = new ProjectHeader();
            project.SetProgramName(AppResources.Main_DefaultProjectName);
            project.ProjectScreenshot = new PortableImage(); // TODO: add real screenshot

            //XmlParserTempProjectHelper.Project = project;

            if(writeToDisk)
                WriteLooksToDisk(Path.Combine(project.BasePath, Project.ImagesPath));

            FillSprites(project);


            if(writeToDisk)
                project.Save();

            return project;
        }

        private void WriteLooksToDisk(string basePathToLookFiles)
        {
            var lookFiles = new List<string>
            {
                LookFileNameBackground,
                LookFileNameCat,
                LookFileNameRain,
                LookFileNameSun,
                LookFileNameWater,
                LookFileNameCloud1,
                LookFileNameCloud2
            };

            using (var storage = StorageSystem.GetStorage())
            {
                using (var loader = ServiceLocator.ResourceLoaderFactory.CreateResourceLoader())
                {
                    foreach (string lookFile in lookFiles)
                    {
                        var inputStream = loader.OpenResourceStream(ResourceScope.IdePhone, // TODO: change resourceScope to suppot phone and store app
                            Path.Combine(ResourcePathToLookFiles, lookFile));

                        var outputStream = storage.OpenFile(Path.Combine(basePathToLookFiles, lookFile),
                            StorageFileMode.Create, StorageFileAccess.Write);

                        inputStream.CopyTo(outputStream);
                        outputStream.Flush();
                        inputStream.Dispose();
                        outputStream.Dispose();
                    }
                }
            }
        }

        private void FillSprites(Project project)
        {
            var objectBackground = new Sprite {Name = AppResources.DefaultProject_Background};
            var objectCat = new Sprite { Name = AppResources.DefaultProject_Cat };
            var objectRain = new Sprite { Name = AppResources.DefaultProject_Rain };
            var objectSun = new Sprite { Name = AppResources.DefaultProject_Sun };
            var objectWater = new Sprite { Name = AppResources.DefaultProject_Water };
            var objectCloud = new Sprite { Name = AppResources.DefaultProject_Cloud };

            objectBackground.Costumes.Costumes.Add(new Costume
            {
                Name = AppResources.DefaultProject_Background,
                FileName = LookFileNameBackground
            });
            var image = objectBackground.Costumes.Costumes[0].Image;

            objectCat.Costumes.Costumes.Add(new Costume
            {
                Name = AppResources.DefaultProject_Cat,
                FileName = LookFileNameCat
            });

            objectRain.Costumes.Costumes.Add(new Costume
            {
                Name = AppResources.DefaultProject_Rain,
                FileName = LookFileNameRain
            });

            objectSun.Costumes.Costumes.Add(new Costume
            {
                Name = AppResources.DefaultProject_Sun,
                FileName = LookFileNameSun
            });

            objectWater.Costumes.Costumes.Add(new Costume
            {
                Name = AppResources.DefaultProject_Water,
                FileName = LookFileNameWater
            });

            objectCloud.Costumes.Costumes.Add(new Costume
            {
                Name = AppResources.DefaultProject_Cloud + "1",
                FileName = LookFileNameCloud1
            });

            objectCloud.Costumes.Costumes.Add(new Costume
            {
                Name = AppResources.DefaultProject_Cloud + "2",
                FileName = LookFileNameCloud2
            });


            project.SpriteList.Sprites.Add(objectBackground);
            project.SpriteList.Sprites.Add(objectCat);
            project.SpriteList.Sprites.Add(objectRain);
            project.SpriteList.Sprites.Add(objectSun);
            project.SpriteList.Sprites.Add(objectWater);
            project.SpriteList.Sprites.Add(objectCloud);
        }
    }
}
