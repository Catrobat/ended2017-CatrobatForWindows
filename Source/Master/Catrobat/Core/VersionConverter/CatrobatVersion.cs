using System.Collections.Generic;
using System.Xml.Linq;

namespace Catrobat.Core.VersionConverter
{
    public abstract class CatrobatVersion
    {
        public abstract CatrobatVersionPair CatrobatVersionPair { get; }

        protected abstract List<CatrobatVersionPropertyRenameEntry> AttributesToRename { get; }

        protected abstract List<CatrobatVersionElementRenameEntry> ElementsToRename { get; }

        #region Convert

        public void Convert(XDocument document)
        {
            ConvertRemoveElements(document);
            ConvertRemoveProperties(document);

            foreach (var renameElementEntry in ElementsToRename)
            {
                foreach (var element in document.Elements(renameElementEntry.ElementName))
                {
                    element.Name = renameElementEntry.NewElementName;
                }
            }

            foreach (var renameAttributeEntry in AttributesToRename)
            {
                foreach (var element in document.Elements(renameAttributeEntry.ElementName))
                {
                    foreach (var attribute in element.Attributes(renameAttributeEntry.PropertyName))
                        element.Attribute(renameAttributeEntry.NewPropertyName);
                }
            }

            ConvertStructure(document);
        }

        protected abstract void ConvertRemoveElements(XDocument document);

        protected abstract void ConvertRemoveProperties(XDocument document);

        protected abstract void ConvertStructure(XDocument document);

        #endregion

        #region Convert back

        public void ConvertBack(XDocument document)
        {
            ConvertBackStructure(document);

            foreach (var renameElementEntry in ElementsToRename)
            {
                foreach (var element in document.Elements(renameElementEntry.NewElementName))
                {
                    element.Name = renameElementEntry.ElementName;
                }
            }

            foreach (var renameAttributeEntry in AttributesToRename)
            {
                foreach (var element in document.Elements(renameAttributeEntry.ElementName))
                {
                    foreach (var attribute in element.Attributes(renameAttributeEntry.NewPropertyName))
                        element.Attribute(renameAttributeEntry.PropertyName);
                }
            }


            ConvertBackRemoveProperties(document);
            ConvertBackRemoveElements(document);
        }

        protected abstract void ConvertBackRemoveElements(XDocument document);

        protected abstract void ConvertBackRemoveProperties(XDocument document);

        protected abstract void ConvertBackStructure(XDocument document);

        #endregion

    }
}
