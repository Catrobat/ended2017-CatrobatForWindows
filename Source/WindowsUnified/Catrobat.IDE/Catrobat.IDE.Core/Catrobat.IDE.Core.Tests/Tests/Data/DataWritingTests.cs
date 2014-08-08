using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using Catrobat.IDE.Core.ExtensionMethods;
using Catrobat.IDE.Core.Models;
using Catrobat.IDE.Core.Models.Bricks;
using Catrobat.IDE.Core.Models.Formulas.Tree;
using Catrobat.IDE.Core.Models.Scripts;
using Catrobat.IDE.Core.Services.Common;
using Catrobat.IDE.Core.Services.Storage;
using Catrobat.IDE.Core.Tests.Misc;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Catrobat.IDE.Core.Tests.Tests.Data
{
    [TestClass]
    public class DataWritingTests
    {
        [ClassInitialize()]
        public static void TestClassInitialize(TestContext testContext)
        {
            TestHelper.InitializeTests();
        }

        [TestMethod] // Test for Bug 511
        public async Task VariableReferenceTest()
        {
            var programGenerator = new ProgramGeneratorTestSimple();

            var program = programGenerator.GenerateProgram();

            var globatVariable = new GlobalVariable{Name = "GlobalVariable1"};
            var localVariable = new LocalVariable { Name = "LocalVariable1" };
            program.GlobalVariables.Add(globatVariable);
            program.Sprites[0].LocalVariables.Add(localVariable);

            var startScript = new StartScript();
            var localSetVariableBrick = new SetVariableBrick {Value = new FormulaNodeNumber {Value = 0.0}, Variable = localVariable};
            var globalSetVariableBrick = new SetVariableBrick { Value = new FormulaNodeNumber { Value = 0.0 }, Variable = globatVariable };
            startScript.Bricks.Add(localSetVariableBrick);
            startScript.Bricks.Add(globalSetVariableBrick);

            program.Sprites[0].Scripts.Add(startScript);


            var xmlString = program.ToXmlObject().ToXmlString();

            var document = XDocument.Load(new StringReader(xmlString));

            var localVariableElement = document.Descendants("userVariable").ElementAt(0);
            var localVariablePath = localVariableElement.Attributes("reference").ElementAt(0).Value;
            Assert.Equals("../../../../../../../variables/objectVariableList/entry[1]/list/userVariable[1]", localVariablePath);


            var globalVariableElement = document.Descendants("userVariable").ElementAt(1);
            var globalVariablePath = globalVariableElement.Attributes("reference").ElementAt(0).Value;
            Assert.Equals("../../../../../../../variables/programVariableList/userVariable[1]", globalVariablePath);

        }
    }
}
