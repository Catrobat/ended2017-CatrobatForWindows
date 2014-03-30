using System.Globalization;
using Catrobat.IDE.Core.CatrobatObjects.Variables;

namespace Catrobat.IDE.Core.CatrobatObjects.Formulas.XmlFormula
{
    /// <remarks> See /catroid/src/org/catrobat/catroid/formulaeditor</remarks>
    class XmlFormulaTreeFactory
    {
        public static XmlFormulaTree CreateFormulaTree()
        {
            // default formula is a 0
            return CreateNumberNode(0);
        }

        #region Constants

        public static XmlFormulaTree CreateNumberNode(double value)
        {
            return new XmlFormulaTree
            {
                VariableType = "NUMBER", 
                VariableValue = value.ToString(CultureInfo.InvariantCulture)
            };
        }

        public static XmlFormulaTree CreatePiNode()
        {
            return new XmlFormulaTree
            {
                VariableType = "FUNCTION", 
                VariableValue = "PI"
            };
        }

        public static XmlFormulaTree CreateTrueNode()
        {
            return new XmlFormulaTree
            {
                VariableType = "FUNCTION",
                VariableValue = "TRUE",
                LeftChild = null,
                RightChild = null
            };
        }
        public static XmlFormulaTree CreateFalseNode()
        {
            return new XmlFormulaTree
            {
                VariableType = "FUNCTION",
                VariableValue = "FALSE",
                LeftChild = null,
                RightChild = null
            };
        }


        #endregion

        #region Operators

        public static XmlFormulaTree CreateAddNode(XmlFormulaTree leftChild, XmlFormulaTree rightChild)
        {
            return new XmlFormulaTree
            {
                VariableType = "OPERATOR",
                VariableValue = "PLUS",
                LeftChild = leftChild,
                RightChild = rightChild
            };
        }
        public static XmlFormulaTree CreateSubtractNode(XmlFormulaTree leftChild, XmlFormulaTree rightChild)
        {
            return new XmlFormulaTree
            {
                VariableType = "OPERATOR",
                VariableValue = "MINUS",
                LeftChild = leftChild,
                RightChild = rightChild
            };
        }
        public static XmlFormulaTree CreateNegativeSignNode(XmlFormulaTree child)
        {
            return new XmlFormulaTree
            {
                VariableType = "OPERATOR",
                VariableValue = "MINUS",
                LeftChild = null,
                RightChild = child
            };
        }
        public static XmlFormulaTree CreateMultiplyNode(XmlFormulaTree leftChild, XmlFormulaTree rightChild)
        {
            return new XmlFormulaTree
            {
                VariableType = "OPERATOR",
                VariableValue = "MULT",
                LeftChild = leftChild,
                RightChild = rightChild
            };
        }
        public static XmlFormulaTree CreateDivideNode(XmlFormulaTree leftChild, XmlFormulaTree rightChild)
        {
            return new XmlFormulaTree
            {
                VariableType = "OPERATOR",
                VariableValue = "DIVIDE",
                LeftChild = leftChild,
                RightChild = rightChild
            };
        }
        public static XmlFormulaTree CreatePowerNode(XmlFormulaTree leftChild, XmlFormulaTree rightChild)
        {
            return new XmlFormulaTree
            {
                VariableType = "OPERATOR",
                VariableValue = "POW",
                LeftChild = leftChild,
                RightChild = rightChild
            };
        }

        public static XmlFormulaTree CreateEqualsNode(XmlFormulaTree leftChild, XmlFormulaTree rightChild)
        {
            return new XmlFormulaTree
            {
                VariableType = "OPERATOR",
                VariableValue = "EQUAL",
                LeftChild = leftChild,
                RightChild = rightChild
            };
        }
        public static XmlFormulaTree CreateNotEqualsNode(XmlFormulaTree leftChild, XmlFormulaTree rightChild)
        {
            return new XmlFormulaTree
            {
                VariableType = "OPERATOR",
                VariableValue = "NOT_EQUAL",
                LeftChild = leftChild,
                RightChild = rightChild
            };
        }

        public static XmlFormulaTree CreateLessNode(XmlFormulaTree leftChild, XmlFormulaTree rightChild)
        {
            return new XmlFormulaTree
            {
                VariableType = "OPERATOR",
                VariableValue = "SMALLER_THAN",
                LeftChild = leftChild,
                RightChild = rightChild
            };
        }
        public static XmlFormulaTree CreateLessEqualNode(XmlFormulaTree leftChild, XmlFormulaTree rightChild)
        {
            return new XmlFormulaTree
            {
                VariableType = "OPERATOR",
                VariableValue = "SMALLER_OR_EQUAL",
                LeftChild = leftChild,
                RightChild = rightChild
            };
        }
        public static XmlFormulaTree CreateGreaterNode(XmlFormulaTree leftChild, XmlFormulaTree rightChild)
        {
            return new XmlFormulaTree
            {
                VariableType = "OPERATOR",
                VariableValue = "GREATER_THAN",
                LeftChild = leftChild,
                RightChild = rightChild
            };
        }
        public static XmlFormulaTree CreateGreaterEqualNode(XmlFormulaTree leftChild, XmlFormulaTree rightChild)
        {
            return new XmlFormulaTree
            {
                VariableType = "OPERATOR",
                VariableValue = "GREATER_OR_EQUAL",
                LeftChild = leftChild,
                RightChild = rightChild
            };
        }

