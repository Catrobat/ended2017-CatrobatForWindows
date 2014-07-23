using Catrobat.Data.Xml.XmlObjects.Bricks;
using Catrobat.Data.Xml.XmlObjects.Bricks.ControlFlow;
using Catrobat.Data.Xml.XmlObjects.Formulas;
using Catrobat.IDE.Core.ExtensionMethods;
using Context = Catrobat.IDE.Core.Xml.Converter.XmlProjectConverter.ConvertBackContext;

// ReSharper disable once CheckNamespace
namespace Catrobat.IDE.Core.Models.Bricks
{
    partial class DelayBrick
    {
        protected internal override XmlBrick ToXmlObject2(Context context)
        {
            return new XmlWaitBrick
            {
                TimeToWaitInSeconds = Duration == null ? new XmlFormula() : Duration.ToXmlObject()
            };
        }
    }
}
