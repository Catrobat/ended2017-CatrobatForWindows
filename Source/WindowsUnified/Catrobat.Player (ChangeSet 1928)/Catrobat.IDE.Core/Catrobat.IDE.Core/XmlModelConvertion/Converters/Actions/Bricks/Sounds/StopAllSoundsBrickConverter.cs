using Catrobat.IDE.Core.Models;
using Catrobat.IDE.Core.Models.Bricks;
using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.Xml.XmlObjects.Bricks.Sounds;

namespace Catrobat.IDE.Core.XmlModelConvertion.Converters.Actions.Bricks
{
    public class StopAllSoundsBrickConverter : BrickConverterBase<XmlStopAllSoundsBrick, StopSoundsBrick>
    {
        public StopAllSoundsBrickConverter() { }

        public override StopSoundsBrick Convert1(XmlStopAllSoundsBrick o, XmlModelConvertContext c)
        {
            return new StopSoundsBrick();
        }

        public override XmlStopAllSoundsBrick Convert1(StopSoundsBrick m, XmlModelConvertBackContext c)
        {
            return new XmlStopAllSoundsBrick();
        }
    }
}
