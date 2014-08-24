using Catrobat.IDE.Core.Xml.XmlObjects.Scripts;
using Context = Catrobat.IDE.Core.Xml.Converter.XmlProgramConverter.ConvertBackContext;

// ReSharper disable once CheckNamespace
namespace Catrobat.IDE.Core.Models.Scripts
{
    partial class StartScript
    {
        protected internal override XmlScript ToXmlObject2(Context context)
        {
            return new XmlStartScript();
        }
    }
}
