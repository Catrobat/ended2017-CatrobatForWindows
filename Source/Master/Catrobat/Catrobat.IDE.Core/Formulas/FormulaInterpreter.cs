using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using Catrobat.IDE.Core.CatrobatObjects;
using Catrobat.IDE.Core.ExtensionMethods;
using Catrobat.IDE.Core.Models.Formulas.FormulaToken;
using Catrobat.IDE.Core.Models.Formulas.FormulaTree;

namespace Catrobat.IDE.Core.Formulas
{
    /// <remarks>
    /// <para>This class is implemented manually to improve parsing errors. </para>
    /// <para>All functions have running time O(n). </para>
    /// <para>This class internally uses yield return to report early errors first. </para>
    /// <para>Parsing error and token tracking is implemented with local members to improve readability (see <see cref="ParsingError"/> and <see cref="_origin"/>). </para>
    /// </remarks>
    internal class FormulaInterpreter
    {
        #region Static functions

        public static IFormulaTree Interpret(IList<IFormulaToken> tokens, out ParsingError parsingError)
        {
            // TODO: split to InterpretNumber and InterpretLogic

            // TODO: remove this
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

            // TODO: remove this
            sw.Stop();
            // Debug.WriteLine("Interpreter.Interpret needed " + sw.ElapsedMilliseconds + "ms");

            return result;
        }

        public static Range Complete(IList<IFormulaToken> tokens, int index)
        {
            // TODO: remove this
            var sw = new Stopwatch();
            sw.Start();

            // validate input
            if (tokens == null || !(0 <= index && index < tokens.Count)) return Range.Empty(0);

            // complete token
            var instance = new FormulaInterpreter();
            var result = instance.Complete2(tokens, index);

            // TODO: remove this
            sw.Stop();
            // Debug.WriteLine("Interpreter.CompleteToken needed " + sw.ElapsedMilliseconds + "ms");

            return result;
        }

        #endregion

        /// <remarks>See <see cref="http://stackoverflow.com/questions/160118/static-and-instance-methods-with-the-same-name" />. </remarks>
        private IFormulaTree Interpret2(IList<IFormulaToken> tokens)
        {
            // interpret syntax
            var tokens2 = SetOrigin(tokens);
            tokens2 = InterpretNumbers(tokens2);
            tokens2 = InterpretBrackets(tokens2);
            tokens2 = InterpretNonParameter(tokens2);
            tokens2 = InterpretFunctions(tokens2);
            tokens2 = InterpretMinusTokenForward(tokens2);
            tokens2 = InterpretOperators(tokens2);
            var formula = tokens2.Cast<IFormulaTree>().FirstOrDefault();
            if (IsCancellationRequested) return null;

            // valid formula
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

            // interpret semantics
            try
            {
                // validate evaluation constraints like sin(True + 4)
                var isNumber = formula.IsNumber();
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
        private Range Complete2(IList<IFormulaToken> tokens, int index)
        {
            tokens = SetOrigin(tokens).ToList();
            var token = tokens[index];

            if (token is FormulaNodeNumber || token is FormulaTokenDecimalSeparator) return GetOrigin(CompleteNumber(tokens, index));
            if (token is FormulaTokenParenthesis) return GetOrigin(CompleteBracket(tokens, index));
            if (token is IFormulaFunction) return GetOrigin(CompleteFunction(tokens, index) ?? token);
            if (token is IFormulaOperator) return GetOrigin(CompleteOperator(tokens, index));

            return GetOrigin(token);
        }

        #region Members

        /// <summary>Tracks the origin indices of interpreted tokens</summary>
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
                // clones tokens because _origin depends on unique tokens
                token = (IFormulaToken) token.Clone();
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
        private IEnumerable<IFormulaToken> InterpretNumbers(IEnumerable<IFormulaToken> tokens)
        {
            var decimalSeparator = CultureInfo.InvariantCulture.NumberFormat.NumberDecimalSeparator;
            var valueTokens = new List<IFormulaToken>();
            var valueBuilder = new StringBuilder();
            var valueContainsDecimalSeparator = false;

            // helper to avoid duplicate code
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
                // append digit to value
                var digitToken = token as FormulaNodeNumber;
                if (digitToken != null)
                {
                    valueTokens.Add(token);
                    valueBuilder.Append(digitToken.Value.ToString("R", CultureInfo.InvariantCulture));
                    continue;
                }

                // append decimal separator to value
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

                // create common token of value
                if (valueTokens.Count != 0)
                {
                    var commonToken = createCommonToken();
                    if (commonToken == null) yield break;
                    yield return commonToken;
                }

                // yield any non-number token
                yield return token;
            }

            // create common token of value
            if (valueTokens.Count != 0)
            {
                var commonToken = createCommonToken();
                if (commonToken == null) yield break;
                yield return commonToken;
            }
        }

        /// <remarks>Compare <see cref="InterpretNumbers"/>. </remarks>
        private IEnumerable<IFormulaToken> CompleteNumbers(IEnumerable<IFormulaToken> tokens)
        {
            var valueTokens = new List<IFormulaToken>();
            var valueContainsDecimalSeparator = false;

            // helper to avoid duplicate code
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
                // append digit to value
                var digitToken = token as FormulaNodeNumber;
                if (digitToken != null)
                {
                    valueTokens.Add(token);
                    continue;
                }

                // append decimal separator to value
                var decimalSeparatorToken = token as FormulaTokenDecimalSeparator;
                if (decimalSeparatorToken != null)
                {
                    // duplicate decimal separator
                    if (valueContainsDecimalSeparator) yield break;
                    valueTokens.Add(token);
                    valueContainsDecimalSeparator = true;
                    continue;
                }

                // create common token of value
                if (valueTokens.Count != 0)
                {
                    var commonToken = createCommonToken();
                    if (commonToken == null) yield break;
                    yield return commonToken;
                }

                // yield any non-number token
                yield return token;
            }

            // create common token of value
            if (valueTokens.Count != 0)
            {
                var commonToken = createCommonToken();
                if (commonToken == null) yield break;
                yield return commonToken;
            }
        }

