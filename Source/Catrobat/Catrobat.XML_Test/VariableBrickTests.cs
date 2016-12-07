using System.IO;
using System.Xml.Linq;
using Catrobat.IDE.Core.Xml.XmlObjects.Bricks.Variables;
using Catrobat.IDE.Core.Xml.XmlObjects.Formulas;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;

namespace Catrobat.XML_Test
{
    [TestClass]
    public class VariableBrickTests
    {
        [TestMethod]
        public void ChangeVariableBrickHasToAllowNullReferenceOfUserVariableAsInitialBrick()
        {
            TextReader sr = new StringReader("<brick type=\"ChangeVariableBrick\"><formulaList><formula category=\"VARIABLE_CHANGE\"><type>NUMBER</type><value>2</value></formula></formulaList></brick>");
            var xRoot = XElement.Load(sr);

            var testObject = new XmlChangeVariableBrick(xRoot);

            var referenceObject = new XmlChangeVariableBrick()
            {
                VariableFormula = new XmlFormula()
                {
                    FormulaTree = new XmlFormulaTree()
                    {
                        VariableType = "NUMBER",
                        VariableValue = "2"
                    }
                },
                UserVariable = null
            };

            Assert.AreEqual(referenceObject.CreateXml().ToString(), testObject.CreateXml().ToString());
        }

        [TestMethod]
        public void SetVariableBrickHasToAllowNullReferenceOfUserVariableAsInitialBrick()
        {
            TextReader sr = new StringReader("<brick type=\"SetVariableBrick\"><formulaList><formula category=\"VARIABLE\"><type>NUMBER</type><value>2</value></formula></formulaList></brick>");
            var xRoot = XElement.Load(sr);

            var testObject = new XmlSetVariableBrick(xRoot);

            var referenceObject = new XmlSetVariableBrick()
            {
                VariableFormula = new XmlFormula()
                {
                    FormulaTree = new XmlFormulaTree()
                    {
                        VariableType = "NUMBER",
                        VariableValue = "2"
                    }
                },
                UserVariable = null
            };

            Assert.AreEqual(referenceObject.CreateXml().ToString(), testObject.CreateXml().ToString());
        }
    }
}
