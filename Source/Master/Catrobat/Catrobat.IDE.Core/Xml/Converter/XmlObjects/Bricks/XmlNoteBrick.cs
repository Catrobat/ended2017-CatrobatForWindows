using Catrobat.IDE.Core.Models.Bricks;
using Context = Catrobat.IDE.Core.Xml.Converter.XmlProjectConverter.ConvertContext;

// ReSharper disable once CheckNamespace
namespace Catrobat.IDE.Core.Xml.XmlObjects.Bricks
{
    partial class XmlNoteBrick
    {
        protected override Brick ToModel2(Context context)
        {
            return new CommentBrick
            {
                Value = Note
            };
        }
    }
}