using Catrobat.Data.Xml.XmlObjects.Bricks;
using Context = Catrobat.IDE.Core.Xml.Converter.XmlProjectConverter.ConvertBackContext;

// ReSharper disable once CheckNamespace
namespace Catrobat.IDE.Core.Models.Bricks
{
    partial class CommentBrick : Brick
    {
        protected internal override XmlBrick ToXmlObject2(Context context)
        {
            return new XmlNoteBrick
            {
                Note = Value
            };
        }
    }
}
