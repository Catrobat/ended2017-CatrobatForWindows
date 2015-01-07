using System;
using System.Threading.Tasks;
using System.Threading;
using System.Collections.Generic;
using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.Utilities.Helpers;
using Catrobat.IDE.Core.Utilities.JSON;
using Catrobat.IDE.Core.CatrobatObjects;
using Catrobat.IDE.Core.Tests.SampleData;
using System.IO;
using Catrobat.IDE.Core.Xml.XmlObjects;
using Catrobat.IDE.Core.Models;
using Catrobat.IDE.Core.Xml;

namespace Catrobat.IDE.Core.Tests.Services.Common
{
    public class ContextServiceTest : IContextService
    {
        public async Task<string> ConvertToValidFileName(string fileName)
        {
            return fileName;
        }

        public async Task<string> FindUniqueName(string requestedName, List<string> nameList)
        {
            return requestedName;
        }

        public async Task<string> FindUniqueProgramName(string programName)
        {
            throw new NotImplementedException();
        }

        public async Task<XmlProgramRenamerResult> RenameProgram(
            string programCodeFilePath, string newProgramName)
        {
            throw new NotImplementedException();
        }

        public async Task<Program> LoadProgramByName(string programName)
        {
            throw new NotImplementedException();
        }

        public async Task<XmlProgram> LoadXmlProgramByName(string programName)
        {
            throw new NotImplementedException();
        }

        private static async Task<XmlProgram> LoadNewProgramByNameWithoutTryCatch(
            string programName)
        {
            throw new NotImplementedException();
        }

        public async Task<Program> RestoreDefaultProgram(string programName)
        {
            throw new NotImplementedException();
        }

        public async Task<Program> CreateEmptyProgram(string newProgramName)
        {
            throw new NotImplementedException();
        }

        public async Task<Program> CopyProgram(string sourceProgramName,
            string newProgramName)
        {
            throw new NotImplementedException();
        }

        public async Task StoreLocalSettings(LocalSettings localSettings)
        {
            throw new NotImplementedException();
        }

        public async Task<LocalSettings> RestoreLocalSettings()
        {
            throw new NotImplementedException();
        }

        public async Task CreateThumbnailsForNewProgram(string programName)
        {
            throw new NotImplementedException();
        }

        public void UpdateProgramHeader(XmlProgram program)
        {
            throw new NotImplementedException();
        }


        public Task<string> CopyProgramPart1(string sourceProgramName)
        {
            throw new NotImplementedException();
        }

        public Task<Program> CopyProgramPart2(string sourceProgramName, string newProgramName)
        {
            throw new NotImplementedException();
        }
    }

}
