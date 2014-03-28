using Catrobat.IDE.Core.CatrobatObjects.Formulas.XmlFormula;
using Catrobat.IDE.Core.CatrobatObjects.Variables;

namespace Catrobat.IDE.Core.CatrobatObjects.Formulas.FormulaTree
{
    /// <seealso cref="XmlFormulaTreeFactory"/>
    class FormulaTreeFactory
    {
        #region Constants

        public static FormulaNodeNumber CreateNumberNode(double value)
        {
            return new FormulaNodeNumber
            {
                Value = value
            };
        }

        public static FormulaNodePi CreatePiNode()
        {
            return new FormulaNodePi();
        }

        public static FormulaNodeTrue CreateTrueNode()
        {
            return new FormulaNodeTrue();
        }
        public static FormulaNodeFalse CreateFalseNode()
        {
            return new FormulaNodeFalse();
        }
        public static ConstantFormulaTree CreateTruthValueNode(bool value)
        {
            return value ? (ConstantFormulaTree)CreateTrueNode() : CreateFalseNode();
        }

        #endregion

        #region Operators

        public static FormulaNodeAdd CreateAddNode(IFormulaTree leftChild, IFormulaTree rightChild)
        {
            return new FormulaNodeAdd
            {
                LeftChild = leftChild, 
                RightChild = rightChild
            };
        }

        public static FormulaNodeSubtract CreateSubtractNode(IFormulaTree leftChild, IFormulaTree rightChild)
        {
            return new FormulaNodeSubtract
            {
                LeftChild = leftChild,
                RightChild = rightChild
            };
        }
        public static FormulaNodeNegativeSign CreateNegativeSignNode(IFormulaTree child)
        {
            return new FormulaNodeNegativeSign
            {
                Child = child
            };
        }
        public static FormulaNodeMultiply CreateMultiplyNode(IFormulaTree leftChild, IFormulaTree rightChild)
        {
            return new FormulaNodeMultiply
            {
                LeftChild = leftChild,
                RightChild = rightChild
            };
        }
        public static FormulaNodeDivide CreateDivideNode(IFormulaTree leftChild, IFormulaTree rightChild)
        {
            return new FormulaNodeDivide
            {
                LeftChild = leftChild,
                RightChild = rightChild
            };
        }
        public static FormulaNodePower CreatePowerNode(IFormulaTree leftChild, IFormulaTree rightChild)
        {
            return new FormulaNodePower
            {
                LeftChild = leftChild,
                RightChild = rightChild
            };
        }


        public static FormulaNodeEquals CreateEqualsNode(IFormulaTree leftChild, IFormulaTree rightChild)
        {
            return new FormulaNodeEquals
            {
                LeftChild = leftChild,
                RightChild = rightChild
            };
        }
        public static FormulaNodeNotEquals CreateNotEqualsNode(IFormulaTree leftChild, IFormulaTree rightChild)
        {
            return new FormulaNodeNotEquals
            {
                LeftChild = leftChild,
                RightChild = rightChild
            };
        }

        public static FormulaNodeLess CreateLessNode(IFormulaTree leftChild, IFormulaTree rightChild)
        {
            return new FormulaNodeLess
            {
                LeftChild = leftChild,
                RightChild = rightChild
            };
        }
        public static FormulaNodeLessEqual CreateLessEqualNode(IFormulaTree leftChild, IFormulaTree rightChild)
        {
            return new FormulaNodeLessEqual
            {
                LeftChild = leftChild,
                RightChild = rightChild
            };
        }
        public static FormulaNodeGreater CreateGreaterNode(IFormulaTree leftChild, IFormulaTree rightChild)
        {
            return new FormulaNodeGreater
            {
                LeftChild = leftChild,
                RightChild = rightChild
            };
        }
        public static FormulaNodeGreaterEqual CreateGreaterEqualNode(IFormulaTree leftChild, IFormulaTree rightChild)
        {
            return new FormulaNodeGreaterEqual
            {
                LeftChild = leftChild,
                RightChild = rightChild
            };
        }

        public static FormulaNodeAnd CreateAndNode(IFormulaTree leftChild, IFormulaTree rightChild)
        {
            return new FormulaNodeAnd
            {
                LeftChild = leftChild,
                RightChild = rightChild
            };
        }
        public static FormulaNodeOr CreateOrNode(IFormulaTree leftChild, IFormulaTree rightChild)
        {
            return new FormulaNodeOr
            {
                LeftChild = leftChild,
                RightChild = rightChild
            };
        }

        public static FormulaNodeNot CreateNotNode(IFormulaTree child)
        {
            return new FormulaNodeNot
            {
                Child = child
            };
        }

        #endregion

        #region Functions

        public static FormulaNodeExp CreateExpNode(IFormulaTree child)
        {
            return new FormulaNodeExp
            {
                Child = child
            };
        }
        public static FormulaNodeLog CreateLogNode(IFormulaTree child)
        {
            return new FormulaNodeLog
            {
                Child = child
            };
        }
        public static FormulaNodeLn CreateLnNode(IFormulaTree child)
        {
            return new FormulaNodeLn
            {
                Child = child
            };
        }


