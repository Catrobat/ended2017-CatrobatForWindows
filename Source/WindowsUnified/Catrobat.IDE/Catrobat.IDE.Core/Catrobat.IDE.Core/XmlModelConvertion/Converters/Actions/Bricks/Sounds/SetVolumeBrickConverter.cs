using Catrobat.IDE.Core.Models;
using Catrobat.IDE.Core.Models.Bricks;
using Catrobat.IDE.Core.Models.Formulas.Tree;
using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.Xml.XmlObjects.Bricks.Sounds;
using Catrobat.IDE.Core.Xml.XmlObjects.Formulas;

namespace Catrobat.IDE.Core.XmlModelConvertion.Converters.Actions.Bricks
{
    public class SetVolumeBrickConverter : BrickConverterBase<XmlSetVolumeToBrick, SetVolumeBrick>
    {
        public SetVolumeBrickConverter(IXmlModelConversionService converter)  
            : base(converter) { }

        public override SetVolumeBrick Convert1(XmlSetVolumeToBrick o, XmlModelConvertContext c)
        {
            return new SetVolumeBrick
            {
                Percentage = o.Volume == null ? null : (FormulaTree)Converter.Convert(o.Volume)
            };
        }

        public override XmlSetVolumeToBrick Convert1(SetVolumeBrick m, XmlModelConvertBackContext c)
        {
            return new XmlSetVolumeToBrick
            {
                Volume = m.Percentage == null ? null : (XmlFormula)Converter.Convert(m.Percentage)
            };
        }
    }
}
