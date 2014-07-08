using Catrobat.IDE.Core.Xml.Converter;
using Catrobat.IDE.Core.Xml.XmlObjects;
using System;
using System.Globalization;

// ReSharper disable once CheckNamespace
namespace Catrobat.IDE.Core.Models
{
    partial class UploadHeader : IXmlObjectConvertible<XmlProjectHeader>
    {
        XmlProjectHeader IXmlObjectConvertible<XmlProjectHeader>.ToXmlObject()
        {
            return new XmlProjectHeader
            {
                DateTimeUpload = Uploaded == null ? string.Empty : Uploaded.Value.ToString(CultureInfo.InvariantCulture), 
                MediaLicense = MediaLicense, 
                ProgramLicense = ProgramLicense, 
                RemixOf = RemixOf, 
                Tags = String.Join(";", Tags), 
                Url = Url, 
                UserHandle = UserId
            };
        }
    }
}
