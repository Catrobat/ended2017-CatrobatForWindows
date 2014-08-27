using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Catrobat.IDE.Core.Models;
using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.Xml.Converter;
using Catrobat.IDE.Core.Xml.XmlObjects;
using System.Globalization;
using Catrobat.IDE.Core.ExtensionMethods;

namespace Catrobat.IDE.Core.XmlModelConvertion.Converters
{
    public class UploadHeaderConverter : XmlModelConverter<XmlProjectHeader, UploadHeader>
    {
        public UploadHeaderConverter(IXmlModelConversionService converter) : base(converter)
        {
        }

        public override UploadHeader Convert(XmlProjectHeader o, XmlModelConvertContext c)
        {
            return new UploadHeader
            {
                MediaLicense = o.MediaLicense,
                ProgramLicense = o.ProgramLicense,
                RemixOf = o.RemixOf,
                Tags = o.Tags.Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries).ToObservableCollection(),
                Uploaded = string.IsNullOrEmpty(o.DateTimeUpload)
                    ? (DateTime?)null
                    : DateTime.Parse(o.DateTimeUpload, CultureInfo.InvariantCulture),
                Url = o.Url,
                UserId = o.UserHandle
            };
        }

        public override XmlProjectHeader Convert(UploadHeader m, XmlModelConvertBackContext c)
        {
            return new XmlProjectHeader
            {
                DateTimeUpload = m.Uploaded == null ? string.Empty : m.Uploaded.Value.ToString(CultureInfo.InvariantCulture),
                MediaLicense = m.MediaLicense,
                ProgramLicense = m.ProgramLicense,
                RemixOf = m.RemixOf,
                Tags = String.Join(";", m.Tags),
                Url = m.Url,
                UserHandle = m.UserId
            };
        }
    }
}
