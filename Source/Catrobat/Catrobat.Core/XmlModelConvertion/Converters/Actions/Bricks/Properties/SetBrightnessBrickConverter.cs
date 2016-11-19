using Catrobat.IDE.Core.Models;
using Catrobat.IDE.Core.Models.Bricks;
using Catrobat.IDE.Core.Models.Formulas.Tree;
using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.Xml.XmlObjects.Bricks.Properties;
using Catrobat.IDE.Core.Xml.XmlObjects.Formulas;
using Catrobat.IDE.Core.XmlModelConvertion.Converters.Formulas;

namespace Catrobat.IDE.Core.XmlModelConvertion.Converters.Actions.Bricks
{
    public class SetBrightnessBrickConverter : BrickConverterBase<XmlSetBrightnessBrick, SetBrightnessBrick>
    {
        public override SetBrightnessBrick Convert1(XmlSetBrightnessBrick o, XmlModelConvertContext c)
        {
            var formulaConverter = new FormulaConverter();

            return new SetBrightnessBrick
            {
                Percentage = o.Brightness == null ? null : formulaConverter.Convert(o.Brightness, c)
            };
        }

        public override XmlSetBrightnessBrick Convert1(SetBrightnessBrick m, XmlModelConvertBackContext c)
        {
            var formulaConverter = new FormulaConverter();

            return new XmlSetBrightnessBrick
            {
                Brightness = m.Percentage == null ? new XmlFormula() : formulaConverter.Convert(m.Percentage, c)
            };
        }
    }
}
