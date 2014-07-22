using Catrobat.Data.Xml.XmlObjects.Formulas;

namespace Catrobat.IDE.Core.Models.Formulas.Tree
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

        public static FormulaNodeAdd CreateAddNode(FormulaTree leftChild, FormulaTree rightChild)
        {
            return new FormulaNodeAdd
            {
                LeftChild = leftChild, 
                RightChild = rightChild
            };
        }

        public static FormulaNodeSubtract CreateSubtractNode(FormulaTree leftChild, FormulaTree rightChild)
        {
            return new FormulaNodeSubtract
            {
                LeftChild = leftChild,
                RightChild = rightChild
            };
        }
        public static FormulaNodeNegativeSign CreateNegativeSignNode(FormulaTree child)
        {
            return new FormulaNodeNegativeSign
            {
                Child = child
            };
        }
        public static FormulaNodeMultiply CreateMultiplyNode(FormulaTree leftChild, FormulaTree rightChild)
        {
            return new FormulaNodeMultiply
            {
                LeftChild = leftChild,
                RightChild = rightChild
            };
        }
        public static FormulaNodeDivide CreateDivideNode(FormulaTree leftChild, FormulaTree rightChild)
        {
            return new FormulaNodeDivide
            {
                LeftChild = leftChild,
                RightChild = rightChild
            };
        }
        public static FormulaNodePower CreatePowerNode(FormulaTree leftChild, FormulaTree rightChild)
        {
            return new FormulaNodePower
            {
                LeftChild = leftChild,
                RightChild = rightChild
            };
        }

        public static FormulaNodeEquals CreateEqualsNode(FormulaTree leftChild, FormulaTree rightChild)
        {
            return new FormulaNodeEquals
            {
                LeftChild = leftChild,
                RightChild = rightChild
            };
        }
        public static FormulaNodeNotEquals CreateNotEqualsNode(FormulaTree leftChild, FormulaTree rightChild)
        {
            return new FormulaNodeNotEquals
            {
                LeftChild = leftChild,
                RightChild = rightChild
            };
        }

        public static FormulaNodeLess CreateLessNode(FormulaTree leftChild, FormulaTree rightChild)
        {
            return new FormulaNodeLess
            {
                LeftChild = leftChild,
                RightChild = rightChild
            };
        }
        public static FormulaNodeLessEqual CreateLessEqualNode(FormulaTree leftChild, FormulaTree rightChild)
        {
            return new FormulaNodeLessEqual
            {
                LeftChild = leftChild,
                RightChild = rightChild
            };
        }
        public static FormulaNodeGreater CreateGreaterNode(FormulaTree leftChild, FormulaTree rightChild)
        {
            return new FormulaNodeGreater
            {
                LeftChild = leftChild,
                RightChild = rightChild
            };
        }
        public static FormulaNodeGreaterEqual CreateGreaterEqualNode(FormulaTree leftChild, FormulaTree rightChild)
        {
            return new FormulaNodeGreaterEqual
            {
                LeftChild = leftChild,
                RightChild = rightChild
            };
        }

        public static FormulaNodeAnd CreateAndNode(FormulaTree leftChild, FormulaTree rightChild)
        {
            return new FormulaNodeAnd
            {
                LeftChild = leftChild,
                RightChild = rightChild
            };
        }
        public static FormulaNodeOr CreateOrNode(FormulaTree leftChild, FormulaTree rightChild)
        {
            return new FormulaNodeOr
            {
                LeftChild = leftChild,
                RightChild = rightChild
            };
        }

        public static FormulaNodeNot CreateNotNode(FormulaTree child)
        {
            return new FormulaNodeNot
            {
                Child = child
            };
        }

        public static FormulaNodeModulo CreateModuloNode(FormulaTree leftChild, FormulaTree rightChild)
        {
            return new FormulaNodeModulo
            {
                LeftChild = leftChild,
                RightChild = rightChild
            };
        }

        #endregion

        #region Functions

        public static FormulaNodeExp CreateExpNode(FormulaTree child)
        {
            return new FormulaNodeExp
            {
                Child = child
            };
        }
        public static FormulaNodeLog CreateLogNode(FormulaTree child)
        {
            return new FormulaNodeLog
            {
                Child = child
            };
        }
        public static FormulaNodeLn CreateLnNode(FormulaTree child)
        {
            return new FormulaNodeLn
            {
                Child = child
            };
        }


        public static FormulaNodeMin CreateMinNode(FormulaTree firstChild, FormulaTree secondChild)
        {
            return new FormulaNodeMin
            {
                FirstChild = firstChild,
                SecondChild = secondChild
            };
        }
        public static FormulaNodeMax CreateMaxNode(FormulaTree firstChild, FormulaTree secondChild)
        {
            return new FormulaNodeMax
            {
                FirstChild = firstChild,
                SecondChild = secondChild
            };
        }

        public static FormulaNodeSin CreateSinNode(FormulaTree child)
        {
            return new FormulaNodeSin
            {
                Child = child
            };
        }
        public static FormulaNodeCos CreateCosNode(FormulaTree child)
        {
            return new FormulaNodeCos
            {
                Child = child
            };
        }
        public static FormulaNodeTan CreateTanNode(FormulaTree child)
        {
            return new FormulaNodeTan
            {
                Child = child
            };
        }
        public static FormulaNodeArcsin CreateArcsinNode(FormulaTree child)
        {
            return new FormulaNodeArcsin
            {
                Child = child
            };
        }
        public static FormulaNodeArccos CreateArccosNode(FormulaTree child)
        {
            return new FormulaNodeArccos
            {
                Child = child
            };
        }
        public static FormulaNodeArctan CreateArctanNode(FormulaTree child)
        {
            return new FormulaNodeArctan
            {
                Child = child
            };
        }

        public static FormulaNodeSqrt CreateSqrtNode(FormulaTree child)
        {
            return new FormulaNodeSqrt
            {
                Child = child
            };
        }

        public static FormulaNodeAbs CreateAbsNode(FormulaTree child)
        {
            return new FormulaNodeAbs
            {
                Child = child
            };
        }

        public static FormulaNodeRound CreateRoundNode(FormulaTree child)
        {
            return new FormulaNodeRound
            {
                Child = child
            };
        }

        public static FormulaNodeRandom CreateRandomNode(FormulaTree firstChild, FormulaTree secondChild)
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
        public static FormulaNodeTransparency CreateTransparencyNode()
        {
            return new FormulaNodeTransparency();
        }

        #endregion

        #region Variables
        
        public static FormulaNodeLocalVariable CreateLocalVariableNode(LocalVariable variable)
        {
            return new FormulaNodeLocalVariable
            {
                Variable = variable
            };
        }
        public static FormulaNodeGlobalVariable CreateGlobalVariableNode(GlobalVariable variable)
        {
            return new FormulaNodeGlobalVariable
            {
                Variable = variable
            };
        }

        #endregion

        #region Brackets

        public static FormulaNodeParentheses CreateParenthesesNode(FormulaTree child)
        {
            return new FormulaNodeParentheses
            {
                Child = child
            };
        }

        #endregion
    }
}
