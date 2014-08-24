using Catrobat.IDE.Core.Xml.Converter;
using Catrobat.IDE.Core.Xml.XmlObjects;

// ReSharper disable once CheckNamespace
namespace Catrobat.IDE.Core.Models
{
    partial class Look : IXmlObjectConvertible<XmlLook>
    {
        XmlLook IXmlObjectConvertible<XmlLook>.ToXmlObject()
        {
            return new XmlLook
            {
                Name = Name, 
                FileName = FileName
            };
        }
    }
}
