using Catrobat.IDE.Core.Models;
using Catrobat.IDE.Core.Models.Bricks;
using Catrobat.IDE.Core.Models.Formulas.Tree;
using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.Xml.XmlObjects.Bricks.Sounds;
using Catrobat.IDE.Core.Xml.XmlObjects.Formulas;
using Catrobat.IDE.Core.XmlModelConvertion.Converters.Formulas;

namespace Catrobat.IDE.Core.XmlModelConvertion.Converters.Actions.Bricks
{
    public class SetVolumeBrickConverter : BrickConverterBase<XmlSetVolumeToBrick, SetVolumeBrick>
    {
        public override SetVolumeBrick Convert1(XmlSetVolumeToBrick o, XmlModelConvertContext c)
        {
            var formulaConverter = new FormulaConverter();

            return new SetVolumeBrick
            {
                Percentage = o.Volume == null ? null : formulaConverter.Convert(o.Volume, c)
            };
        }

        public override XmlSetVolumeToBrick Convert1(SetVolumeBrick m, XmlModelConvertBackContext c)
        {
            var formulaConverter = new FormulaConverter();

            return new XmlSetVolumeToBrick
            {
                Volume = m.Percentage == null ? null : formulaConverter.Convert(m.Percentage, c)
            };
        }
    }
}
