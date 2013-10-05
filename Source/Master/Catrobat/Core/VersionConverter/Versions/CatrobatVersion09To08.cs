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
    public class CatrobatVersion09To08 : CatrobatVersion
    {
        public override CatrobatVersionPair CatrobatVersionPair
        {
            get
            {
                return new CatrobatVersionPair("0.9", "0.8");
            }
        }
        #region Convert

        protected override void ConvertStructure(XDocument document)
        {
            // TODO: add conversion code if needed
        }

        protected override void ConvertBackRemoveProperties(XDocument document)
        {
            // TODO: add conversion code if needed
        }

        #endregion

        #region Convert back

        protected override void ConvertBackRemoveElements(XDocument document)
        {
            // TODO: add conversion code if needed
        }

        protected override void ConvertBackStructure(XDocument document)
        {
            // TODO: add conversion code if needed
        }

        #endregion
    }
}
