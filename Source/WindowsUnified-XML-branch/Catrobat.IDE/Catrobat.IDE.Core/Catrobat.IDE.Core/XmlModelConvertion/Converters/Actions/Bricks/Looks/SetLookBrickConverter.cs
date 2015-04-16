using Catrobat.IDE.Core.Models;
using Catrobat.IDE.Core.Models.Bricks;
using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.Xml.XmlObjects;
using Catrobat.IDE.Core.Xml.XmlObjects.Bricks.Looks;

namespace Catrobat.IDE.Core.XmlModelConvertion.Converters.Actions.Bricks
{
    public class SetLookBrickConverter : BrickConverterBase<XmlSetLookBrick, SetLookBrick>
    {
        public SetLookBrickConverter() { }

        public override SetLookBrick Convert1(XmlSetLookBrick o, XmlModelConvertContext c)
        {
            Look look = null;
            if (o.Look != null) c.Looks.TryGetValue(o.Look, out look);
            return new SetLookBrick
            {
                Value = look
            };
        }

        public override XmlSetLookBrick Convert1(SetLookBrick m, XmlModelConvertBackContext c)
        {
            XmlLook look = null;
            if (m.Value != null) c.Looks.TryGetValue(m.Value, out look);
            return new XmlSetLookBrick
            {
                Look = look
            };
        }
    }
}
