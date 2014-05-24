using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Catrobat.IDE.Core.Annotations;
using Catrobat.IDE.Core.CatrobatObjects;
using Catrobat.IDE.Core.UI.PortableUI;
using Catrobat.IDE.Core.Xml;
using Catrobat.IDE.Core.Xml.XmlObjects;

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
        Task<SoundServiceResult> CreateSoundFromRecorder(XmlSprite sprite);

        Task<SoundServiceResult> CreateSoundFromMediaLibrary(XmlSprite sprite);

        void Finished(SoundServiceResult result);
    }
}
