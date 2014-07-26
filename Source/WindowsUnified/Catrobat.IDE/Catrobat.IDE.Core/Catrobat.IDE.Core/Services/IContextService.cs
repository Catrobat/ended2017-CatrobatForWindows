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
        Task<XmlProjectRenamerResult> RenameProjectFromFile(
            string projectCodeFilePath, string newProjectName);

        Task<Project> LoadProjectByNameStatic(string projectName);

        Task<XmlProject> LoadXmlProjectByNameStatic(string projectName);

        Task<XmlProject> LoadNewProjectByNameStaticWithoutTryCatch(string projectName);

        Task<Project> RestoreDefaultProjectStatic(string projectName);

        Task<Project> CreateEmptyProject(string newProjectName);

        Task<Project> CopyProject(string sourceProjectName, string newProjectName);

        Task StoreLocalSettingsStatic(LocalSettings localSettings);

        Task<LocalSettings> RestoreLocalSettingsStatic();
    }
}
