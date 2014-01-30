using Catrobat.IDE.Core.CatrobatObjects.Formulas.FormulaNodes;
using Catrobat.IDE.Core.CatrobatObjects.Formulas.FormulaTokens;
using Catrobat.IDE.Core.CatrobatObjects.Variables;

namespace Catrobat.IDE.Core.FormulaEditor.Editor
{
    class FormulaTokenFactory
    {
        #region numbers

        public static FormulaNodeNumber CreateDigitToken(int value)
        {
            return FormulaTreeFactory.CreateNumberNode(value);
        }

        public static FormulaNodeNumber CreateNumberToken(double value)
        {
            return FormulaTreeFactory.CreateNumberNode(value);
        }

        public static FormulaTokenDecimalSpearator CreateDecimalSeparatorToken()
        {
            return new FormulaTokenDecimalSpearator();
        }

        public static FormulaTokenParameterSpearator CreateArgumentSeparatorToken()
        {
            return new FormulaTokenParameterSpearator();
        }

        public static FormulaNodePi CreatePiToken()
        {
            return FormulaTreeFactory.CreatePiNode();
        }

        #endregion

        #region arithmetic

        public static FormulaNodeAdd CreatePlusToken()
        {
            return FormulaTreeFactory.CreateAddNode(null, null);
        }

        public static FormulaNodeSubtract CreateMinusToken()
        {
            return FormulaTreeFactory.CreateSubtractNode(null, null);
        }

        public static FormulaNodeMultiply CreateMultiplyToken()
        {
            return FormulaTreeFactory.CreateMultiplyNode(null, null);
        }

        public static FormulaNodeDivide CreateDivideToken()
        {
            return FormulaTreeFactory.CreateDivideNode(null, null);
        }

        #endregion

        #region relational operators

        public static FormulaNodeEquals CreateEqualsToken()
        {
            return FormulaTreeFactory.CreateEqualsNode(null, null);
        }

        public static FormulaNodeNotEquals CreateNotEqualsToken()
        {
            return FormulaTreeFactory.CreateNotEqualsNode(null, null);
        }

        public static FormulaNodeLess CreateLessToken()
        {
            return FormulaTreeFactory.CreateLessNode(null, null);
        }

        public static FormulaNodeLessEqual CreateLessEqualToken()
        {
            return FormulaTreeFactory.CreateLessEqualNode(null, null);
        }

        public static FormulaNodeGreater CreateGreaterToken()
        {
            return FormulaTreeFactory.CreateGreaterNode(null, null);
        }

        public static FormulaNodeGreaterEqual CreateGreaterEqualToken()
        {
            return FormulaTreeFactory.CreateGreaterEqualNode(null, null);
        }

        #endregion

        #region logic

        public static FormulaNodeTrue CreateTrueToken()
        {
            return FormulaTreeFactory.CreateTrueNode();
        }

        public static FormulaNodeFalse CreateFalseToken()
        {
            return FormulaTreeFactory.CreateFalseNode();
        }

        public static FormulaNodeAnd CreateAndToken()
        {

            return FormulaTreeFactory.CreateAndNode(null, null);
        }

        public static FormulaNodeOr CreateOrToken()
        {
            return FormulaTreeFactory.CreateOrNode(null, null);
        }

        public static FormulaNodeNot CreateNotToken()
        {
            return FormulaTreeFactory.CreateNotNode(null);
        }


        #endregion

        #region min/max

        public static FormulaNodeMin CreateMinToken()
        {
            return FormulaTreeFactory.CreateMinNode(null, null);
        }

        public static FormulaNodeMax CreateMaxToken()
        {
            return FormulaTreeFactory.CreateMaxNode(null, null);
        }

        #endregion

        #region exponential function and logarithms

        public static FormulaNodeExp CreateExpToken()
        {
            return FormulaTreeFactory.CreateExpNode(null);
        }

        public static FormulaNodeLog CreateLogToken()
        {
            return FormulaTreeFactory.CreateLogNode(null);
        }

        public static FormulaNodeLn CreateLnToken()
        {
            return FormulaTreeFactory.CreateLnNode(null);
        }

        #endregion

        #region trigonometric functions

        public static FormulaNodeSin CreateSinToken()
        {
            return FormulaTreeFactory.CreateSinNode(null);
        }

        public static FormulaNodeCos CreateCosToken()
        {
            return FormulaTreeFactory.CreateCosNode(null);
        }