        public static XmlFormulaTree CreateAndNode(XmlFormulaTree leftChild, XmlFormulaTree rightChild)
        {
            return new XmlFormulaTree
            {
                VariableType = "OPERATOR",
                VariableValue = "LOGICAL_AND",
                LeftChild = leftChild,
                RightChild = rightChild
            };
        }
        public static XmlFormulaTree CreateOrNode(XmlFormulaTree leftChild, XmlFormulaTree rightChild)
        {
            return new XmlFormulaTree
            {
                VariableType = "OPERATOR",
                VariableValue = "LOGICAL_OR",
                LeftChild = leftChild,
                RightChild = rightChild
            };
        }

        public static XmlFormulaTree CreateNotNode(XmlFormulaTree child)
        {
            return new XmlFormulaTree
            {
                VariableType = "OPERATOR",
                VariableValue = "LOGICAL_NOT",
                LeftChild = null,
                RightChild = child
            };
        }

        #endregion

        #region Functions

        public static XmlFormulaTree CreateExpNode(XmlFormulaTree child)
        {
            return new XmlFormulaTree
            {
                VariableType = "FUNCTION",
                VariableValue = "EXP",
                LeftChild = child,
                RightChild = null
            };
        }
        public static XmlFormulaTree CreateLogNode(XmlFormulaTree child)
        {
            return new XmlFormulaTree
            {
                VariableType = "FUNCTION",
                VariableValue = "LOG",
                LeftChild = child,
                RightChild = null
            };
        }
        public static XmlFormulaTree CreateLnNode(XmlFormulaTree child)
        {
            return new XmlFormulaTree
            {
                VariableType = "FUNCTION",
                VariableValue = "LN",
                LeftChild = child,
                RightChild = null
            };
        }

        public static XmlFormulaTree CreateMinNode(XmlFormulaTree leftChild, XmlFormulaTree rightChild)
        {
            return new XmlFormulaTree
            {
                VariableType = "FUNCTION",
                VariableValue = "MIN",
                LeftChild = leftChild,
                RightChild = rightChild
            };
        }
        public static XmlFormulaTree CreateMaxNode(XmlFormulaTree leftChild, XmlFormulaTree rightChild)
        {
            return new XmlFormulaTree
            {
                VariableType = "FUNCTION",
                VariableValue = "MAX",
                LeftChild = leftChild,
                RightChild = rightChild
            };
        }

        public static XmlFormulaTree CreateSinNode(XmlFormulaTree child)
        {
            return new XmlFormulaTree
            {
                VariableType = "FUNCTION",
                VariableValue = "SIN",
                LeftChild = child,
                RightChild = null
            };
        }
        public static XmlFormulaTree CreateCosNode(XmlFormulaTree child)
        {
            return new XmlFormulaTree
            {
                VariableType = "FUNCTION",
                VariableValue = "COS",
                LeftChild = child,
                RightChild = null
            };
        }
        public static XmlFormulaTree CreateTanNode(XmlFormulaTree child)
        {
            return new XmlFormulaTree
            {
                VariableType = "FUNCTION",
                VariableValue = "TAN",
                LeftChild = child,
                RightChild = null
            };
        }
        public static XmlFormulaTree CreateArcsinNode(XmlFormulaTree child)
        {
            return new XmlFormulaTree
            {
                VariableType = "FUNCTION",
                VariableValue = "ARCSIN",
                LeftChild = child,
                RightChild = null
            };
        }
        public static XmlFormulaTree CreateArccosNode(XmlFormulaTree child)
        {
            return new XmlFormulaTree
            {
                VariableType = "FUNCTION",
                VariableValue = "ARCCOS",
                LeftChild = child,
                RightChild = null
            };
        }
        public static XmlFormulaTree CreateArctanNode(XmlFormulaTree child)
        {
            return new XmlFormulaTree
            {
                VariableType = "FUNCTION",
                VariableValue = "ARCTAN",
                LeftChild = child,
                RightChild = null
            };
        }

        public static XmlFormulaTree CreateSqrtNode(XmlFormulaTree child)
        {
            return new XmlFormulaTree
            {
                VariableType = "FUNCTION",
                VariableValue = "SQRT",
                LeftChild = child,
                RightChild = null
            };
        }

        public static XmlFormulaTree CreateAbsNode(XmlFormulaTree child)
        {
            return new XmlFormulaTree
            {
                VariableType = "FUNCTION",
                VariableValue = "ABS",
                LeftChild = child,
                RightChild = null
            };
        }

        public static XmlFormulaTree CreateModNode(XmlFormulaTree leftChild, XmlFormulaTree rightChild)
        {
            return new XmlFormulaTree
            {
                VariableType = "FUNCTION",
                VariableValue = "MOD",
                LeftChild = leftChild,
                RightChild = rightChild
            };
        }

