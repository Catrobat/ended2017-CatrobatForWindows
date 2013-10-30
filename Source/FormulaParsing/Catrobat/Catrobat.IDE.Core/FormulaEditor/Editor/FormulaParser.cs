using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Catrobat.IDE.Core.CatrobatObjects.Formulas;

namespace Catrobat.IDE.Core.FormulaEditor.Editor
{
    class FormulaParser
    {

        public bool Parse(string input, out IFormulaTree formula, out IEnumerable<string> parsingErrors)
        {
            // TODO: how to translate parsing errors?
            var parsingErrors2 = new List<string>();
            parsingErrors = parsingErrors2;
            if (string.IsNullOrWhiteSpace(input))
            {
                formula = FormulaTreeFactory.CreateFormulaTree();
                return true;
            }
            throw new NotImplementedException();
        }

        private bool ParseNumber(string input, out IFormulaTree numberNode, ref List<string> parsingErrors)
        {
            numberNode = FormulaTreeFactory.CreateNumberNode(0);
            throw new NotImplementedException();
            return true;
        }

        private bool ParseOperator(string input, out IFormulaTree operatorNode, ref List<string> parsingErrors)
        {
            throw new NotImplementedException();
            //operatorNode = FormulaTreeFactory.CreateExponentialNode();
            //operatorNode = FormulaTreeFactory.CreateSineNode();
            return true;
        }

    }
}
