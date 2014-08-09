using Catrobat.IDE.Core.ExtensionMethods;
using Catrobat.IDE.Core.Models.Bricks;
using Context = Catrobat.IDE.Core.Xml.Converter.XmlProgramConverter.ConvertContext;

// ReSharper disable once CheckNamespace
namespace Catrobat.IDE.Core.Xml.XmlObjects.Bricks.Properties
{
    partial class XmlPlaceAtBrick
    {
        protected override Brick ToModel2(Context context)
        {
            return new SetPositionBrick
            {
                ValueX = XPosition == null ? null : XPosition.ToModel(context), 
                ValueY = YPosition == null ? null : YPosition.ToModel(context)
            };
        }
    }
}