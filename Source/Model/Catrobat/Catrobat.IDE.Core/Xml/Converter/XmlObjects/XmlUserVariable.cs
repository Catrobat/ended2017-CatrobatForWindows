using Catrobat.IDE.Core.Models;
using Catrobat.IDE.Core.Xml.Converter;

// ReSharper disable once CheckNamespace
namespace Catrobat.IDE.Core.Xml.XmlObjects.Variables
{
    partial class XmlUserVariable : IModelConvertible<LocalVariable>, IModelConvertible<GlobalVariable>
    {
        LocalVariable IModelConvertible<LocalVariable>.ToModel()
        {
            return new LocalVariable
            {
                Name = Name
            };
        }

        GlobalVariable IModelConvertible<GlobalVariable>.ToModel()
        {
            return new GlobalVariable
            {
                Name = Name
            };
        }
    }
}
