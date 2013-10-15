using System.Xml.Linq;

namespace Catrobat.IDE.Core.Utilities.Helpers
{
    public static class XElementExtensions
    {
        public static bool HasAttribute(this XElement element, string name)
        {
            return element.Attribute(name) != null;
        }

    }
}