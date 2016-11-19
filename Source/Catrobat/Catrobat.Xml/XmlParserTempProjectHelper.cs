using System;
using Catrobat.IDE.Core.Xml.XmlObjects;
using Catrobat.IDE.Core.Xml.XmlObjects.Scripts;
using System.Xml.Linq;

namespace Catrobat.IDE.Core.Utilities.Helpers
{
    [Obsolete("Pfui!")]
    public static class XmlParserTempProjectHelper
    {
        public static XDocument Document { get; set; }
        
        public static XmlProgram Program { get; set; }

        public static XmlSprite Sprite { get; set; }

        public static XmlScript Script { get; set; }


        #region for references

        public static uint currentObjectNum = 0;
        public static uint currentScriptNum = 0;
        public static uint currentBrickNum = 0;
        public static uint currentVariableNum = 0;

        public static bool inObjectVarList = false;
        public static bool inProgramVarList = false;

        #endregion

    }
}
