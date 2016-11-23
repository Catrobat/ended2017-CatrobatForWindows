using System;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using Catrobat.IDE.Core.Utilities.Helpers;
using Catrobat.IDE.Core.Xml.XmlObjects;
using Catrobat.IDE.Core.Xml.XmlObjects.Variables;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using Catrobat.IDE.Core;
using Catrobat.IDE.Core.Xml.XmlObjects.Bricks.Variables;

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
            var testObject =
                programm.VariableList.ObjectVariableList.ObjectVariableEntries[1].XmlSpriteReference.CreateXml()
                    .ToString();

            Assert.AreEqual(referenceObject, testObject);
        }


        [TestMethod]
        public void XmlObjectUservariableReferenceTest()
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

                    }
                }
            };

            TextReader sr = new StringReader("<objectList>" +
                                            "  <object name=\"Hintergrund\">" +
                                            "  </object>" +
                                            "  <object name=\"FlaPacMan\">" +
                                            "  <scriptList>" +
                                            "	<script type=\"BroadcastScript\">" +
                                            "     <brickList>" +
                                            "		<brick type=\"SetVariableBrick\">" +
                                            "            <formulaList>" +
                                            "              <formula category=\"VARIABLE\">" +
                                            "                <type>NUMBER</type>" +
                                            "                <value>1</value>" +
                                            "              </formula>" +
                                            "            </formulaList>" +
                                            "            <userVariable>paclives</userVariable>" +
                                            "          </brick>" +
                                            "          <brick type=\"SetVariableBrick\">" +
                                            "            <formulaList>" +
                                            "              <formula category=\"VARIABLE\">" +
                                            "                <type>NUMBER</type>" +
                                            "                <value>0.0</value>" +
                                            "              </formula>" +
                                            "            </formulaList>" +
                                            "            <userVariable>change anim</userVariable>" +
                                            "          </brick>" +
                                            "          <brick type=\"SetVariableBrick\">" +
                                            "            <formulaList>" +
                                            "              <formula category=\"VARIABLE\">" +
                                            "                <type>NUMBER</type>" +
                                            "                <value>1</value>" +
                                            "              </formula>" +
                                            "            </formulaList>" +
                                            "            <userVariable>animframe</userVariable>" +
                                            "          </brick>" +
                                            "       </brickList>" +
                                            "     </script>" +
                                            "   </scriptList>" +
                                            "  </object>" +
                                            "  <object name =\"handmade referenceobject\">" +
                                            "  <scriptList>" +
                                            "	<script type=\"BroadcastScript\">" +
                                            "     <brickList>" +
                                            "       <brick type =\"ChangeVariableBrick\">" +
                                            "            <formulaList>" +
                                            "              <formula category=\"VARIABLE_CHANGE\">" +
                                            "                <type>NUMBER</type>" +
                                            "                <value>7</value>" +
                                            "              </formula>" +
                                            "            </formulaList>" +
                                            "            <userVariable reference=\"../../../../../../object[2]/scriptList/script/brickList/brick[3]/userVariable\" />" +
                                            "       </brick>" +
                                            "      </brickList > " +
                                            "     </script>" +
                                            "   </scriptList>" +
                                            "  </object>" + 
                                            "</objectList>");
            var objectList = XElement.Load(sr);

            programm.SpriteList.LoadFromXml(objectList);
            
            XmlParserTempProjectHelper.Program = programm;

            //change these 3 to get differnt deep references as you are specifying the "iterator" in the imaginary XML-Docucment
            XmlParserTempProjectHelper.currentObjectNum = 3; 
            XmlParserTempProjectHelper.currentBrickNum = 1;
            XmlParserTempProjectHelper.currentVariableNum = 1;

            //the variable you wanna reference to from the position of the "iterator"  - ! it has to exist in the above XML-snippet but you can also modify this if you know what you are doing! 
            var userVariableRefTest = new XmlUserVariableReference(); 
            userVariableRefTest.UserVariable = ((XmlSetVariableBrick) programm.SpriteList.Sprites[1].Scripts.Scripts[0].Bricks.Bricks[2]).UserVariable;
            userVariableRefTest.UserVariable.ObjectNum = 2;
            userVariableRefTest.UserVariable.ScriptNum = 1;
            userVariableRefTest.UserVariable.BrickNum = 3;
            userVariableRefTest.UserVariable.VariableNum = 1;

            String testString = userVariableRefTest.CreateXml().ToString();

            //string of variable reference from xml-snippet
            //be in mind to adapt the deep accordingly to your changes above
            //if you change the refence be in mind that in the Catrobat XML language (at leats until 0.94) counting starts at 1 not at 0 and [1]'s are not shown/written 
            var referenceXElement = objectList.Descendants(XmlConstants.UserVariable).Where(uservar => uservar.HasAttributes).First();
            String referenceString = referenceXElement.ToString();//should be "<userVariable reference=\"../../../../../../object[2]/scriptList/script/brickList/brick[3]/userVariable\" />";
            Assert.AreEqual(referenceString, testString, "XmlObjectUservariableReferenceTest failed at the CreateXML part");

            var userVariableRefLoaded = new XmlUserVariableReference();
            userVariableRefLoaded.LoadFromXml(referenceXElement);
            userVariableRefLoaded.UserVariable.ObjectNum = 2;
            userVariableRefLoaded.UserVariable.ScriptNum = 1;
            userVariableRefLoaded.UserVariable.BrickNum = 3;
            userVariableRefLoaded.UserVariable.VariableNum = 1;

            Assert.AreEqual(userVariableRefLoaded.UserVariable, userVariableRefTest.UserVariable, "XmlObjectUservariableReferenceTest failed at the LoadXML part");
            


        }


    }
}
