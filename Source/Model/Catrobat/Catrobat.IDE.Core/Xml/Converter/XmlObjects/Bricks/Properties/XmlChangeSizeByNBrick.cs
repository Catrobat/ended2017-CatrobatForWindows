using Catrobat.IDE.Core.ExtensionMethods;
using Catrobat.IDE.Core.Models.Bricks;
using Context = Catrobat.IDE.Core.Xml.Converter.XmlProjectConverter.ConvertContext;

// ReSharper disable once CheckNamespace
namespace Catrobat.IDE.Core.Xml.XmlObjects.Bricks.Properties
{
    partial class XmlChangeSizeByNBrick
    {
        protected internal override Brick ToModel2(Context context)
        {
            return new ChangeSizeBrick
            {
                RelativePercentage = Size == null ? null : Size.ToModel(context)
            };
        }
    }
}