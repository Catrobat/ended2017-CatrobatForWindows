using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace Catrobat.Core.Utilities.Helpers
{
    public static class XPathHelper
    {
        public static XElement GetElement(XElement referenceElement, string xPath)
        {
            var parts = xPath.Split('/');
            var currentElement = referenceElement;
            foreach (var part in parts)
            {
                if (part == "..")
                {
                    currentElement = currentElement.Parent;
                }
                else
                {
                    int index = 0;
                    var splitPath = part.Split('[');
                    var childElementName = splitPath[0];

                    if (splitPath.Length > 1)
                    {
                        var indexAsString = splitPath[1].Split(']')[0];
                        index = int.Parse(indexAsString);
                        index --;
                    }

                    currentElement = currentElement.Descendants(childElementName).ToArray()[index];
                }
            }

            return currentElement;
        }

        public static string GetXPath(XElement fromElement, XElement toElement)
        {
            var path = "";
            
            var absolutFromPath = GetAbsolutPath(fromElement);
            var absolutToPath = GetAbsolutPath(toElement);

            int commonPath = 0;
            while (commonPath < absolutFromPath.Count && commonPath < absolutToPath.Count && absolutFromPath[commonPath] == absolutToPath[commonPath])
                commonPath++;

            for (int i = 0; i < absolutFromPath.Count - commonPath + 1; i++)
                path += "../";

            for (int i = commonPath; i < absolutToPath.Count; i++)
            {
                path += GetNameWithIndex(absolutToPath[i]) + "/";
            }

            path += GetNameWithIndex(toElement);

            //path = path.Replace("[1]", "");

            return path;
        }


        public static bool XPathEquals(string xPath1, string xPath2)
        {
            xPath1 = xPath1.Replace("[1]", "");
            xPath2 = xPath2.Replace("[1]", "");
            return xPath1 == xPath2;
        }

        private static List<XElement> GetAbsolutPath(XElement element)
        {
            var reverePath = new List<XElement>();
            var currentElement = element;

            while (currentElement.Parent != null)
            {
                reverePath.Add(currentElement.Parent);
                currentElement = currentElement.Parent;
            }

            var path = new List<XElement>();

            for(int i = reverePath.Count - 1; i >= 0; i--)
                path.Add(reverePath[i]);

            return path;
        }

        private static string GetNameWithIndex(XElement element)
        {
            var index = 0;

            foreach (var sibling in element.ElementsBeforeSelf(element.Name))
                if (sibling != element)
                    index++;

            index++;
            var nameWithIndex = element.Name + "[" + index + "]";
            return nameWithIndex;
        }
    }
}
