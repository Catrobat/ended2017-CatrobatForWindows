using System.IO;
using System.Threading.Tasks;
using Catrobat.IDE.Core.CatrobatObjects;

namespace Catrobat.IDE.Core.Services
{
    public interface IProgramImportService
    {
        void SetProjectStream(Stream projectStream);

        void SetDownloadHeader(OnlineProgramHeader projectHeader);

        Task<ExtractProgramResult> ExtractProgram();

        Task<CheckProgramImportResult> CheckProgram();

        Task<string> AcceptTempProject();

        Task CancelImport();

        Task TryImportWithStatusNotifications();
    }


    public enum ProgramImportStatus { Valid, Damaged, VersionTooOld, VersionTooNew }
    public class CheckProgramImportResult
    {
        public ProgramImportStatus Status { get; set; }

        public LocalProjectHeader ProjectHeader { get; set; }
    }

    public enum ExtractProgramStatus { Success, Error }
    public class ExtractProgramResult
    {
        public ExtractProgramStatus Status { get; set; }
    }

}
