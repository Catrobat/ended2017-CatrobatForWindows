using Catrobat.IDE.Core.Models;
using Catrobat.IDE.Core.Models.Bricks;
using Catrobat.IDE.Core.Models.Formulas.Tree;
using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.Xml.XmlObjects.Bricks.Properties;
using Catrobat.IDE.Core.Xml.XmlObjects.Formulas;

namespace Catrobat.IDE.Core.XmlModelConvertion.Converters.Actions.Bricks
{
    public class SetBrightnessBrickConverter : BrickConverterBase<XmlSetBrightnessBrick, SetBrightnessBrick>
    {
        public SetBrightnessBrickConverter(IXmlModelConversionService converter)  
            : base(converter) { }

        public override SetBrightnessBrick Convert1(XmlSetBrightnessBrick o, XmlModelConvertContext c)
        {
            return new SetBrightnessBrick
            {
                Percentage = o.Brightness == null ? null : (FormulaTree)Converter.Convert(o.Brightness)
            };
        }

        public override XmlSetBrightnessBrick Convert1(SetBrightnessBrick m, XmlModelConvertBackContext c)
        {
            return new XmlSetBrightnessBrick
            {
                Brightness = m.Percentage == null ? new XmlFormula() : (XmlFormula)Converter.Convert(m.Percentage)
            };
        }
    }
}
