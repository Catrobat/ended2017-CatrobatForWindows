using Catrobat.IDE.Core.CatrobatObjects.Variables;
using Catrobat.IDE.Core.Models.Formulas.FormulaTree;
using System.Collections.Generic;
using System.Linq;

namespace Catrobat.IDE.Core.Formulas
{
    class FormulaParser
    {
        private readonly FormulaTokenizer _tokenizer;

        public FormulaParser(IEnumerable<UserVariable> localVariables, IEnumerable<UserVariable> globalVariables)
        {
            _tokenizer = new FormulaTokenizer(localVariables, globalVariables);
        }

        public IFormulaTree Parse(string input, out ParsingError parsingError)
        {
            var tokens = _tokenizer.Tokenize(input, out parsingError);
            if (parsingError != null) return null;

            return tokens == null ? null : FormulaInterpreter.Interpret(tokens.ToList(), out parsingError);
        }
    }
}
