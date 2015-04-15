using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using Catrobat.IDE.Core.Utilities.Helpers;

namespace Catrobat.IDE.Core.Xml.VersionConverter.Versions
{
    public class CatrobatVersion093To092 : CatrobatVersion
    {
        public override CatrobatVersionPair CatrobatVersionPair
        {
            get
            {
                return new CatrobatVersionPair("0.93", "0.92");
            }
        }

        #region Convert

        protected override void ConvertStructure(XDocument document)
        {
            // TODO: implement me
        }

        #endregion

        #region Convert back

        protected override void ConvertBackStructure(XDocument document)
        {
            // TODO: implement me
        }

        #endregion
    }
}
