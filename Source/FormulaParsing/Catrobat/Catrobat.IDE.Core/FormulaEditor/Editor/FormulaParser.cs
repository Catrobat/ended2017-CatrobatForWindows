using Catrobat.IDE.Core.CatrobatObjects.Formulas;
using Catrobat.IDE.Core.CatrobatObjects.Variables;
using System.Collections.Generic;
using System.Linq;

namespace Catrobat.IDE.Core.FormulaEditor.Editor
{
    class FormulaParser
    {

        private readonly FormulaTokenizer _tokenizer;
        private readonly FormulaInterpreter _interpreter = new FormulaInterpreter();

        public FormulaParser(IEnumerable<UserVariable> userVariables, ObjectVariableEntry objectVariable)
        {
            _tokenizer = new FormulaTokenizer(userVariables, objectVariable);
        }

        public bool Parse(string input, out IFormulaTree formula, out IEnumerable<string> parsingErrors)
        {
            // TODO: how to translate parsing errors?

            formula = null;
            parsingErrors = Enumerable.Empty<string>();

            if (string.IsNullOrWhiteSpace(input)) return true;

            IEnumerable<IFormulaToken> tokens;
            IEnumerable<string> parsingErrors1;
            var parsingErrors2 = Enumerable.Empty<string>();
            var result = _tokenizer.Tokenize(input, out tokens, out parsingErrors1) && _interpreter.Interpret(tokens, out formula, out parsingErrors2);
            parsingErrors = parsingErrors1.Concat(parsingErrors2);
            return result;
        }

    }
}
