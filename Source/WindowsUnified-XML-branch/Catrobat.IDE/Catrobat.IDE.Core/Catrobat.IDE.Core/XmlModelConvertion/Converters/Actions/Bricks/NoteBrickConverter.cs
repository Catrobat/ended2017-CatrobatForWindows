using Catrobat.IDE.Core.Models;
using Catrobat.IDE.Core.Models.Bricks;
using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.Xml.XmlObjects.Bricks;

namespace Catrobat.IDE.Core.XmlModelConvertion.Converters.Actions.Bricks
{
    public class NoteBrickConverter : BrickConverterBase<XmlNoteBrick, CommentBrick>
    {
        public NoteBrickConverter() { }

        public override CommentBrick Convert1(XmlNoteBrick o, XmlModelConvertContext c)
        {
            return new CommentBrick
            {
                Value = o.Note
            };
        }

        public override XmlNoteBrick Convert1(CommentBrick m, XmlModelConvertBackContext c)
        {
            return new XmlNoteBrick
            {
                Note = m.Value
            };
        }
    }
}
