using Catrobat.IDE.Core.CatrobatObjects.Formulas.FormulaToken;
using Catrobat.IDE.Core.CatrobatObjects.Formulas.FormulaTree;
using Catrobat.IDE.Core.ExtensionMethods;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;

namespace Catrobat.IDE.Core.FormulaEditor
{
    internal class FormulaInterpreter
    {
        public static IFormulaTree Interpret(IList<IFormulaToken> tokens, out ParsingError parsingError)
        {
            // TODO: split to InterpretNumber and InterpretLogic

            var sw = new Stopwatch();
            sw.Start();

            // validate input
            if (tokens == null)
            {
                parsingError = null;
                return null;
            }

            // interpret tokens
            var instance = new FormulaInterpreter();
            var result = instance.Interpret2(tokens);
            parsingError = instance.ParsingError;

            sw.Stop();
            Debug.WriteLine("Interpreter.Interpret needed " + sw.ElapsedMilliseconds + "ms");

            return result;
        }

        public static Range CompleteToken(IList<IFormulaToken> tokens, int index)
        {
            var sw = new Stopwatch();
            sw.Start();

            // validate input
            if (tokens == null || !(0 <= index && index < tokens.Count)) return Range.Empty(0);

            // complete token
            var instance = new FormulaInterpreter();
            var result = instance.CompleteToken2(tokens, index);

            sw.Stop();
            Debug.WriteLine("Interpreter.CompleteToken needed " + sw.ElapsedMilliseconds + "ms");

            return result;
        }

        private IFormulaTree Interpret2(IList<IFormulaToken> tokens)
        {
            IEnumerable<IFormulaToken> tokens2 = SetOrigin(tokens);
            tokens2 = InterpretNumbersForward(tokens2);
            tokens2 = InterpretBrackets(tokens2);
            tokens2 = InterpretNonParameter(tokens2);
            tokens2 = InterpretFunctions(tokens2);
            tokens2 = InterpretMinusTokenForward(tokens2);
            tokens2 = InterpretOperators(tokens2);
            var formula = tokens2.Cast<IFormulaTree>().FirstOrDefault();

            if (IsCancellationRequested) return null;

            if (formula == null)
            {
                SetParsingError(null, "Empty formula");
                return null;
            }
            if (GetOrigin(formula).Length < tokens.Count)
            {
                SetParsingError(Range.Empty(GetOrigin(formula).End), "Missing infix operator. ");
                return null;
            }

            // TODO: don't throw exceptions (300ms!)
            try
            {
                var isNumber = InterpretSemantic(formula);
            }
            catch (SemanticsErrorException ex)
            {
                var selection = ex.Node == null ? Range.Empty(0) : GetOrigin(ex.Node);
                ParsingError = new ParsingError(ex.Message, selection.Start, selection.Length);
                return null;
            }

            return formula;
        }

        /// <remarks>See <see cref="http://stackoverflow.com/questions/160118/static-and-instance-methods-with-the-same-name" />. </remarks>
        private Range CompleteToken2(IList<IFormulaToken> tokens, int index)
        {
            tokens = SetOrigin(tokens).ToList();
            var token = tokens[index];

            // complete number token
            if (token is FormulaNodeNumber || token is FormulaTokenDecimalSeparator)
            {
                var numberTokens = CompleteNumberToken(tokens, index);
                return GetOrigin(numberTokens);
            }

            // complete parenthesis
            var parenthesisToken = token as FormulaTokenParenthesis;
            if (parenthesisToken != null)
            {
                var parenthesesNode = (parenthesisToken.IsOpening
                    ? CompleteBrackets(tokens.Skip(index))
                    : CompleteBrackets(tokens.Take(index + 1).Reverse(), false)).FirstOrDefault();
                if (parenthesesNode != null) return GetOrigin(parenthesesNode);
            }

            // complete function
            if (token is IFormulaFunction)
            {
                var tokensAftwerwards = tokens.Skip(index);
                tokensAftwerwards = CompleteNumbers(tokensAftwerwards);
                tokensAftwerwards = CompleteBrackets(tokensAftwerwards);
                tokensAftwerwards = CompleteFunctions(tokensAftwerwards);

                var functionNode = tokensAftwerwards.FirstOrDefault();
                if (functionNode != null) return GetOrigin(functionNode);
            }

            // complete prefix operator
            if (token is FormulaNodePrefixOperator)
            {
                // TODO: implement me
                return GetOrigin(token);
            }

            var minusToken = token as FormulaNodeSubtract;
            if (minusToken != null)
            {
                // TODO: implement me
                return GetOrigin(token);
            }

            // complete infix operator
            var infixOperatorToken = token as FormulaNodeInfixOperator;
            if (infixOperatorToken != null)
            {
                var dummyToken = FormulaTokenFactory.CreatePiToken();

                infixOperatorToken.LeftChild = dummyToken;
                var tokensAfterwards = Enumerable.Repeat(dummyToken, 1).Concat(tokens.Skip(index));
                SetOrigin(dummyToken, Range.FromIndices(index - 1, 1));
                tokensAfterwards = CompleteNumbers(tokensAfterwards);
                tokensAfterwards = CompleteBrackets(tokensAfterwards);
                tokensAfterwards = CompleteFunctions(tokensAfterwards);
                tokensAfterwards = InterpretMinusTokenForward(tokensAfterwards);
                tokensAfterwards = CompleteOperators(tokensAfterwards);
                var tmp = (FormulaNodeInfixOperator) tokensAfterwards.FirstOrDefault();

                var tokensBefore = tokens.Take(index + 1).Reverse().Concat(Enumerable.Repeat(dummyToken, 1));
                SetOrigin(dummyToken, Range.FromIndices(index + 1, 1));
                tokensBefore = CompleteNumbers(tokensBefore);
                tokensBefore = CompleteBrackets(tokensBefore, false);
                tokensBefore = CompleteFunctions(tokensBefore);
                tokensBefore = InterpretMinusTokenBackwards(tokensBefore);
                tokensBefore = CompleteOperatorsBackwards(tokensBefore);
                var tmp2 = (FormulaNodeInfixOperator) tokensBefore.FirstOrDefault();

                // TODO: SetOrigin(token) = common range of tmp and tmp2
            }

            // complete constant, parameter separator token or failed tokens from above
            return GetOrigin(token);
        }

