using Catrobat.IDE.Core.Models;
using Catrobat.IDE.Core.Models.Bricks;
using Catrobat.IDE.Core.Models.Formulas.Tree;
using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.Xml.XmlObjects.Bricks.Properties;
using Catrobat.IDE.Core.Xml.XmlObjects.Formulas;

namespace Catrobat.IDE.Core.XmlModelConvertion.Converters.Actions.Bricks
{
    public class ChangePositionYBrickConverter : BrickConverterBase<XmlChangeYByBrick, ChangePositionYBrick>
    {
        public ChangePositionYBrickConverter(IXmlModelConversionService converter)  
            : base(converter) { }

        public override ChangePositionYBrick Convert1(XmlChangeYByBrick o, XmlModelConvertContext c)
        {
            return new ChangePositionYBrick
            {
                RelativeValue = o.YMovement == null ? null : (FormulaTree)Converter.Convert(o.YMovement)
            };
        }

        public override XmlChangeYByBrick Convert1(ChangePositionYBrick m, XmlModelConvertBackContext c)
        {
            return new XmlChangeYByBrick
            {
                YMovement = m.RelativeValue == null ? new XmlFormula() : (XmlFormula)Converter.Convert(m.RelativeValue)
            };
        }
    }
}
