using Catrobat.IDE.Core.Models;
using Catrobat.IDE.Core.Xml.Converter;

// ReSharper disable once CheckNamespace
namespace Catrobat.IDE.Core.Xml.XmlObjects
{
    partial class XmlSound : IModelConvertible<Sound>
    {
        Sound IModelConvertible<Sound>.ToModel()
        {
            return new Sound
            {
                Name = Name, 
                FileName = FileName
            };
        }
    }
}