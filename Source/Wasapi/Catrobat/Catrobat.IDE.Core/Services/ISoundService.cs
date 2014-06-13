using System.IO;
using System.Threading.Tasks;
using Catrobat.IDE.Core.Models;

namespace Catrobat.IDE.Core.Services
{
    public enum SoundServiceStatus
    {
        Success,
        Cancelled,
        Error
    }

    public class SoundServiceResult
    {
        public SoundServiceStatus Status { get; set; }
        public Stream Result { get; set; }
    }

    public interface ISoundService
    {
        Task<SoundServiceResult> CreateSoundFromRecorder(Sprite sprite);

        Task<SoundServiceResult> CreateSoundFromMediaLibrary(Sprite sprite);

        void Finished(SoundServiceResult result);
    }
}
