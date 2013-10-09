using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Catrobat.Core.Annotations;
using Catrobat.Core.CatrobatObjects;
using Catrobat.Core.Utilities.Storage;

namespace Catrobat.Core.Services
{
    public interface IProjectImporterService
    {
        Task<ProjectDummyHeader> ImportProjects(Stream projectStream);

        Task<string> AcceptTempProject();

        Task CancelImport();
    }
}
