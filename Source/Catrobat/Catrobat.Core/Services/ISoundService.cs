using Catrobat.IDE.Core.Models;
using System.Collections.Generic;
using System.IO;

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
        List<string> SupportedFileTypes { get; }

        void CreateSoundFromRecorder(Sprite sprite);

        void CreateSoundFromMediaLibrary(Sprite sprite);

        void RecievedFiles(IEnumerable<object> files);
    }
}
