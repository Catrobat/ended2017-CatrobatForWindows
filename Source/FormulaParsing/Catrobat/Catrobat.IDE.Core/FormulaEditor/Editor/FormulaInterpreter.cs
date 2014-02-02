using Catrobat.IDE.Core.CatrobatObjects.Formulas;
using Catrobat.IDE.Core.CatrobatObjects.Formulas.FormulaNodes;
using Catrobat.IDE.Core.CatrobatObjects.Formulas.FormulaTokens;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace Catrobat.IDE.Core.FormulaEditor.Editor
{
    internal class FormulaInterpreter
    {
        public IFormulaTree Interpret(IEnumerable<IFormulaToken> tokens, out string parsingError)
        {
            // TODO: add also parsingErrors for formulas like sin(True + 2.3)
            parsingError = null;
            var tokens2 = tokens.ToList();
            InterpretNumbers(ref tokens2);
            InterpretBrackets(ref tokens2, out parsingError);
            return parsingError != null ? null : InterpretNodes(tokens2.Cast<IFormulaTree>().ToList(), out parsingError);
        }

        private void InterpretNumbers(ref List<IFormulaToken> tokens)
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
        }

        [Obsolete("Rewrite into a single function with two lists (interpreted and not interpreted yet)")]
        private void InterpretBrackets(ref List<IFormulaToken> tokens, out string parsingError)
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
                        parsingError = "An error occured. ";
                        return;
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
                        parsingError = "An error occured. ";
                        return;
                    }

                    // interpret tokens between parentheses
                    var childTokens = tokens.Skip(index + 1).Take(childTokensCount).ToList();
                    var child = Interpret(childTokens, out parsingError);
                    if (parsingError != null) return;
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
            parsingError = null;
        }

        [Obsolete("Rewrite into a single function with two lists (interpreted and not interpreted yet)")]
        private IFormulaTree InterpretNodes(List<IFormulaTree> nodes, out string parsingError)
        {
            InterpretUnaryFunctions(nodes, out parsingError);
            if (parsingError != null) return null;

            InterpretInfixOperators(nodes, out parsingError);
            if (parsingError != null) return null;

            var openNodes = nodes.Where(node => node.Children.Any(child => child == null));
            if (openNodes.Any()) return null; // throw new NotImplementedException();

            if (nodes.Count > 1)
            {
                // TODO: add parsing error
                return null;
            }

            return nodes.Single();
        }

        /// <summary>Sets <see cref="FormulaNodeUnaryFunction.Child"/> to the next node. </summary>
        [Obsolete("Rewrite into a single function with two lists (interpreted and not interpreted yet)")]
        private void InterpretUnaryFunctions(List<IFormulaTree> nodes, out string parsingError)
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
                    parsingError = "An error occured. ";
                    return;
                }
                node.Child = nodes[childIndex];
                nodes.RemoveAt(childIndex);

                // remove redundant brackets
                if (node.Child.GetType() == typeof(FormulaNodeParentheses))
                {
                    node.Child = ((FormulaNodeParentheses)node.Child).Child;
                }
            }
            parsingError = null;
        }

        /// <summary>Sets <see cref="FormulaNodeInfixOperator.LeftChild"/> and <see cref="FormulaNodeInfixOperator.RightChild"/> to the adjacent nodes. </summary>
        [Obsolete("Rewrite into a single function with two lists (interpreted and not interpreted yet)")]
        private void InterpretInfixOperators(List<IFormulaTree> nodes, out string parsingError)
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
                    parsingError = "An error occured. ";
                    return;
                }
                node.RightChild = nodes[index + 1];
                nodes.RemoveAt(index + 1);
                if (index == 0)
                {
                    // TODO: add parsing error like "missing value before operator"
                    parsingError = "An error occured. ";
                    return;
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
                    parsingError = "An error occured. ";
                    return;
                }
                node.RightChild = nodes[index + 1];
                nodes.RemoveAt(index + 1);
                if (index == 0)
                {
                    // TODO: add parsing error like "missing value before operator"
                    parsingError = "An error occured. ";
                    return;
                }
                node.LeftChild = nodes[index - 1];
                nodes.RemoveAt(index - 1);
            }
            parsingError = null;
        }
   

}
}
