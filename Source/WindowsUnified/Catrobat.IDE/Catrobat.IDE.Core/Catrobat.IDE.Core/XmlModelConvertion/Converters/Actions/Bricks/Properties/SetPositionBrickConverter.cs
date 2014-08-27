using Catrobat.IDE.Core.Models;
using Catrobat.IDE.Core.Models.Bricks;
using Catrobat.IDE.Core.Models.Formulas.Tree;
using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.Xml.XmlObjects.Bricks.Properties;
using Catrobat.IDE.Core.Xml.XmlObjects.Formulas;

namespace Catrobat.IDE.Core.XmlModelConvertion.Converters.Actions.Bricks
{
    public class SetPositionBrickConverter : BrickConverterBase<XmlPlaceAtBrick, SetPositionBrick>
    {
        public SetPositionBrickConverter(IXmlModelConversionService converter)  
            : base(converter) { }

        public override SetPositionBrick Convert1(XmlPlaceAtBrick o, XmlModelConvertContext c)
        {
            return new SetPositionBrick
            {
                ValueX = o.XPosition == null ? null : (FormulaTree)Converter.Convert(o.XPosition),
                ValueY = o.YPosition == null ? null : (FormulaTree)Converter.Convert(o.YPosition)
            };
        }

        public override XmlPlaceAtBrick Convert1(SetPositionBrick m, XmlModelConvertBackContext c)
        {
            return new XmlPlaceAtBrick
            {
                XPosition = m.ValueX == null ? new XmlFormula() : (XmlFormula)Converter.Convert(m.ValueX),
                YPosition = m.ValueY == null ? new XmlFormula() : (XmlFormula)Converter.Convert(m.ValueY)
            };
        }
    }
}
