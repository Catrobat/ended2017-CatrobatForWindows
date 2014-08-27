using Catrobat.IDE.Core.Models;
using Catrobat.IDE.Core.Models.Bricks;
using Catrobat.IDE.Core.Models.Formulas.Tree;
using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.Xml.XmlObjects.Bricks.Properties;
using Catrobat.IDE.Core.Xml.XmlObjects.Formulas;

namespace Catrobat.IDE.Core.XmlModelConvertion.Converters.Actions.Bricks
{
    public class MoveBrickConverter : BrickConverterBase<XmlMoveNStepsBrick, MoveBrick>
    {
        public MoveBrickConverter(IXmlModelConversionService converter)  
            : base(converter) { }

        public override MoveBrick Convert1(XmlMoveNStepsBrick o, XmlModelConvertContext c)
        {
            return new MoveBrick
            {
                Steps = o.Steps == null ? null : (FormulaTree)Converter.Convert(o.Steps)
            };
        }

        public override XmlMoveNStepsBrick Convert1(MoveBrick m, XmlModelConvertBackContext c)
        {
            return new XmlMoveNStepsBrick
            {
                Steps = m.Steps == null ? new XmlFormula() : (XmlFormula)Converter.Convert(m.Steps)
            };
        }
    }
}
