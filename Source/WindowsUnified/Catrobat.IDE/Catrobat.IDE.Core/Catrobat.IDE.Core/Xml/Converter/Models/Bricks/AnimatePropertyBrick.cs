using Catrobat.IDE.Core.ExtensionMethods;
using Catrobat.IDE.Core.Xml.XmlObjects.Bricks;
using Catrobat.IDE.Core.Xml.XmlObjects.Bricks.Properties;
using Catrobat.IDE.Core.Xml.XmlObjects.Formulas;
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
                DurationInSeconds = Duration == null ? new XmlFormula() : Duration.ToXmlObject(), 
                XDestination = ToX == null ? new XmlFormula() : ToX.ToXmlObject(), 
                YDestination = ToY == null ? new XmlFormula() : ToY.ToXmlObject()
            };
        }
    }

    #endregion
}
