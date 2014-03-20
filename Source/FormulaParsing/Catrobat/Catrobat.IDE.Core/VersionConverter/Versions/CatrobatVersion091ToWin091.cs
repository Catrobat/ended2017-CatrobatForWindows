using Catrobat.IDE.Core.Utilities.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace Catrobat.IDE.Core.VersionConverter.Versions
{
    public class CatrobatVersion091ToWin091 : CatrobatVersion
    {
        public override CatrobatVersionPair CatrobatVersionPair
        {
            get
            {
                return new CatrobatVersionPair("0.91", "Win0.91");
            }
        }

        #region Convert

        protected override void ConvertStructure(XDocument document)
        {
            ConvertObjects(document);
            ConvertLookBricks(document);
            ConvertSounds(document);
            ConvertVariables(document);
            ConvertForeverBricks(document);
            ConvertRepeatBricks(document);
            ConvertIfBricks(document);
            ConvertPointToBricks(document);
        }

        protected void ConvertObjects(XDocument document)
        {
            var program = document.Element("program");
            var objectList = program.Element("objectList");

            // move object definitions to objectList
            var objects = objectList.Elements("object");
            MoveDefinitionsToReference(objectList, objects);

            // move orphaned object definitions from objectVariableList to objectList
            var orphanedEntries = program.
                Elements("variables").
                Elements("objectVariableList").
                Elements("entry").
                Elements("object").
                Where(element => !element.HasReference()).
                ToList();
            foreach (var definition in orphanedEntries)
            {
                var reference = new XElement("object");
                objectList.Add(reference);
                reference.SetReferenceTarget(definition);
                MoveDefinitionToReference(program, definition, reference);
            }

            // remove object self references
            var selfReferences = program.Descendants("object").Where(reference =>
            {
                var target = reference.GetReferenceTarget();
                return target != null && target.Descendants().Contains(reference);
            }).ToList();
            foreach (var selfReference in selfReferences)
            {
                selfReference.Remove();
            }
        }

        protected void ConvertSounds(XDocument document)
        {
            var objectList = document.Element("program").Element("objectList");

            // move sound definitions to soundList
            var sounds = objectList.Descendants("soundList").Elements();
            MoveDefinitionsToReference(objectList, sounds);
        }

        protected void ConvertLookBricks(XDocument document)
        {
            // seems unnecessary since look list lies before any possible references
        }

        protected void ConvertVariables(XDocument document)
        {
            var program = document.Element("program");
            var variables = program.Element("variables");

            // move global variable definitions to programVariableList
            var globalVariables = variables.Elements("programVariableList").Elements("userVariable");
            MoveDefinitionsToReference(program, globalVariables);

            // move local variable definitions to objectVariableList
            var userVariables = variables.Elements("objectVariableList").Descendants("list").Elements("userVariable");
            MoveDefinitionsToReference(program, userVariables);
        }

        private static void ConvertForeverBricks(XDocument document)
        {
            var brickLists = document.Descendants("brickList").ToList();

            // move definition of loopEndBrick to loopEndlessBrick
            var loopEndBricks = brickLists.Descendants("loopEndBrick");
            MoveDefinitionsToReferenceAfterParent(brickLists, loopEndBricks, "loopEndlessBrick");
        }

        private static void ConvertRepeatBricks(XDocument document)
        {
            var brickLists = document.Descendants("brickList").ToList();

            // move definition of loopEndBrick to loopEndBrick
            var loopEndBricks = brickLists.Descendants("loopEndBrick");
            MoveDefinitionsToReferenceAfterParent(brickLists, loopEndBricks, "loopEndBrick");
        }

        private static void ConvertIfBricks(XDocument document)
        {
            var brickLists = document.Descendants("brickList").ToList();

            // move definition of ifElseBrick to ifLogicElseBrick
            var ifElseBricks = brickLists.Descendants("ifElseBrick");
            MoveDefinitionsToReferenceAfterParent(brickLists, ifElseBricks, "ifLogicElseBrick");

            // move definition of ifEndBrick to ifLogicEndBrick
            var ifEndBricks = brickLists.Descendants("ifEndBrick");
            MoveDefinitionsToReferenceAfterParent(brickLists, ifEndBricks, "ifLogicEndBrick");
        }

        protected void ConvertPointToBricks(XDocument document)
        {
            var objectList = document.Element("program").Element("objectList");

            // rename pointedObject to object 
            var pointedObjects = objectList.Descendants("pointToBrick").Descendants("pointedObject");
            foreach (var pointedObject in pointedObjects)
            {
                pointedObject.Name = "object";
            }
        }

        #endregion

        #region Convert back

        protected override void ConvertBackStructure(XDocument document)
        {
            var program = document.Descendants("program").ToList();
            MoveDefinitionsToFirstOccurence(program, program.Descendants());

            //TODO: convert back remaining things (see ConvertStructure)
        }

        protected static void MoveDefinitionsToFirstOccurence(List<XElement> rootElements, IEnumerable<XElement> elements)
        {
            var definitions = elements.Where(element => !element.HasReference());
            var pendingElements = definitions.
                Select(definition => new
                {
                    Definition = definition, 
                    FirstOccurence = rootElements.Descendants().
                        FirstOrDefault(element => 
                            element == definition || 
                            element.GetReferenceTarget() == definition)
                }).
                Where(x => x.Definition != x.FirstOccurence);

            // run query every time instead of .ToList() and foreach
            // because elements may have been moved in previous iteration
            AdaptiveForEach(
                elements: pendingElements,
                body: element => MoveDefinitionToReference(rootElements, element.Definition, element.FirstOccurence));
        }

        #endregion

        #region move nodes

        protected static void MoveDefinitionsToReference(XElement rootElement, IEnumerable<XElement> elements)
        {
            MoveDefinitionsToReference(Enumerable.Repeat(rootElement, 1).ToList(), elements);
        }

        protected static void MoveDefinitionsToReference(List<XElement> rootElements, IEnumerable<XElement> references)
        {
            var pendingDefinitions = references.
                Select(reference => new
                {
                    Definition = reference.GetReferenceTarget(),
                    Reference = reference
                }).
                Where(x => x.Definition != null);

            // run query every time instead of .ToList() and foreach
            // because elements may have been moved in previous iteration
            AdaptiveForEach(
                elements: pendingDefinitions,
                body: x => MoveDefinitionToReference(rootElements, x.Definition, x.Reference));
        }

        protected static void MoveDefinitionsToReferenceAfterParent(List<XElement> rootElements, IEnumerable<XElement> elements, string name)
        {
            var definitions = elements.Where(element => !element.HasReference());
            var pendingElements = definitions.
                Select(definition =>
                new
                {
                    Definition = definition,
                    Reference = definition.Parent.ElementsAfterSelf(name).
                        FirstOrDefault(element => element.GetReferenceTarget() == definition)
                }).
                Where(x => x.Reference != null);

            // run query every time instead of .ToList() and foreach
            // because elements may have been moved in previous iteration
            AdaptiveForEach(
                elements: pendingElements,
                body: element => MoveDefinitionToReference(rootElements, element.Definition, element.Reference));
        }


        protected static void MoveDefinitionToReference(XElement rootElement, XElement definition, XElement reference)
        {
            MoveDefinitionToReference(Enumerable.Repeat(rootElement, 1).ToList(), definition, reference);
        }

        protected static void MoveDefinitionToReference(List<XElement> rootElements, XElement definition, XElement reference)
        {
            reference.Attribute("reference").Remove();
            MoveNodes(rootElements, definition, reference);
            definition.SetReferenceTarget(reference);
        }

        protected static void MoveNodes(List<XElement> rootElements, XElement elementFrom, XElement elementTo)
        {
            // copy elements and attributes
            elementTo.ReplaceNodes(elementFrom.Nodes());
            var movedElements = elementFrom.DescendantsAndSelf().
                Zip(elementTo.DescendantsAndSelf(), (oldElement, newElement) => new
                {
                    OldElement = oldElement,
                    NewElement = newElement
                }).ToList();

            // update references of moved elements
            var movedReferenceElements = movedElements.
                Where(movedElement => movedElement.NewElement.HasReference()).
                ToList();
            foreach (var movedElement in movedReferenceElements)
            {
                var target = movedElement.OldElement.GetReferenceTarget();
                movedElement.NewElement.SetReferenceTarget(target);

            }

            // update references to moved elements
            var outdatedReferenceElements = rootElements.Descendants().
                Where(element => element.HasReference()).
                Join(movedElements,
                outerKeySelector: referenceElement => referenceElement.GetReferenceTarget(),
                innerKeySelector: movedElement => movedElement.OldElement,
                resultSelector: (referenceElement, movedElement) => new
                {
                    ReferenceElement = referenceElement,
                    NewTarget = movedElement.NewElement
                }).ToList();
            foreach (var outdatedElement in outdatedReferenceElements)
            {
                outdatedElement.ReferenceElement.SetReferenceTarget(outdatedElement.NewTarget);
            }

            // delete copied nodes
            elementFrom.RemoveNodes();
        }

        #endregion

        /// <summary>
        /// Adaptive version of a foreach-loop 
        /// that supports and requires modification of <paramref name="elements">the source</paramref> 
        /// in <paramref name="body">the loop's body</paramref>. 
        /// </summary>
        /// <param name="elements">The elements to loop through. </param>
        /// <param name="body">The loop's body. </param>
        protected static void AdaptiveForEach<TElement>(IEnumerable<TElement> elements, Action<TElement> body) where TElement : class
        {
            while (true)
            {
                // ReSharper disable once PossibleMultipleEnumeration
                var element = elements.FirstOrDefault();
                if (element == null) break;
                body(element);
            }
        }

        [Obsolete("Not used any more. ")]
        protected void ResolveReferencesToReferences(XDocument document)
        {
            foreach (var element in document.Descendants())
            {
                var target = element;
                var referenceAttribute = element.Attribute("reference");
                var referencesCount = 0;
                while (referenceAttribute != null)
                {
                    target = XPathHelper.GetElement(target, referenceAttribute.Value);
                    referenceAttribute = target == null ? null : target.Attribute("reference");
                    referencesCount++;
                }
                if (target != null && referencesCount > 1)
                {
                    element.SetReferenceTarget(target);
                }
            }
        }
    }
}
