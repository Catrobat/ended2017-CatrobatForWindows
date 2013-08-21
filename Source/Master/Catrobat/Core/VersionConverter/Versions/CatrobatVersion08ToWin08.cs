using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Catrobat.Core.Misc;

namespace Catrobat.Core.VersionConverter.Versions
{
    public class CatrobatVersion08ToWin08 : CatrobatVersion
    {
        public override CatrobatVersionPair CatrobatVersionPair
        {
            get
            {
                return new CatrobatVersionPair { InputVersion = "0.8", OutputVersion = "Win0.8" };
            }
        }

        #region Convert

        protected void ConvertRemoveElements(XDocument document)
        {
            //throw new NotImplementedException();
        }

        protected void ConvertRemoveProperties(XDocument document)
        {
            var program = document.Element("program");
            var objectList = program.Element("objectList");
            var variables = program.Element("variables");

            foreach (var sprite in objectList.Descendants("object"))
            {
                if (sprite.Elements("reference").Any())
                {
                    sprite.Remove();
                }
            }

            foreach (var pointToBrick in objectList.Descendants("pointToBrick"))
                foreach (var pointedObject in pointToBrick.Descendants("pointedObject"))
                    pointedObject.Name = "object";
        }

        protected override void ConvertStructure(XDocument document)
        {
            UnifyReferences(document);
        }

        protected void UnifyReferences(XDocument document)
        {
            UnifyObjectReferences(document);
            UnifyLookReferences(document);
            UnifySoundReferences(document);
            UnifyVariableReferences(document);
            ResolveReferencesToReferences(document);
            UnifyForeverBrickReferences(document);
            UnifyRepeatBrickReferences(document);
            UnifyIfLogicBeginBrickReferences(document);

            ConvertRemoveElements(document);
            ConvertRemoveProperties(document);
        }

        private void UnifyForeverBrickReferences(XDocument document)
        {
            var loopEndlessBricks = document.Descendants("brickList").Descendants("loopEndlessBrick").ToList();
            SwapCrossReferences(document, loopEndlessBricks);
        }

        private void UnifyRepeatBrickReferences(XDocument document)
        {
            var loopEndlessBricks = document.Descendants("brickList").Descendants("loopEndBrick").ToList();
            SwapCrossReferences(document, loopEndlessBricks);
        }

        private void UnifyIfLogicBeginBrickReferences(XDocument document)
        {
            var loopEndlessBricks = document.Descendants("brickList").Descendants("ifLogicElseBrick").ToList();
            SwapCrossReferences(document, loopEndlessBricks);
            RemoveSelfReferences(document);

            loopEndlessBricks = document.Descendants("brickList").Descendants("ifLogicEndBrick").ToList();
            SwapCrossReferences(document, loopEndlessBricks);
            RemoveSelfReferences(document);
        }

        protected void SwapReferencesInList(XDocument document, List<XElement> listNodes)
        {
            //var listNodes = document.Descendants(listName);
            foreach (var listNode in listNodes)
            {
                var listElements = listNode.Elements();
                var listElementsToSwap = from a in listElements
                                         where a.Attribute("reference") != null
                                         select a;

                SwapReferences(document, listElementsToSwap.ToList());
            }
        }

        protected void SwapReferences(XDocument document, List<XElement> elementsToSwap)
        {
            foreach (var listElement in elementsToSwap)
            {
                var referenceAttribute = listElement.Attribute("reference");
                var referencedElement = XPathHelper.GetElement(listElement, referenceAttribute.Value);
                listElement.ReplaceNodes(referencedElement.Elements());
                var newReferencePath = XPathHelper.GetXPath(referencedElement, listElement);
                referencedElement.SetAttributeValue("reference", newReferencePath);
                listElement.SetAttributeValue("reference", null);
                referencedElement.RemoveNodes();
            }
        }

        protected void SwapCrossReferences(XDocument document, List<XElement> elementsToSwap)
        {
            foreach (var listElement in elementsToSwap)
            {
                var referenceAttribute = listElement.Attribute("reference");

                if (referenceAttribute == null)
                    continue;

                var referencedElement = XPathHelper.GetElement(listElement, referenceAttribute.Value);


                var referenceElements = from a in document.Descendants()
                                        where a.Attribute("reference") != null
                                        select a;

                var changedReferences = new List<XElement>();

                foreach (var referenceElement in referenceElements)
                {
                    var path = referenceElement.Attribute("reference").Value;
                    if (XPathHelper.GetElement(referenceElement, path) == referencedElement)
                    {
                        changedReferences.Add(referenceElement);
                    }
                }


                listElement.ReplaceNodes(referencedElement.Elements());

                var newReferencePath = XPathHelper.GetXPath(referencedElement, listElement);
                referencedElement.SetAttributeValue("reference", newReferencePath);
                listElement.SetAttributeValue("reference", null);

                foreach (var child in listElement.Elements())
                    if (child.Attributes("reference").Any())
                    {
                        var newChildPath = XPathHelper.GetXPath(child, referencedElement.Parent);
                        child.SetAttributeValue("reference", newChildPath);
                    }
                    else
                    {
                        foreach (var grandChild in child.Elements())
                        {
                            var newGrandChildPath = XPathHelper.GetXPath(grandChild, referencedElement.Parent);
                            grandChild.SetAttributeValue("reference", newGrandChildPath);
                        }
                    }

                foreach (var referenceElement in changedReferences)
                {
                    var newPath = XPathHelper.GetXPath(referenceElement, referencedElement);
                    referenceElement.SetAttributeValue("reference", newPath);
                }

                referencedElement.RemoveNodes();
            }
        }

