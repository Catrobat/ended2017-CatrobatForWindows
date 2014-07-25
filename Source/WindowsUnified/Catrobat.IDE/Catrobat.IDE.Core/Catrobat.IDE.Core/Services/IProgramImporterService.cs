using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Catrobat.IDE.Core.Annotations;
using Catrobat.IDE.Core.CatrobatObjects;

namespace Catrobat.IDE.Core.Services
{
    public interface IProgramImporterService
    {
        void SetProjectStream(Stream projectStream);

        Task<ProgramImportResult> StartImportProject();

        Task<string> AcceptTempProject();

        Task CancelImport();
    }


    public enum ProgramImportStatus { Valid, Damaged, VersionTooOld, VersionTooNew }

    public class ProgramImportResult
    {
        public ProgramImportStatus Status { get; set; }

        public LocalProjectHeader ProjectHeader { get; set; }
    }

}
