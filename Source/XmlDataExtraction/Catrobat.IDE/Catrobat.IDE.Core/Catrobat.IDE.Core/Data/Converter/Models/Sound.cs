using Catrobat.Data.Xml.XmlObjects;
using Catrobat.IDE.Core.Xml.Converter;

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
