using System.Collections.Generic;
using System.Linq;
using Catrobat.IDE.Core.Models;
using Catrobat.IDE.Core.Models.Formulas.Tree;

namespace Catrobat.IDE.Core.Formulas
{
    public class FormulaParser
    {
        private readonly FormulaTokenizer _tokenizer;

        public FormulaParser(IEnumerable<LocalVariable> localVariables, IEnumerable<GlobalVariable> globalVariables)
        {
            _tokenizer = new FormulaTokenizer(localVariables, globalVariables);
        }

        public FormulaTree Parse(string input, out ParsingError parsingError)
        {
            var tokens = _tokenizer.Tokenize(input, out parsingError);
            if (parsingError != null) return null;

            return tokens == null ? null : FormulaInterpreter.Interpret(tokens.ToList(), out parsingError);
        }
    }
}
