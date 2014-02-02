using Catrobat.IDE.Core.CatrobatObjects.Formulas;
using Catrobat.IDE.Core.CatrobatObjects.Formulas.FormulaNodes;
using Catrobat.IDE.Core.CatrobatObjects.Variables;

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

        #endregion

        #region relational operators

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

        public static ConstantFormulaTree CreateTruthValueNode(bool value)
        {
            return value ? (ConstantFormulaTree) CreateTrueNode() : CreateFalseNode();
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

        #region min/max

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

        #endregion

        #region exponential function and logarithms

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

        #endregion

        #region trigonometric functions

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

        #endregion

        #region miscellaneous functions

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

        public static FormulaNodeMod CreateModNode(IFormulaTree leftChild, IFormulaTree rightChild)
        {
            return new FormulaNodeMod
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

        public static FormulaNodeBrightness CreateBrightnessNode(ObjectVariableEntry variable)
        {
            return new FormulaNodeBrightness
            {
                Variable = variable
            };
        }

        public static FormulaNodeDirection CreateDirectionNode(ObjectVariableEntry variable)
        {
            return new FormulaNodeDirection
            {
                Variable = variable
            };
        }

        public static FormulaNodeGhostEffect CreateGhostEffectNode(ObjectVariableEntry variable)
        {
            return new FormulaNodeGhostEffect
            {
                Variable = variable
            };
        }

        public static FormulaNodeLayer CreateLayerNode(ObjectVariableEntry variable)
        {
            return new FormulaNodeLayer
            {
                Variable = variable
            };
        }

        public static FormulaNodePositionX CreatePositionXNode(ObjectVariableEntry variable)
        {
            return new FormulaNodePositionX
            {
                Variable = variable
            };
        }

        public static FormulaNodePositionY CreatePositionYNode(ObjectVariableEntry variable)
        {
            return new FormulaNodePositionY
            {
                Variable = variable
            };
        }

        public static FormulaNodeRotation CreateRotationNode(ObjectVariableEntry variable)
        {
            return new FormulaNodeRotation
            {
                Variable = variable
            };
        }

        public static FormulaNodeSize CreateSizeNode(ObjectVariableEntry variable)
        {
            return new FormulaNodeSize
            {
                Variable = variable
            };
        }

        public static FormulaNodeOpacity CreateOpacityNode(ObjectVariableEntry variable)
        {
            return new FormulaNodeOpacity
            {
                Variable = variable
            };
        }

        #endregion

        #region user variables
        
        public static FormulaNodeUserVariable CreateUserVariableNode(UserVariable variable)
        {
            return new FormulaNodeUserVariable
            {
                Variable = variable
            };
        }

        #endregion

        #region brackets

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
