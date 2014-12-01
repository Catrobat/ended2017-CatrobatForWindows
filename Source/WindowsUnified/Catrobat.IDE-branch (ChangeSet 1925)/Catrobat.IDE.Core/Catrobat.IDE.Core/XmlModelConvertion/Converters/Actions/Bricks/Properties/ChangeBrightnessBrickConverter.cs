using Catrobat.IDE.Core.Models;
using Catrobat.IDE.Core.Models.Bricks;
using Catrobat.IDE.Core.Models.Formulas.Tree;
using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.Xml.XmlObjects.Bricks.Properties;
using Catrobat.IDE.Core.Xml.XmlObjects.Formulas;
using Catrobat.IDE.Core.XmlModelConvertion.Converters.Formulas;

namespace Catrobat.IDE.Core.XmlModelConvertion.Converters.Actions.Bricks
{
    public class ChangeBrightnessBrickConverter : BrickConverterBase<XmlChangeBrightnessBrick, ChangeBrightnessBrick>
    {
        public ChangeBrightnessBrickConverter() { }

        public override ChangeBrightnessBrick Convert1(XmlChangeBrightnessBrick o, XmlModelConvertContext c)
        {
            var formulaConverter = new FormulaConverter();

            return new ChangeBrightnessBrick
            {
                RelativePercentage = o.ChangeBrightness == null ? null : formulaConverter.Convert(o.ChangeBrightness, c)
            };
        }

        public override XmlChangeBrightnessBrick Convert1(ChangeBrightnessBrick m, XmlModelConvertBackContext c)
        {
            var formulaConverter = new FormulaConverter();

            return new XmlChangeBrightnessBrick
            {
                ChangeBrightness = m.RelativePercentage == null ? 
                new XmlFormula() : formulaConverter.Convert(m.RelativePercentage, c)
            };
        }
    }
}
