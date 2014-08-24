using Catrobat.IDE.Core.ExtensionMethods;
using Catrobat.IDE.Core.Models.Bricks;
using Context = Catrobat.IDE.Core.Xml.Converter.XmlProgramConverter.ConvertContext;

// ReSharper disable once CheckNamespace
namespace Catrobat.IDE.Core.Xml.XmlObjects.Bricks.Properties
{
    partial class XmlGlideToBrick
    {
        protected override Brick ToModel2(Context context)
        {
            return new AnimatePositionBrick
            {
                Duration = DurationInSeconds == null ? null : DurationInSeconds.ToModel(context), 
                ToX = XDestination == null ? null : XDestination.ToModel(context), 
                ToY = YDestination == null ? null : YDestination.ToModel(context)
            };
        }
    }
}