        public static XmlFormulaTree CreateRoundNode(XmlFormulaTree child)
        {
            return new XmlFormulaTree
            {
                VariableType = "FUNCTION",
                VariableValue = "ROUND",
                LeftChild = child,
                RightChild = null
            };
        }

        public static XmlFormulaTree CreateRandomNode(XmlFormulaTree leftChild, XmlFormulaTree rightChild)
        {
            return new XmlFormulaTree
            {
                VariableType = "FUNCTION",
                VariableValue = "RAND",
                LeftChild = leftChild,
                RightChild = rightChild
            };
        }

        #endregion

        #region Sensors

        public static XmlFormulaTree CreateAccelerationXNode()
        {
            return new XmlFormulaTree
            {
                VariableType = "SENSOR",
                VariableValue = "X_ACCELERATION",
                LeftChild = null,
                RightChild = null
            };
        }
        public static XmlFormulaTree CreateAccelerationYNode()
        {
            return new XmlFormulaTree
            {
                VariableType = "SENSOR",
                VariableValue = "Y_ACCELERATION",
                LeftChild = null,
                RightChild = null
            };
        }
        public static XmlFormulaTree CreateAccelerationZNode()
        {
            return new XmlFormulaTree
            {
                VariableType = "SENSOR",
                VariableValue = "Z_ACCELERATION",
                LeftChild = null,
                RightChild = null
            };
        }

        public static XmlFormulaTree CreateCompassNode()
        {
            return new XmlFormulaTree
            {
                VariableType = "SENSOR",
                VariableValue = "COMPASS_DIRECTION",
                LeftChild = null,
                RightChild = null
            };
        }

        public static XmlFormulaTree CreateInclinationXNode()
        {
            return new XmlFormulaTree
            {
                VariableType = "SENSOR",
                VariableValue = "X_INCLINATION",
                LeftChild = null,
                RightChild = null
            };
        }
        public static XmlFormulaTree CreateInclinationYNode()
        {
            return new XmlFormulaTree
            {
                VariableType = "SENSOR",
                VariableValue = "Y_INCLINATION",
                LeftChild = null,
                RightChild = null
            };
        }

        public static XmlFormulaTree CreateLoudnessNode()
        {
            return new XmlFormulaTree
            {
                VariableType = "SENSOR",
                VariableValue = "LOUDNESS",
                LeftChild = null,
                RightChild = null
            };
        }

        #endregion

        #region Properties

        public static XmlFormulaTree CreateBrightnessNode()
        {
            return new XmlFormulaTree
            {
                VariableType = "SENSOR",
                VariableValue = "OBJECT_BRIGHTNESS",
                LeftChild = null,
                RightChild = null
            };
        }
        public static XmlFormulaTree CreateLayerNode()
        {
            return new XmlFormulaTree
            {
                VariableType = "SENSOR",
                VariableValue = "OBJECT_LAYER",
                LeftChild = null,
                RightChild = null
            };
        }
        public static XmlFormulaTree CreateTransparencyNode()
        {
            return new XmlFormulaTree
            {
                VariableType = "SENSOR",
                VariableValue = "OBJECT_GHOSTEFFECT",
                LeftChild = null,
                RightChild = null
            };
        }
        public static XmlFormulaTree CreatePositionXNode()
        {
            return new XmlFormulaTree
            {
                VariableType = "SENSOR",
                VariableValue = "OBJECT_X",
                LeftChild = null,
                RightChild = null
            };
        }
        public static XmlFormulaTree CreatePositionYNode()
        {
            return new XmlFormulaTree
            {
                VariableType = "SENSOR",
                VariableValue = "OBJECT_Y",
                LeftChild = null,
                RightChild = null
            };
        }
        public static XmlFormulaTree CreateRotationNode()
        {
            return new XmlFormulaTree
            {
                VariableType = "SENSOR",
                VariableValue = "OBJECT_ROTATION",
                LeftChild = null,
                RightChild = null
            };
        }
        public static XmlFormulaTree CreateSizeNode()
        {
            return new XmlFormulaTree
            {
                VariableType = "SENSOR",
                VariableValue = "OBJECT_SIZE",
                LeftChild = null,
                RightChild = null
            };
        }

        #endregion

        #region Variables

        public static XmlFormulaTree CreateLocalVariableNode(UserVariable variable)
        {
            return CreateVariableNode(variable);
        }
        public static XmlFormulaTree CreateGlobalVariableNode(UserVariable variable)
        {
            return CreateVariableNode(variable);
        }
        private static XmlFormulaTree CreateVariableNode(UserVariable variable)
        {
            return new XmlFormulaTree
            {
                VariableType = "USER_VARIABLE",
                VariableValue = variable != null ? variable.Name : null,
                LeftChild = null,
                RightChild = null
            };
        }

        #endregion

        #region Brackets

        public static XmlFormulaTree CreateParenthesesNode(XmlFormulaTree child)
        {
            return new XmlFormulaTree
            {
                VariableType = "BRACKET",
                VariableValue = string.Empty, 
                LeftChild = null,
                RightChild = child
            };
        }

        #endregion
    }
}
