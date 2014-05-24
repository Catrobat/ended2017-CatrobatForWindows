using Catrobat.IDE.Core.ExtensionMethods;
using Catrobat.IDE.Core.Xml.XmlObjects.Bricks;
using Catrobat.IDE.Core.Xml.XmlObjects.Bricks.Properties;
using Context = Catrobat.IDE.Core.Xml.Converter.XmlProjectConverter.ConvertBackContext;

// ReSharper disable once CheckNamespace
namespace Catrobat.IDE.Core.Models.Bricks
{
    partial class AnimatePropertyBrick
    {
    }

    #region Implementations

    partial class AnimatePositionBrick
    {
        protected internal override XmlBrick ToXmlObject2(Context context)
        {
            return new XmlGlideToBrick
            {
                DurationInSeconds = Duration == null ? null : Duration.ToXmlObject(), 
                XDestination = ToX == null ? null : ToX.ToXmlObject(), 
                YDestination = ToY == null ? null : ToY.ToXmlObject()
            };
        }
    }

    #endregion
}
