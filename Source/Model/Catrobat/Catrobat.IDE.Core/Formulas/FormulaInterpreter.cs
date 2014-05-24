using Catrobat.IDE.Core.CatrobatObjects;
using Catrobat.IDE.Core.ExtensionMethods;
using Catrobat.IDE.Core.Models.Formulas.Tokens;
using Catrobat.IDE.Core.Models.Formulas.Tree;
using Catrobat.IDE.Core.Resources.Localization;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;

namespace Catrobat.IDE.Core.Formulas
{
    /// <remarks>
    /// <para>This class is implemented manually to improve parsing errors. </para>
    /// <para>All functions have running time O(n). </para>
    /// <para>This class internally uses yield return to report early errors first. </para>
    /// <para>Parsing error and token tracking is implemented with local members to improve readability (see <see cref="ParsingError"/> and <see cref="_origin"/>). </para>
    /// </remarks>
    public class FormulaInterpreter
    {
        #region Static functions

        public static FormulaTree Interpret(IList<IFormulaToken> tokens, out ParsingError parsingError)
        {
            // TODO: split to InterpretNumber and InterpretLogic

            // TODO: remove this
            var sw = new Stopwatch();
            sw.Start();

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
        private FormulaTree Interpret2(IList<IFormulaToken> tokens)
        {
            // valid tokens
            if (tokens == null)
            {
                SetParsingError(null, AppResources.FormulaInterpreter_NullOrEmpty);
                return null;
            }

            // interpret syntax
            var tokens2 = SetOrigin(tokens).ToList();
            var tokens3 = tokens2.AsEnumerable();
            tokens3 = InterpretBrackets(tokens3);
            tokens3 = InterpretNumbers(tokens3, tokens2);
            tokens3 = InterpretNonParameter(tokens3);
            tokens3 = InterpretFunctions(tokens3);
            tokens3 = InterpretMinusTokenForward(tokens3);
            tokens3 = InterpretOperators(tokens3, tokens2);
            var formula = tokens3.Cast<FormulaTree>().FirstOrDefault();
            if (IsCancellationRequested) return null;

            // valid formula
            if (formula == null)
            {
                SetParsingError(
                    source: Range.FromLength(0, tokens.Count), 
                    message: AppResources.FormulaInterpreter_NullOrEmpty);
                return null;
            }
            var formulaRange = GetOrigin(formula);
            if (formulaRange.Length < tokens.Count)
            {
                var nextIndex = formulaRange.End;
                var nextToken = tokens2.First(token => GetOrigin(token).Start == nextIndex);
                var nextParenthesis = nextToken as FormulaTokenParenthesis;
                if (nextParenthesis != null && nextParenthesis.IsClosing)
                {
                    SetParsingError(
                        source: nextParenthesis, 
                        message: AppResources.FormulaInterpreter_Brackets_UnmatchedClosingParenthesis);
                }
                else if (nextToken is FormulaTokenParameterSeparator)
                {
                    SetParsingError(
                        source: nextToken,
                        message: AppResources.FormulaInterpreter_Brackets_NonArgumentParameterSeparator);
                }
                else
                {
                    SetParsingError(
                        source: Range.Empty(GetOrigin(formula).End),
                        message: AppResources.FormulaInterpreter_DoubleValue);
                }
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
                SetParsingError2(selection, ex.Message);
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
        private readonly Dictionary<IFormulaToken, Range> _origin = new Dictionary<IFormulaToken, Range>();
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
                token = token.Clone();
                SetOrigin(token, Range.Single(index));
                yield return token;
            }
        }
 
        private ParsingError ParsingError { get; set; }
        [Obsolete("Translate message")]
        private void SetParsingError2(Range source, string message)
        {
            ParsingError = new ParsingError(message, source.Start, source.Length);
        }
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
        /// Merges <see cref="Models.Formulas.FormulaToken.IFormulaNumber" /> to <see cref="FormulaTreeFactory.CreateNumberNode" />. 
        /// </summary>
        private IEnumerable<IFormulaToken> InterpretNumbers(IEnumerable<IFormulaToken> tokens, IEnumerable<IFormulaToken> lookAhead)
        {
            var decimalSeparator = CultureInfo.InvariantCulture.NumberFormat.NumberDecimalSeparator;
            var numberTokens = new List<IFormulaToken>();
            var sbValue = new StringBuilder();
            var containsDecimalSeparator = false;

            var lookAhead2 = lookAhead.GetEnumerator();
            foreach (var token in tokens)
            {
                // yield any non-number token
                var numberToken = token as IFormulaNumber;
                if (numberToken == null)
                {
                    yield return token;
                    continue;
                }

                // append digit
                var digitToken = numberToken as FormulaNodeNumber;
                if (digitToken != null)
                {
                    numberTokens.Add(numberToken);
                    sbValue.Append(digitToken.Value.ToString("R", CultureInfo.InvariantCulture));
                }

                // append decimal separator
                var decimalSeparatorToken = numberToken as FormulaTokenDecimalSeparator;
                if (decimalSeparatorToken != null)
                {
                    if (containsDecimalSeparator)
                    {
                        SetParsingError(numberToken, AppResources.FormulaInterpreter_Number_DoubleDecimalSeparator);
                        yield break;
                    }
                    numberTokens.Add(numberToken);
                    sbValue.Append(decimalSeparator);
                    containsDecimalSeparator = true;
                }

                // advance lookAhead parallel to tokens
                var newIndex = GetOrigin(numberToken).End;
                var nextToken = lookAhead2.FirstOrDefault(token2 => GetOrigin(token2).Start == newIndex);

                // yield common number of value tokens
                if (nextToken is IFormulaNumber) continue;
                FormulaTree commonToken;
                if (numberTokens.Count == 1)
                {
                    if (containsDecimalSeparator)
                    {
                        SetParsingError(numberTokens[0], AppResources.FormulaInterpreter_Number_SingleDecimalSeparator);
                        yield break;
                    }
                    commonToken = (FormulaTree) numberTokens[0];
                }
                else
                {
                    // parse value
                    double value;
                    if (!double.TryParse(sbValue.ToString(), NumberStyles.Number, CultureInfo.InvariantCulture, out value))
                    {
                        SetParsingError(GetOrigin(numberTokens), AppResources.FormulaInterpreter_Number_Overflow);
                        yield break;
                    }
                    commonToken = FormulaTreeFactory.CreateNumberNode(value);
                    SetOrigin(commonToken, GetOrigin(numberTokens));
                }
                yield return commonToken;
                numberTokens.Clear();
                sbValue.Clear();
                containsDecimalSeparator = false;
            }
        }

        /// <remarks>Compare <see cref="InterpretNumbers"/>. </remarks>
        private IEnumerable<IFormulaToken> CompleteNumbers(IEnumerable<IFormulaToken> tokens)
        {
            var numberTokens = new List<IFormulaToken>();
            var decimalSeparators = 0;

            foreach (var token in tokens.WithContext().Select(context => context[1]))
            {
                // append digit
                if (token is FormulaNodeNumber)
                {
                    numberTokens.Add(token);
                    continue;
                }

                // append decimal separator
                if (token is FormulaTokenDecimalSeparator)
                {
                    if (decimalSeparators == 0)
                    {
                        numberTokens.Add(token);
                        decimalSeparators++;
                        continue;
                    }
                    decimalSeparators++;
                }

                // create common token of value
                if (numberTokens.Count != 0)
                {
                    IFormulaToken commonToken;
                    if (numberTokens.Count == 1)
                    {
                        if (decimalSeparators != 0) yield break;
                        commonToken = numberTokens[0];
                    }
                    else
                    {
                        commonToken = FormulaTreeFactory.CreateNumberNode(default(double));
                        SetOrigin(commonToken, GetOrigin(numberTokens));
                    }
                    yield return commonToken;
                    if (decimalSeparators > 1) yield break;
                    numberTokens.Clear();
                    decimalSeparators = 0;
                }

                // yield any non-number token
                yield return token;
            }
        }

        private IEnumerable<IFormulaToken> CompleteNumber(IList<IFormulaToken> tokens, int index)
        {
            var token = tokens[index];
            var containsDecimalSeparator = tokens[index] is FormulaTokenDecimalSeparator;

            // gather tokens before
            var tokensBefore = new List<IFormulaToken>();
            foreach (var numberToken in tokens.Take(index).Reverse().Select(token2 => token2 as IFormulaNumber).TakeWhile(numberToken => numberToken != null))
            {
                if (numberToken is FormulaTokenDecimalSeparator)
                {
                    if (containsDecimalSeparator) break;
                    containsDecimalSeparator = true;
                }
                tokensBefore.Insert(0, numberToken);
            }
            foreach (var token2 in tokensBefore)
            {
                yield return token2;
            }

            yield return token;

            // gather tokens afterwards
            foreach (var numberToken in tokens.Skip(index + 1).Select(token2 => token2 as IFormulaNumber).TakeWhile(numberToken => numberToken != null))
            {
                if (numberToken is FormulaTokenDecimalSeparator)
                {
                    if (containsDecimalSeparator) break;
                    containsDecimalSeparator = true;
                }
                yield return numberToken;
            }
        }

        #endregion

        #region Brackets

        /// <summary>
        /// Maps all opening and closing parentheses and packs them with their interpreted children into <see cref="FormulaNodeParentheses"/> or <see cref="Models.Formulas.FormulaToken.FormulaTokenParameter"/>. 
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
                        if (previousToken is FormulaNodeUnaryFunction)
                        {
                            parentheses.Push(FormulaTokenFactory.CreateUnaryParameterToken(null));
                        }
                        else if (previousToken is FormulaNodeBinaryFunction)
                        {
                            parentheses.Push(FormulaTokenFactory.CreateBinaryParameterToken(null, null));
                        }
                        else
                        {
                            parentheses.Push(FormulaTreeFactory.CreateParenthesesNode(null));
                        }
                    }