        private IEnumerable<IFormulaToken> CompleteNumber(IList<IFormulaToken> tokens, int index)
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
        private IEnumerable<IFormulaToken> InterpretBrackets(IEnumerable<IFormulaToken> tokens)
        {
            var parenthesesTokens = new Stack<List<IFormulaToken>>();
            var parentheses = new Stack<IFormulaToken>();
            foreach (var context in tokens.WithContext())
            {
                if (IsCancellationRequested) yield break;
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

                if (token == null)
                {
                    if (parenthesesTokens.Count != 0)
                    {
                        SetParsingError(
                            source: Range.Empty(GetOrigin(parenthesesTokens.Peek().Last()).End),
                            message: "Add missing closing bracket. ");
                    }
                    continue;
                }

                // stash tokens inside parentheses
                if (parenthesesTokens.Count != 0)
                {
                    parenthesesTokens.Peek().Add(token);
                    continue;
                }

                // yield any token outside parentheses
                yield return token;
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
                        parentheses.Push(FormulaTreeFactory.CreateParenthesesNode(null));
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

                // complete incomplete parentheses
                if (token == null)
                {
                    while (parenthesesTokens.Count != 0)
                    {
                        parenthesesTokens.Peek().Add(previousToken);

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
                if (parenthesesTokens.Count != 0) continue;

                // yield any token outside parentheses
                yield return token;
            }
        }

        private IFormulaToken CompleteBracket(IList<IFormulaToken> tokens, int index)
        {
            var parenthesisToken = (FormulaTokenParenthesis) tokens[index];
            return (parenthesisToken.IsOpening
                ? CompleteBrackets(tokens.Skip(index))
                : CompleteBrackets(tokens.Take(index + 1).Reverse(), false)).First();
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
                interpretedChildren = InterpretMinusTokenForward(interpretedChildren);
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
                interpretedChildren = InterpretMinusTokenForward(interpretedChildren);
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
                if (IsCancellationRequested) yield break;
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

        /// <summary>
        /// Attaches the children of <see cref="FormulaNodeUnaryFunction"/> and <see cref="FormulaNodeBinaryFunction"/>. 
        /// </summary>
        private IEnumerable<IFormulaToken> InterpretFunctions(IEnumerable<IFormulaToken> tokens)
        {
            foreach (var context in tokens.WithContext())
            {
                if (IsCancellationRequested) yield break;
                var previousToken = context[0];
                var token = context[1];

                // attach parentheses to unary function
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

                // attach parameters to binary function
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

                // yield any other token
                if (token != null && !(token is IFormulaFunction))
                {
                    yield return token;
                }
            }
        }

        /// <remarks>Compare <see cref="InterpretFunctions"/>. </remarks>
        private IEnumerable<IFormulaToken> CompleteFunctionsForward(IEnumerable<IFormulaToken> tokens)
        {
            foreach (var context in tokens.WithContext())
            {
                var previousToken = context[0];
                var token = context[1];

                // attach parentheses to function
                if (previousToken is IFormulaFunction)
                {
                    // missing function argument
                    if (!(token is FormulaNodeParentheses)) yield break;

                    SetOrigin(previousToken, new[] { previousToken, token });
                    yield return previousToken;
                    continue;
                }

                // yield any other token
                if (token != null && !(token is IFormulaFunction)) yield return token;
            }
        }

        private IEnumerable<IFormulaToken> CompleteFunctionsBackwards(IEnumerable<IFormulaToken> tokens)
        {
            foreach (var context in tokens.WithContext())
            {
                var nextToken = context[0];
                var token = context[1];

                // attach parentheses to function
                if (token is IFormulaFunction)
                {
                    // missing function argument
                    if (!(nextToken is FormulaNodeParentheses)) yield break;

                    SetOrigin(token, new[] { token, nextToken });
                    yield return token;
                    continue;
                }

                // yield unattached parentheses
                if (nextToken is FormulaNodeParentheses) yield return nextToken;

                // yield any other token
                if (token != null && !(token is FormulaNodeParentheses)) yield return token;
            }
        }

        private IFormulaToken CompleteFunction(IEnumerable<IFormulaToken> tokens, int index)
        {
            var tokensAftwerwards = tokens.Skip(index);
            tokensAftwerwards = CompleteBrackets(tokensAftwerwards);
            tokensAftwerwards = CompleteFunctionsForward(tokensAftwerwards);
            return tokensAftwerwards.FirstOrDefault();
        }

        #endregion

        #region Operators

        /// <summary>
        /// Resolves the ambiguity between <see cref="FormulaNodeSubtract"/> and <seealso cref="FormulaNodeNegativeSign"/>. 
        /// </summary>
        private IEnumerable<IFormulaToken> InterpretMinusTokenForward(IEnumerable<IFormulaToken> tokens)
        {
            foreach (var context in tokens.WithContext())
            {
                if (IsCancellationRequested) yield break;
                var previousToken = context[0];
                var token = context[1];

                // handle minus token
                var minusToken = token as FormulaNodeSubtract;
                if (minusToken != null) token = InterpretMinusToken(previousToken, minusToken);

                if (token != null) yield return token;
            }
        }

        /// <summary>
        /// Resolves the ambiguity between <see cref="FormulaNodeSubtract"/> and <seealso cref="FormulaNodeNegativeSign"/>. 
        /// </summary>
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
                    nextToken = InterpretMinusToken(token, minusToken);
                    yield return nextToken;
                }

                // yield any non minus token
                if (token != null && !(token is FormulaNodeSubtract)) yield return token;
            }
        }

