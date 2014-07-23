using System.Xml.Linq;

namespace Catrobat.Data.Utilities.Helpers
{
    public static class XElementExtensions
    {
        public const string ReferenceAttributeName = "reference";

        public static bool HasAttribute(this XElement element, string name)
        {
            return element.Attribute(name) != null;
        }

        public static bool HasReference(this XElement element)
        {
            return element.HasAttribute(ReferenceAttributeName);
        }

        public static bool HasReferenceTo(this XElement element, XElement target)
        {
            var referenceAttribute = element.Attribute(ReferenceAttributeName);
            return referenceAttribute != null && 
                XPathHelper.GetElement(element, referenceAttribute.Value) == target;
        }

        public static XElement GetReferenceTarget(this XElement element)
        {
            var referenceAttribute = element.Attribute(ReferenceAttributeName);
            return referenceAttribute == null ? 
                null : 
                XPathHelper.GetElement(element, referenceAttribute.Value);
        }

        public static void SetReferenceTarget(this XElement element, XElement target)
        {
            element.SetAttributeValue(ReferenceAttributeName, XPathHelper.GetXPath(element, target));
        }

    }
}