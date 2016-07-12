using Catrobat.IDE.Core.Models;
using Catrobat.IDE.Core.Models.Bricks;
using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.Xml.XmlObjects.Bricks.Sounds;

namespace Catrobat.IDE.Core.XmlModelConvertion.Converters.Actions.Bricks
{
    public class SpeakBrickConverter : BrickConverterBase<XmlSpeakBrick, SpeakBrick>
    {
        public SpeakBrickConverter() { }

        public override SpeakBrick Convert1(XmlSpeakBrick o, XmlModelConvertContext c)
        {
            return new SpeakBrick
            {
                Value = o.Text
            };
        }

        public override XmlSpeakBrick Convert1(SpeakBrick m, XmlModelConvertBackContext c)
        {
            return new XmlSpeakBrick
            {
                Text = m.Value
            };
        }
    }
}
