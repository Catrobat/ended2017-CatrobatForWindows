using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using Catrobat.IDE.Core.Utilities.Helpers;

namespace Catrobat.IDE.Core.Xml.VersionConverter.Versions
{
    public class CatrobatVersion092To091 : CatrobatVersion
    {
        public override CatrobatVersionPair CatrobatVersionPair
        {
            get
            {
                return new CatrobatVersionPair("0.92", "0.91");
            }
        }

        #region Convert

        protected override void ConvertStructure(XDocument document)
        {
   
        }

        #endregion

        #region Convert back

        protected override void ConvertBackStructure(XDocument document)
        {
 
        }

        #endregion
    }
}
