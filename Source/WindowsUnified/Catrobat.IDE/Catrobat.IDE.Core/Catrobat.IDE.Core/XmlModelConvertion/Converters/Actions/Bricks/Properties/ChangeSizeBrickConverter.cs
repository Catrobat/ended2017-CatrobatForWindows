using Catrobat.IDE.Core.Models;
using Catrobat.IDE.Core.Models.Bricks;
using Catrobat.IDE.Core.Models.Formulas.Tree;
using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.Xml.XmlObjects.Bricks.Properties;
using Catrobat.IDE.Core.Xml.XmlObjects.Formulas;

namespace Catrobat.IDE.Core.XmlModelConvertion.Converters.Actions.Bricks
{
    public class ChangeSizeBrickConverter : BrickConverterBase<XmlChangeSizeByNBrick, ChangeSizeBrick>
    {
        public ChangeSizeBrickConverter(IXmlModelConversionService converter)  
            : base(converter) { }

        public override ChangeSizeBrick Convert1(XmlChangeSizeByNBrick o, XmlModelConvertContext c)
        {
            return new ChangeSizeBrick
            {
                RelativePercentage = o.Size == null ? null : (FormulaTree)Converter.Convert(o.Size)
            };
        }

        public override XmlChangeSizeByNBrick Convert1(ChangeSizeBrick m, XmlModelConvertBackContext c)
        {
            return new XmlChangeSizeByNBrick
            {
                Size = m.RelativePercentage == null ? new XmlFormula() : (XmlFormula)Converter.Convert(m.RelativePercentage)
            };
        }
    }
}
