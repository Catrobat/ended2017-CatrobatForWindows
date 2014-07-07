using Catrobat.IDE.Core.Xml.Converter;

// ReSharper disable once CheckNamespace
namespace Catrobat.IDE.Core.Models
{
    partial class BroadcastMessage : IXmlObjectConvertible<string>
    {
        string IXmlObjectConvertible<string>.ToXmlObject()
        {
            return Content;
        }
    }
}
