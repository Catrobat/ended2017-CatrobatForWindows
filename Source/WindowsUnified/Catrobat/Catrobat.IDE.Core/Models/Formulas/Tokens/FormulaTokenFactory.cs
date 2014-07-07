using Catrobat.IDE.Core.Models.Formulas.Tree;

namespace Catrobat.IDE.Core.Models.Formulas.Tokens
{
    class FormulaTokenFactory
    {
        #region Constants

        public static FormulaNodeNumber CreateDigitToken(int value)
        {
            return FormulaTreeFactory.CreateNumberNode(value);
        }
        public static FormulaTokenDecimalSeparator CreateDecimalSeparatorToken()
        {
            return new FormulaTokenDecimalSeparator();
        }

        public static FormulaNodePi CreatePiToken()
        {
            return FormulaTreeFactory.CreatePiNode();
        }

        public static FormulaNodeTrue CreateTrueToken()
        {
            return FormulaTreeFactory.CreateTrueNode();
        }
        public static FormulaNodeFalse CreateFalseToken()
        {
            return FormulaTreeFactory.CreateFalseNode();
        }
        public static ConstantFormulaTree CreateTruthValueToken(bool value)
        {
            return value ? (ConstantFormulaTree)CreateTrueToken() : CreateFalseToken();
        }

        #endregion

        #region Operators

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
        public static FormulaNodePower CreateCaretToken()
        {
            return FormulaTreeFactory.CreatePowerNode(null, null);
        }

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

        public static FormulaNodeModulo CreateModToken()
        {
            return FormulaTreeFactory.CreateModuloNode(null, null);
        }

        #endregion

        #region Functions

        public static FormulaTokenParameterSeparator CreateParameterSeparatorToken()
        {
            return new FormulaTokenParameterSeparator();
        }
        internal static FormulaTokenUnaryParameter CreateUnaryParameterToken(FormulaTree arg)
        {
            return new FormulaTokenUnaryParameter
            {
                Parameter = arg,
            };
        }
        internal static FormulaTokenBinaryParameter CreateBinaryParameterToken(FormulaTree arg1, FormulaTree arg2)
        {
            return new FormulaTokenBinaryParameter
            {
                FirstParameter = arg1,
                SecondParameter = arg2
            };
        }

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

        public static FormulaNodeMin CreateMinToken()
        {
            return FormulaTreeFactory.CreateMinNode(null, null);
        }
        public static FormulaNodeMax CreateMaxToken()
        {
            return FormulaTreeFactory.CreateMaxNode(null, null);
        }

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

        public static FormulaNodeSqrt CreateSqrtToken()
        {
            return FormulaTreeFactory.CreateSqrtNode(null);
        }

        public static FormulaNodeAbs CreateAbsToken()
        {
            return FormulaTreeFactory.CreateAbsNode(null);
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

        #region Sensors

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
        public static FormulaNodeLoudness CreateLoudnessToken()
        {
            return FormulaTreeFactory.CreateLoudnessNode();
        }

        #endregion

        #region Properties

        public static FormulaNodeBrightness CreateBrightnessToken()
        {
            return FormulaTreeFactory.CreateBrightnessNode();
        }
        public static FormulaNodeLayer CreateLayerToken()
        {
            return FormulaTreeFactory.CreateLayerNode();
        }
        public static FormulaNodePositionX CreatePositionXToken()
        {
            return FormulaTreeFactory.CreatePositionXNode();
        }
        public static FormulaNodePositionY CreatePositionYToken()
        {
            return FormulaTreeFactory.CreatePositionYNode();
        }
        public static FormulaNodeRotation CreateRotationToken()
        {
            return FormulaTreeFactory.CreateRotationNode();
        }
        public static FormulaNodeSize CreateSizeToken()
        {
            return FormulaTreeFactory.CreateSizeNode();
        }
        public static FormulaNodeTransparency CreateTransparencyToken()
        {
            return FormulaTreeFactory.CreateTransparencyNode();
        }

        #endregion

        #region Variables
        
        public static FormulaNodeLocalVariable CreateLocalVariableToken(LocalVariable variable)
        {
            return FormulaTreeFactory.CreateLocalVariableNode(variable);
        }
        public static FormulaNodeGlobalVariable CreateGlobalVariableToken(GlobalVariable variable)
        {
            return FormulaTreeFactory.CreateGlobalVariableNode(variable);
        }

        #endregion

        #region Brackets

        public static FormulaTokenParenthesis CreateParenthesisToken(bool isOpening)
        {
            return new FormulaTokenParenthesis
            {
                IsOpening = isOpening
            };
        }
 
        #endregion
    }
}
