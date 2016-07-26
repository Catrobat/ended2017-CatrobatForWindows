using System.Xml.Linq;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using Catrobat.IDE.Core;
using Catrobat.IDE.Core.Xml.XmlObjects.Formulas;
using System.IO;
using Catrobat.IDE.Core.Xml.XmlObjects.Bricks.ControlFlow;

namespace Catrobat.XML_Test
{
    [TestClass]
    public class ControlFlowTests
    {
        [TestMethod]
        public void XmlBroadcastBrickTest()
        {
            TextReader sr = new StringReader("<brick type=\"BroadcastBrick\"><broadcastMessage>Initialize!</broadcastMessage></brick>");
            var xRoot = XElement.Load(sr);

            var testObject = new XmlBroadcastBrick(xRoot);

            var referenceObject = new XmlBroadcastBrick()
            {
                BroadcastMessage = "Initialize!"
            };

            Assert.AreEqual(referenceObject, testObject);
        }

        [TestMethod]
        public void XmlBroadcastWaitBrickTest()
        {
            TextReader sr = new StringReader("<brick type=\"BroadcastWaitBrick\"><broadcastMessage>Move!</broadcastMessage></brick>");
            var xRoot = XElement.Load(sr);

            var testObject = new XmlBroadcastWaitBrick(xRoot);

            var referenceObject = new XmlBroadcastWaitBrick()
            {
                BroadcastMessage = "Move!"
            };

            Assert.AreEqual(referenceObject, testObject);
        }

        [TestMethod]
        public void XmlForeverBrickTest()
        {
            TextReader sr = new StringReader("<brick type=\"ForeverBrick\"></brick>");
            var xRoot = XElement.Load(sr);

            var testObject = new XmlForeverBrick(xRoot);

            var referenceObject = new XmlForeverBrick();

            Assert.AreEqual(referenceObject, testObject);
        }


    }
}