        #region Members

        private readonly Dictionary<IFormulaToken, Range> _origin = new Dictionary<IFormulaToken, Range>(new ReferenceEqualityComparer<IFormulaToken>());
        private Range GetOrigin(IFormulaToken token)
        {
            return _origin[token];
        }
        private Range GetOrigin(IFormulaToken from, IFormulaToken to)
        {
            return Range.Combine(GetOrigin(from), GetOrigin(to));
        }
        private Range GetOrigin(ICollection<IFormulaToken> tokens)
        {
            return tokens.Count == 0
                ? Range.Empty(0)
                : GetOrigin(tokens.First(), tokens.Last());
        }

        private Range GetOrigin(IEnumerable<IFormulaToken> tokens)
        {
            return GetOrigin(tokens.ToList());
        }
        private void SetOrigin(IFormulaToken token, Range value)
        {
            _origin[token] = value;
        }
        private void SetOrigin(IFormulaToken token, IFormulaToken otherToken)
        {
            _origin[token] = GetOrigin(otherToken);
        }
        private void SetOrigin(IFormulaToken token, ICollection<IFormulaToken> tokens)
        {
            _origin[token] = GetOrigin(tokens);
        }
        private IEnumerable<IFormulaToken> SetOrigin(IList<IFormulaToken> tokens)
        {
            for (var index = 0; index < tokens.Count; index++)
            {
                var token = tokens[index];
                // IFormulaTree is misused as IFormulaToken
                if (token is IFormulaTree) token = (IFormulaToken)token.Clone();
                SetOrigin(token, Range.Single(index));
                yield return token;
            }
        }
 
        private ParsingError ParsingError { get; set; }
        private void SetParsingError(Range source, string message)
        {
            if (IsCancellationRequested) return;
            ParsingError = new ParsingError(message, source.Start, source.Length);
        }
        private void SetParsingError(IFormulaToken source, string message)
        {
            SetParsingError(source == null ? Range.Empty(0) : GetOrigin(source), message);
        }
        private bool IsCancellationRequested
        {
            get { return ParsingError != null; }
        }

        #endregion

        #region Numbers

