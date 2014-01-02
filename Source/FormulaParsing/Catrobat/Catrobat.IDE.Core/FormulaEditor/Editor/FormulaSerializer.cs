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
            else if (type == typeof(FormulaNodeLess)) Serialize(sb, (FormulaNodeLess)formula);
            else if (type == typeof(FormulaNodeLessEqual)) Serialize(sb, (FormulaNodeLessEqual)formula);
            else if (type == typeof(FormulaNodeGreater)) Serialize(sb, (FormulaNodeGreater)formula);
            else if (type == typeof(FormulaNodeGreaterEqual)) Serialize(sb, (FormulaNodeGreaterEqual)formula);

            // logic

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

            // object variables

            // user variables
            else if (type == typeof(FormulaNodeUserVariable)) Serialize(sb, (FormulaNodeUserVariable)formula);

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
            throw new NotImplementedException();
        }

        private void Serialize(StringBuilder sb, FormulaNodeFalse formula)
        {
            throw new NotImplementedException();
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



        #endregion

        #region object variables


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
