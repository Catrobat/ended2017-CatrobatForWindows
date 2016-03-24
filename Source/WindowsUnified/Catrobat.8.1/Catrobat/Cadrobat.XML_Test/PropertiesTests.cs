using System;
using System.Xml.Linq;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using Catrobat.IDE.Core.Xml.XmlObjects.Bricks.Properties;
using Catrobat.IDE.Core;
using Catrobat.IDE.Core.Xml.XmlObjects.Formulas;
using System.IO;

namespace Catrobat.XML_Test
{
    [TestClass]
    public class PropertiesTest
    {
        [TestMethod]
        public void XmlPlaceAtBrickTest()
        {
            TextReader sr = new StringReader("<brick type=\"PlaceAtBrick\">  <formulaList>  <formula category=\"Y_POSITION\"> <rightChild> <type>NUMBER</type> <value>80</value> </rightChild> <type>OPERATOR</type> <value>MINUS</value>  </formula>  <formula category=\"X_POSITION\"> <rightChild> <type>NUMBER</type> <value>115</value> </rightChild> <type>OPERATOR</type> <value>MINUS</value>  </formula>  </formulaList>  </brick>");
            var xRoot = XElement.Load(sr);

            var testObject = new XmlPlaceAtBrick(xRoot);

            var referenceObject = new XmlPlaceAtBrick()
            {
                XPosition = new XmlFormula(xRoot, XmlConstants.XPosition),
                YPosition = new XmlFormula(xRoot, XmlConstants.YPosition),
            };

            Assert.AreEqual(referenceObject, testObject);
        }
    }
}