        /// <summary>
        /// Merges <see cref="FormulaTokenFactory.CreateDigitToken" /> and <see cref="FormulaTokenFactory.CreateDecimalSeparatorToken" /> to <see cref="FormulaTreeFactory.CreateNumberNode" />. 
        /// </summary>
        /// <remarks>Running time O(n). </remarks>
        /// <remarks>
        /// This function is implemented using yield return to report early errors first. 
        /// </remarks>
        private IEnumerable<IFormulaToken> InterpretNumbersForward(IEnumerable<IFormulaToken> tokens)
        {
            var decimalSeparator = CultureInfo.InvariantCulture.NumberFormat.NumberDecimalSeparator;
            var valueTokens = new List<IFormulaToken>();
            var valueBuilder = new StringBuilder();
            var valueContainsDecimalSeparator = false;

            // Helper to avoid duplicate code
            Func<IFormulaToken> createCommonToken = () =>
            {
                IFormulaToken commonToken;
                if (valueTokens.Count == 1)
                {
                    if (valueTokens[0] is FormulaTokenDecimalSeparator)
                    {
                        SetParsingError(valueTokens[0], "Remove decimal separator. ");
                        return null;
                    }
                    commonToken = valueTokens[0];
                }
                else
                {
                    var commonOrigin = GetOrigin(valueTokens);
                    var valueString = valueBuilder.ToString();
                    double value;
                    if (!double.TryParse(valueString, NumberStyles.Number, CultureInfo.InvariantCulture, out value))
                    {
                        SetParsingError(commonOrigin, "Overflow error. ");
                        return null;
                    }
                    commonToken = FormulaTreeFactory.CreateNumberNode(value);
                    SetOrigin(commonToken, commonOrigin);
                }
                valueTokens.Clear();
                valueBuilder.Clear();
                valueContainsDecimalSeparator = false;
                return commonToken;
            };

            foreach (var token in tokens)
            {
                // Append digit to value
                var digitToken = token as FormulaNodeNumber;
                if (digitToken != null)
                {
                    valueTokens.Add(token);
                    valueBuilder.Append(digitToken.Value.ToString("R", CultureInfo.InvariantCulture));
                    continue;
                }

                // Append decimal separator to value
                var decimalSeparatorToken = token as FormulaTokenDecimalSeparator;
                if (decimalSeparatorToken != null)
                {
                    if (valueContainsDecimalSeparator)
                    {
                        SetParsingError(token, "Remove duplicate decimal separator. ");
                        yield break;
                    }
                    valueTokens.Add(token);
                    valueBuilder.Append(decimalSeparator);
                    valueContainsDecimalSeparator = true;
                    continue;
                }

                // Create common token of value
                if (valueTokens.Count != 0)
                {
                    var commonToken = createCommonToken();
                    if (commonToken == null) yield break;
                    yield return commonToken;
                }

                // Yield any non-number token
                yield return token;
            }

            // Create common token of value
            if (valueTokens.Count != 0)
            {
                var commonToken = createCommonToken();
                if (commonToken == null) yield break;
                yield return commonToken;
            }
        }

        /// <summary>
        /// Merges <see cref="FormulaTokenFactory.CreateDigitToken" /> and <see cref="FormulaTokenFactory.CreateDecimalSeparatorToken" /> to <see cref="FormulaTreeFactory.CreateNumberNode" />. 
        /// </summary>
        /// <remarks>Running time O(n). </remarks>
        /// <remarks>
        /// This function is implemented using yield return to make completion of partiallay invalid tokens possible. 
        /// </remarks>
        private IEnumerable<IFormulaToken> CompleteNumbers(IEnumerable<IFormulaToken> tokens)
        {
            var valueTokens = new List<IFormulaToken>();
            var valueContainsDecimalSeparator = false;

            // Helper to avoid duplicate code
            Func<IFormulaToken> createCommonToken = () =>
            {
                IFormulaToken commonToken;
                if (valueTokens.Count == 1)
                {
                    // single decimal separator
                    if (valueTokens[0] is FormulaTokenDecimalSeparator) return null;
                    commonToken = valueTokens[0];
                }
                else
                {
                    commonToken = FormulaTreeFactory.CreateNumberNode(default(double));
                    SetOrigin(commonToken, GetOrigin(valueTokens));
                }
                valueTokens.Clear();
                valueContainsDecimalSeparator = false;
                return commonToken;
            };

            foreach (var token in tokens)
            {
                // Append digit to value
                var digitToken = token as FormulaNodeNumber;
                if (digitToken != null)
                {
                    valueTokens.Add(token);
                    continue;
                }

                // Append decimal separator to value
                var decimalSeparatorToken = token as FormulaTokenDecimalSeparator;
                if (decimalSeparatorToken != null)
                {
                    // duplicate decimal separator
                    if (valueContainsDecimalSeparator) yield break;
                    valueTokens.Add(token);
                    valueContainsDecimalSeparator = true;
                    continue;
                }

                // Create common token of value
                if (valueTokens.Count != 0)
                {
                    var commonToken = createCommonToken();
                    if (commonToken == null) yield break;
                    yield return commonToken;
                }

                // Yield any non-number token
                yield return token;
            }

            // Create common token of value
            if (valueTokens.Count != 0)
            {
                var commonToken = createCommonToken();
                if (commonToken == null) yield break;
                yield return commonToken;
            }
        }