        private IFormulaOperator InterpretMinusToken(IFormulaToken previousToken, FormulaNodeSubtract minusToken)
        {
            // change from subtract to negative sign token
            if (previousToken == null || previousToken is IFormulaOperator || previousToken is FormulaTokenParameterSeparator)
            {
                var negativeSignToken = FormulaTreeFactory.CreateNegativeSignNode(null);
                SetOrigin(negativeSignToken, minusToken);
                return negativeSignToken;
            }
            return minusToken;
        }

        /// <summary>
        /// Attaches the children of <see cref="FormulaNodePrefixOperator"/> and <see cref="FormulaNodeInfixOperator"/>. 
        /// </summary>
        private IEnumerable<IFormulaToken> InterpretOperators(IEnumerable<IFormulaToken> tokens)
        {
            var pending = new Stack<IFormulaTree>();
            foreach (var context in tokens.WithContext())
            {
                if (IsCancellationRequested) yield break;
                var previousToken = context[0];
                var token = context[1];

                if ((token == null || token is FormulaTokenParameterSeparator) && previousToken is IFormulaOperator)
                {
                    SetParsingError(
                        source: Range.Empty(GetOrigin(previousToken).End), 
                        message: previousToken is FormulaNodePrefixOperator ? "Missing value. " : "Missing right value. ");
                    yield break;
                }
                if (token is FormulaNodeInfixOperator && (previousToken == null || previousToken is FormulaTokenParameterSeparator || previousToken is IFormulaOperator))
                {
                    SetParsingError(
                        source: Range.Empty(GetOrigin(token).Start), 
                        message: "Missing left value. ");
                    yield break;
                }

                // interpret any token except parameter separator with additional lookahead
                if (previousToken != null && !(previousToken is FormulaTokenParameterSeparator))
                {
                    var previousToken2 = (IFormulaTree)previousToken;
                    var interpretedToken = InterpretOperators2(previousToken2, token, pending);
                    if (interpretedToken != null) yield return interpretedToken;
                }

                // yield parameter separator without lookahead
                if (token is FormulaTokenParameterSeparator) yield return token;
            }
        }

