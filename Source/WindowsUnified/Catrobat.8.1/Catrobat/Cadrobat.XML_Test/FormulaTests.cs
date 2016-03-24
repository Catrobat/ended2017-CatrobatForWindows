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
    public class FormulaTests
    {
        [TestMethod]
        public void XmlFormulaTest()
        {
            TextReader sr = new StringReader("<formula category=\"IF_CONDITION\"> <leftChild> <leftChild> <leftChild> <leftChild> <leftChild> <type>USER_VARIABLE</type> <value>x speed</value> </leftChild> <rightChild> <type>USER_VARIABLE</type> <value>x speed</value> </rightChild> <type>OPERATOR</type> <value>MULT</value> </leftChild> <rightChild> <leftChild> <type>USER_VARIABLE</type> <value>y speed</value> </leftChild> <rightChild> <type>USER_VARIABLE</type> <value>y speed</value> </rightChild> <type>OPERATOR</type> <value>MULT</value> </rightChild> <type>OPERATOR</type> <value>PLUS</value> </leftChild> <type>FUNCTION</type> <value>SQRT</value> </leftChild> <rightChild> <type>NUMBER</type> <value>2</value> </rightChild> <type>OPERATOR</type> <value>GREATER_THAN</value> </leftChild> <rightChild> <leftChild> <type>SENSOR</type> <value>OBJECT_X</value> </leftChild> <rightChild> <type>NUMBER</type> <value>55</value> </rightChild> <type>OPERATOR</type> <value>SMALLER_THAN</value> </rightChild> <type>OPERATOR</type> <value>LOGICAL_AND</value> </formula>");
            var xRoot = XElement.Load(sr);
            TextReader refSr = new StringReader("<formula category=\"IF_CONDITION\"> <leftChild> <leftChild> <leftChild> <leftChild> <leftChild> <type>USER_VARIABLE</type> <value>x speed</value> </leftChild> <rightChild> <type>USER_VARIABLE</type> <value>x speed</value> </rightChild> <type>OPERATOR</type> <value>MULT</value> </leftChild> <rightChild> <leftChild> <type>USER_VARIABLE</type> <value>y speed</value> </leftChild> <rightChild> <type>USER_VARIABLE</type> <value>y speed</value> </rightChild> <type>OPERATOR</type> <value>MULT</value> </rightChild> <type>OPERATOR</type> <value>PLUS</value> </leftChild> <type>FUNCTION</type> <value>SQRT</value> </leftChild> <rightChild> <type>NUMBER</type> <value>2</value> </rightChild> <type>OPERATOR</type> <value>GREATER_THAN</value> </leftChild> <rightChild> <leftChild> <type>SENSOR</type> <value>OBJECT_X</value> </leftChild> <rightChild> <type>NUMBER</type> <value>55</value> </rightChild> <type>OPERATOR</type> <value>SMALLER_THAN</value> </rightChild> <type>OPERATOR</type> <value>LOGICAL_AND</value> </formula>");
            var refXRoot = XElement.Load(refSr);

            var testObject = new XmlFormula(xRoot);

            var referenceObject = new XmlFormula()
            {
                FormulaTree = new XmlFormulaTree(refXRoot)
            };

            Assert.AreEqual(referenceObject, testObject);
        }

        [TestMethod]
        public void XmlFormulaTreeTest()
        {
            TextReader sr = new StringReader("<formula category=\"IF_CONDITION\"> <leftChild> <leftChild> <leftChild> <leftChild> <leftChild> <type>USER_VARIABLE</type> <value>x speed</value> </leftChild> <rightChild> <type>USER_VARIABLE</type> <value>x speed</value> </rightChild> <type>OPERATOR</type> <value>MULT</value> </leftChild> <rightChild> <leftChild> <type>USER_VARIABLE</type> <value>y speed</value> </leftChild> <rightChild> <type>USER_VARIABLE</type> <value>y speed</value> </rightChild> <type>OPERATOR</type> <value>MULT</value> </rightChild> <type>OPERATOR</type> <value>PLUS</value> </leftChild> <type>FUNCTION</type> <value>SQRT</value> </leftChild> <rightChild> <type>NUMBER</type> <value>2</value> </rightChild> <type>OPERATOR</type> <value>GREATER_THAN</value> </leftChild> <rightChild> <leftChild> <type>SENSOR</type> <value>OBJECT_X</value> </leftChild> <rightChild> <type>NUMBER</type> <value>55</value> </rightChild> <type>OPERATOR</type> <value>SMALLER_THAN</value> </rightChild> <type>OPERATOR</type> <value>LOGICAL_AND</value> </formula>");
            var xRoot = XElement.Load(sr);

            var testObject = new XmlFormulaTree(xRoot);

            var referenceObject = new XmlFormulaTree()
            {
                VariableType = "OPERATOR",
                VariableValue = "LOGICAL_AND",
                LeftChild = new XmlFormulaTree()
                {
                    VariableType = "OPERATOR",
                    VariableValue = "GREATER_THAN",
                    LeftChild = new XmlFormulaTree()
                    {
                        VariableType = "FUNCTION",
                        VariableValue = "SQRT",
                        LeftChild = new XmlFormulaTree()
                        {
                            VariableType = "OPERATOR",
                            VariableValue = "PLUS",
                            LeftChild = new XmlFormulaTree()
                            {
                                VariableType = "OPERATOR",
                                VariableValue = "MULT",
                                LeftChild = new XmlFormulaTree()
                                {
                                    VariableType = "USER_VARIABLE",
                                    VariableValue = "x speed",
                                },
                                RightChild = new XmlFormulaTree()
                                {
                                    VariableType = "USER_VARIABLE",
                                    VariableValue = "x speed",
                                }
                            },
                            RightChild = new XmlFormulaTree()
                            {
                                VariableType = "OPERATOR",
                                VariableValue = "MULT",
                                LeftChild = new XmlFormulaTree()
                                {
                                    VariableType = "USER_VARIABLE",
                                    VariableValue = "y speed",
                                },
                                RightChild = new XmlFormulaTree()
                                {
                                    VariableType = "USER_VARIABLE",
                                    VariableValue = "y speed",
                                }
                            }
                        }
                        // SQRT no RightChild
                    },
                    RightChild = new XmlFormulaTree()
                    {
                        VariableType = "NUMBER",
                        VariableValue = "2",
                    }
                },
                RightChild = new XmlFormulaTree()
                {
                    VariableType = "OPERATOR",
                    VariableValue = "SMALLER_THAN",
                    LeftChild = new XmlFormulaTree()
                    {
                        VariableType = "SENSOR",
                        VariableValue = "OBJECT_X",
                    },
                    RightChild = new XmlFormulaTree()
                    {
                        VariableType = "NUMBER",
                        VariableValue = "55",
                    }
                }
            };

            Assert.AreEqual(referenceObject, testObject);
        }
    }
}
