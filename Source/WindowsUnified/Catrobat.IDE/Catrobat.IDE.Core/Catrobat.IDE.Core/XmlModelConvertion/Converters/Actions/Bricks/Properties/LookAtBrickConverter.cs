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
        public LookAtBrickConverter(IXmlModelConversionService converter)  
            : base(converter) { }

        public override LookAtBrick Convert1(XmlPointToBrick o, XmlModelConvertContext c)
        {
            return new LookAtBrick
            {
                Target = o.PointedSprite == null ? null : (Sprite)Converter.Convert(o.PointedSprite, pointerOnly: true)
            };
        }

        public override XmlPointToBrick Convert1(LookAtBrick m, XmlModelConvertBackContext c)
        {
            return new XmlPointToBrick
            {
                PointedSprite = m.Target == null ? null : (XmlSprite)Converter.Convert(m.Target, pointerOnly: true)
            };
        }
    }
}
