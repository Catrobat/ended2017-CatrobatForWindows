using Catrobat.IDE.Core.Xml.Converter;
using Catrobat.IDE.Core.Xml.XmlObjects;

// ReSharper disable once CheckNamespace
namespace Catrobat.IDE.Core.Models
{
    partial class Sound : IXmlObjectConvertible<XmlSound>
    {
        XmlSound IXmlObjectConvertible<XmlSound>.ToXmlObject()
        {
            return new XmlSound
            {
                Name = Name, 
                FileName = FileName
            };
        }
    }
}
