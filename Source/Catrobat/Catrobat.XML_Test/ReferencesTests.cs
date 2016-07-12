using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using Catrobat.IDE.Core.Utilities.Helpers;
using Catrobat.IDE.Core.Xml.XmlObjects;
using Catrobat.IDE.Core.Xml.XmlObjects.Variables;
using System.Collections.Generic;

namespace Catrobat.XML_Test
{
    [TestClass]
    public class ReferencesTests
    {
        [TestMethod]
        public void XmlSpriteReferencesTest()
        {
            XmlProgram programm = new XmlProgram()
            {
                SpriteList = new XmlSpriteList()
                {
                    Sprites = new List<XmlSprite>(),
                },
                VariableList = new XmlVariableList() 
                {
                    ObjectVariableList = new XmlObjectVariableList()
                    {
                        ObjectVariableEntries = new List<XmlObjectVariableEntry>(),
                    }
                }
            };

            programm.SpriteList.Sprites.Add(new XmlSprite());
            programm.SpriteList.Sprites.Add(new XmlSprite());

            programm.VariableList.ObjectVariableList.ObjectVariableEntries.Add(new XmlObjectVariableEntry());
            programm.VariableList.ObjectVariableList.ObjectVariableEntries.Add(new XmlObjectVariableEntry());

            XmlParserTempProjectHelper.Program = programm;


            programm.VariableList.ObjectVariableList.ObjectVariableEntries[0].Sprite = programm.SpriteList.Sprites[0];
            programm.VariableList.ObjectVariableList.ObjectVariableEntries[1].Sprite = programm.SpriteList.Sprites[1];

            var referenceObject = "<object reference=\"../../../../objectList/object[2]\" />";
            var testObject = programm.VariableList.ObjectVariableList.ObjectVariableEntries[1].XmlSpriteReference.CreateXml().ToString();

            Assert.AreEqual(referenceObject, testObject);
        }
    }
}
