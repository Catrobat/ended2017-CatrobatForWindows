using Catrobat.IDE.Core.ExtensionMethods;
using Catrobat.IDE.Core.Models.Bricks;
using Context = Catrobat.IDE.Core.Xml.Converter.XmlProjectConverter.ConvertContext;

// ReSharper disable once CheckNamespace
namespace Catrobat.IDE.Core.Xml.XmlObjects.Bricks.Properties
{
    partial class XmlSetBrightnessBrick
    {
        protected override Brick ToModel2(Context context)
        {
            return new SetBrightnessBrick
            {
                Percentage = Brightness == null ? null : Brightness.ToModel(context)
            };
        }
    }
}