        private IFormulaTree InterpretOperators2(IFormulaTree token, IFormulaToken nextToken, Stack<IFormulaTree> pending)
        {
            // stash operators
            if (token is IFormulaOperator)
            {
                pending.Push(token);
                return null;
            }


            // merge with pending tokens (regarding operator order)
            var nextInfixOperatorToken = nextToken as FormulaNodeInfixOperator;
            if (nextInfixOperatorToken == null || (pending.Count != 0 && ((IFormulaOperator)pending.Peek()).Order > nextInfixOperatorToken.Order))
            {
                while (pending.Count != 0)
                {
                    var pendingOperator = pending.Pop();

                    // attach token to prefix operator
                    var pendingPrefixOperator = pendingOperator as FormulaNodePrefixOperator;
                    if (pendingPrefixOperator != null)
                    {
                        var numberToken = token as FormulaNodeNumber;

                        // merge negative sign and number
                        if (numberToken != null && pendingPrefixOperator is FormulaNodeNegativeSign)
                        {
                            numberToken.Value *= -1;
                            SetOrigin(token, new[] { pendingOperator, token });
                        }
                        else
                        {
                            pendingPrefixOperator.Child = token;
                            SetOrigin(pendingPrefixOperator, new[] { pendingOperator, token });
                            token = pendingOperator;
                        }
                    }

                    // attach token to infix operator
                    var pendingInfixOperator = pendingOperator as FormulaNodeInfixOperator;
                    if (pendingInfixOperator != null)
                    {
                        pendingInfixOperator.LeftChild = pending.Pop();
                        pendingInfixOperator.RightChild = token;
                        SetOrigin(pendingInfixOperator, new[] { pendingInfixOperator.LeftChild, pendingOperator, token });
                        token = pendingOperator;
                    }
                }
            }

            // stash to infix operator attached tokens
            if (nextInfixOperatorToken != null)
            {
                pending.Push(token);
                return null;
            }

            // yield finished or unattached tokens
            return token;
        }

        private IEnumerable<IFormulaToken> CompleteOperatorForward(IEnumerable<IFormulaToken> tokens, IFormulaOperator operatorToken)
        {
            var operatorOrder = operatorToken.Order;
            return tokens
                .TakeWhile(token => !(token is FormulaTokenParameterSeparator))
                .WithContext()
                .TakeWhile(context =>
                {
                    var previousToken = context[0];
                    var token = context[1];
                    var pending = previousToken == null || previousToken is IFormulaOperator;
                    var operatorToken2 = token as IFormulaOperator;
                    return token != null &&
                        !(pending && operatorToken2 is FormulaNodeInfixOperator) && // missing value of operator
                        (pending || (operatorToken2 != null && operatorToken2.Order > operatorOrder)); // operator fully completed
                }).Select(context => context[1]);
        }

        private IEnumerable<IFormulaToken> CompleteOperatorBackwards(IEnumerable<IFormulaToken> tokens, IFormulaOperator operatorToken)
        {
            var operatorOrder = operatorToken.Order;
            return tokens
                .TakeWhile(token => !(token is FormulaTokenParameterSeparator))
                .WithContext()
                .TakeWhile(context =>
                {
                    var nextToken = context[0];
                    var token = context[1];
                    var pending = nextToken == null || nextToken is FormulaNodeInfixOperator;
                    var infixOperatorToken2 = token as FormulaNodeInfixOperator;
                    return token != null &&
                        !(pending && token is IFormulaOperator) && // missing value of operator
                        (pending || (infixOperatorToken2 != null && infixOperatorToken2.Order > operatorOrder)); // operator fully completed
                }).Select(context => context[1]);
        }

        private IEnumerable<IFormulaToken> CompleteOperator(IList<IFormulaToken> tokens, int index)
        {
            var operatorToken = (IFormulaOperator) tokens[index];
            var operatorTokens = Enumerable.Repeat((IFormulaToken) operatorToken, 1);

            // handle minus token
            var minusToken = operatorToken as FormulaNodeSubtract;
            if (minusToken != null) operatorToken = InterpretMinusToken(index == 0 ? null : tokens[index - 1], minusToken);

            // gather tokens before
            if (operatorToken is FormulaNodeInfixOperator)
            {
                var tokensBefore = tokens.Take(index).Reverse();
                tokensBefore = CompleteBrackets(tokensBefore, false);
                tokensBefore = CompleteFunctionsBackwards(tokensBefore);
                tokensBefore = CompleteNumbers(tokensBefore);
                tokensBefore = InterpretMinusTokenBackwards(tokensBefore);
                operatorTokens = CompleteOperatorBackwards(tokensBefore, operatorToken).Concat(operatorTokens);
            }

            // gather tokens afterwards
            var tokensAfterwards = tokens.Skip(index + 1);
            tokensAfterwards = CompleteBrackets(tokensAfterwards);
            tokensAfterwards = CompleteFunctionsForward(tokensAfterwards);
            tokensAfterwards = CompleteNumbers(tokensAfterwards);
            tokensAfterwards = InterpretMinusTokenForward(tokensAfterwards);
            operatorTokens = operatorTokens.Concat(CompleteOperatorForward(tokensAfterwards, operatorToken));

            return operatorTokens;
        }

        #endregion
    }
}
