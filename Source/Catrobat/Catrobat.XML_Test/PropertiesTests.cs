using System;
using System.Xml.Linq;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using Catrobat.IDE.Core.Xml.XmlObjects.Bricks.Properties;
using Catrobat.IDE.Core;
using Catrobat.IDE.Core.Xml.XmlObjects.Formulas;
using System.IO;
using Catrobat.IDE.Core.Xml.XmlObjects;

namespace Catrobat.XML_Test
{
    [TestClass]
    public class PropertiesTest
    {
         [TestMethod]
        public void XmlChangeBrightnessBrickTest()
        {
            TextReader sr = new StringReader("<brick type=\"ChangeBrightnessByNBrick\"><formulaList><formula category=\"CHANGE_BRIGHTNESS\"><type>NUMBER</type><value>25</value></formula></formulaList></brick>");
            var xRoot = XElement.Load(sr);

            var testObject = new XmlChangeBrightnessBrick(xRoot);

            var referenceObject = new XmlChangeBrightnessBrick()
            {
                ChangeBrightness = new XmlFormula(xRoot, XmlConstants.ChangeBrightness),
            };

            Assert.AreEqual(referenceObject, testObject);
        }

         [TestMethod]
         public void XmlChangeGhostEffectBrickTest()
         {
             TextReader sr = new StringReader("<brick type=\"ChangeGhostEffectByNBrick\"><formulaList><formula category=\"TRANSPARENCY_CHANGE\"><type>NUMBER</type><value>2.5</value></formula></formulaList></brick>");
             var xRoot = XElement.Load(sr);

             var testObject = new XmlChangeGhostEffectBrick(xRoot);

             var referenceObject = new XmlChangeGhostEffectBrick()
             {
                 ChangeGhostEffect = new XmlFormula(xRoot, XmlConstants.ChangeGhostEffect),
             };

             Assert.AreEqual(referenceObject, testObject);
         }

         [TestMethod]
         public void XmlChangeSizeByNBrickTest()
         {
             TextReader sr = new StringReader("<brick type=\"ChangeSizeByNBrick\"> <formulaList> <formula category=\"SIZE_CHANGE\"> <type>NUMBER</type> <value>1.5</value> </formula> </formulaList> </brick>");
             var xRoot = XElement.Load(sr);

             var testObject = new XmlChangeSizeByNBrick(xRoot);

             var referenceObject = new XmlChangeSizeByNBrick()
             {
                 Size = new XmlFormula(xRoot, XmlConstants.SizeChange),
             };

             Assert.AreEqual(referenceObject, testObject);
         }

        [TestMethod]
         public void XmlChangeXByBrickTest()
         {
             TextReader sr = new StringReader("<brick type=\"ChangeXByNBrick\"> <formulaList> <formula category=\"X_POSITION_CHANGE\"> <type>USER_VARIABLE</type> <value>x speed</value> </formula> </formulaList> </brick>");
             var xRoot = XElement.Load(sr);

             var testObject = new XmlChangeXByBrick(xRoot);

             var referenceObject = new XmlChangeXByBrick()
             {
                 XMovement = new XmlFormula(xRoot, XmlConstants.XPositionChange),
             };

             Assert.AreEqual(referenceObject, testObject);
         }

        [TestMethod]
        public void XmlChangeYByBrickTest()
        {
            TextReader sr = new StringReader("<brick type=\"ChangeYByNBrick\"> <formulaList> <formula category=\"Y_POSITION_CHANGE\"> <type>USER_VARIABLE</type> <value>x speed</value> </formula> </formulaList> </brick>");
            var xRoot = XElement.Load(sr);

            var testObject = new XmlChangeYByBrick(xRoot);

            var referenceObject = new XmlChangeYByBrick()
            {
                YMovement = new XmlFormula(xRoot, XmlConstants.YPositionChange),
            };

            Assert. AreEqual(referenceObject, testObject);
        }

