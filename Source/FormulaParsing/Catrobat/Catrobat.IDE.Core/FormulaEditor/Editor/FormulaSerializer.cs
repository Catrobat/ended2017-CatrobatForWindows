using System.Text;
using Catrobat.IDE.Core.CatrobatObjects.Formulas;
using System;
using Catrobat.IDE.Core.CatrobatObjects.Formulas.FormulaNodes;

namespace Catrobat.IDE.Core.FormulaEditor.Editor
{
    public class FormulaSerializer
    {

        public string Serialize(IFormulaTree formula)
        {
            var sb = new StringBuilder();
            Serialize(sb, formula);
            return sb.ToString();
        }

        private void Serialize(StringBuilder sb, IFormulaTree formula)
        {
            if (formula == null) return;

            // TODO: tight (1+2) or loose (1 + 2)? 
            // TODO: localize serialization

            var type = formula.GetType();

            // numbers
            if (type == typeof(FormulaNodeNumber)) Serialize(sb, (FormulaNodeNumber)formula);
            else if (type == typeof(FormulaNodePi)) Serialize(sb, (FormulaNodePi)formula);

            // arithmetic
            else if (type == typeof(FormulaNodeAdd)) Serialize(sb, (FormulaNodeAdd)formula);
            else if (type == typeof(FormulaNodeSubtract)) Serialize(sb, (FormulaNodeSubtract)formula);
            else if (type == typeof(FormulaNodeMultiply)) Serialize(sb, (FormulaNodeMultiply)formula);
            else if (type == typeof(FormulaNodeDivide)) Serialize(sb, (FormulaNodeDivide)formula);

            // relational operators
            else if (type == typeof(FormulaNodeEquals)) Serialize(sb, (FormulaNodeEquals)formula);
            else if (type == typeof(FormulaNodeNotEquals)) Serialize(sb, (FormulaNodeNotEquals)formula);
            else if (type == typeof(FormulaNodeLess)) Serialize(sb, (FormulaNodeLess)formula);
            else if (type == typeof(FormulaNodeLessEqual)) Serialize(sb, (FormulaNodeLessEqual)formula);
            else if (type == typeof(FormulaNodeGreater)) Serialize(sb, (FormulaNodeGreater)formula);
            else if (type == typeof(FormulaNodeGreaterEqual)) Serialize(sb, (FormulaNodeGreaterEqual)formula);

            // logic
            else if (type == typeof(FormulaNodeTrue)) Serialize(sb, (FormulaNodeTrue)formula);
            else if (type == typeof(FormulaNodeFalse)) Serialize(sb, (FormulaNodeFalse)formula);
            else if (type == typeof(FormulaNodeAnd)) Serialize(sb, (FormulaNodeAnd)formula);
            else if (type == typeof(FormulaNodeOr)) Serialize(sb, (FormulaNodeOr)formula);
            else if (type == typeof(FormulaNodeNot)) Serialize(sb, (FormulaNodeNot)formula);

            // min/max
            else if (type == typeof(FormulaNodeMin)) Serialize(sb, (FormulaNodeMin)formula);
            else if (type == typeof(FormulaNodeMax)) Serialize(sb, (FormulaNodeMax)formula);

            // exponential functions and logarithms
            else if (type == typeof(FormulaNodeExp)) Serialize(sb, (FormulaNodeExp)formula);
            else if (type == typeof(FormulaNodeLog)) Serialize(sb, (FormulaNodeLog)formula);
            else if (type == typeof(FormulaNodeLn)) Serialize(sb, (FormulaNodeLn)formula);

            // trigonometric functions
            else if (type == typeof(FormulaNodeSin)) Serialize(sb, (FormulaNodeSin)formula);
            else if (type == typeof(FormulaNodeCos)) Serialize(sb, (FormulaNodeCos)formula);
            else if (type == typeof(FormulaNodeTan)) Serialize(sb, (FormulaNodeTan)formula);
            else if (type == typeof(FormulaNodeArcsin)) Serialize(sb, (FormulaNodeArcsin)formula);
            else if (type == typeof(FormulaNodeArccos)) Serialize(sb, (FormulaNodeArccos)formula);
            else if (type == typeof(FormulaNodeArctan)) Serialize(sb, (FormulaNodeArctan)formula);

            // miscellaneous functions
            else if (type == typeof(FormulaNodeSqrt)) Serialize(sb, (FormulaNodeSqrt)formula);
            else if (type == typeof(FormulaNodeAbs)) Serialize(sb, (FormulaNodeAbs)formula);
            else if (type == typeof(FormulaNodeMod)) Serialize(sb, (FormulaNodeMod)formula);

            // sensors
            else if (type == typeof(FormulaNodeAccelerationX)) Serialize(sb, (FormulaNodeAccelerationX)formula);
            else if (type == typeof(FormulaNodeAccelerationY)) Serialize(sb, (FormulaNodeAccelerationY)formula);
            else if (type == typeof(FormulaNodeAccelerationZ)) Serialize(sb, (FormulaNodeAccelerationZ)formula);
            else if (type == typeof(FormulaNodeCompass)) Serialize(sb, (FormulaNodeCompass)formula);
            else if (type == typeof(FormulaNodeInclinationX)) Serialize(sb, (FormulaNodeInclinationX)formula);
            else if (type == typeof(FormulaNodeInclinationY)) Serialize(sb, (FormulaNodeInclinationY)formula);

            // object variables
            else if (type == typeof(FormulaNodeBrightness)) Serialize(sb, (FormulaNodeBrightness)formula);
            else if (type == typeof(FormulaNodeDirection)) Serialize(sb, (FormulaNodeDirection)formula);
            else if (type == typeof(FormulaNodeGhostEffect)) Serialize(sb, (FormulaNodeGhostEffect)formula);
            else if (type == typeof(FormulaNodeLayer)) Serialize(sb, (FormulaNodeLayer)formula);
            else if (type == typeof(FormulaNodePositionX)) Serialize(sb, (FormulaNodePositionX)formula);
            else if (type == typeof(FormulaNodePositionY)) Serialize(sb, (FormulaNodePositionY)formula);
            else if (type == typeof(FormulaNodeRotation)) Serialize(sb, (FormulaNodeRotation)formula);
            else if (type == typeof(FormulaNodeSize)) Serialize(sb, (FormulaNodeSize)formula);
            else if (type == typeof(FormulaNodeOpacity)) Serialize(sb, (FormulaNodeOpacity)formula);

            // user variables
            else if (type == typeof (FormulaNodeUserVariable)) Serialize(sb, (FormulaNodeUserVariable) formula);

            // brackets
            else if (type == typeof(FormulaNodeParentheses)) Serialize(sb, (FormulaNodeParentheses)formula);

            else throw new NotImplementedException();
        }

