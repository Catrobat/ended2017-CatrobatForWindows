using Catrobat.IDE.Core.CatrobatObjects.Formulas;
using Catrobat.IDE.Core.CatrobatObjects.Formulas.FormulaNodes;

namespace Catrobat.IDE.Core.FormulaEditor.Editor
{
    class FormulaTreeFactory
    {
        public static IFormulaTree CreateFormulaTree()
        {
            // default formula is a 0
            return CreateNumberNode(0);
        }

        #region numbers

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

        #endregion

        #region arithmetic

        public static FormulaNodeAdd CreateAddNode(IFormulaTree leftChild = null, IFormulaTree rightChild = null)
        {
            return new FormulaNodeAdd
            {
                LeftChild = leftChild, 
                RightChild = rightChild
            };
        }

        public static FormulaNodeSubtract CreateSubtractNode(IFormulaTree leftChild = null, IFormulaTree rightChild = null)
        {
            return new FormulaNodeSubtract
            {
                LeftChild = leftChild,
                RightChild = rightChild
            };
        }

        public static FormulaNodeMultiply CreateMultiplyNode(IFormulaTree leftChild = null, IFormulaTree rightChild = null)
        {
            return new FormulaNodeMultiply
            {
                LeftChild = leftChild,
                RightChild = rightChild
            };
        }

        public static FormulaNodeDivide CreateDivideNode(IFormulaTree leftChild = null, IFormulaTree rightChild = null)
        {
            return new FormulaNodeDivide
            {
                LeftChild = leftChild,
                RightChild = rightChild
            };
        }

        #endregion

        #region relational operators

        public static FormulaNodeEquals CreateEqualsNode(IFormulaTree leftChild = null, IFormulaTree rightChild = null)
        {
            return new FormulaNodeEquals
            {
                LeftChild = leftChild,
                RightChild = rightChild
            };
        }

        public static FormulaNodeNotEquals CreateNotEqualsNode(IFormulaTree leftChild = null, IFormulaTree rightChild = null)
        {
            return new FormulaNodeNotEquals
            {
                LeftChild = leftChild,
                RightChild = rightChild
            };
        }

        public static FormulaNodeLess CreateLessNode(IFormulaTree leftChild = null, IFormulaTree rightChild = null)
        {
            return new FormulaNodeLess
            {
                LeftChild = leftChild,
                RightChild = rightChild
            };
        }

        public static FormulaNodeLessEqual CreateLessEqualNode(IFormulaTree leftChild = null, IFormulaTree rightChild = null)
        {
            return new FormulaNodeLessEqual
            {
                LeftChild = leftChild,
                RightChild = rightChild
            };
        }

        public static FormulaNodeGreater CreateGreaterNode(IFormulaTree leftChild = null, IFormulaTree rightChild = null)
        {
            return new FormulaNodeGreater
            {
                LeftChild = leftChild,
                RightChild = rightChild
            };
        }

        public static FormulaNodeGreaterEqual CreateGreaterEqualNode(IFormulaTree leftChild = null, IFormulaTree rightChild = null)
        {
            return new FormulaNodeGreaterEqual
            {
                LeftChild = leftChild,
                RightChild = rightChild
            };
        }

        #endregion

        #region logic

        public static FormulaNodeTrue CreateTrueNode()
        {
            return new FormulaNodeTrue();
        }

        public static FormulaNodeFalse CreateFalseNode()
        {
            return new FormulaNodeFalse();
        }

        public static FormulaNodeAnd CreateAndNode(IFormulaTree leftChild = null, IFormulaTree rightChild = null)
        {
            return new FormulaNodeAnd
            {
                LeftChild = leftChild,
                RightChild = rightChild
            };
        }

        public static FormulaNodeOr CreateOrNode(IFormulaTree leftChild = null, IFormulaTree rightChild = null)
        {
            return new FormulaNodeOr
            {
                LeftChild = leftChild,
                RightChild = rightChild
            };
        }

        public static FormulaNodeNot CreateNotNode(IFormulaTree child = null)
        {
            return new FormulaNodeNot
            {
                Child = child
            };
        }


