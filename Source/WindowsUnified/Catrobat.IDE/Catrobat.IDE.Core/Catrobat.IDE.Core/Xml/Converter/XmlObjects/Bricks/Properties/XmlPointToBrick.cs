using Catrobat.IDE.Core.ExtensionMethods;
using Catrobat.IDE.Core.Models.Bricks;
using Context = Catrobat.IDE.Core.Xml.Converter.XmlProjectConverter.ConvertContext;

// ReSharper disable once CheckNamespace
namespace Catrobat.IDE.Core.Xml.XmlObjects.Bricks.Properties
{
    partial class XmlPointToBrick
    {
        protected override Brick ToModel2(Context context)
        {
            return new LookAtBrick
            {
                Target = PointedSprite == null ? null : PointedSprite.ToModel(context, pointerOnly: true)
            };
        }
    }
}