using Catrobat.IDE.Core.Models;
using Catrobat.IDE.Core.Models.Bricks;
using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.Xml.XmlObjects;
using Catrobat.IDE.Core.Xml.XmlObjects.Bricks.Sounds;

namespace Catrobat.IDE.Core.XmlModelConvertion.Converters.Actions.Bricks
{
    public class PlaySoundBrickConverter : BrickConverterBase<XmlPlaySoundBrick, PlaySoundBrick>
    {
        public override PlaySoundBrick Convert1(XmlPlaySoundBrick o, XmlModelConvertContext c)
        {
            var soundConverter = new SoundConverter();
   
            return new PlaySoundBrick
            {
                Value = soundConverter.Convert(o.Sound, c)
            };
        }

        public override XmlPlaySoundBrick Convert1(PlaySoundBrick m, XmlModelConvertBackContext c)
        {
            var soundConverter = new SoundConverter();

            return new XmlPlaySoundBrick
            {
                Sound = soundConverter.Convert(m.Value, c)
            };
        }
    }
}
