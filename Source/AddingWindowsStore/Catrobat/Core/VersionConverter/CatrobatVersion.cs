using System.Collections.Generic;
using System.Xml.Linq;

namespace Catrobat.Core.VersionConverter
{
    public abstract class CatrobatVersion
    {
        public abstract CatrobatVersionPair CatrobatVersionPair { get; }

        #region Convert

        public void Convert(XDocument document)
        {
            ConvertStructure(document);
        }

        protected abstract void ConvertStructure(XDocument document);

        #endregion

        #region Convert back

        public void ConvertBack(XDocument document)
        {
            ConvertBackStructure(document);

            ConvertBackRemoveProperties(document);
            ConvertBackRemoveElements(document);
        }

        protected abstract void ConvertBackRemoveElements(XDocument document);

        protected abstract void ConvertBackRemoveProperties(XDocument document);

        protected abstract void ConvertBackStructure(XDocument document);

        #endregion

    }
}
