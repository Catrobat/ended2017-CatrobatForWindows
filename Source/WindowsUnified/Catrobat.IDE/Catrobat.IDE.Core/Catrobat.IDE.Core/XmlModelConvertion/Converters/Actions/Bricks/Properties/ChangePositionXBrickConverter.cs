using Catrobat.IDE.Core.Models;
using Catrobat.IDE.Core.Models.Bricks;
using Catrobat.IDE.Core.Models.Formulas.Tree;
using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.Xml.XmlObjects.Bricks.Properties;
using Catrobat.IDE.Core.Xml.XmlObjects.Formulas;

namespace Catrobat.IDE.Core.XmlModelConvertion.Converters.Actions.Bricks
{
    public class ChangePositionXBrickConverter : BrickConverterBase<XmlChangeXByBrick, ChangePositionXBrick>
    {
        public ChangePositionXBrickConverter(IXmlModelConversionService converter)  
            : base(converter) { }

        public override ChangePositionXBrick Convert1(XmlChangeXByBrick o, XmlModelConvertContext c)
        {
            return new ChangePositionXBrick
            {
                RelativeValue = o.XMovement == null ? null : (FormulaTree)Converter.Convert(o.XMovement)
            };
        }

        public override XmlChangeXByBrick Convert1(ChangePositionXBrick m, XmlModelConvertBackContext c)
        {
            return new XmlChangeXByBrick
            {
                XMovement = m.RelativeValue == null ? new XmlFormula() : (XmlFormula)Converter.Convert(m.RelativeValue)
            };
        }
    }
}
