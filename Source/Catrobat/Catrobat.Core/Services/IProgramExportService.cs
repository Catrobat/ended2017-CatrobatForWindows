using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catrobat.IDE.Core.Services
{
    public interface IProgramExportService
    {
        Task<Stream> CreateProgramPackageForExport(string programName);

        Task ExportToPocketCodeOrgWithNotifications(string programName, string currentUserName, string currentToken);

        Task CancelExport();

        Task CleanUpExport();
    }
}
