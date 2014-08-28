using Catrobat.IDE.Core.Models;
using Catrobat.IDE.Core.Models.Bricks;
using Catrobat.IDE.Core.Models.Formulas.Tree;
using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.Xml.XmlObjects.Bricks.Properties;
using Catrobat.IDE.Core.Xml.XmlObjects.Formulas;

namespace Catrobat.IDE.Core.XmlModelConvertion.Converters.Actions.Bricks
{
    public class ChangeBrightnessBrickConverter : BrickConverterBase<XmlChangeBrightnessBrick, ChangeBrightnessBrick>
    {
        public ChangeBrightnessBrickConverter(IXmlModelConversionService converter)  
            : base(converter) { }

        public override ChangeBrightnessBrick Convert1(XmlChangeBrightnessBrick o, XmlModelConvertContext c)
        {
            return new ChangeBrightnessBrick
            {
                RelativePercentage = o.ChangeBrightness == null ? null : (FormulaTree)Converter.Convert(o.ChangeBrightness)
            };
        }

        public override XmlChangeBrightnessBrick Convert1(ChangeBrightnessBrick m, XmlModelConvertBackContext c)
        {
            return new XmlChangeBrightnessBrick
            {
                ChangeBrightness = m.RelativePercentage == null ? new XmlFormula() : (XmlFormula)Converter.Convert(m.RelativePercentage)
            };
        }
    }
}