        private void Serialize(StringBuilder sb, FormulaNodeInfixOperator formula, string infixValue)
        {
            Serialize(sb, formula.LeftChild);
            sb.Append(infixValue);
            Serialize(sb, formula.RightChild);
        }

        #region numbers

        private void Serialize(StringBuilder sb, FormulaNodeNumber formula)
        {
            sb.Append(formula.Value);
        }

        private void Serialize(StringBuilder sb, FormulaNodePi formula)
        {
            sb.Append("pi");
        }

        #endregion

        #region arithmetic

        private void Serialize(StringBuilder sb, FormulaNodeAdd formula)
        {
            Serialize(sb, formula, "+");
        }

        private void Serialize(StringBuilder sb, FormulaNodeSubtract formula)
        {
            Serialize(sb, formula, "-");
        }

        private void Serialize(StringBuilder sb, FormulaNodeMultiply formula)
        {
            Serialize(sb, formula, "*");
        }

        private void Serialize(StringBuilder sb, FormulaNodeDivide formula)
        {
            Serialize(sb, formula, "/");
        }

        #endregion

        #region relational operators

        private void Serialize(StringBuilder sb, FormulaNodeEquals formula)
        {
            // TODO: discuss = ==
            Serialize(sb, formula, "=");
        }

        private void Serialize(StringBuilder sb, FormulaNodeNotEquals formula)
        {
            // TODO: discuss <> != not equals
            Serialize(sb, formula, "<>");
        }

        private void Serialize(StringBuilder sb, FormulaNodeLess formula)
        {
            Serialize(sb, formula, "<");
        }

        private void Serialize(StringBuilder sb, FormulaNodeLessEqual formula)
        {
            Serialize(sb, formula, "<=");
        }

        private void Serialize(StringBuilder sb, FormulaNodeGreater formula)
        {
            Serialize(sb, formula, ">");
        }

        private void Serialize(StringBuilder sb, FormulaNodeGreaterEqual formula)
        {
            Serialize(sb, formula, ">=");
        }

