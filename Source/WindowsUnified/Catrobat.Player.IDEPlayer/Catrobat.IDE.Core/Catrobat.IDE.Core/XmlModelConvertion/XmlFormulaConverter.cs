using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using Catrobat.IDE.Core.ExtensionMethods;
using Catrobat.IDE.Core.Models.Formulas.Tree;
using Catrobat.IDE.Core.Models;
using Catrobat.IDE.Core.Xml.XmlObjects.Formulas;

namespace Catrobat.IDE.Core.XmlModelConvertion
{
    /// <summary>Converts between <see cref="XmlFormula"/> and <see cref="FormulaTree"/>. 
    /// See <see cref="XmlFormulaTreeFactory"/> and <see cref="FormulaTreeFactory"/> for types to implement. </summary>
    public class XmlFormulaConverter
    {
        #region Convert

        private readonly IDictionary<string, LocalVariable> _localVariables;
        private readonly IDictionary<string, GlobalVariable> _globalVariables;

        public XmlFormulaConverter(IEnumerable<LocalVariable> localVariables, IEnumerable<GlobalVariable> globalVariables)
        {
            if (localVariables == null) throw new ArgumentNullException("localVariables");
            if (globalVariables == null) throw new ArgumentNullException("globalVariables");
            _localVariables = localVariables.ToDictionary(variable => variable.Name);
            _globalVariables = globalVariables.ToDictionary(variable => variable.Name);
        }

        public FormulaTree Convert(XmlFormula formula)
        {
            return formula == null ? null : Convert(formula.FormulaTree);
        }

        private FormulaTree Convert(XmlFormulaTree formula)
        {
            if (formula == null) return null;

            if (formula.VariableType == "NUMBER") return FormulaTreeFactory.CreateNumberNode(double.Parse(formula.VariableValue, CultureInfo.InvariantCulture));
            if (formula.VariableType == "OPERATOR") return ConvertOperatorNode(formula);
            if (formula.VariableType == "FUNCTION") return ConvertFunctionNode(formula);
            if (formula.VariableType == "SENSOR") return ConvertSensorOrPropertiesNode(formula);
            if (formula.VariableType == "USER_VARIABLE") return ConvertVariableNode(formula);
            if (formula.VariableType == "BRACKET") return ConvertParenthesesNode(formula);

            if (String.IsNullOrEmpty(formula.VariableType)) return null;
            Debugger.Break();
            throw new NotImplementedException();
        }

        private FormulaTree ConvertOperatorNode(XmlFormulaTree node)
        {
            // arithmetic
            if (node.VariableValue == "PLUS") return Convert(node, FormulaTreeFactory.CreateAddNode);
            if (node.VariableValue == "MINUS" && node.LeftChild == null) return Convert(node, FormulaTreeFactory.CreateNegativeSignNode, false);
            if (node.VariableValue == "MINUS") return Convert(node, FormulaTreeFactory.CreateSubtractNode);
            if (node.VariableValue == "MULT") return Convert(node, FormulaTreeFactory.CreateMultiplyNode);
            if (node.VariableValue == "DIVIDE") return Convert(node, FormulaTreeFactory.CreateDivideNode);
            if (node.VariableValue == "POW") return Convert(node, FormulaTreeFactory.CreatePowerNode);

            // relational operators
            if (node.VariableValue == "EQUAL") return Convert(node, FormulaTreeFactory.CreateEqualsNode);
            if (node.VariableValue == "NOT_EQUAL") return Convert(node, FormulaTreeFactory.CreateNotEqualsNode);
            if (node.VariableValue == "SMALLER_THAN") return Convert(node, FormulaTreeFactory.CreateLessNode);
            if (node.VariableValue == "SMALLER_OR_EQUAL") return Convert(node, FormulaTreeFactory.CreateLessEqualNode);
            if (node.VariableValue == "GREATER_THAN") return Convert(node, FormulaTreeFactory.CreateGreaterNode);
            if (node.VariableValue == "GREATER_OR_EQUAL") return Convert(node, FormulaTreeFactory.CreateGreaterEqualNode);

            // logic
            if (node.VariableValue == "LOGICAL_AND") return Convert(node, FormulaTreeFactory.CreateAndNode);
            if (node.VariableValue == "LOGICAL_OR") return Convert(node, FormulaTreeFactory.CreateOrNode);
            if (node.VariableValue == "LOGICAL_NOT") return Convert(node, FormulaTreeFactory.CreateNotNode, false);

            throw new NotImplementedException();
        }

