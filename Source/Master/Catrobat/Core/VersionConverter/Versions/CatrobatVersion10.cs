using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Catrobat.Core.Misc;

namespace Catrobat.Core.VersionConverter.Versions
{
    public class CatrobatVersion10 : CatrobatVersion
    {
        public override CatrobatVersionPair CatrobatVersionPair { 
            get
            {
                return new CatrobatVersionPair {InputVersion = "1.0", OutputVersion = "Win1.0"};
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

        protected void UnifyObjectReferences(XDocument document)
        {
            var objectElementList = document.Descendants("objectList").ToArray()[0];
            var objectElements = document.Descendants("object");

            foreach (var oldElement in objectElements)
            {
                if (oldElement.Parent.Name != "objectList" && oldElement.Attribute("reference") == null)
                {
                    var newElement = new XElement(oldElement);
                    objectElementList.Add(newElement);

                    var possibleElements = document.Descendants("object");

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

                    oldElement.Remove();
                }
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
