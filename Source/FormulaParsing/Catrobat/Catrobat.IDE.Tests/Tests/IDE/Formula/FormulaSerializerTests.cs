using Catrobat.IDE.Core.FormulaEditor.Editor;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Catrobat.IDE.Tests.Tests.IDE.Formula
{
    [TestClass]
    public class FormulaSerializerTests
    {
        private readonly FormulaSerializer _serializer = new FormulaSerializer();

        [TestMethod]
        public void FormulaSerializerTests_Null()
        {
            Assert.AreEqual(null, _serializer.Serialize(null));
        }

        #region numbers

        [TestMethod]
        public void FormulaSerializerTests_Pi()
        {
            Assert.AreEqual("pi", _serializer.Serialize(FormulaTreeFactory.CreatePiNode()));
        }

        #endregion

        #region arithmetic

        [TestMethod]
        public void FormulaSerializerTests_Plus()
        {
            Assert.Inconclusive();
        }

        #endregion

    }
}
