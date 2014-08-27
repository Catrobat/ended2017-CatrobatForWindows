using Catrobat.IDE.Core.Models;
using Catrobat.IDE.Core.Models.Bricks;
using Catrobat.IDE.Core.Models.Formulas.Tree;
using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.Xml.XmlObjects.Bricks.Properties;
using Catrobat.IDE.Core.Xml.XmlObjects.Formulas;

namespace Catrobat.IDE.Core.XmlModelConvertion.Converters.Actions.Bricks
{
    public class SetSizeBrickConverter : BrickConverterBase<XmlSetSizeToBrick, SetSizeBrick>
    {
        public SetSizeBrickConverter(IXmlModelConversionService converter)  
            : base(converter) { }

        public override SetSizeBrick Convert1(XmlSetSizeToBrick o, XmlModelConvertContext c)
        {
            return new SetSizeBrick
            {
                Percentage = o.Size == null ? null : (FormulaTree)Converter.Convert(o.Size)
            };
        }

        public override XmlSetSizeToBrick Convert1(SetSizeBrick m, XmlModelConvertBackContext c)
        {
            return new XmlSetSizeToBrick
            {
                Size = m.Percentage == null ? new XmlFormula() : (XmlFormula)Converter.Convert(m.Percentage)
            };
        }
    }
}
