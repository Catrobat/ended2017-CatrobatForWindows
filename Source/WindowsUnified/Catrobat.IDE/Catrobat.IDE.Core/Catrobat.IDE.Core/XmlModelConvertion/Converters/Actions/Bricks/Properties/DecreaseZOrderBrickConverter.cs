using Catrobat.IDE.Core.Models;
using Catrobat.IDE.Core.Models.Bricks;
using Catrobat.IDE.Core.Models.Formulas.Tree;
using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.Xml.XmlObjects.Bricks.Properties;
using Catrobat.IDE.Core.Xml.XmlObjects.Formulas;

namespace Catrobat.IDE.Core.XmlModelConvertion.Converters.Actions.Bricks
{
    public class DecreaseZOrderBrickConverter : BrickConverterBase<XmlGoNStepsBackBrick, DecreaseZOrderBrick>
    {
        public DecreaseZOrderBrickConverter(IXmlModelConversionService converter)  
            : base(converter) { }

        public override DecreaseZOrderBrick Convert1(XmlGoNStepsBackBrick o, XmlModelConvertContext c)
        {
            return new DecreaseZOrderBrick
            {
                RelativeValue = o.Steps == null ? null : (FormulaTree)Converter.Convert(o.Steps)
            };
        }

        public override XmlGoNStepsBackBrick Convert1(DecreaseZOrderBrick m, XmlModelConvertBackContext c)
        {
            return new XmlGoNStepsBackBrick
            {
                Steps = m.RelativeValue == null ? new XmlFormula() : (XmlFormula)Converter.Convert(m.RelativeValue)
            };
        }
    }
}
