using System.IO;
using System.Threading.Tasks;
using System.Threading;
using Catrobat.IDE.Core.CatrobatObjects;

namespace Catrobat.IDE.Core.Services
{
    public interface IProgramImportService
    {
        void SetProgramStream(Stream programStream);

        void SetDownloadHeader(OnlineProgramHeader programHeader);

        Task<ExtractProgramResult> ExtractProgram(CancellationToken taskCancellationToken);

        Task<string> AcceptTempProgram();

        Task CancelImport();

        Task TryImportWithStatusNotifications();
    }


    public enum ExtractProgramStatus { Success, Error }
    public class ExtractProgramResult
    {
        public ExtractProgramStatus Status { get; set; }
    }

}
