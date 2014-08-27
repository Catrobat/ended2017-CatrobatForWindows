using Catrobat.IDE.Core.Models;
using Catrobat.IDE.Core.Models.Bricks;
using Catrobat.IDE.Core.Models.Formulas.Tree;
using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.Xml.XmlObjects.Bricks.Sounds;
using Catrobat.IDE.Core.Xml.XmlObjects.Formulas;

namespace Catrobat.IDE.Core.XmlModelConvertion.Converters.Actions.Bricks
{
    public class ChangeVolumeBrickConverter : BrickConverterBase<XmlChangeVolumeByBrick, ChangeVolumeBrick>
    {
        public ChangeVolumeBrickConverter(IXmlModelConversionService converter)  
            : base(converter) { }

        public override ChangeVolumeBrick Convert1(XmlChangeVolumeByBrick o, XmlModelConvertContext c)
        {
            return new ChangeVolumeBrick
            {
                RelativePercentage = o.Volume == null ? null : (FormulaTree)Converter.Convert(o.Volume)
            };
        }

        public override XmlChangeVolumeByBrick Convert1(ChangeVolumeBrick m, XmlModelConvertBackContext c)
        {
            return new XmlChangeVolumeByBrick
            {
                Volume = m.RelativePercentage == null ? null : (XmlFormula)Converter.Convert(m.RelativePercentage)
            };
        }
    }
}