        private IEnumerable<IFormulaToken> CompleteNumberToken(IList<IFormulaToken> tokens, int index)
        {
            var token = tokens[index];
            var containsDecimalSeparator = tokens[index] is FormulaTokenDecimalSeparator;

            // gather tokens before
            var tokensBefore = new List<IFormulaToken>();
            for (var index2 = index - 1; index2 >= 0; index2--)
            {
                var token2 = tokens[index2];
                if (token2 is FormulaNodeNumber)
                {
                    tokensBefore.Insert(0, token2);
                    continue;
                }
                if (token2 is FormulaTokenDecimalSeparator)
                {
                    if (containsDecimalSeparator) break;
                    tokensBefore.Insert(0, token2);
                    containsDecimalSeparator = true;
                    continue;
                }
                break;
            }
            foreach (var token2 in tokensBefore)
            {
                yield return token2;
            }
            yield return token;

            // gather tokens afterwards
            for (var index2 = index + 1; index2 < tokens.Count; index2++)
            {
                var token2 = tokens[index2];
                if (token2 is FormulaNodeNumber)
                {
                    yield return token2;
                    continue;
                }
                if (token2 is FormulaTokenDecimalSeparator)
                {
                    if (containsDecimalSeparator) break;
                    yield return token2;
                    containsDecimalSeparator = true;
                    continue;
                }
                break;
            }
        }

        #endregion

        #region Brackets

        /// <summary>
        /// Maps all opening and closing parentheses and packs them with their interpreted children into <see cref="FormulaNodeParentheses"/> or <see cref="FormulaTokenParameters"/>. 
        /// </summary>
        /// <remarks>Running time O(n). </remarks>
        private IEnumerable<IFormulaToken> InterpretBrackets(IEnumerable<IFormulaToken> tokens)
        {
            var parenthesesTokens = new Stack<List<IFormulaToken>>();
            var parentheses = new Stack<IFormulaToken>();
            foreach (var context in tokens.WithContext())
            {
                var previousToken = context[0];
                var token = context[1];

                var parenthesisToken = token as FormulaTokenParenthesis;
                if (parenthesisToken != null)
                {
                    // handle opening parenthesis
                    if (parenthesisToken.IsOpening)
                    {
                        parenthesesTokens.Push(new List<IFormulaToken>());
                        parenthesesTokens.Peek().Add(token);
                        parentheses.Push(previousToken is FormulaNodeBinaryFunction 
                            ? (IFormulaToken) FormulaTokenFactory.CreateParametersToken(null, null) 
                            : FormulaTreeFactory.CreateParenthesesNode(null));
                    }

                    // handle closing parenthesis
                    else
                    {
                        if (parenthesesTokens.Count == 0)
                        {
                            SetParsingError(parenthesisToken, "Remove unmatched closing bracket. ");
                            yield break;
                        }
                        parenthesesTokens.Peek().Add(token);

                        // interpret parentheses
                        var commonToken = parentheses.Pop();
                        InterpretChildren(commonToken, parenthesesTokens.Pop());
                        if (IsCancellationRequested) yield break;
                        if (parenthesesTokens.Count != 0)
                        {
                            parenthesesTokens.Peek().Add(commonToken);
                        }
                        else
                        {
                            yield return commonToken;
                        }
                    }
                    continue;
                }

                // stash tokens inside parentheses
                if (token != null && parenthesesTokens.Count != 0)
                {
                    parenthesesTokens.Peek().Add(token);
                    continue;
                }

                // yield any token outside parentheses
                if (token != null) yield return token;
            }

            if (!IsCancellationRequested && parenthesesTokens.Count != 0)
            {
                SetParsingError(
                    source: Range.Empty(GetOrigin(parenthesesTokens.Peek().Last()).End), 
                    message: "Add missing closing bracket. ");
            }
        }

        /// <remarks>Compare <see cref="InterpretBrackets"/>. </remarks>
        private IEnumerable<IFormulaToken> CompleteBrackets(IEnumerable<IFormulaToken> tokens, bool forward = true)
        {
            var parenthesesTokens = new Stack<List<IFormulaToken>>();
            var parentheses = new Stack<IFormulaToken>();
            foreach (var context in tokens.WithContext())
            {
                var previousToken = context[0];
                var token = context[1];

                var parenthesisToken = token as FormulaTokenParenthesis;
                if (parenthesisToken != null)
                {
                    if (forward ? parenthesisToken.IsOpening : parenthesisToken.IsClosing)
                    {
                        parenthesesTokens.Push(new List<IFormulaToken>());
                        parenthesesTokens.Peek().Add(token);
                        parentheses.Push(previousToken is FormulaNodeBinaryFunction
                            ? (IFormulaToken)FormulaTokenFactory.CreateParametersToken(null, null)
                            : FormulaTreeFactory.CreateParenthesesNode(null));
                    }
                    else
                    {
                        // unmatched bracket
                        if (parenthesesTokens.Count == 0) yield break;

                        parenthesesTokens.Peek().Add(token);

                        var commonToken = parentheses.Pop();
                        CompleteChildren(commonToken, parenthesesTokens.Pop());
                        if (parenthesesTokens.Count != 0)
                        {
                            parenthesesTokens.Peek().Add(commonToken);
                        }
                        else
                        {
                            yield return commonToken;
                        }
                    }
                    continue;
                }

                // dismiss tokens inside parentheses
                if (token != null && parenthesesTokens.Count != 0)
                {
                    continue;
                }

                // yield any token outside parentheses
                if (token != null) yield return token;
            }
        }

