using System;
using System.Collections.Generic;
using System.Linq;
using Catrobat.IDE.Core.CatrobatObjects.Formulas.FormulaToken;
using Catrobat.IDE.Core.CatrobatObjects.Formulas.FormulaTree;
using Catrobat.IDE.Core.CatrobatObjects.Variables;

namespace Catrobat.IDE.Core.FormulaEditor
{
    [Obsolete("Use FormulaInterpreter instead. ")]
    class FormulaParser
    {
        private readonly FormulaTokenizer _tokenizer;

        public FormulaParser(IEnumerable<UserVariable> localVariables, IEnumerable<UserVariable> globalVariables)
        {
            _tokenizer = new FormulaTokenizer(localVariables, globalVariables);
        }

        public bool Parse(string input, out IFormulaTree formula, out ParsingError parsingError)
        {
            formula = null;
            parsingError = null;

            if (string.IsNullOrWhiteSpace(input)) return true;

            IEnumerable<IFormulaToken> tokens;
            IEnumerable<string> parsingErrors1;
            if (!_tokenizer.Tokenize(input, out tokens, out parsingErrors1))
            {
                parsingError = new ParsingError(parsingErrors1.FirstOrDefault());
                return false;
            }
            formula = FormulaInterpreter.Interpret(tokens.ToList(), out parsingError);
            return parsingError == null;
        }

    }
}
