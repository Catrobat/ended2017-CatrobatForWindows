using Catrobat.IDE.Core.Models;
using Catrobat.IDE.Core.Xml.Converter;

// ReSharper disable once CheckNamespace
namespace Catrobat.IDE.Core.Xml.XmlObjects
{
    partial class XmlLook : IModelConvertible<Look>
    {
        Look IModelConvertible<Look>.ToModel()
        {
            return new Look
            {
                Name = Name,
                FileName = FileName
            };
        }
    }
}