        [TestMethod]
        public void XmlClearGraphicEffectBrickTest()
        {
            TextReader sr = new StringReader("<brick type=\"ClearGraphicEffectBrick\"></brick>");
            var xRoot = XElement.Load(sr);

            var testObject = new XmlClearGraphicEffectBrick(xRoot);

            var referenceObject = new XmlClearGraphicEffectBrick(xRoot);

            Assert.AreEqual(referenceObject, testObject);
        }

        [TestMethod]
        public void XmlComeToFrontBrickTest()
        {
            TextReader sr = new StringReader("<brick type=\"ComeToFrontBrick\"/>");
            var xRoot = XElement.Load(sr);

            var testObject = new XmlComeToFrontBrick(xRoot);

            var referenceObject = new XmlComeToFrontBrick(xRoot);

            Assert.AreEqual(referenceObject, testObject);
        }

        [TestMethod]
        public void XmlGlideToBrickTest()
        {
            TextReader sr = new StringReader("<brick type=\"GlideToBrick\"> <formulaList> <formula category=\"Y_DESTINATION\"> <leftChild> <type>USER_VARIABLE</type> <value>Distance factor</value> </leftChild> <rightChild> <type>SENSOR</type> <value>Y_INCLINATION</value> </rightChild> <type>OPERATOR</type> <value>MULT</value> </formula> <formula category=\"X_DESTINATION\"> <leftChild> <type>USER_VARIABLE</type> <value>Distance factor</value> </leftChild> <rightChild> <type>SENSOR</type> <value>X_INCLINATION</value> </rightChild> <type>OPERATOR</type> <value>MULT</value> </formula> <formula category=\"DURATION_IN_SECONDS\"> <type>NUMBER</type> <value>1</value> </formula> </formulaList> </brick>");
            var xRoot = XElement.Load(sr);

            var testObject = new XmlGlideToBrick(xRoot);

            var referenceObject = new XmlGlideToBrick()
            {
                DurationInSeconds = new XmlFormula(xRoot, XmlConstants.DurationInSeconds),
                XDestination = new XmlFormula(xRoot, XmlConstants.XDestination),
                YDestination = new XmlFormula(xRoot, XmlConstants.YDestination),
            };

            Assert.AreEqual(referenceObject, testObject);
        }

        [TestMethod]
        public void XmlGoNStepsBackBrickTest()
        {
            TextReader sr = new StringReader("<brick type=\"GoNStepsBackBrick\"> <formulaList> <formula category=\"STEPS\"> <type>NUMBER</type> <value>2</value> </formula> </formulaList> </brick>");
            var xRoot = XElement.Load(sr);

            var testObject = new XmlGoNStepsBackBrick(xRoot);

            var referenceObject = new XmlGoNStepsBackBrick()
            {
                Steps = new XmlFormula(xRoot, XmlConstants.Steps),
            };

            Assert.AreEqual(referenceObject, testObject);
        }

        [TestMethod]
        public void XmlHideBrickTest()
        {
            TextReader sr = new StringReader("<brick type=\"HideBrick\"/>");
            var xRoot = XElement.Load(sr);

            var testObject = new XmlHideBrick(xRoot);

            var referenceObject = new XmlHideBrick(xRoot);

            Assert.AreEqual(referenceObject, testObject);
        }

        [TestMethod]
        public void XmlIfOnEdgeBounceBrickTest()
        {
            TextReader sr = new StringReader("<brick type=\"IfOnEdgeBounceBrick\"/>");
            var xRoot = XElement.Load(sr);

            var testObject = new XmlIfOnEdgeBounceBrick(xRoot);

            var referenceObject = new XmlIfOnEdgeBounceBrick(xRoot);

            Assert.AreEqual(referenceObject, testObject);
        }

        [TestMethod]
        public void XmlMoveNStepsBrickTest()
        {
            TextReader sr = new StringReader("<brick type=\"MoveNStepsBrick\"> <formulaList> <formula category=\"STEPS\"> <type>NUMBER</type> <value>20</value> </formula> </formulaList> </brick>");
            var xRoot = XElement.Load(sr);

            var testObject = new XmlMoveNStepsBrick(xRoot);

            var referenceObject = new XmlMoveNStepsBrick()
            {
                Steps = new XmlFormula(xRoot, XmlConstants.Steps),
            };

            Assert.AreEqual(referenceObject, testObject);
        }

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