                    // handle closing parenthesis
                    else
                    {
                        if (parenthesesTokens.Count == 0)
                        {
                            if (previousToken is IFormulaFunction)
                            {
                                SetParsingError(Range.Empty(GetOrigin(parenthesisToken).Start), AppResources.FormulaInterpreter_Function_Empty);
                            }
                            else
                            {
                                SetParsingError(parenthesisToken, AppResources.FormulaInterpreter_Brackets_UnmatchedClosingParenthesis);
                            }
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

                // last token
                if (token == null)
                {
                    if (parenthesesTokens.Count != 0)
                    {
                        SetParsingError(
                            source: Range.Empty(GetOrigin(parenthesesTokens.Peek().Last()).End),
                            message: AppResources.FormulaInterpreter_Brackets_UnmatchedOpeningParenthesis);
                        yield break;
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
            SetOrigin(commonToken, parenthesesTokens);

            // children are all tokens except the enclosing parentheses
            var children = parenthesesTokens.GetRange(1, parenthesesTokens.Count - 2);

            // interpret FormulaTokenParameter
            var parameterToken = commonToken as FormulaTokenParameter;
            if (parameterToken != null)
            {
                var unaryParameterToken = parameterToken as FormulaTokenUnaryParameter;
                var binaryParameterToken = parameterToken as FormulaTokenBinaryParameter;
                var parametersCount = unaryParameterToken != null ? 1 : 2;

                var interpretedChildren = children.AsEnumerable();
                interpretedChildren = InterpretNumbers(interpretedChildren, lookAhead: children);
                interpretedChildren = InterpretFunctions(interpretedChildren);
                interpretedChildren = InterpretMinusTokenForward(interpretedChildren);
                interpretedChildren = InterpretOperators(interpretedChildren, lookAhead: children);
                interpretedChildren = InterpretParameters(interpretedChildren);
                var parameters = interpretedChildren.Cast<FormulaTree>().Take(parametersCount).ToList();

                // valid parameters
                if (IsCancellationRequested) return;
                if (parameters.Count == 0)
                {
                    SetParsingError(
                        source: Range.Empty(GetOrigin(parenthesesTokens.First()).End),
                        message: AppResources.FormulaInterpreter_Brackets_EmptyArgument);
                    return;
                }
                if (parameters.Count < parametersCount)
                {
                    SetParsingError(
                        source: Range.Empty(GetOrigin(parenthesesTokens.Last()).Start),
                        message: AppResources.FormulaInterpreter_Brackets_TooFewArguments);
                    return;
                }
                var parametersRange = GetOrigin(parameters);
                if (parametersRange.Length < GetOrigin(children).Length)
                {
                    var nextIndex = parametersRange.End;
                    var nextToken = children.First(child => GetOrigin(child).Start == nextIndex);
                    if (nextToken is FormulaTokenParameterSeparator)
                    {
                        SetParsingError(
                            source: Range.FromLength(parametersRange.End, GetOrigin(children).Length - parametersRange.Length),
                            message: AppResources.FormulaInterpreter_Brackets_TooManyArguments);
                    }
                    else
                    {
                        SetParsingError(
                            source: Range.Empty(parametersRange.End),
                            message: AppResources.FormulaInterpreter_Brackets_ArgumentDoubleValue);
                    }
                    return;
                }

                // attach parameters
                if (unaryParameterToken != null)
                {
                    unaryParameterToken.Parameter = RemoveParentheses(parameters[0]);
                }
                if (binaryParameterToken != null)
                {
                    binaryParameterToken.FirstParameter = RemoveParentheses(parameters[0]);
                    binaryParameterToken.SecondParameter = RemoveParentheses(parameters[1]);
                }
            }

            // interpret FormulaNodeParentheses
            var parenthesesNode = commonToken as FormulaNodeParentheses;
            if (parenthesesNode != null)
            {
                var interpretedChildren = children.AsEnumerable();
                interpretedChildren = InterpretNonParameter(interpretedChildren);
                interpretedChildren = InterpretNumbers(interpretedChildren, lookAhead: children);
                interpretedChildren = InterpretFunctions(interpretedChildren);
                interpretedChildren = InterpretMinusTokenForward(interpretedChildren);
                interpretedChildren = InterpretOperators(interpretedChildren, lookAhead: children);
                var child = (FormulaTree) interpretedChildren.FirstOrDefault();

                // valid child
                if (IsCancellationRequested) return;
                if (child == null)
                {
                    SetParsingError(
                        source: Range.Empty(GetOrigin(parenthesesTokens.Last()).Start),
                        message: AppResources.FormulaInterpreter_Brackets_EmptyParentheses);
                    return;
                }
                var childRange = GetOrigin(child);
                if (childRange.Length < GetOrigin(children).Length)
                {
                    SetParsingError(
                        source: Range.Empty(childRange.End),
                        message: AppResources.FormulaInterpreter_DoubleValue);
                    return;
                }

                // attach child
                parenthesesNode.Child = RemoveParentheses(child);
            }

        }

        private static FormulaTree RemoveParentheses(FormulaTree node)
        {
            while (true)
            {
                var parenthesesNode = node as FormulaNodeParentheses;
                if (parenthesesNode == null) return node;
                node = parenthesesNode.Child;
            }
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
                        SetParsingError(
                            source: Range.Empty(GetOrigin(token).Start),
                            message: AppResources.FormulaInterpreter_Brackets_EmptyArgument);
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
                        SetParsingError(
                            source: Range.Empty(GetOrigin(token).Start), 
                            message: AppResources.FormulaInterpreter_Brackets_ArgumentDoubleValue);
                        yield break;
                    }
                    yield return token;
                    expectSeparator = true;
                    continue;
                }

                // last token
                if (token == null && !expectSeparator)
                {
                    SetParsingError(
                        source: Range.Empty(GetOrigin(previousToken).End), 
                        message: AppResources.FormulaInterpreter_Brackets_EmptyArgument);
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
                    SetParsingError(token, AppResources.FormulaInterpreter_Brackets_NonArgumentParameterSeparator);
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

                // attach argument to function
                if (previousToken  is IFormulaFunction)
                {
                    if (!(token is FormulaTokenParameter))
                    {
                        SetParsingError(
                            source: Range.Empty(GetOrigin(previousToken).End), 
                            message: AppResources.FormulaInterpreter_Function_Empty);
                        yield break;
                    }

                    var unaryFunctionToken = previousToken as FormulaNodeUnaryFunction;
                    if (unaryFunctionToken != null)
                    {
                        var unaryParameterToken = (FormulaTokenUnaryParameter) token;
                        unaryFunctionToken.Child = unaryParameterToken.Parameter;
                    }
                    var binaryFunctionToken = previousToken as FormulaNodeBinaryFunction;
                    if (binaryFunctionToken != null)
                    {
                        var binaryParameterToken = (FormulaTokenBinaryParameter) token;
                        binaryFunctionToken.FirstChild = binaryParameterToken.FirstParameter;
                        binaryFunctionToken.SecondChild = binaryParameterToken.SecondParameter;
                    }

                    SetOrigin(previousToken, new[] { previousToken, token });
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
        private IEnumerable<IFormulaToken> InterpretOperators(IEnumerable<IFormulaToken> tokens, IEnumerable<IFormulaToken> lookAhead)
        {
            var pending = new Stack<FormulaTree>();
            lookAhead = InterpretMinusTokenForward(lookAhead);
            var lookAhead2 = lookAhead.GetEnumerator();
            foreach (var context in tokens.WithContext())
            {
                if (IsCancellationRequested) yield break;
                var previousToken = context[0];
                var token = context[1];
                if (token == null) yield break;

                // yield parameter separators
                if (token is FormulaTokenParameterSeparator)
                {
                    yield return token;
                    continue;
                }
                var token2 = (FormulaTree) token;
                var operatorToken = token2 as IFormulaOperator;

                // advance lookAhead parallel to tokens
                var nextIndex = GetOrigin(token).End;
                var nextToken = lookAhead2.FirstOrDefault(token3 => GetOrigin(token3).Start == nextIndex);

                // stash operators
                if (operatorToken != null)
                {
                    if ((previousToken == null || previousToken is FormulaTokenParameterSeparator) && operatorToken is FormulaNodeInfixOperator)
                    {
                        SetParsingError(
                            source: Range.Empty(GetOrigin(token2).Start),
                            message: AppResources.FormulaInterpreter_Operator_LeftEmptyInfixOperator);
                        yield break;
                    }
                    if (nextToken == null || nextToken is FormulaTokenParameterSeparator || nextToken is FormulaNodeInfixOperator)
                    {
                        SetParsingError(
                            source: Range.Empty(GetOrigin(token2).End),
                            message: operatorToken is FormulaNodePrefixOperator
                                ? AppResources.FormulaInterpreter_Operator_EmptyPrefixOperator
                                : AppResources.FormulaInterpreter_Operator_RightEmptyInfixOperator);
                        yield break;
                    }

                    pending.Push(token2);
                    continue;
                }

                // merge with pending tokens (regarding operator order)
                var nextInfixOperatorToken = nextToken as FormulaNodeInfixOperator;
                if (nextInfixOperatorToken == null || (pending.Count != 0 && ((IFormulaOperator) pending.Peek()).Order > nextInfixOperatorToken.Order))
                {
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
                                SetOrigin(token2, new[] {pendingOperator, token2});
                            }
                            else
                            {
                                pendingPrefixOperator.Child = token2;
                                SetOrigin(pendingPrefixOperator, new[] {pendingOperator, token2});
                                token2 = pendingOperator;
                            }
                        }

                        // attach token to infix operator
                        var pendingInfixOperator = pendingOperator as FormulaNodeInfixOperator;
                        if (pendingInfixOperator != null)
                        {
                            pendingInfixOperator.LeftChild = pending.Pop();
                            pendingInfixOperator.RightChild = token2;
                            SetOrigin(pendingInfixOperator,
                                new[] {pendingInfixOperator.LeftChild, pendingOperator, token2});
                            token2 = pendingOperator;
                        }
                    }
                }

                // stash to infix operator attached tokens
                if (nextInfixOperatorToken != null)
                {
                    pending.Push(token2);
                    continue;
                }

                // yield finished or unattached tokens
                yield return token2;
            }
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
                    if (token == null) return false;
                    var infixOperatorToken = token as FormulaNodeInfixOperator;

                    var expectValue = previousToken == null || previousToken is IFormulaOperator;
                    if (expectValue)
                    {
                        // missing value of operator
                        return infixOperatorToken == null;
                    }

                    // operator fully completed
                    return infixOperatorToken != null && infixOperatorToken.Order > operatorOrder;
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
                    if (token == null) return false;
                    var operatorToken2 = token as IFormulaOperator;

                    var expectValue = nextToken == null || nextToken is FormulaNodeInfixOperator;
                    if (expectValue)
                    {
                        // missing value of operator
                        return operatorToken2 == null;
                    }

                    // operator fully completed
                    return operatorToken2 != null && operatorOrder < operatorToken2.Order;
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
                operatorTokens = CompleteOperatorBackwards(tokensBefore, operatorToken).Reverse().Concat(operatorTokens);
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
