using System;
using System.IO;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Collections.Generic;
using Catrobat.IDE.Core.Models;
using Catrobat.IDE.Core.CatrobatObjects;
using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.Services.Common;
using Catrobat.IDE.Core.Services.Storage;
using Catrobat.IDE.Core.Tests.Misc.Storage;
using Catrobat.IDE.Core.Xml.XmlObjects;

namespace Catrobat.IDE.Core.Tests.SampleData
{
    public static class SampleLoader
    {
        private static readonly string path = BasePathHelper.GetSampleProjectsPath();

        public static XDocument LoadSampleXDocument(string sampleName)
        {
            string xml = null;
            using (var resourceLoader = ServiceLocator.ResourceLoaderFactory.CreateResourceLoader())
            {
                var stream = resourceLoader.OpenResourceStream(ResourceScope.TestCommon, path + sampleName + ".xml");
                var reader = new StreamReader(stream);

                xml = reader.ReadToEnd();
                reader.Close();
                reader.Dispose();
                stream.Dispose();
            }
            return XDocument.Load(new StringReader(xml));
        }

        public static async Task<Program> LoadSampleProject(string sampleName, string sampleProjectName)
        {
            var zipService = new ZipService();

            using (var resourceLoader = ServiceLocator.ResourceLoaderFactory.CreateResourceLoader())
            {
                var stream = resourceLoader.OpenResourceStream(ResourceScope.TestCommon, path + sampleName);
                var projectPath = Path.Combine(StorageConstants.ProgramsPath, sampleProjectName);
                using (var storage = StorageSystem.GetStorage())
                {
                    await storage.DeleteDirectoryAsync(projectPath);
                }

                await zipService.UnzipCatrobatPackageIntoIsolatedStorage(stream, projectPath);
                stream.Dispose();
            }
            return await ServiceLocator.ContextService.LoadProgramByName(sampleProjectName);
        }

        [Obsolete("Use ProgramGenerator instead. ", false)]
        public static async Task<XmlProgram> LoadSampleXmlProject(string sampleName, string sampleProjectName)
        {
            var zipService = new ZipService();

            using (var resourceLoader = ServiceLocator.ResourceLoaderFactory.CreateResourceLoader())
            {
                var stream = resourceLoader.OpenResourceStream(ResourceScope.TestCommon, path + sampleName);
                await zipService.UnzipCatrobatPackageIntoIsolatedStorage(stream, StorageConstants.ProgramsPath + "/" + sampleProjectName);
                stream.Dispose();
            }
            return await ServiceLocator.ContextService.LoadXmlProgramByName(sampleProjectName);
        }

        public static OnlineProgramHeader GetSampleOnlineProjectHeader()
        {
            OnlineProgramHeader onlineProjectHeader = new OnlineProgramHeader
            {
                ProjectId = "1769",
                ProjectName = "Radio Fun City",
                ProjectNameShort = "Radio Fun City",
                ScreenshotBig = "resources/thumbnails/1769_large.png",
                ScreenshotSmall = "resources/thumbnails/1769_small.png",
                Author = "Skater5",
                Description = "This is my radio channel . Just downlad and listen",
                Uploaded = "1406382848.5354",
                UploadedString = "1 Stunde",
                Version = "0.9.9",
                Views = "2",
                Downloads = "5",
                ProjectUrl = "details/1769",
                DownloadUrl = "download/1769.catrobat"
            };
            return onlineProjectHeader;
        }

        public static List<OnlineProgramHeader> GetSampleOnlineProjectHeaderList(int count)
        {
            List<OnlineProgramHeader> onlineProjectHeaders = new List<OnlineProgramHeader>();
            for (int i = 1; i <= count; i++)
            {
                OnlineProgramHeader onlineProjectHeader = new OnlineProgramHeader
                {
                    ProjectId = i.ToString(),
                    ProjectName = "Radio Fun City Nr." + i.ToString(),
                    ProjectNameShort = "Radio Fun City",
                    ScreenshotBig = "resources/thumbnails/1769_large.png",
                    ScreenshotSmall = "resources/thumbnails/1769_small.png",
                    Author = "Skater5",
                    Description = "This is my radio channel . Just downlad and listen",
                    Uploaded = "1406382848.5354",
                    UploadedString = "1 Stunde",
                    Version = "0.9.9",
                    Views = "2",
                    Downloads = "5",
                    ProjectUrl = "details/1769",
                    DownloadUrl = "download/1769.catrobat"
                };
                onlineProjectHeaders.Add(onlineProjectHeader);
            }
            return onlineProjectHeaders;
        }
    }
}