        private void InterpretChildren(IFormulaToken commonToken, List<IFormulaToken> parenthesesTokens)
        {
            // children are all tokens except the enclosing parentheses
            var children = parenthesesTokens.GetRange(1, parenthesesTokens.Count - 2);

            // interpret FormulaTokenParameters
            var parametersToken = commonToken as FormulaTokenParameters;
            if (parametersToken != null)
            {
                var interpretedChildren = InterpretFunctions(children);
                interpretedChildren = InterpretOperators(interpretedChildren);
                interpretedChildren = InterpretParameters(interpretedChildren);
                var parameters = interpretedChildren.Cast<IFormulaTree>().Take(2).ToList();
                if (IsCancellationRequested) return;
                var parametersRange = GetOrigin(parameters);
                if (parameters.Count < 2)
                {
                    SetParsingError(
                        source: Range.Empty(GetOrigin(parenthesesTokens.Last()).Start),
                        message: "Missing parameter separator. ");
                    return;
                }
                if (parametersRange.Length < GetOrigin(children).Length)
                {
                    SetParsingError(
                        source: Range.FromLength(parametersRange.End, GetOrigin(children).Length - parametersRange.Length),
                        message: "Remove superfluous parameter(s). ");
                    return;
                }

                parametersToken.FirstParameter = parameters[0];
                parametersToken.SecondParameter = parameters[1];
            }

            // interpret FormulaNodeParentheses
            var parenthesesNode = commonToken as FormulaNodeParentheses;
            if (parenthesesNode != null)
            {
                var interpretedChildren = InterpretNonParameter(children);
                interpretedChildren = InterpretFunctions(interpretedChildren);
                interpretedChildren = InterpretOperators(interpretedChildren);
                var child = (IFormulaTree) interpretedChildren.FirstOrDefault();
                if (IsCancellationRequested) return;
                if (child == null)
                {
                    SetParsingError(
                        source: Range.Empty(GetOrigin(parenthesesTokens.Last()).Start),
                        message: "Missing parentheses' content. ");
                    return;
                }
                var childRange = GetOrigin(child);
                if (childRange.Length < GetOrigin(children).Length)
                {
                    SetParsingError(
                        source: Range.FromLength(childRange.End, GetOrigin(children).Length - childRange.Length),
                        message: "Missing infix operator. ");
                    return;
                }

                // remove redundant parentheses
                while (child is FormulaNodeParentheses)
                {
                    child = ((FormulaNodeParentheses) child).Child;
                }

                parenthesesNode.Child = child;
            }

            SetOrigin(commonToken, parenthesesTokens);
        }

        private void CompleteChildren(IFormulaToken commonToken, List<IFormulaToken> parenthesesTokens)
        {
            SetOrigin(commonToken, parenthesesTokens);
        }


        #endregion

        #region Parameters

        /// <summary>
        /// Ensures all parameters are separated by exactly one <see cref="FormulaTokenParameterSeparator"/> and suppresses all occurences of <see cref="FormulaTokenParameterSeparator"/>. 
        /// </summary>
        private IEnumerable<IFormulaToken> InterpretParameters(IEnumerable<IFormulaToken> tokens)
        {
            var expectSeparator = false;
            foreach (var context in tokens.WithContext())
            {
                var previousToken = context[0];
                var token = context[1];

                // parameter separator
                if (token is FormulaTokenParameterSeparator)
                {
                    if (!expectSeparator)
                    {
                        SetParsingError(Range.Empty(GetOrigin(token).Start), "Missing parameter. ");
                        yield break;
                    }
                    expectSeparator = false;
                    continue;
                }

                //  any argument
                if (token != null)
                {
                    if (expectSeparator)
                    {
                        SetParsingError(Range.Empty(GetOrigin(token).Start), "Missing parameter separator ");
                        yield break;
                    }
                    yield return token;
                    expectSeparator = true;
                    continue;
                }

                // last token
                if (token == null && previousToken is FormulaTokenParameterSeparator)
                {
                    SetParsingError(Range.Empty(GetOrigin(previousToken).End), "Missing parameter. ");
                    yield break;
                }
            }
        }

        /// <summary>
        /// Ensures there are no occurences of <see cref="FormulaTokenParameterSeparator"/>. 
        /// </summary>
        private IEnumerable<IFormulaToken> InterpretNonParameter(IEnumerable<IFormulaToken> tokens)
        {
            foreach (var token in tokens)
            {
                if (token is FormulaTokenParameterSeparator)
                {
                    SetParsingError(token, "Remove parameter separator. ");
                    yield break;
                }
                yield return token;
            }
        }

