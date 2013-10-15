using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace Catrobat.IDE.Core.Utilities.Helpers
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
                    var splitPath = part.Split('[');
                    var childElementName = splitPath[0];
                    var index = 0;

                    if (splitPath.Length > 1)
                    {
                        var indexAsString = splitPath[1].Split(']')[0];

                        // numbering starts with 1
                        index = int.Parse(indexAsString) - 1;
                    }

                    currentElement = currentElement.Descendants(childElementName).ElementAtOrDefault(index);
                }
                if (currentElement == null) break;
            }

            return currentElement;
        }

        public static string GetXPath(XElement fromElement, XElement toElement)
        {
            var path = "";
            
            var absolutePathFrom = GetAbsolutePath(fromElement);
            var absolutePathTo = GetAbsolutePath(toElement);

            var commonPath = 0;
            while (commonPath < absolutePathFrom.Count && commonPath < absolutePathTo.Count && absolutePathFrom[commonPath] == absolutePathTo[commonPath])
                commonPath++;

            for (var i = 0; i < absolutePathFrom.Count - commonPath + 1; i++)
                path += "../";

            for (var i = commonPath; i < absolutePathTo.Count; i++)
            {
                path += GetNameWithIndex(absolutePathTo[i]) + "/";
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

        private static List<XElement> GetAbsolutePath(XElement element)
        {
            var reversePath = new List<XElement>();
            var currentElement = element;

            while (currentElement.Parent != null)
            {
                reversePath.Add(currentElement.Parent);
                currentElement = currentElement.Parent;
            }

            reversePath.Reverse();

            return reversePath;
        }

        private static string GetNameWithIndex(XElement element)
        {
            // numbering starts with 1
            var index = element.ElementsBeforeSelf(element.Name).Count() + 1;

            // add index only when there are more then 1 elements
            if (index == 1 && !element.ElementsAfterSelf(element.Name).Any()) return element.Name.ToString();

            return element.Name + "[" + index + "]";;
        }
    }
}
