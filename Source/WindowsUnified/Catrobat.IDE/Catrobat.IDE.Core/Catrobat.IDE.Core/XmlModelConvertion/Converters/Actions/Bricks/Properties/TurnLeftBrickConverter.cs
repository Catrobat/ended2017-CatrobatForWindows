using Catrobat.IDE.Core.Models;
using Catrobat.IDE.Core.Models.Bricks;
using Catrobat.IDE.Core.Models.Formulas.Tree;
using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.Xml.XmlObjects.Bricks.Properties;
using Catrobat.IDE.Core.Xml.XmlObjects.Formulas;

namespace Catrobat.IDE.Core.XmlModelConvertion.Converters.Actions.Bricks
{
    public class TurnLeftBrickConverter : BrickConverterBase<XmlTurnLeftBrick, TurnLeftBrick>
    {
        public TurnLeftBrickConverter(IXmlModelConversionService converter)  
            : base(converter) { }

        public override TurnLeftBrick Convert1(XmlTurnLeftBrick o, XmlModelConvertContext c)
        {
            return new TurnLeftBrick
            {
                RelativeValue = o.Degrees == null ? null : (FormulaTree)Converter.Convert(o.Degrees)
            };
        }

        public override XmlTurnLeftBrick Convert1(TurnLeftBrick m, XmlModelConvertBackContext c)
        {
            return new XmlTurnLeftBrick
            {
                Degrees = m.RelativeValue == null ? new XmlFormula() : (XmlFormula)Converter.Convert(m.RelativeValue)
            };
        }
    }
}
