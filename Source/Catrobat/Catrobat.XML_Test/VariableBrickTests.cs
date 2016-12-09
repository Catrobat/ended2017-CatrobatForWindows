using System.IO;
using System.Xml.Linq;
using Catrobat.IDE.Core.Xml.XmlObjects.Bricks.Variables;
using Catrobat.IDE.Core.Xml.XmlObjects.Formulas;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using Catrobat.IDE.Core.Models;
using Catrobat.IDE.Core.Models.Bricks;
using Catrobat.IDE.Core.Models.Scripts;
using Catrobat.IDE.Core.Utilities.Helpers;

namespace Catrobat.XML_Test
{
    [TestClass]
    public class VariableBrickTests
    {

        [TestMethod]
        public void AssignedVariablesShouldBeNullAfterDeletingVariables()
        {
            var testprog = new Program();

            Sprite testsprite = new Sprite();
            testprog.Sprites.Add(testsprite);

            var varbrick = new SetVariableBrick();
            var chnbrick = new ChangeVariableBrick();
            var startscript = new StartScript();

            startscript.Bricks.Add(varbrick);
            startscript.Bricks.Add(chnbrick);
            testprog.Sprites[0].Scripts.Add(startscript);

            var newGlobalVariable = new GlobalVariable { Name = "GlobalTestVariable" };
            testprog.GlobalVariables.Add(newGlobalVariable);
            varbrick.Variable = newGlobalVariable;
            chnbrick.Variable = newGlobalVariable;

            Assert.AreNotEqual(varbrick.Variable, null);
            Assert.AreNotEqual(chnbrick.Variable, null);
            Assert.AreNotEqual(testprog.GlobalVariables.Count, 0);

            VariableHelper.DeleteGlobalVariable(testprog, newGlobalVariable);

            Assert.AreEqual(varbrick.Variable, null);
            Assert.AreEqual(chnbrick.Variable, null);
            Assert.AreEqual(testprog.GlobalVariables.Count, 0);

            var newLocalVariable = new LocalVariable { Name = "LocalTestVariable" };
            testsprite.LocalVariables.Add(newLocalVariable);
            varbrick.Variable = newLocalVariable;
            chnbrick.Variable = newLocalVariable;

            Assert.AreNotEqual(varbrick.Variable, null);
            Assert.AreNotEqual(chnbrick.Variable, null);
            Assert.AreNotEqual(testsprite.LocalVariables.Count, 0);

            VariableHelper.DeleteLocalVariable(testprog, testsprite, newLocalVariable);

            Assert.AreEqual(varbrick.Variable, null);
            Assert.AreEqual(chnbrick.Variable, null);
            Assert.AreEqual(testsprite.LocalVariables.Count, 0);
        }

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
    }
}
