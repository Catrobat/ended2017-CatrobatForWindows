using System.Xml.Linq;

namespace Catrobat.IDE.Core.Xml.VersionConverter
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
        }

        protected abstract void ConvertBackStructure(XDocument document);

        #endregion

    }
}