        #endregion

        #region Functions

        private IEnumerable<IFormulaToken> InterpretFunctions(IEnumerable<IFormulaToken> tokens)
        {
            foreach (var context in tokens.WithContext())
            {
                var previousToken = context[0];
                var token = context[1];

                // assign parentheses to unary function
                var unaryFunctionToken = previousToken as FormulaNodeUnaryFunction;
                if (unaryFunctionToken != null)
                {
                    var parameterToken = token as FormulaNodeParentheses;
                    if (parameterToken == null)
                    {
                        SetParsingError(Range.Empty(GetOrigin(previousToken).End), "Missing function argument. ");
                        yield break;
                    }

                    unaryFunctionToken.Child = parameterToken.Child;
                    SetOrigin(unaryFunctionToken, new IFormulaToken[] { unaryFunctionToken, parameterToken });
                    yield return previousToken;
                    continue;
                }

                // assign parameters to binary function
                var binaryFunctionToken = previousToken as FormulaNodeBinaryFunction;
                if (binaryFunctionToken != null)
                {
                    var parameterToken = token as FormulaTokenParameters;
                    if (parameterToken == null)
                    {
                        SetParsingError(Range.Empty(GetOrigin(previousToken).End), "Missing function argument. ");
                        yield break;
                    }

                    binaryFunctionToken.FirstChild = parameterToken.FirstParameter;
                    binaryFunctionToken.SecondChild = parameterToken.SecondParameter;
                    SetOrigin(binaryFunctionToken, new IFormulaToken[] { binaryFunctionToken, parameterToken });
                    yield return previousToken;
                    continue;
                }

                if (token is FormulaTokenParameters)
                {
                    SetParsingError(Range.Empty(GetOrigin(token).Start), "Missing binary function name");
                    yield break;
                }

                // yield any other token
                if (token != null && !(token is IFormulaFunction))
                {
                    yield return token;
                }
            }
        }

        /// <remarks>Compare <see cref="InterpretFunctions"/>. </remarks>
        private IEnumerable<IFormulaToken> CompleteFunctions(IEnumerable<IFormulaToken> tokens)
        {
            foreach (var context in tokens.WithContext())
            {
                var previousToken = context[0];
                var token = context[1];

                // assign parentheses to unary function
                var unaryFunctionToken = previousToken as FormulaNodeUnaryFunction;
                if (unaryFunctionToken != null)
                {
                    var parameterToken = token as FormulaNodeParentheses;

                    // missing function argument
                    if (parameterToken == null) yield break;

                    SetOrigin(unaryFunctionToken, new IFormulaToken[] { unaryFunctionToken, parameterToken });
                    yield return previousToken;
                    continue;
                }

                // assign parameters to binary function
                var binaryFunctionToken = previousToken as FormulaNodeBinaryFunction;
                if (binaryFunctionToken != null)
                {
                    var parameterToken = token as FormulaTokenParameters;

                    // missing function argument
                    if (parameterToken == null) yield break;

                    SetOrigin(binaryFunctionToken, new IFormulaToken[] { binaryFunctionToken, parameterToken });
                    yield return previousToken;
                    continue;
                }

                // missing binary function
                if (token is FormulaTokenParameters) yield break;

                // yield any other token
                if (token != null && !(token is IFormulaFunction)) yield return token;
            }
        }

        #endregion

        #region Operators

        /// <summary>
        /// Resolves the ambiguity between <see cref="FormulaNodeSubtract"/> and <seealso cref="FormulaNodeNegativeSign"/>. 
        /// </summary>
        /// <remarks>Running time O(n). </remarks>
        /// <remarks>
        /// This function is implemented using yield return to report early errors first.
        /// </remarks>
        private IEnumerable<IFormulaToken> InterpretMinusTokenForward(IEnumerable<IFormulaToken> tokens)
        {
            foreach (var context in tokens.WithContext())
            {
                var previousToken = context[0];
                var token = context[1];

                // handle minus token
                var minusToken = token as FormulaNodeSubtract;
                if (minusToken != null)
                {
                    // change from minus token to negative sign token
                    if (previousToken == null || previousToken is IFormulaOperator || previousToken is FormulaTokenParameterSeparator)
                    {
                        var negativeSignToken = FormulaTreeFactory.CreateNegativeSignNode(null);
                        SetOrigin(negativeSignToken, minusToken);
                        yield return negativeSignToken;
                        continue;
                    }
                }

                // yield any other token
                if (token != null) yield return token;
            }
        }

