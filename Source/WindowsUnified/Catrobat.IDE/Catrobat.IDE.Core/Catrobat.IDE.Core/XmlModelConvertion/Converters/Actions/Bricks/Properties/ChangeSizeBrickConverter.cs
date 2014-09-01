using Catrobat.IDE.Core.Models;
using Catrobat.IDE.Core.Models.Bricks;
using Catrobat.IDE.Core.Models.Formulas.Tree;
using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.Xml.XmlObjects.Bricks.Properties;
using Catrobat.IDE.Core.Xml.XmlObjects.Formulas;
using Catrobat.IDE.Core.XmlModelConvertion.Converters.Formulas;

namespace Catrobat.IDE.Core.XmlModelConvertion.Converters.Actions.Bricks
{
    public class ChangeSizeBrickConverter : BrickConverterBase<XmlChangeSizeByNBrick, ChangeSizeBrick>
    {
        public ChangeSizeBrickConverter() { }

        public override ChangeSizeBrick Convert1(XmlChangeSizeByNBrick o, XmlModelConvertContext c)
        {
            var formulaConverter = new FormulaConverter();

            return new ChangeSizeBrick
            {
                RelativePercentage = o.Size == null ? null : formulaConverter.Convert(o.Size, c)
            };
        }

        public override XmlChangeSizeByNBrick Convert1(ChangeSizeBrick m, XmlModelConvertBackContext c)
        {
            var formulaConverter = new FormulaConverter();

            return new XmlChangeSizeByNBrick
            {
                Size = m.RelativePercentage == null ? new XmlFormula() : formulaConverter.Convert(m.RelativePercentage, c)
            };
        }
    }
}
