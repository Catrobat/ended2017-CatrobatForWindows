using System;
using System.IO;
using System.Threading.Tasks;
using Catrobat.IDE.Core.Services.Storage;
using Catrobat.IDE.Core.Xml.VersionConverter;
using Catrobat.IDE.Core.Xml.XmlObjects;
using Newtonsoft.Json.Converters;

namespace Catrobat.IDE.Core.Services.Common
{
    public class ProgramExportService : IProgramExportService
    {
        public async Task<Stream> CreateProgramPackageForExport(string programName)
        {
            var streamResult = new MemoryStream();

            var tempPath = Path.Combine(StorageConstants.TempProgramExportPath, programName);
            var programFolderPath = Path.Combine(StorageConstants.ProgramsPath, programName);
            var projectCodePath = Path.Combine(tempPath, StorageConstants.ProgramCodePath);

            using (var storage = StorageSystem.GetStorage())
            {
                if (await storage.DirectoryExistsAsync(tempPath))
                    await storage.DeleteDirectoryAsync(tempPath);

                await storage.CopyDirectoryAsync(programFolderPath, tempPath);

                var converterResult = await CatrobatVersionConverter.
                ConvertToXmlVersion(projectCodePath, Constants.TargetOutputVersion);

                if (converterResult.Error != CatrobatVersionConverter.VersionConverterStatus.NoError)
                    return null;
            }

            var zipService = new ZipService();
            //await zipService.ZipCatrobatPackage(streamResult, programFolderPath);
            await zipService.ZipCatrobatPackage(streamResult, tempPath);
            streamResult.Seek(0, SeekOrigin.Begin);
            return streamResult;
        }
    }
}