        private FormulaTree ConvertFunctionNode(XmlFormulaTree node)
        {
            // numbers
            if (node.VariableValue == "PI") return Convert(node, FormulaTreeFactory.CreatePiNode);

            // logic
            if (node.VariableValue == "TRUE") return Convert(node, FormulaTreeFactory.CreateTrueNode);
            if (node.VariableValue == "FALSE") return Convert(node, FormulaTreeFactory.CreateFalseNode);

            // min/max
            if (node.VariableValue == "MIN") return Convert(node, FormulaTreeFactory.CreateMinNode);
            if (node.VariableValue == "MAX") return Convert(node, FormulaTreeFactory.CreateMaxNode);

            // exponential function and logarithms
            if (node.VariableValue == "EXP") return Convert(node, FormulaTreeFactory.CreateExpNode);
            if (node.VariableValue == "LOG") return Convert(node, FormulaTreeFactory.CreateLogNode);
            if (node.VariableValue == "LN") return Convert(node, FormulaTreeFactory.CreateLnNode);

            // trigonometric functions
            if (node.VariableValue == "SIN") return Convert(node, FormulaTreeFactory.CreateSinNode);
            if (node.VariableValue == "COS") return Convert(node, FormulaTreeFactory.CreateCosNode);
            if (node.VariableValue == "TAN") return Convert(node, FormulaTreeFactory.CreateTanNode);
            if (node.VariableValue == "ARCSIN") return Convert(node, FormulaTreeFactory.CreateArcsinNode);
            if (node.VariableValue == "ARCCOS") return Convert(node, FormulaTreeFactory.CreateArccosNode);
            if (node.VariableValue == "ARCTAN") return Convert(node, FormulaTreeFactory.CreateArctanNode);

            // miscellaneous functions
            if (node.VariableValue == "SQRT") return Convert(node, FormulaTreeFactory.CreateSqrtNode);
            if (node.VariableValue == "ABS") return Convert(node, FormulaTreeFactory.CreateAbsNode);
            if (node.VariableValue == "MOD") return Convert(node, FormulaTreeFactory.CreateModuloNode);
            if (node.VariableValue == "ROUND") return Convert(node, FormulaTreeFactory.CreateRoundNode);
            if (node.VariableValue == "RAND") return Convert(node, FormulaTreeFactory.CreateRandomNode);
            
            throw new NotImplementedException();
        }

        private FormulaTree ConvertSensorOrPropertiesNode(XmlFormulaTree node)
        {
            // sensors
            if (node.VariableValue == "X_ACCELERATION") return Convert(node, FormulaTreeFactory.CreateAccelerationXNode);
            if (node.VariableValue == "Y_ACCELERATION") return Convert(node, FormulaTreeFactory.CreateAccelerationYNode);
            if (node.VariableValue == "Z_ACCELERATION") return Convert(node, FormulaTreeFactory.CreateAccelerationZNode);
            if (node.VariableValue == "COMPASS_DIRECTION") return Convert(node, FormulaTreeFactory.CreateCompassNode);
            if (node.VariableValue == "X_INCLINATION") return Convert(node, FormulaTreeFactory.CreateInclinationXNode);
            if (node.VariableValue == "Y_INCLINATION") return Convert(node, FormulaTreeFactory.CreateInclinationYNode);
            if (node.VariableValue == "LOUDNESS") return Convert(node, FormulaTreeFactory.CreateLoudnessNode);

            // properties
            if (node.VariableValue == "OBJECT_BRIGHTNESS") return Convert(node, FormulaTreeFactory.CreateBrightnessNode);
            if (node.VariableValue == "OBJECT_LAYER") return Convert(node, FormulaTreeFactory.CreateLayerNode);
            if (node.VariableValue == "OBJECT_X") return Convert(node, FormulaTreeFactory.CreatePositionXNode);
            if (node.VariableValue == "OBJECT_Y") return Convert(node, FormulaTreeFactory.CreatePositionYNode);
            if (node.VariableValue == "OBJECT_ROTATION") return Convert(node, FormulaTreeFactory.CreateRotationNode);
            if (node.VariableValue == "OBJECT_SIZE") return Convert(node, FormulaTreeFactory.CreateSizeNode);
            if (node.VariableValue == "OBJECT_GHOSTEFFECT") return Convert(node, FormulaTreeFactory.CreateTransparencyNode);

            throw new NotImplementedException();
        }

        private FormulaTree ConvertVariableNode(XmlFormulaTree node)
        {
            if (node.VariableValue != null)
            {
                LocalVariable localVariable;
                if (_localVariables.TryGetValue(node.VariableValue, out localVariable)) return FormulaTreeFactory.CreateLocalVariableNode(localVariable);
                
                GlobalVariable globalVariable;
                if (_globalVariables.TryGetValue(node.VariableValue, out globalVariable)) return FormulaTreeFactory.CreateGlobalVariableNode(globalVariable);
            }
            Debug.Assert(false, "Invalid project");
            return null;
        }

        private FormulaTree ConvertParenthesesNode(XmlFormulaTree node)
        {
            return Convert(node, FormulaTreeFactory.CreateParenthesesNode, false);
        }

        private TNode Convert<TNode>(XmlFormulaTree node, Func<TNode> creator) where TNode : ConstantFormulaTree
        {
            return creator();
        }

        private TNode Convert<TNode>(XmlFormulaTree node, Func<FormulaTree, TNode> creator, bool leftChild = true) where TNode : UnaryFormulaTree
        {
            return creator(Convert(leftChild ? node.LeftChild : node.RightChild));
        }

        private TNode Convert<TNode>(XmlFormulaTree node, Func<FormulaTree, FormulaTree, TNode> creator) where TNode : BinaryFormulaTree
        {
            return creator(Convert(node.LeftChild), Convert(node.RightChild));
        }

        #endregion

        #region ConvertBack

        /// <summary>Use this constructor when <see cref="ConvertBack"/> is needed only. </summary>
        public XmlFormulaConverter()
        {
        }

        /// <remarks>Implemented by <see cref="IXmlObjectConvertible{XmlFormulaTree}" />.</remarks>
        public XmlFormula ConvertBack(FormulaTree formula)
        {
            return formula == null ? null : formula.ToXmlObject();
        }

        #endregion
    }
}
