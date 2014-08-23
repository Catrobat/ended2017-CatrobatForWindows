using Catrobat.IDE.Core.ExtensionMethods;
using Catrobat.IDE.Core.Models.Bricks;
using Context = Catrobat.IDE.Core.Xml.Converter.XmlProgramConverter.ConvertContext;

// ReSharper disable once CheckNamespace
namespace Catrobat.IDE.Core.Xml.XmlObjects.Bricks.Properties
{
    partial class XmlChangeSizeByNBrick
    {
        protected override Brick ToModel2(Context context)
        {
            return new ChangeSizeBrick
            {
                RelativePercentage = Size == null ? null : Size.ToModel(context)
            };
        }
    }
}