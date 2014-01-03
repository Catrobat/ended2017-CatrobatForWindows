using System.Globalization;
using Catrobat.IDE.Core.CatrobatObjects.Formulas.FormulaToken;
using Catrobat.IDE.Core.ExtensionMethods;
using Catrobat.IDE.Core.CatrobatObjects.Formulas;
using Catrobat.IDE.Core.CatrobatObjects.Formulas.FormulaNodes;
using Catrobat.IDE.Core.CatrobatObjects.Variables;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Catrobat.IDE.Core.FormulaEditor.Editor
{
    class FormulaParser
    {

        private readonly IDictionary<string, UserVariable> _userVariables;
        private readonly ObjectVariableEntry _objectVariable;

        public FormulaParser(IEnumerable<UserVariable> userVariables, ObjectVariableEntry objectVariable)
        {
            _userVariables = userVariables.ToDictionary(variable => variable.Name);
            _objectVariable = objectVariable;
        }

        public bool Parse(string input, out IFormulaTree formula, out IEnumerable<string> parsingErrors)
        {
            // TODO: how to translate parsing errors?

            var parsingErrors2 = new List<string>();
            parsingErrors = parsingErrors2;

            return Parse(input, out formula, ref parsingErrors2);
        }

        private bool Parse(string input, out IFormulaTree formula, ref List<string> parsingErrors)
        {
            formula = null;

            if (string.IsNullOrWhiteSpace(input)) return true;

            // syntax
            List<IFormulaToken> tokens;
            if (!Tokenize(input, out tokens, ref parsingErrors)) return false;

            // semantics
            if (!Interpret(tokens, out formula, ref parsingErrors)) return false;

            return true;
        }

        private bool Tokenize(string input, out List<IFormulaToken> tokens, ref List<string> parsingErrors)
        {
            tokens = new List<IFormulaToken>();

            var index = 0;
            while (index < input.Length)
            {
                // ignore whitespace
                if (char.IsWhiteSpace(input[index])) index++;

                // brackets
                else if (Tokenize(input, ref index, "(", () => FormulaTokenFactory.CreateParenthesisToken(true), ref tokens)) { }
                else if (Tokenize(input, ref index, ")", () => FormulaTokenFactory.CreateParenthesisToken(false), ref tokens)) { }
                else if (Tokenize(input, ref index, "[", () => FormulaTokenFactory.CreateSquareBracketToken(true), ref tokens)) { }
                else if (Tokenize(input, ref index, "]", () => FormulaTokenFactory.CreateSquareBracketToken(false), ref tokens)) { }
                else if (Tokenize(input, ref index, "{", () => FormulaTokenFactory.CreateCurlyBraceToken(true), ref tokens)) { }
                else if (Tokenize(input, ref index, "}", () => FormulaTokenFactory.CreateCurlyBraceToken(false), ref tokens)) { }

                // numbers
                else if (Tokenize(input, ref index, "pi", FormulaTokenFactory.CreatePiToken, ref tokens)) { }

                // arithmetic
                else if (Tokenize(input, ref index, "+", FormulaTokenFactory.CreatePlusToken, ref tokens)) { }
                else if (Tokenize(input, ref index, "-", FormulaTokenFactory.CreateMinusToken, ref tokens)) { }
                else if (Tokenize(input, ref index, "*", FormulaTokenFactory.CreateMultiplyToken, ref tokens)) { }
                else if (Tokenize(input, ref index, "/", FormulaTokenFactory.CreateDivideToken, ref tokens)) { }
                else if (Tokenize(input, ref index, ":", FormulaTokenFactory.CreateDivideToken, ref tokens)) { }

                // relational operators
                else if (Tokenize(input, ref index, "==", FormulaTokenFactory.CreateEqualsToken, ref tokens)) { }
                else if (Tokenize(input, ref index, "=", FormulaTokenFactory.CreateEqualsToken, ref tokens)) { }
                else if (Tokenize(input, ref index, "<>", FormulaTokenFactory.CreateNotEqualsToken, ref tokens)) { }
                else if (Tokenize(input, ref index, "!=", FormulaTokenFactory.CreateNotEqualsToken, ref tokens)) { }
                else if (Tokenize(input, ref index, "<=", FormulaTokenFactory.CreateLessEqualToken, ref tokens)) { }
                else if (Tokenize(input, ref index, "<", FormulaTokenFactory.CreateLessToken, ref tokens)) { }
                else if (Tokenize(input, ref index, ">=", FormulaTokenFactory.CreateGreaterEqualToken, ref tokens)) { }
                else if (Tokenize(input, ref index, ">", FormulaTokenFactory.CreateGreaterToken, ref tokens)) { }
                
                // logic
                // TODO

                // min/max
                else if (Tokenize(input, ref index, "min", FormulaTokenFactory.CreateMinToken, ref tokens)) { }
                else if (Tokenize(input, ref index, "max", FormulaTokenFactory.CreateMaxToken, ref tokens)) { }

                // exponential function and logarithms
                else if (Tokenize(input, ref index, "exp", FormulaTokenFactory.CreateExpToken, ref tokens)) { }
                else if (Tokenize(input, ref index, "e^", FormulaTokenFactory.CreateExpToken, ref tokens)) { }
                else if (Tokenize(input, ref index, "log", FormulaTokenFactory.CreateLogToken, ref tokens)) { }
                else if (Tokenize(input, ref index, "ln", FormulaTokenFactory.CreateLnToken, ref tokens)) { }

                // trigonometric functions
                else if (Tokenize(input, ref index, "sin", FormulaTokenFactory.CreateSinToken, ref tokens)) { }
                else if (Tokenize(input, ref index, "cos", FormulaTokenFactory.CreateCosToken, ref tokens)) { }
                else if (Tokenize(input, ref index, "tan", FormulaTokenFactory.CreateTanToken, ref tokens)) { }
                else if (Tokenize(input, ref index, "arcsin", FormulaTokenFactory.CreateArcsinToken, ref tokens)) { }
                else if (Tokenize(input, ref index, "arccos", FormulaTokenFactory.CreateArccosToken, ref tokens)) { }
                else if (Tokenize(input, ref index, "arctan", FormulaTokenFactory.CreateArctanToken, ref tokens)) { }

                // miscellaneous functions
                else if (Tokenize(input, ref index, "sqrt", FormulaTokenFactory.CreateSqrtToken, ref tokens)) { }
                else if (Tokenize(input, ref index, "|", FormulaTokenFactory.CreateVerticalBarToken, ref tokens)) { }
                else if (Tokenize(input, ref index, "abs", FormulaTokenFactory.CreateVerticalBarToken, ref tokens)) { }
                else if (Tokenize(input, ref index, "mod", FormulaTokenFactory.CreateModToken, ref tokens)) { }

                // sensors
                // TODO

                // object variables
                // TODO

                // user variables

                // numbers
                else if (TokenizeNumber(input, ref index, ref tokens)) { }
                else
                {
                    // TODO: add parsing error like "Unknown token at ..."
                    parsingErrors.Add("An error occured. ");
                    return false;
                }
            }
            return true;
        }

        private bool Tokenize(string input, ref int startIndex, string tokenValue, Func<IFormulaToken> constructor, ref List<IFormulaToken> tokens)
        {
            if (!input.StartsWith(tokenValue, startIndex, StringComparison.CurrentCultureIgnoreCase)) return false;
            tokens.Add(constructor());
            startIndex += tokenValue.Length;
            return true;
        }

        private bool TokenizeNumber(string input, ref int startIndex, ref List<IFormulaToken> tokens)
        {
            int length;
            double value = 0;
            for (length = 1; startIndex + length <= input.Length; length++)
            {
                double parsedValue = 0;
                if (input[startIndex + length - 1] != '+' &&
                    input[startIndex + length - 1] != '-' &&
                    double.TryParse(
                        s: input.Substring(startIndex, length),
                        style: NumberStyles.Number,
                        provider: CultureInfo.CurrentCulture,
                        result: out parsedValue))
                {
                    value = parsedValue;
                } else
                {
                    length--;
                    break;
                }
            }
            if (length == 0) return false;
            tokens.Add(FormulaTokenFactory.CreateNumberToken(value));
            startIndex += length;
            return true;
        }

        private bool Interpret(List<IFormulaToken> tokens, out IFormulaTree formula, ref List<string> parsingErrors)
        {
            formula = null;
            if (!(InterpretBrackets(ref tokens, ref parsingErrors) &&
                  InterpretVerticalBar(ref tokens, ref parsingErrors))) return false;

            return InterpretNodes(tokens.Cast<IFormulaTree>().ToList(), out formula, ref parsingErrors);
        }

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
                    if (!openingBracket.IsOpening)
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
                            if (((FormulaTokenBracket)token).IsOpening) balance++; else balance--;
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
            return true;
        }

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

        private bool InterpretNodes(List<IFormulaTree> nodes, out IFormulaTree formula, ref List<string> parsingErrors)
        {
            formula = null;

            var openNodes = nodes.Where(node => node.Children.Any(child => child == null));

            // unary functions
            var currentNode = openNodes.OfType<FormulaNodeUnaryFunction>().FirstOrDefault();
            while (currentNode != null)
            {
                var index = nodes.IndexOf(currentNode);
                if (index == nodes.Count - 1)
                {
                    // TODO: add parsing error like "missing value for function"
                    parsingErrors.Add("An error occured. ");
                    return false;
                }
                currentNode.Child = nodes[index + 1];

                // brackets are included in unary functions
                if (currentNode.Child.GetType() == typeof(FormulaNodeParentheses))
                {
                    currentNode.Child = ((FormulaNodeParentheses)currentNode.Child).Child;
                }
                nodes.RemoveAt(index + 1);
                currentNode = openNodes.OfType<FormulaNodeUnaryFunction>().FirstOrDefault();
            }

            // first priority infix operators
            var currentNode2 = openNodes.OfType<FormulaNodeInfixOperator>().FirstOrDefault(node => node.GetType() != typeof(FormulaNodeAdd));
            while (currentNode2 != null)
            {
                var index = nodes.IndexOf(currentNode2);
                if (index == 0)
                {
                    // TODO: add parsing error like "missing value before operator"
                    parsingErrors.Add("An error occured. ");
                    return false;
                }
                if (index == nodes.Count - 1)
                {
                    // TODO: add parsing error like "missing value after operator"
                    parsingErrors.Add("An error occured. ");
                    return false;
                }
                currentNode2.RightChild = nodes[index + 1];
                nodes.RemoveAt(index + 1);
                currentNode2.LeftChild = nodes[index - 1];
                nodes.RemoveAt(index - 1);

                currentNode2 = openNodes.OfType<FormulaNodeInfixOperator>().FirstOrDefault(node => node.GetType() != typeof(FormulaNodeAdd));
            }

            // second priority infix operators
            var currentNode3 = openNodes.OfType<FormulaNodeInfixOperator>().FirstOrDefault(node => node.GetType() == typeof(FormulaNodeAdd));
            while (currentNode3 != null)
            {
                var index = nodes.IndexOf(currentNode3);
                if (index == 0)
                {
                    // TODO: add parsing error like "missing value before operator"
                    parsingErrors.Add("An error occured. ");
                    return false;
                }
                if (index == nodes.Count - 1)
                {
                    // TODO: add parsing error like "missing value after operator"
                    parsingErrors.Add("An error occured. ");
                    return false;
                }
                currentNode3.RightChild = nodes[index + 1];
                nodes.RemoveAt(index + 1);
                currentNode3.LeftChild = nodes[index - 1];
                nodes.RemoveAt(index - 1);

                currentNode3 = openNodes.OfType<FormulaNodeInfixOperator>().FirstOrDefault(node => node.GetType() == typeof(FormulaNodeAdd));
            }
            if (openNodes.Any()) throw new NotImplementedException();

            if (nodes.Count > 1)
            {
                // TODO: add parsing error
                return false;
            }

            formula = nodes.Single();
            return true;
        }

    }
}
