using Catrobat.IDE.Core.Xml.XmlObjects.Bricks;
using Catrobat.IDE.Core.Xml.XmlObjects.Bricks.ControlFlow;
using Catrobat.IDE.Core.ExtensionMethods;
using Catrobat.IDE.Core.Xml.XmlObjects.Formulas;
using Context = Catrobat.IDE.Core.Xml.Converter.XmlProgramConverter.ConvertBackContext;

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
