using Catrobat.IDE.Core.ExtensionMethods;
using Catrobat.IDE.Core.Models.Bricks;
using Context = Catrobat.IDE.Core.Xml.Converter.XmlProgramConverter.ConvertContext;

// ReSharper disable once CheckNamespace
namespace Catrobat.IDE.Core.Xml.XmlObjects.Bricks.Properties
{
    partial class XmlSetGhostEffectBrick
    {
        protected override Brick ToModel2(Context context)
        {
            return new SetTransparencyBrick
            {
                Percentage = Transparency == null ? null : Transparency.ToModel(context)
            };
        }
    }
}