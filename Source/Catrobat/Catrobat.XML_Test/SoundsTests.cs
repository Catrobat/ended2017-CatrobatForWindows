using System.Xml.Linq;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using Catrobat.IDE.Core;
using Catrobat.IDE.Core.Xml.XmlObjects.Formulas;
using System.IO;
using Catrobat.IDE.Core.Xml.XmlObjects.Bricks.Sounds;

namespace Catrobat.XML_Test
{
    [TestClass]
    public class SoundsTests
    {
        [TestMethod]
        public void XmlChangeVolumeByBrickTest()
        {
            TextReader sr = new StringReader("<brick type=\"ChangeVolumeByNBrick\"> <formulaList> <formula category=\"VOLUME_CHANGE\"> <rightChild> <type>NUMBER</type> <value>80</value> </rightChild> <type>OPERATOR</type> <value>MINUS</value> </formula> </formulaList> </brick>");
            var xRoot = XElement.Load(sr);

            var testObject = new XmlChangeVolumeByBrick(xRoot);

            var referenceObject = new XmlChangeVolumeByBrick()
            {
                Volume = new XmlFormula(xRoot, XmlConstants.VolumeChange),
            };

            Assert.AreEqual(referenceObject, testObject);
        }

                [TestMethod]
        public void XmlSetVolumeToBrickTest()
        {
            TextReader sr = new StringReader("<brick type=\"SetVolumeToBrick\"> <formulaList> <formula category=\"VOLUME\"> <type>NUMBER</type> <value>60.0</value> </formula> </formulaList> </brick>");
            var xRoot = XElement.Load(sr);

            var testObject = new XmlSetVolumeToBrick(xRoot);

            var referenceObject = new XmlSetVolumeToBrick()
            {
                Volume = new XmlFormula(xRoot, XmlConstants.Volume),
            };

            Assert.AreEqual(referenceObject, testObject);
        }
    }
}