        #endregion

        #region min/max

        public static FormulaNodeMin CreateMinNode(IFormulaTree leftChild = null, IFormulaTree rightChild = null)
        {
            return new FormulaNodeMin
            {
                LeftChild = leftChild,
                RightChild = rightChild
            };
        }

        public static FormulaNodeMax CreateMaxNode(IFormulaTree leftChild = null, IFormulaTree rightChild = null)
        {
            return new FormulaNodeMax
            {
                LeftChild = leftChild,
                RightChild = rightChild
            };
        }

        #endregion

        #region exponential function and logarithms

        public static FormulaNodeExp CreateExpNode(IFormulaTree child = null)
        {
            return new FormulaNodeExp
            {
                Child = child
            };
        }

        public static FormulaNodeLog CreateLogNode(IFormulaTree child = null)
        {
            return new FormulaNodeLog
            {
                Child = child
            };
        }

        public static FormulaNodeLn CreateLnNode(IFormulaTree child = null)
        {
            return new FormulaNodeLn
            {
                Child = child
            };
        }

        #endregion

        #region trigonometric functions

        public static FormulaNodeSin CreateSinNode(IFormulaTree child = null)
        {
            return new FormulaNodeSin
            {
                Child = child
            };
        }

        public static FormulaNodeCos CreateCosNode(IFormulaTree child = null)
        {
            return new FormulaNodeCos
            {
                Child = child
            };
        }

        public static FormulaNodeTan CreateTanNode(IFormulaTree child = null)
        {
            return new FormulaNodeTan
            {
                Child = child
            };
        }

        public static FormulaNodeArcsin CreateArcsinNode(IFormulaTree child = null)
        {
            return new FormulaNodeArcsin
            {
                Child = child
            };
        }

        public static FormulaNodeArccos CreateArccosNode(IFormulaTree child = null)
        {
            return new FormulaNodeArccos
            {
                Child = child
            };
        }

        public static FormulaNodeArctan CreateArctanNode(IFormulaTree child = null)
        {
            return new FormulaNodeArctan
            {
                Child = child
            };
        }

        #endregion

        #region miscellaneous functions

        public static FormulaNodeSqrt CreateSqrtNode(IFormulaTree child = null)
        {
            return new FormulaNodeSqrt
            {
                Child = child
            };
        }

        public static FormulaNodeAbs CreateAbsNode(IFormulaTree child = null)
        {
            return new FormulaNodeAbs
            {
                Child = child
            };
        }

        public static FormulaNodeMod CreateModNode(IFormulaTree leftChild = null, IFormulaTree rightChild = null)
        {
            return new FormulaNodeMod
            {
                LeftChild = leftChild,
                RightChild = rightChild
            };
        }

        public static FormulaNodeRound CreateRoundNode(IFormulaTree child = null)
        {
            return new FormulaNodeRound
            {
                Child = child
            };
        }

        public static FormulaNodeRandom CreateRandomNode(IFormulaTree leftChild = null, IFormulaTree rightChild = null)
        {
            return new FormulaNodeRandom
            {
                LeftChild = leftChild,
                RightChild = rightChild
            };
        }

        #endregion

        #region sensors

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


        #endregion

        #region object variables

        public static FormulaNodeBrightness CreateBrightnessNode()
        {
            return new FormulaNodeBrightness();
        }

        public static FormulaNodeDirection CreateDirectionNode()
        {
            return new FormulaNodeDirection();
        }

        public static FormulaNodeGhostEffect CreateGhostEffectNode()
        {
            return new FormulaNodeGhostEffect();
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

        public static FormulaNodeOpacity CreateOpacityNode()
        {
            return new FormulaNodeOpacity();
        }

        #endregion

        public static FormulaNodeUserVariable CreateUserVariableNode(string variableName)
        {
            return new FormulaNodeUserVariable
            {
                VariableName = variableName
            };
        }

        #region brackets

        public static FormulaNodeParentheses CreateParenthesesNode(IFormulaTree child = null)
        {
            return new FormulaNodeParentheses
            {
                Child = child
            };
        }

        #endregion

    }
}
