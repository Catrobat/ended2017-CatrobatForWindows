using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Catrobat.IDE.Core.Models;
using Catrobat.IDE.Core.Xml;
using Catrobat.IDE.Core.Xml.XmlObjects;

namespace Catrobat.IDE.Core.Services
{
    public interface IContextService
    {
        Task<XmlProjectRenamerResult> RenameProgramFromFile(
            string projectCodeFilePath, string newProjectName);

        Task<string> FindUniqueName(string programName);

        Task<Project> LoadProjectByNameStatic(string programName);

        Task<XmlProject> LoadXmlProjectByNameStatic(string programName);

        Task<XmlProject> LoadNewProjectByNameStaticWithoutTryCatch(string programName);

        Task<Project> RestoreDefaultProjectStatic(string programName);

        Task<Project> CreateEmptyProject(string newProgramName);

        Task<Project> CopyProject(string sourceProgramName, string newProgramName);

        Task StoreLocalSettingsStatic(LocalSettings localSettings);

        Task<LocalSettings> RestoreLocalSettingsStatic();
    }
}
