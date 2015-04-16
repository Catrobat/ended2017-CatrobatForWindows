using Catrobat.IDE.Core.Models;
using Catrobat.IDE.Core.Models.Bricks;
using Catrobat.IDE.Core.Models.Formulas.Tree;
using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.Xml.XmlObjects;
using Catrobat.IDE.Core.Xml.XmlObjects.Bricks.Properties;
using Catrobat.IDE.Core.Xml.XmlObjects.Formulas;

namespace Catrobat.IDE.Core.XmlModelConvertion.Converters.Actions.Bricks
{
    public class LookAtBrickConverter : BrickConverterBase<XmlPointToBrick, LookAtBrick>
    {
        public LookAtBrickConverter() { }

        public override LookAtBrick Convert1(XmlPointToBrick o, XmlModelConvertContext c)
        {
            var spriteConverter = new SpriteConverter();

            return new LookAtBrick
            {
                Target = o.PointedSprite == null ? null : (Sprite)spriteConverter.Convert(o.PointedSprite, c, pointerOnly: true)
            };
        }

        public override XmlPointToBrick Convert1(LookAtBrick m, XmlModelConvertBackContext c)
        {
            var spriteConverter = new SpriteConverter();

            return new XmlPointToBrick
            {
                PointedSprite = m.Target == null ? null : (XmlSprite)spriteConverter.Convert(m.Target, c, pointerOnly: true)
            };
        }
    }
}
