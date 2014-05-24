using Catrobat.IDE.Core.ExtensionMethods;
using Catrobat.IDE.Core.Models.Bricks;
using Context = Catrobat.IDE.Core.Xml.Converter.XmlProjectConverter.ConvertContext;

// ReSharper disable once CheckNamespace
namespace Catrobat.IDE.Core.Xml.XmlObjects.Bricks.Properties
{
    partial class XmlChangeYByBrick
    {
        protected internal override Brick ToModel2(Context context)
        {
            return new ChangePositionYBrick
            {
                RelativeValue = YMovement == null ? null : YMovement.ToModel(context)
            };
        }
    }
}