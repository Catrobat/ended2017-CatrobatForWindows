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

        [TestMethod]
        public void XmlForeverLoopEndBrick()
        {
            TextReader sr = new StringReader("<brick type=\"LoopEndlessBrick\"/>");
            var xRoot = XElement.Load(sr);

            var testObject = new XmlForeverLoopEndBrick(xRoot);

            var referenceObject = new XmlForeverLoopEndBrick();

            Assert.AreEqual(referenceObject, testObject);
        }

        [TestMethod]
        public void XmlIfLogicBeginBrickTest()
        {
            TextReader sr = new StringReader("<brick type=\"IfLogicBeginBrick\"><formulaList><formula category=\"IF_CONDITION\"><type>USER_VARIABLE</type><value>Opponent was hit!</value></formula></formulaList></brick>");
            var xRoot = XElement.Load(sr);

            var testObject = new XmlIfLogicBeginBrick(xRoot);

            var referenceObject = new XmlIfLogicBeginBrick()
            {
                IfCondition = new XmlFormula(xRoot, XmlConstants.XmlIFCONDITION),
            };

            Assert.AreEqual(referenceObject, testObject);
        }

        [TestMethod]
        public void XmlIfLogicElseBrickTest()
        {
            TextReader sr = new StringReader("<brick type=\"IfLogicElseBrick\"/>");
            var xRoot = XElement.Load(sr);

            var testObject = new XmlForeverLoopEndBrick(xRoot);

            var referenceObject = new XmlForeverLoopEndBrick();

            Assert.AreEqual(referenceObject, testObject);
        }

        [TestMethod]
        public void XmlIfLogicEndBrickTest()
        {
            TextReader sr = new StringReader("<brick type=\"IfLogicEndBrick\"/>");
            var xRoot = XElement.Load(sr);

            var testObject = new XmlForeverLoopEndBrick(xRoot);

            var referenceObject = new XmlForeverLoopEndBrick();

            Assert.AreEqual(referenceObject, testObject);
        }

        [TestMethod]
        public void XmlRepeatBrickTest()
        {
            TextReader sr = new StringReader("<brick type=\"RepeatBrick\"><formulaList><formula category=\"TIMES_TO_REPEAT\"><type>NUMBER</type><value>50</value></formula></formulaList></brick>");
            var xRoot = XElement.Load(sr);

            var testObject = new XmlRepeatBrick(xRoot);

            var referenceObject = new XmlRepeatBrick()
            {
                TimesToRepeat = new XmlFormula(xRoot, XmlConstants.TimesToRepeat),
            };

            Assert.AreEqual(referenceObject, testObject);
        }

        [TestMethod]
        public void XmlRepeatLoopEndBrickTest()
        {
            TextReader sr = new StringReader("<brick type=\"LoopEndBrick\"/>");
            var xRoot = XElement.Load(sr);

            var testObject = new XmlRepeatLoopEndBrick(xRoot);

            var referenceObject = new XmlRepeatLoopEndBrick();

            Assert.AreEqual(referenceObject, testObject);
        }

        [TestMethod]
        public void XmlWaitBrickTest()
        {
            TextReader sr = new StringReader("<brick type=\"WaitBrick\"><formulaList><formula category=\"TIME_TO_WAIT_IN_SECONDS\"><type>NUMBER</type><value>1.0</value></formula></formulaList></brick>");
            var xRoot = XElement.Load(sr);

            var testObject = new XmlWaitBrick(xRoot);

            var referenceObject = new XmlWaitBrick()
            {
                TimeToWaitInSeconds = new XmlFormula(xRoot, XmlConstants.TimeToWaitInSeconds),
            };

            Assert.AreEqual(referenceObject, testObject);
        }
    }
}