        /// <summary>
        /// Resolves the ambiguity between <see cref="FormulaNodeSubtract"/> and <seealso cref="FormulaNodeNegativeSign"/>. 
        /// </summary>
        /// <remarks>Running time O(n). </remarks>
        /// <remarks>
        /// This function is implemented using yield return to report early errors first.
        /// </remarks>
        private IEnumerable<IFormulaToken> InterpretMinusTokenBackwards(IEnumerable<IFormulaToken> tokens)
        {
            foreach (var context in tokens.WithContext())
            {
                var nextToken = context[0];
                var token = context[1];

                // handle minus token
                var minusToken = nextToken as FormulaNodeSubtract;
                if (minusToken != null)
                {
                    // change from minus token to negative sign token
                    if (token == null || token is IFormulaOperator || token is FormulaTokenParameterSeparator)
                    {
                        var negativeSignToken = FormulaTreeFactory.CreateNegativeSignNode(null);
                        SetOrigin(negativeSignToken, minusToken);
                        yield return negativeSignToken;
                    }
                    yield return nextToken;
                }

                // yield any non minus token
                if (token != null && !(token is FormulaNodeSubtract)) yield return token;
            }
        }

        private IEnumerable<IFormulaToken> InterpretOperators(IEnumerable<IFormulaToken> tokens)
        {
            var pending = new Stack<IFormulaTree>();
            foreach (var context in tokens.WithContext())
            {
                var token = context[0];
                var nextToken = context[1];

                if (token == null) continue;

                // yield parameter separator
                if (token is FormulaTokenParameterSeparator)
                {
                    if (nextToken is FormulaNodeInfixOperator)
                    {
                        SetParsingError(Range.Empty(GetOrigin(token).End), "Missing left value. ");
                        yield break;
                    }
                    yield return token;
                    continue;
                }
                var token2 = (IFormulaTree)token;

                // stash operator
                var operatorToken = token2 as IFormulaOperator;
                if (operatorToken != null)
                {
                    if (operatorToken is FormulaNodeInfixOperator && pending.Count == 0)
                    {
                        SetParsingError(Range.Empty(GetOrigin(token2).Start), "Missing left value. ");
                        yield break;
                    }
                    if (nextToken == null || nextToken is FormulaNodeInfixOperator || nextToken is FormulaTokenParameterSeparator)
                    {
                        SetParsingError(Range.Empty(GetOrigin(token2).End), operatorToken is FormulaNodeInfixOperator ? "Missing right value. " : "Missing value. ");
                        yield break;
                    }
                    pending.Push(token2);
                    continue;
                }

                // stash any other attached tokens (regarding operator order)
                var nextInfixOperatorToken = nextToken as FormulaNodeInfixOperator;
                if (nextInfixOperatorToken != null && (pending.Count == 0 || ((IFormulaOperator)pending.Peek()).Order <= nextInfixOperatorToken.Order))
                {
                    pending.Push(token2);
                    continue;
                }

                // merge with pending tokens
                while (pending.Count != 0)
                {
                    var pendingOperator = pending.Pop();

                    // attach token to prefix operator
                    var pendingPrefixOperator = pendingOperator as FormulaNodePrefixOperator;
                    if (pendingPrefixOperator != null)
                    {
                        var numberToken = token2 as FormulaNodeNumber;

                        // merge negative sign and number
                        if (numberToken != null && pendingPrefixOperator is FormulaNodeNegativeSign)
                        {
                            numberToken.Value *= -1;
                            SetOrigin(token2, new[] { pendingOperator, token2 });
                        }
                        else
                        {
                            pendingPrefixOperator.Child = token2;
                            SetOrigin(pendingPrefixOperator, new[] { pendingOperator, token2 });
                            token2 = pendingOperator;
                        }
                    }

                    // attach token to infix operator
                    var pendingInfixOperator = pendingOperator as FormulaNodeInfixOperator;
                    if (pendingInfixOperator != null)
                    {
                        pendingInfixOperator.LeftChild = pending.Pop();
                        pendingInfixOperator.RightChild = token2;
                        SetOrigin(pendingInfixOperator, new[] { pendingInfixOperator.LeftChild, pendingOperator, token2 });
                        token2 = pendingOperator;
                    }
                }

                // yield unattached or merged token
                if (nextInfixOperatorToken == null)
                {
                    yield return token2;
                }
                else
                {
                    pending.Push(token2);
                }
            }
        }