        #endregion

        #region logic

        private void Serialize(StringBuilder sb, FormulaNodeTrue formula)
        {
            // TODO: translate
            sb.Append("True");
        }

        private void Serialize(StringBuilder sb, FormulaNodeFalse formula)
        {
            // TODO: translate
            sb.Append("False");
        }

        private void Serialize(StringBuilder sb, FormulaNodeAnd formula)
        {
            // TODO: translate
            Serialize(sb, formula, " And ");
        }

        private void Serialize(StringBuilder sb, FormulaNodeOr formula)
        {
            // TODO: translate
            Serialize(sb, formula, " Or ");
        }

        private void Serialize(StringBuilder sb, FormulaNodeNot formula)
        {
            // TODO: discuss ! not
            sb.Append("Not ");
            Serialize(sb, formula.Child);
        }

        #endregion

        #region min/max

        private void Serialize(StringBuilder sb, FormulaNodeMin formula)
        {
            sb.Append("min{");
            Serialize(sb, formula.FirstChild);
            sb.Append(",");
            Serialize(sb, formula.SecondChild);
            sb.Append("}");
        }

        private void Serialize(StringBuilder sb, FormulaNodeMax formula)
        {
            sb.Append("max{");
            Serialize(sb, formula.FirstChild);
            sb.Append(",");
            Serialize(sb, formula.SecondChild);
            sb.Append("}");
        }

        #endregion

        #region exponential function and logarithms

        private void Serialize(StringBuilder sb, FormulaNodeExp formula)
        {
            sb.Append("exp(");
            Serialize(sb, formula.Child);
            sb.Append(")");
        }

        private void Serialize(StringBuilder sb, FormulaNodeLog formula)
        {
            sb.Append("log(");
            Serialize(sb, formula.Child);
            sb.Append(")");
        }

        private void Serialize(StringBuilder sb, FormulaNodeLn formula)
        {
            sb.Append("ln(");
            Serialize(sb, formula.Child);
            sb.Append(")");
        }


        #endregion

        #region trigonometric functions

        private void Serialize(StringBuilder sb, FormulaNodeSin formula)
        {
            sb.Append("sin(");
            Serialize(sb, formula.Child);
            sb.Append(")");
        }

        private void Serialize(StringBuilder sb, FormulaNodeCos formula)
        {
            sb.Append("cos(");
            Serialize(sb, formula.Child);
            sb.Append(")");
        }

        private void Serialize(StringBuilder sb, FormulaNodeTan formula)
        {
            sb.Append("tan(");
            Serialize(sb, formula.Child);
            sb.Append(")");
        }

        private void Serialize(StringBuilder sb, FormulaNodeArcsin formula)
        {
            sb.Append("arcsin(");
            Serialize(sb, formula.Child);
            sb.Append(")");
        }

        private void Serialize(StringBuilder sb, FormulaNodeArccos formula)
        {
            sb.Append("arccos(");
            Serialize(sb, formula.Child);
            sb.Append(")");
        }

        private void Serialize(StringBuilder sb, FormulaNodeArctan formula)
        {
            sb.Append("arctan(");
            Serialize(sb, formula.Child);
            sb.Append(")");
        }

        #endregion

        #region miscellaneous functions

        private void Serialize(StringBuilder sb, FormulaNodeSqrt formula)
        {
            sb.Append("sqrt(");
            Serialize(sb, formula.Child);
            sb.Append(")");
        }

        private void Serialize(StringBuilder sb, FormulaNodeAbs formula)
        {
            sb.Append("|");
            Serialize(sb, formula.Child);
            sb.Append("|");
        }

        private void Serialize(StringBuilder sb, FormulaNodeMod formula)
        {
            Serialize(sb, formula, " mod ");
        }

        #endregion

        #region sensors

        private void Serialize(StringBuilder sb, FormulaNodeSensor formula)
        {
            throw new NotImplementedException();
        }


        #endregion

        #region object variables

        private void Serialize(StringBuilder sb, FormulaNodeObjectVariable formula)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region user variables

        private void Serialize(StringBuilder sb, FormulaNodeUserVariable formula)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region brackets

        private void Serialize(StringBuilder sb, FormulaNodeParentheses formula)
        {
            sb.Append("(");
            Serialize(sb, formula.Child);
            sb.Append(")");
        }

        #endregion
    }
}
