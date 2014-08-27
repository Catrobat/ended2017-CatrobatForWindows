using Catrobat.IDE.Core.Models;
using Catrobat.IDE.Core.Models.Bricks;
using Catrobat.IDE.Core.Models.Formulas.Tree;
using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.Xml.XmlObjects.Bricks.Properties;
using Catrobat.IDE.Core.Xml.XmlObjects.Formulas;

namespace Catrobat.IDE.Core.XmlModelConvertion.Converters.Actions.Bricks
{
    public class SetRotationBrickConverter : BrickConverterBase<XmlPointInDirectionBrick, SetRotationBrick>
    {
        public SetRotationBrickConverter(IXmlModelConversionService converter)  
            : base(converter) { }

        public override SetRotationBrick Convert1(XmlPointInDirectionBrick o, XmlModelConvertContext c)
        {
            return new SetRotationBrick
            {
                Value = o.Degrees == null ? null : (FormulaTree)Converter.Convert(o.Degrees)
            };
        }

        public override XmlPointInDirectionBrick Convert1(SetRotationBrick m, XmlModelConvertBackContext c)
        {
            return new XmlPointInDirectionBrick
            {
                Degrees = m.Value == null ? new XmlFormula() : (XmlFormula)Converter.Convert(m.Value)
            };
        }
    }
}
