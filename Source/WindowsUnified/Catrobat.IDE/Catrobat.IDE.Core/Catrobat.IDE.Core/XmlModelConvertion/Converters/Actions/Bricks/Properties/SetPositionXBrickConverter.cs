using Catrobat.IDE.Core.Models;
using Catrobat.IDE.Core.Models.Bricks;
using Catrobat.IDE.Core.Models.Formulas.Tree;
using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.Xml.XmlObjects.Bricks.Properties;
using Catrobat.IDE.Core.Xml.XmlObjects.Formulas;

namespace Catrobat.IDE.Core.XmlModelConvertion.Converters.Actions.Bricks
{
    public class SetPositionXBrickConverter : BrickConverterBase<XmlSetXBrick, SetPositionXBrick>
    {
        public SetPositionXBrickConverter(IXmlModelConversionService converter)  
            : base(converter) { }

        public override SetPositionXBrick Convert1(XmlSetXBrick o, XmlModelConvertContext c)
        {
            return new SetPositionXBrick
            {
                Value = o.XPosition == null ? null : (FormulaTree)Converter.Convert(o.XPosition)
            };
        }

        public override XmlSetXBrick Convert1(SetPositionXBrick m, XmlModelConvertBackContext c)
        {
            return new XmlSetXBrick
            {
                XPosition = m.Value == null ? new XmlFormula() : (XmlFormula)Converter.Convert(m.Value)
            };
        }
    }
}