        [TestMethod]
        public void XmlPointInDirectionBrickTest()
        {
            TextReader sr = new StringReader("<brick type=\"PointInDirectionBrick\"> <formulaList> <formula category=\"DEGREES\"> <type>USER_VARIABLE</type> <value>Airplane&apos;s direction</value> </formula> </formulaList> </brick>");
            var xRoot = XElement.Load(sr);

            var testObject = new XmlPointInDirectionBrick(xRoot);

            var referenceObject = new XmlPointInDirectionBrick()
            {
                Degrees = new XmlFormula(xRoot, XmlConstants.Degrees),
            };

            Assert.AreEqual(referenceObject, testObject);
        }

        [TestMethod]
        public void XmlPointToBrickReferencedObjectTest()
        {
            TextReader sr = new StringReader("<brick type=\"PointToBrick\"> <pointedObject reference=\"../../../../../../object[2]/scriptList/script[3]/brickList/brick/pointedObject\"/> </brick>");
            var xRoot = XElement.Load(sr);

            var testObject = new XmlPointToBrick(xRoot);

            TextReader spriteSr = new StringReader("<object name=\"Airplane\"> <lookList></lookList> <soundList> </soundList> <scriptList> <script type=\"WhenScript\"> <brickList> </brickList> </script> </scriptList> </object>");
            var SpriteXRoot = XElement.Load(spriteSr);

            testObject.PointedXmlSpriteReference.Sprite = new XmlSprite(SpriteXRoot);

            var referenceObject = new XmlPointToBrick()
            {
                PointedXmlSpriteReference = new XmlSpriteReference(xRoot.Element(XmlConstants.PointedObject)),
            };

            referenceObject.PointedXmlSpriteReference.Sprite = new XmlSprite(SpriteXRoot);

            Assert.AreEqual(referenceObject, testObject);
        }

        //TODO: implement
        /*[TestMethod]
        public void XmlPointToBrickRealObjectTest()
         */

        [TestMethod]
        public void XmlSetBrightnessBrickTest()
        {
            TextReader sr = new StringReader("<brick type=\"SetBrightnessBrick\">  <formulaList>  <formula category=\"BRIGHTNESS\">  <type>NUMBER</type>  <value>50.0</value>  </formula>  </formulaList>  </brick>");
            var xRoot = XElement.Load(sr);

            var testObject = new XmlSetBrightnessBrick(xRoot);

            var referenceObject = new XmlSetBrightnessBrick()
            {
                Brightness = new XmlFormula(xRoot, XmlConstants.Brightness),
            };

            Assert.AreEqual(referenceObject, testObject);
        }

        [TestMethod]
        public void XmlSetGhostEffectBrickTest()
        {
            TextReader sr = new StringReader("<brick type=\"SetGhostEffectBrick\">  <formulaList>  <formula category=\"TRANSPARENCY\">  <type>NUMBER</type>  <value>0</value>  </formula>  </formulaList>  </brick>");
            var xRoot = XElement.Load(sr);

            var testObject = new XmlSetGhostEffectBrick(xRoot);

            var referenceObject = new XmlSetGhostEffectBrick()
            {
                Transparency = new XmlFormula(xRoot, XmlConstants.Transparency),
            };

            Assert.AreEqual(referenceObject, testObject);
        }

        [TestMethod]
        public void XmlSetSizeToBrickTest()
        {
            TextReader sr = new StringReader("<brick type=\"SetSizeToBrick\">  <formulaList>  <formula category=\"SIZE\">  <type>NUMBER</type>  <value>20</value>  </formula>  </formulaList>  </brick>");
            var xRoot = XElement.Load(sr);

            var testObject = new XmlSetSizeToBrick(xRoot);

            var referenceObject = new XmlSetSizeToBrick()
            {
                Size = new XmlFormula(xRoot, XmlConstants.Size),
            };

            Assert.AreEqual(referenceObject, testObject);
        }

    }
}
