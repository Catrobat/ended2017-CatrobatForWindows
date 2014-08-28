using Catrobat.IDE.Core.Models;
using Catrobat.IDE.Core.Models.Bricks;
using Catrobat.IDE.Core.Models.Formulas.Tree;
using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.Xml.XmlObjects.Bricks.Properties;
using Catrobat.IDE.Core.Xml.XmlObjects.Formulas;

namespace Catrobat.IDE.Core.XmlModelConvertion.Converters.Actions.Bricks
{
    public class SetPositionYBrickConverter : BrickConverterBase<XmlSetYBrick, SetPositionYBrick>
    {
        public SetPositionYBrickConverter(IXmlModelConversionService converter)  
            : base(converter) { }

        public override SetPositionYBrick Convert1(XmlSetYBrick o, XmlModelConvertContext c)
        {
            return new SetPositionYBrick
            {
                Value = o.YPosition == null ? null : (FormulaTree)Converter.Convert(o.YPosition)
            };
        }

        public override XmlSetYBrick Convert1(SetPositionYBrick m, XmlModelConvertBackContext c)
        {
            return new XmlSetYBrick
            {
                YPosition = m.Value == null ? new XmlFormula() : (XmlFormula)Converter.Convert(m.Value)
            };
        }
    }
}
