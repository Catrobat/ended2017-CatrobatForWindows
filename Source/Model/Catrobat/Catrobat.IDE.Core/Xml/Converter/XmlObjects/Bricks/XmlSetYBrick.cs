using Catrobat.IDE.Core.Models.Bricks;
using Context = Catrobat.IDE.Core.Xml.Converter.XmlProjectConverter.ConvertContext;

// ReSharper disable once CheckNamespace
namespace Catrobat.IDE.Core.Xml.XmlObjects.Bricks.Properties
{
    partial class XmlSetYBrick
    {
        protected internal override Brick ToModel2(Context context)
        {
            return new SetPositionYBrick
            {
                Y = YPosition == null ? null : YPosition.ToModel(context)
            };
        }
    }
}