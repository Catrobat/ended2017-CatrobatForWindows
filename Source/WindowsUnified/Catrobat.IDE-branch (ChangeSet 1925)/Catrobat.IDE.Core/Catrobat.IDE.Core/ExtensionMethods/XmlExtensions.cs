using Catrobat.IDE.Core.Models;
using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.Xml.Converter;
using Catrobat.IDE.Core.Xml.XmlObjects;
using Catrobat.IDE.Core.XmlModelConvertion;
using Catrobat.IDE.Core.XmlModelConvertion.Converters;

namespace Catrobat.IDE.Core.ExtensionMethods
{
    public static class XmlExtensions
    {
        internal static TXmlObject ToXmlObject<TXmlObject>(this IXmlObjectConvertible<TXmlObject> model)
        {
            return model.ToXmlObject();
        }
    }
}
