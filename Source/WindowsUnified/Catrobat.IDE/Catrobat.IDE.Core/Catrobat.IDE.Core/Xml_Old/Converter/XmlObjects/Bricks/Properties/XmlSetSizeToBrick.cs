using Catrobat.IDE.Core.Models.Bricks;
using Catrobat.IDE.Core.ExtensionMethods;
using Context = Catrobat.IDE.Core.Xml.Converter.XmlProgramConverter.ConvertContext;

// ReSharper disable once CheckNamespace
namespace Catrobat.IDE.Core.Xml.XmlObjects.Bricks.Properties
{
    partial class XmlSetSizeToBrick
    {
        protected override Brick ToModel2(Context context)
        {
            return new SetSizeBrick
            {
                Percentage = Size == null ? null : Size.ToModel(context)
            };
        }
    }
}