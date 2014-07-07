using Catrobat.IDE.Core.Models;
using Catrobat.IDE.Core.Xml.Converter;

// ReSharper disable once CheckNamespace
namespace Catrobat.IDE.Core.Xml.XmlObjects
{
    partial class XmlCostume : IModelConvertible<Costume>
    {
        Costume IModelConvertible<Costume>.ToModel()
        {
            return new Costume
            {
                Name = Name,
                FileName = FileName
            };
        }
    }
}