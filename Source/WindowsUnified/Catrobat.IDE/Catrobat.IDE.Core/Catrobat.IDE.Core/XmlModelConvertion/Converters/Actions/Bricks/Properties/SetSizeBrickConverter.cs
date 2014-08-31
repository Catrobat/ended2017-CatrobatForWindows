using Catrobat.IDE.Core.Models;
using Catrobat.IDE.Core.Models.Bricks;
using Catrobat.IDE.Core.Models.Formulas.Tree;
using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.Xml.XmlObjects.Bricks.Properties;
using Catrobat.IDE.Core.Xml.XmlObjects.Formulas;
using Catrobat.IDE.Core.XmlModelConvertion.Converters.Formulas;

namespace Catrobat.IDE.Core.XmlModelConvertion.Converters.Actions.Bricks
{
    public class SetSizeBrickConverter : BrickConverterBase<XmlSetSizeToBrick, SetSizeBrick>
    {
        public override SetSizeBrick Convert1(XmlSetSizeToBrick o, XmlModelConvertContext c)
        {
            var formulaConverter = new FormulaConverter();

            return new SetSizeBrick
            {
                Percentage = o.Size == null ? null : formulaConverter.Convert(o.Size, c)
            };
        }

        public override XmlSetSizeToBrick Convert1(SetSizeBrick m, XmlModelConvertBackContext c)
        {
            var formulaConverter = new FormulaConverter();

            return new XmlSetSizeToBrick
            {
                Size = m.Percentage == null ? new XmlFormula() : formulaConverter.Convert(m.Percentage, c)
            };
        }
    }
}
