using Catrobat.Data.Xml.XmlObjects;
using Catrobat.IDE.Core.Xml.Converter;

// ReSharper disable once CheckNamespace
namespace Catrobat.IDE.Core.Models
{
    partial class Costume : IXmlObjectConvertible<XmlCostume>
    {
        XmlCostume IXmlObjectConvertible<XmlCostume>.ToXmlObject()
        {
            return new XmlCostume
            {
                Name = Name, 
                FileName = FileName
            };
        }
    }
}