        protected void RemoveSelfReferences(XDocument document)
        {
            var allElements = document.Descendants("objectList").ToArray()[0].Descendants("object");
            var objectReferences = from a in allElements
                                   where a.Attribute("reference") != null
                                   select a;
            var objectReferencesToRemove = (from objectReference in objectReferences
                                            let referenceAttribute = objectReference.Attribute("reference")
                                            let originalElement = XPathHelper.GetElement(objectReference, referenceAttribute.Value)
                                            where originalElement.Descendants().ToList().Contains(objectReference)
                                            select objectReference).ToList();
            foreach (var objectReferenceToRemove in objectReferencesToRemove)
            {
                objectReferenceToRemove.Remove();
            }
        }

        protected void ResolveReferencesToReferences(XDocument document)
        {
            var allReferenceElements = (from a in document.Descendants()
                                        where a.Attribute("reference") != null
                                        select a).ToList();
            foreach (var referenceElement in allReferenceElements)
            {
                var referenceAttribute = referenceElement.Attribute("reference");
                var targetElement = XPathHelper.GetElement(referenceElement, referenceAttribute.Value);
                var referenceChanged = false;
                while (targetElement.Attribute("reference") != null)
                {
                    referenceAttribute = targetElement.Attribute("reference");
                    targetElement = XPathHelper.GetElement(targetElement, referenceAttribute.Value);
                    referenceChanged = true;
                }
                if (referenceChanged)
                {
                    referenceElement.Attribute("reference").Value = XPathHelper.GetXPath(referenceElement, targetElement);
                }

            }
        }

        protected void UnifyObjectReferences(XDocument document)
        {
            var listNodes = document.Descendants("objectList").ToList();
            SwapReferencesInList(document, listNodes);
            RemoveSelfReferences(document);
            //ResolveReferencesToReferences(document);
        }

        protected void UnifySoundReferences(XDocument document)
        {
            var listNodes = document.Descendants("soundList").ToList();
            SwapReferencesInList(document, listNodes);
            //ResolveReferencesToReferences(document);
        }

        protected void UnifyLookReferences(XDocument document)
        {
            // seems unnecessary since look list lies before any possible references
        }

        protected void UnifyVariableReferences(XDocument document)
        {
            var globalVariableListNodes = document.Descendants("programVariableList").ToList();
            SwapReferencesInList(document, globalVariableListNodes);
            //ResolveReferencesToReferences(document);
            var objectVariableList = document.Descendants("objectVariableList").ToArray();

            if (!objectVariableList.Any()) return;
            var localVariableListNodes = objectVariableList[0].Descendants("list").ToList();
            SwapReferencesInList(document, localVariableListNodes);
            //ResolveReferencesToReferences(document);
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
            //RestoreReferences(document);
        }

        protected void RestoreReferences(XDocument document)
        {
            RestoreObjectReferences(document);
        }

        protected void RestoreObjectReferences(XDocument document)
        {
            var swappedReferences = SwapDefinitionsToFirstReferences(document);
            InsertSelfReferences(document, swappedReferences);
            ResolveReferencesToReferences(document);
        }

        protected List<XElement> SwapDefinitionsToFirstReferences(XDocument document)
        {
            var allElements = document.Descendants("objectList").ToArray()[0].Descendants().ToList();
            var referenceElements = (from a in allElements
                                     where a.Attribute("reference") != null
                                     select a).ToList();
            var swappedReferences = new List<XElement>();
            foreach (var referenceElement in referenceElements)
            {
                var referenceAttribute = referenceElement.Attribute("reference");
                var targetElement = XPathHelper.GetElement(referenceElement, referenceAttribute.Value);
                if (allElements.IndexOf(targetElement) < allElements.IndexOf(referenceElement)) continue;
                if (targetElement.Attribute("reference") != null) continue;
                referenceElement.ReplaceNodes(targetElement.Elements());
                var newReferencePath = XPathHelper.GetXPath(targetElement, referenceElement);
                targetElement.SetAttributeValue("reference", newReferencePath);
                referenceElement.SetAttributeValue("reference", null);
                targetElement.RemoveNodes();
                swappedReferences.Add(targetElement);
            }
            return swappedReferences;
        }

        protected void InsertSelfReferences(XDocument document, List<XElement> swappedReferences)
        {
            var objectElements = document.Descendants("objectList").ToArray()[0].Elements().ToList();
            foreach (var objectElement in objectElements)
            {
                var referenceElements = objectElement.Descendants()
                    .Where(a => a.Attribute("reference") != null)
                    .Where(a => !swappedReferences.Contains(a))
                    .ToList();
                var element = objectElement;
                referenceElements.AddRange(
                    from a in swappedReferences
                    let referenceAttribute = a.Attribute("reference")
                    select XPathHelper.GetElement(a, referenceAttribute.Value)
                        into targetElement
                        where element.Descendants().Contains(targetElement)
                        select targetElement);
                foreach (var referenceElement in referenceElements)
                {
                    var newElement = new XElement(objectElement.Name);
                    newElement.SetAttributeValue("reference", XPathHelper.GetXPath(referenceElement, objectElement));
                    referenceElement.AddBeforeSelf(newElement);
                }
            }
        }


        #endregion
    }
}
