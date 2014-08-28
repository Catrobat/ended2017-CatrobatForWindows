using Catrobat.IDE.Core.Models;
using Catrobat.IDE.Core.Models.Bricks;
using Catrobat.IDE.Core.Models.Formulas.Tree;
using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.Xml.XmlObjects.Bricks.ControlFlow;
using Catrobat.IDE.Core.Xml.XmlObjects.Formulas;

namespace Catrobat.IDE.Core.XmlModelConvertion.Converters.Actions.Bricks
{
    public class WaitBrickConverter : BrickConverterBase<XmlWaitBrick, DelayBrick>
    {
        public WaitBrickConverter(IXmlModelConversionService converter)  
            : base(converter) { }

        public override DelayBrick Convert1(XmlWaitBrick o, XmlModelConvertContext c)
        {
            return new DelayBrick
            {
                Duration = o.TimeToWaitInSeconds == null ? null : (FormulaTree)Converter.Convert(o.TimeToWaitInSeconds)
            };
        }

        public override XmlWaitBrick Convert1(DelayBrick m, XmlModelConvertBackContext c)
        {
            return new XmlWaitBrick
            {
                TimeToWaitInSeconds = m.Duration == null ? new XmlFormula() : (XmlFormula)Converter.Convert(m.Duration)
            };
        }
    }
}