        private IEnumerable<IFormulaToken> CompleteOperators(IEnumerable<IFormulaToken> tokens)
        {
            var pending = new Stack<IFormulaTree>();
            foreach (var context in tokens.WithContext())
            {
                var token = context[0];
                var nextToken = context[1];

                if (token == null) continue;

                // yield parameter separator
                if (token is FormulaTokenParameterSeparator)
                {
                    // missing left value
                    if (nextToken is FormulaNodeInfixOperator) yield break;

                    yield return token;
                    continue;
                }
                var token2 = (IFormulaTree)token;

                // stash operator
                var operatorToken = token2 as IFormulaOperator;
                if (operatorToken != null)
                {
                    // missing left value
                    if (operatorToken is FormulaNodeInfixOperator && pending.Count == 0) yield break;

                    // missing (right) value
                    if (nextToken == null || nextToken is FormulaNodeInfixOperator) yield break;
                    
                    if (nextToken is FormulaTokenParameterSeparator) yield break;

                    pending.Push(token2);
                    continue;
                }

                // stash any other attached tokens (regarding operator order)
                var nextInfixOperatorToken = nextToken as FormulaNodeInfixOperator;
                if (nextInfixOperatorToken != null && (pending.Count == 0 || ((IFormulaOperator)pending.Peek()).Order <= nextInfixOperatorToken.Order))
                {
                    pending.Push(token2);
                    continue;
                }

                // merge with pending tokens
                while (pending.Count != 0)
                {
                    var pendingOperator = pending.Pop();

                    // attach token to prefix operator
                    var pendingPrefixOperator = pendingOperator as FormulaNodePrefixOperator;
                    if (pendingPrefixOperator != null)
                    {
                        SetOrigin(pendingPrefixOperator, new[] { pendingOperator, token2 });
                        token2 = pendingOperator;
                    }

                    // attach token to infix operator
                    var pendingInfixOperator = pendingOperator as FormulaNodeInfixOperator;
                    if (pendingInfixOperator != null)
                    {
                        SetOrigin(pendingInfixOperator, new[] { pending.Pop(), pendingOperator, token2 });
                        token2 = pendingOperator;
                    }
                }

                // yield unattached or merged token
                if (nextInfixOperatorToken == null)
                {
                    yield return token2;
                }
                else
                {
                    pending.Push(token2);
                }
            }
        }

        private IEnumerable<IFormulaToken> CompleteOperatorsBackwards(IEnumerable<IFormulaToken> tokens)
        {
            var pending = new Stack<IFormulaTree>();
            foreach (var context in tokens.WithContext())
            {
                var token = context[0];
                var previousToken = context[1];

                if (token == null) continue;

                // yield parameter separator
                if (token is FormulaTokenParameterSeparator)
                {
                    yield return token;
                    continue;
                }
                var token2 = (IFormulaTree)token;

                // stash infix operator
                var infixOperatorToken = token2 as FormulaNodeInfixOperator;
                if (infixOperatorToken != null)
                {
                    // missing right value
                    if (pending.Count == 0) yield break;
                    
                    // missing left value
                    if (previousToken == null || previousToken is FormulaTokenParameterSeparator || previousToken is IFormulaOperator) yield break;

                    pending.Push(token2);
                    continue;
                }

                // attack pending token to prefix operator
                var prefixOperatorToken = token2 as FormulaNodePrefixOperator;
                if (prefixOperatorToken != null)
                {
                    // missing value
                    if (pending.Count == 0) yield break;

                    var pendingToken = pending.Pop();
                    if (pendingToken != null)
                    {
                        var numberToken = pendingToken as FormulaNodeNumber;

                        // merge negative sign and number
                        if (numberToken != null && prefixOperatorToken is FormulaNodeNegativeSign)
                        {
                            SetOrigin(pendingToken, new[] { token2, pendingToken });
                            token2 = pendingToken;
                        }
                        else
                        {
                            SetOrigin(token2, new[] { token2, pendingToken });
                        }
                    }
                }

                // stash any other attached tokens (regarding operator order)
                var previousOperatorToken = previousToken as IFormulaOperator;
                if (previousOperatorToken != null && (pending.Count == 0 || previousOperatorToken.Order > ((IFormulaOperator)pending.Peek()).Order))
                {
                    pending.Push(token2);
                    continue;
                }

                // merge infix operator with pending tokens
                while (pending.Count != 0)
                {
                    var pendingInfixOperator = (FormulaNodeInfixOperator) pending.Pop();
                    pendingInfixOperator.LeftChild = token2;
                    pendingInfixOperator.RightChild = pending.Pop();
                    SetOrigin(pendingInfixOperator, new[] { pendingInfixOperator.LeftChild, pendingInfixOperator, pendingInfixOperator.RightChild });
                    token2 = pendingInfixOperator;
                }
 
                // yield unattached or merged token
                if (previousOperatorToken == null)
                {
                    yield return token2;
                }
                else
                {
                    pending.Push(token2);
                }
            }
        }

        #endregion

        private bool InterpretSemantic(IFormulaTree formula)
        {
            // validate evaluation constraints like sin(True + 4)
            return formula.IsNumber();
        }
    }
}