        public static FormulaNodeMin CreateMinNode(IFormulaTree firstChild, IFormulaTree secondChild)
        {
            return new FormulaNodeMin
            {
                FirstChild = firstChild,
                SecondChild = secondChild
            };
        }
        public static FormulaNodeMax CreateMaxNode(IFormulaTree firstChild, IFormulaTree secondChild)
        {
            return new FormulaNodeMax
            {
                FirstChild = firstChild,
                SecondChild = secondChild
            };
        }

        public static FormulaNodeSin CreateSinNode(IFormulaTree child)
        {
            return new FormulaNodeSin
            {
                Child = child
            };
        }
        public static FormulaNodeCos CreateCosNode(IFormulaTree child)
        {
            return new FormulaNodeCos
            {
                Child = child
            };
        }
        public static FormulaNodeTan CreateTanNode(IFormulaTree child)
        {
            return new FormulaNodeTan
            {
                Child = child
            };
        }
        public static FormulaNodeArcsin CreateArcsinNode(IFormulaTree child)
        {
            return new FormulaNodeArcsin
            {
                Child = child
            };
        }
        public static FormulaNodeArccos CreateArccosNode(IFormulaTree child)
        {
            return new FormulaNodeArccos
            {
                Child = child
            };
        }
        public static FormulaNodeArctan CreateArctanNode(IFormulaTree child)
        {
            return new FormulaNodeArctan
            {
                Child = child
            };
        }

        public static FormulaNodeSqrt CreateSqrtNode(IFormulaTree child)
        {
            return new FormulaNodeSqrt
            {
                Child = child
            };
        }

        public static FormulaNodeAbs CreateAbsNode(IFormulaTree child)
        {
            return new FormulaNodeAbs
            {
                Child = child
            };
        }

        public static FormulaNodeModulo CreateModuloNode(IFormulaTree leftChild, IFormulaTree rightChild)
        {
            return new FormulaNodeModulo
            {
                LeftChild = leftChild,
                RightChild = rightChild
            };
        }

        public static FormulaNodeRound CreateRoundNode(IFormulaTree child)
        {
            return new FormulaNodeRound
            {
                Child = child
            };
        }

        public static FormulaNodeRandom CreateRandomNode(IFormulaTree firstChild, IFormulaTree secondChild)
        {
            return new FormulaNodeRandom
            {
                FirstChild = firstChild,
                SecondChild = secondChild
            };
        }

        #endregion

        #region Sensors

        public static FormulaNodeAccelerationX CreateAccelerationXNode()
        {
            return new FormulaNodeAccelerationX();
        }
        public static FormulaNodeAccelerationY CreateAccelerationYNode()
        {
            return new FormulaNodeAccelerationY();
        }
        public static FormulaNodeAccelerationZ CreateAccelerationZNode()
        {
            return new FormulaNodeAccelerationZ();
        }
        public static FormulaNodeCompass CreateCompassNode()
        {
            return new FormulaNodeCompass();
        }
        public static FormulaNodeInclinationX CreateInclinationXNode()
        {
            return new FormulaNodeInclinationX();
        }
        public static FormulaNodeInclinationY CreateInclinationYNode()
        {
            return new FormulaNodeInclinationY();
        }
        public static FormulaNodeLoudness CreateLoudnessNode()
        {
            return new FormulaNodeLoudness();
        }

        #endregion

        #region Properties

        public static FormulaNodeBrightness CreateBrightnessNode()
        {
            return new FormulaNodeBrightness();
        }
        public static FormulaNodeLayer CreateLayerNode()
        {
            return new FormulaNodeLayer();
        }
        public static FormulaNodeOpacity CreateOpacityNode()
        {
            return new FormulaNodeOpacity();
        }
        public static FormulaNodePositionX CreatePositionXNode()
        {
            return new FormulaNodePositionX();
        }
        public static FormulaNodePositionY CreatePositionYNode()
        {
            return new FormulaNodePositionY();
        }
        public static FormulaNodeRotation CreateRotationNode()
        {
            return new FormulaNodeRotation();
        }
        public static FormulaNodeSize CreateSizeNode()
        {
            return new FormulaNodeSize();
        }

        #endregion

        #region Variables
        
        public static FormulaNodeLocalVariable CreateLocalVariableNode(UserVariable variable)
        {
            return new FormulaNodeLocalVariable
            {
                Variable = variable
            };
        }
        public static FormulaNodeGlobalVariable CreateGlobalVariableNode(UserVariable variable)
        {
            return new FormulaNodeGlobalVariable
            {
                Variable = variable
            };
        }

        #endregion

        #region Brackets

        public static FormulaNodeParentheses CreateParenthesesNode(IFormulaTree child)
        {
            return new FormulaNodeParentheses
            {
                Child = child
            };
        }

        #endregion
    }
}
