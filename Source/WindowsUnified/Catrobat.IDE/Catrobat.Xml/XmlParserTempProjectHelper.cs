using System;
using Catrobat.IDE.Core.Xml.XmlObjects;
using Catrobat.IDE.Core.Xml.XmlObjects.Scripts;

namespace Catrobat.IDE.Core.Utilities.Helpers
{
    [Obsolete("Pfui!")]
    public static class XmlParserTempProjectHelper
    {
        public static XmlProgram Program { get; set; }

        public static XmlSprite Sprite { get; set; }

        public static XmlScript Script { get; set; }
    }
}
