using System.Globalization;
using Catrobat.IDE.Core.CatrobatObjects.Formulas;
using Catrobat.IDE.Core.CatrobatObjects.Formulas.FormulaNodes;
using Catrobat.IDE.Core.CatrobatObjects.Formulas.FormulaTokens;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Catrobat.IDE.Core.FormulaEditor.Editor
{
    internal class FormulaInterpreter
    {
        public bool Interpret(IEnumerable<IFormulaToken> tokens, out IFormulaTree formula,
            out IEnumerable<string> parsingErrors)
        {
            var parsingErrors2 = new List<string>();
            parsingErrors = parsingErrors2;
            return Interpret(tokens.ToList(), out formula, ref parsingErrors2);
        }

        private bool Interpret(List<IFormulaToken> tokens, out IFormulaTree formula, ref List<string> parsingErrors)
        {
            formula = null;
            if (!(InterpretNumbers(ref tokens) &&
                  InterpretBrackets(ref tokens, ref parsingErrors) &&
                  InterpretVerticalBar(ref tokens, ref parsingErrors))) return false;

            return InterpretNodes(tokens.Cast<IFormulaTree>().ToList(), out formula, ref parsingErrors);
        }

        private bool InterpretNumbers(ref List<IFormulaToken> tokens)
        {
            for (var i = 0; i < tokens.Count; i++)
            {
                var tokenType = tokens[i].GetType();
                if (tokenType != typeof (FormulaNodeNumber) && tokenType != typeof (FormulaTokenDecimalSpearator))
                    continue;
                var numberTokens = tokens.Skip(i)
                    .TakeWhile(
                        token =>
                            token.GetType() == typeof (FormulaNodeNumber) ||
                            token.GetType() == typeof (FormulaTokenDecimalSpearator))
                    .ToList();
                if (numberTokens.Count == 1) continue;
                var value = numberTokens.Aggregate(string.Empty,
                    (accumulate, token) => accumulate + (token.GetType() == typeof (FormulaNodeNumber)
                        ? ((FormulaNodeNumber) token).Value.ToString()
                        : CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator));
                var numberToken = FormulaTokenFactory.CreateNumberToken(double.Parse(value));
                tokens.RemoveRange(i, numberTokens.Count);
                tokens.Insert(i, numberToken);
            }
            return true;
        }

        [Obsolete("Rewrite into a single function with two lists (interpreted and not interpreted yet)")]
        private bool InterpretBrackets(ref List<IFormulaToken> tokens, ref List<string> parsingErrors)
        {
            // changing the collection using IEnumerator is not supported
            var index = 0;
            while (index < tokens.Count)
            {
                var openingBracket = tokens[index] as FormulaTokenBracket;
                if (openingBracket != null)
                {
                    // too many closing brackets
                    if (openingBracket.IsClosing)
                    {
                        // TODO: add parsing error like "Remove unmatched closing bracket. "
                        parsingErrors.Add("An error occured. ");
                        return false;
                    }

                    // find corresponding closing bracket
                    var bracketType = openingBracket.GetType();
                    var balance = 1;
                    var childTokensCount = 0;
                    foreach (var token in tokens.Skip(index + 1))
                    {
                        if (token.GetType() == bracketType)
                        {
                            if (((FormulaTokenBracket) token).IsOpening) balance++;
                            else balance--;
                            if (balance == 0) break;
                        }
                        childTokensCount++;
                    }

                    // too less closing brackets
                    if (balance != 0)
                    {
                        // TODO: add parsing error like "Remove unmatched opening bracket. "
                        parsingErrors.Add("An error occured. ");
                        return false;
                    }

                    // interpret tokens between parentheses
                    var childTokens = tokens.Skip(index + 1).Take(childTokensCount).ToList();
                    IFormulaTree child;
                    if (!Interpret(childTokens, out child, ref parsingErrors)) return false;
                    tokens.RemoveRange(index, childTokensCount + 2);

                    // remove redundant parentheses
                    while (child.GetType() == typeof (FormulaNodeParentheses))
                    {
                        child = ((FormulaNodeParentheses) child).Child;
                    }
                    tokens.Insert(index, FormulaTreeFactory.CreateParenthesesNode(child));
                }
                index++;
            }

            // TODO: enumerate second time reverse to enhance the parsing error for the case of too less closing brackets

            return true;
        }

        [Obsolete("Rewrite into a single function with two lists (interpreted and not interpreted yet)")]
        private bool InterpretVerticalBar(ref List<IFormulaToken> tokens, ref List<string> parsingErrors)
        {
            // changing the collection using IEnumerator is not supported
            var index = 0;
            while (index < tokens.Count)
            {
                var verticalBar = tokens[index] as FormulaTokenVerticalBar;
                if (verticalBar != null)
                {
                    throw new NotImplementedException();
                }
                index++;
            }
            return true;
        }

        [Obsolete("Rewrite into a single function with two lists (interpreted and not interpreted yet)")]
        private bool InterpretNodes(List<IFormulaTree> nodes, out IFormulaTree formula, ref List<string> parsingErrors)
        {
            formula = null;

            if (!(InterpretUnaryFunctions(nodes, ref parsingErrors) &&
                  InterpretInfixOperators(nodes, ref parsingErrors))) return false;

            var openNodes = nodes.Where(node => node.Children.Any(child => child == null));
            if (openNodes.Any()) throw new NotImplementedException();

            if (nodes.Count > 1)
            {
                // TODO: add parsing error
                return false;
            }

            formula = nodes.Single();
            return true;
        }

        /// <summary>Sets <see cref="FormulaNodeUnaryFunction.Child"/> to the next node. </summary>
        [Obsolete("Rewrite into a single function with two lists (interpreted and not interpreted yet)")]
        private bool InterpretUnaryFunctions(List<IFormulaTree> nodes, ref List<string> parsingErrors)
        {
            for (var index = 0; index < nodes.Count; index++)
            {
                // look for FormulaNodeUnaryFunction with missing child
                if (nodes[index].GetType() != typeof(FormulaNodeUnaryFunction)) continue;
                var node = (FormulaNodeUnaryFunction)nodes[index];
                if (node.Child != null) continue;

                // set next node as child
                var childIndex = index + 1;
                if (childIndex == nodes.Count)
                {
                    // TODO: add parsing error like "missing value for function"
                    parsingErrors.Add("An error occured. ");
                    return false;
                }
                node.Child = nodes[childIndex];
                nodes.RemoveAt(childIndex);

                // remove redundant brackets
                if (node.Child.GetType() == typeof(FormulaNodeParentheses))
                {
                    node.Child = ((FormulaNodeParentheses)node.Child).Child;
                }
            }
            return true;
        }

        [Obsolete("Rewrite into a single function with two lists (interpreted and not interpreted yet)")]
        private bool InterpretBinaryFunctions(List<IFormulaTree> nodes, ref List<string> parsingErrors)
        {
            throw new NotImplementedException();
        }
        [Obsolete("Rewrite into a single function with two lists (interpreted and not interpreted yet)")]
        private bool InterpretUnaryFormulaTrees(List<IFormulaTree> nodes, ref List<string> parsingErrors)
        {
            throw new NotImplementedException();
        }

        /// <summary>Sets <see cref="FormulaNodeInfixOperator.LeftChild"/> and <see cref="FormulaNodeInfixOperator.RightChild"/> to the adjacent nodes. </summary>
        [Obsolete("Rewrite into a single function with two lists (interpreted and not interpreted yet)")]
        private bool InterpretInfixOperators(List<IFormulaTree> nodes, ref List<string> parsingErrors)
        {
            var openNodes = nodes.Where(node => node.Children.Any(child => child == null));

            // first priority infix operators
            for (var index = 0; index < nodes.Count; index++)
            {
                // look for */ with missing children
                var nodeType = nodes[index].GetType();
                if (!(nodeType == typeof(FormulaNodeMultiply) || nodeType == typeof(FormulaNodeDivide))) continue;
                var node = (FormulaNodeInfixOperator)nodes[index];
                if (node.LeftChild != null && node.RightChild != null) continue;

                // set adjacent nodes as children
                if (index + 1 == nodes.Count)
                {
                    // TODO: add parsing error like "missing value after operator"
                    parsingErrors.Add("An error occured. ");
                    return false;
                }
                node.RightChild = nodes[index + 1];
                nodes.RemoveAt(index + 1);
                if (index == 0)
                {
                    // TODO: add parsing error like "missing value before operator"
                    parsingErrors.Add("An error occured. ");
                    return false;
                }
                node.LeftChild = nodes[index - 1];
                nodes.RemoveAt(index - 1);
             }

            // remaining infix operators
            // first priority infix operators
            for (var index = 0; index < nodes.Count; index++)
            {
                // look for FormulaNodeInfixOperator with missing children
                if (nodes[index].GetType() != typeof(FormulaNodeInfixOperator)) continue;
                var node = (FormulaNodeInfixOperator)nodes[index];
                if (node.LeftChild != null && node.RightChild != null) continue;

                // set adjacent nodes as children
                if (index + 1 == nodes.Count)
                {
                    // TODO: add parsing error like "missing value after operator"
                    parsingErrors.Add("An error occured. ");
                    return false;
                }
                node.RightChild = nodes[index + 1];
                nodes.RemoveAt(index + 1);
                if (index == 0)
                {
                    // TODO: add parsing error like "missing value before operator"
                    parsingErrors.Add("An error occured. ");
                    return false;
                }
                node.LeftChild = nodes[index - 1];
                nodes.RemoveAt(index - 1);
            }
            return true;
        }
   

}
}
