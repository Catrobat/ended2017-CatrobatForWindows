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
            if (!_tokenizer.Tokenize(input, out tokens, out parsingErrors1)) return false;
            string parsingError2;
            formula = _interpreter.Interpret(tokens, out parsingError2);
            if (parsingError2 != null)
            {
                parsingErrors = Enumerable.Repeat(parsingError2, 1);
                return false;
            }
            return true;
        }

    }
}
