using Catrobat.IDE.Core.ExtensionMethods;
using Catrobat.IDE.Core.Models;
using Catrobat.IDE.Core.Xml.Converter;
using System;
using System.Globalization;

// ReSharper disable once CheckNamespace
namespace Catrobat.IDE.Core.Xml.XmlObjects
{
    partial class XmlProjectHeader : IModelConvertible<UploadHeader>
    {
        UploadHeader IModelConvertible<UploadHeader>.ToModel()
        {
            return new UploadHeader
            {
                MediaLicense = MediaLicense,
                ProgramLicense = ProgramLicense,
                RemixOf = RemixOf,
                Tags = Tags.Split(new[] {';'}, StringSplitOptions.RemoveEmptyEntries).ToObservableCollection(),
                Uploaded = string.IsNullOrEmpty(DateTimeUpload)
                    ? (DateTime?) null
                    : DateTime.Parse(DateTimeUpload, CultureInfo.InvariantCulture),
                Url = Url,
                UserId = UserHandle
            };
        }
    }
}
