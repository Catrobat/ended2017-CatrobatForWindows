using Catrobat.IDE.Core.Models;
using Catrobat.IDE.Core.Models.Bricks;
using Catrobat.IDE.Core.Models.Formulas.Tree;
using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.Xml.XmlObjects.Bricks.Properties;
using Catrobat.IDE.Core.Xml.XmlObjects.Formulas;

namespace Catrobat.IDE.Core.XmlModelConvertion.Converters.Actions.Bricks
{
    public class TurnRightBrickConverter : BrickConverterBase<XmlTurnRightBrick, TurnRightBrick>
    {
        public TurnRightBrickConverter(IXmlModelConversionService converter)  
            : base(converter) { }

        public override TurnRightBrick Convert1(XmlTurnRightBrick o, XmlModelConvertContext c)
        {
            return new TurnRightBrick
            {
                RelativeValue = o.Degrees == null ? null : (FormulaTree)Converter.Convert(o.Degrees)
            };
        }

        public override XmlTurnRightBrick Convert1(TurnRightBrick m, XmlModelConvertBackContext c)
        {
            return new XmlTurnRightBrick
            {
                Degrees = m.RelativeValue == null ? new XmlFormula() : (XmlFormula)Converter.Convert(m.RelativeValue)
            };
        }
    }
}
