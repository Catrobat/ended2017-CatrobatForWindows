using System.Collections.Generic;
using System.Linq;
using Catrobat.IDE.Core.CatrobatObjects.Formulas.FormulaToken;
using Catrobat.IDE.Core.CatrobatObjects.Formulas.FormulaTree;
using Catrobat.IDE.Core.CatrobatObjects.Variables;

namespace Catrobat.IDE.Core.FormulaEditor
{
    class FormulaParser
    {
        private readonly FormulaTokenizer _tokenizer;
        private readonly FormulaInterpreter _interpreter = new FormulaInterpreter();

        public FormulaParser(IEnumerable<UserVariable> localVariables, IEnumerable<UserVariable> globalVariables)
        {
            _tokenizer = new FormulaTokenizer(localVariables, globalVariables);
        }

        public bool Parse(string input, out IFormulaTree formula, out ParsingError parsingError)
        {
            // TODO: how to translate parsing errors?

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
            formula = _interpreter.Interpret(tokens, out parsingError);
            return parsingError == null;
        }

    }
}