        public static FormulaNodeTan CreateTanToken()
        {
            return FormulaTreeFactory.CreateTanNode(null);
        }

        public static FormulaNodeArcsin CreateArcsinToken()
        {
            return FormulaTreeFactory.CreateArcsinNode(null);
        }

        public static FormulaNodeArccos CreateArccosToken()
        {
            return FormulaTreeFactory.CreateArccosNode(null);
        }

        public static FormulaNodeArctan CreateArctanToken()
        {
            return FormulaTreeFactory.CreateArctanNode(null);
        }

        #endregion

        #region miscellaneous functions

        public static FormulaNodeSqrt CreateSqrtToken()
        {
            return FormulaTreeFactory.CreateSqrtNode(null);
        }

        public static FormulaTokenVerticalBar CreateVerticalBarToken()
        {
            return new FormulaTokenVerticalBar();
        }

        public static FormulaNodeAbs CreateAbsToken()
        {
            return FormulaTreeFactory.CreateAbsNode(null);
        }

        public static FormulaNodeMod CreateModToken()
        {
            return FormulaTreeFactory.CreateModNode(null, null);
        }

        public static FormulaNodeRound CreateRoundToken()
        {
            return FormulaTreeFactory.CreateRoundNode(null);
        }

        public static FormulaNodeRandom CreateRandomToken()
        {
            return FormulaTreeFactory.CreateRandomNode(null, null);
        }

        #endregion

        #region sensors

        public static FormulaNodeAccelerationX CreateAccelerationXToken()
        {
            return FormulaTreeFactory.CreateAccelerationXNode();
        }

        public static FormulaNodeAccelerationY CreateAccelerationYToken()
        {
            return FormulaTreeFactory.CreateAccelerationYNode();
        }

        public static FormulaNodeAccelerationZ CreateAccelerationZToken()
        {
            return FormulaTreeFactory.CreateAccelerationZNode();
        }

        public static FormulaNodeCompass CreateCompassToken()
        {
            return FormulaTreeFactory.CreateCompassNode();
        }

        public static FormulaNodeInclinationX CreateInclinationXToken()
        {
            return FormulaTreeFactory.CreateInclinationXNode();
        }

        public static FormulaNodeInclinationY CreateInclinationYToken()
        {
            return FormulaTreeFactory.CreateInclinationYNode();
        }


        #endregion

        #region object variables

        public static FormulaNodeBrightness CreateBrightnessToken()
        {
            return FormulaTreeFactory.CreateBrightnessNode(null);
        }

        public static FormulaNodeDirection CreateDirectionToken()
        {
            return FormulaTreeFactory.CreateDirectionNode(null);
        }

        public static FormulaNodeGhostEffect CreateGhostEffectToken()
        {
            return FormulaTreeFactory.CreateGhostEffectNode(null);
        }

        public static FormulaNodeLayer CreateLayerToken()
        {
            return FormulaTreeFactory.CreateLayerNode(null);
        }

        public static FormulaNodeOpacity CreateOpacityToken()
        {
            return FormulaTreeFactory.CreateOpacityNode(null);
        }

        public static FormulaNodePositionX CreatePositionXToken()
        {
            return FormulaTreeFactory.CreatePositionXNode(null);
        }

        public static FormulaNodePositionY CreatePositionYToken()
        {
            return FormulaTreeFactory.CreatePositionYNode(null);
        }

        public static FormulaNodeRotation CreateRotationToken()
        {
            return FormulaTreeFactory.CreateRotationNode(null);
        }

        public static FormulaNodeSize CreateSizeToken()
        {
            return FormulaTreeFactory.CreateSizeNode(null);
        }

        #endregion

        #region user variables
        
        public static FormulaNodeUserVariable CreateUserVariableToken()
        {
            return FormulaTreeFactory.CreateUserVariableNode(null);
        }

        #endregion

        #region brackets

        public static FormulaTokenParenthesis CreateParenthesisToken(bool isOpening)
        {
            return new FormulaTokenParenthesis
            {
                IsOpening = isOpening
            };
        }

        public static FormulaTokenSquareBracket CreateSquareBracketToken(bool isOpening)
        {
            return new FormulaTokenSquareBracket
            {
                IsOpening = isOpening
            };
        }

        public static FormulaTokenCurlyBrace CreateCurlyBraceToken(bool isOpening)
        {
            return new FormulaTokenCurlyBrace
            {
                IsOpening = isOpening
            };
        }

        #endregion

    }
}
