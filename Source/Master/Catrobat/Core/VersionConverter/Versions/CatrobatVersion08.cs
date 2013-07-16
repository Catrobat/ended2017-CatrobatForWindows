using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Catrobat.Core.Misc;

namespace Catrobat.Core.VersionConverter.Versions
{
    public class CatrobatVersion08 : CatrobatVersion
    {
        public override CatrobatVersionPair CatrobatVersionPair { 
            get
            {
                return new CatrobatVersionPair {InputVersion = "0.8", OutputVersion = "Win0.8"};
            } 
        }

        protected override List<CatrobatVersionPropertyRenameEntry> AttributesToRename
        {
            get
            {
                return new List<CatrobatVersionPropertyRenameEntry>
                {
                    //new CatrobatVersionPropertyRenameEntry{ElementName = "", PropertyName = "", NewPropertyName = ""}
                };
            }
        }

        protected override List<CatrobatVersionElementRenameEntry> ElementsToRename
        {
            get
            {
                return new List<CatrobatVersionElementRenameEntry>
                {
                    //new CatrobatVersionElementRenameEntry{ElementName = "", NewElementName = ""}
                };
            }
        }

        #region Convert

        protected override void ConvertRemoveElements(XDocument document)
        {
            //throw new NotImplementedException();
        }

        protected override void ConvertRemoveProperties(XDocument document)
        {
            //throw new NotImplementedException();
        }

        protected override void ConvertStructure(XDocument document)
        {
            //throw new NotImplementedException();

            UnifyReferences(document);
        }

        protected void UnifyReferences(XDocument document)
        {
            UnifyObjectReferences(document);
            UnifyLookReferences(document);
            UnifySoundReferences(document);
            UnifyVariableReferences(document);
        }


        private static readonly List<string> PossibleObjectNames = new List<string> {"object", "pointedObject"};
        protected void UnifyObjectReferences(XDocument document)
        {
            var objectElementList = document.Descendants("objectList").ToArray()[0];
            //var objectElements = select a document.Descendants();

            var objectElements = from a in document.Descendants()
                          where PossibleObjectNames.Contains(a.Name.LocalName)
                          select a;
            //var possibleElements = from a in document.Descendants()
            //                       where PossibleObjectNames.Contains(a.Name.LocalName)
            //                       select a;

            var elementsToDelete = new List<XElement>();

            var possibleElements = objectElements.ToArray();
            foreach (var oldElement in possibleElements)
            {
                if (oldElement.Parent.Name != "objectList" && oldElement.Attribute("reference") == null)
                {
                    var newElement = new XElement(oldElement);
                    objectElementList.Add(newElement);

                    foreach (var possibleElement in possibleElements)
                    {
                        var path = XPathHelper.GetXPath(possibleElement, oldElement);
                        var referenceAttribute = possibleElement.Attribute("reference");
                        if (referenceAttribute != null && XPathHelper.XPathEquals(referenceAttribute.Value, path))
                        {
                            var newPath = XPathHelper.GetXPath(possibleElement, newElement);
                            referenceAttribute.Value = newPath;
                        }
                    }

                    var updatedPath = XPathHelper.GetXPath(oldElement, newElement);
                    oldElement.RemoveAttributes();
                    oldElement.Descendants().Remove();
                    oldElement.SetAttributeValue("reference", updatedPath);

                    elementsToDelete.Add(oldElement);
                }
            }

            foreach (var objectToRename in objectElementList.Elements().ToArray())
            {
                // TODO: rename objectToRename to "object"
            }
        }

        protected void UnifySoundReferences(XDocument document)
        {
            // TODO: implement me
        }

        protected void UnifyLookReferences(XDocument document)
        {
            // TODO: implement me
        }

        protected void UnifyVariableReferences(XDocument document)
        {
            // TODO: implement me
        }

        #endregion

        #region Convert back

        protected override void ConvertBackRemoveElements(XDocument document)
        {
            throw new NotImplementedException();
        }

        protected override void ConvertBackRemoveProperties(XDocument document)
        {
            throw new NotImplementedException();
        }

        protected override void ConvertBackStructure(XDocument document)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
