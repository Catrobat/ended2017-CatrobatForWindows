using Catrobat.IDE.Core.Models;
using Catrobat.IDE.Core.Models.Bricks;
using Catrobat.IDE.Core.Models.Formulas.Tree;
using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.Xml.XmlObjects.Bricks.Sounds;
using Catrobat.IDE.Core.Xml.XmlObjects.Formulas;
using Catrobat.IDE.Core.XmlModelConvertion.Converters.Formulas;

namespace Catrobat.IDE.Core.XmlModelConvertion.Converters.Actions.Bricks
{
    public class ChangeVolumeBrickConverter : BrickConverterBase<XmlChangeVolumeByBrick, ChangeVolumeBrick>
    {
        public ChangeVolumeBrickConverter() { }

        public override ChangeVolumeBrick Convert1(XmlChangeVolumeByBrick o, XmlModelConvertContext c)
        {
            var formulaConverter = new FormulaConverter();

            return new ChangeVolumeBrick
            {
                RelativePercentage = o.Volume == null ? null : formulaConverter.Convert(o.Volume, c)
            };
        }

        public override XmlChangeVolumeByBrick Convert1(ChangeVolumeBrick m, XmlModelConvertBackContext c)
        {
            var formulaConverter = new FormulaConverter();

            return new XmlChangeVolumeByBrick
            {
                Volume = m.RelativePercentage == null ? null : formulaConverter.Convert(m.RelativePercentage, c)
            };
        }
    }
}
