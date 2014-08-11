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

        Task<CheckProgramResult> CheckProgram();

        Task<string> AcceptTempProject();

        Task CancelImport();

        Task TryImportWithStatusNotifications();
    }


    public enum ExtractProgramStatus { Success, Error }
    public class ExtractProgramResult
    {
        public ExtractProgramStatus Status { get; set; }
    }

}
