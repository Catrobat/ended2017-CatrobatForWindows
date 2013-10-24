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

        public bool Parse(string input, out XmlFormulaTree formula, out IEnumerable<string> parsingErrors)
        {
            // TODO: how to translate parsing errors?
            var parsingErrors2 = new List<string>();
            parsingErrors = parsingErrors2;
            if (string.IsNullOrWhiteSpace(input))
            {
                formula = XmlFormulaTreeFactory.CreateDefaultNode(FormulaEditorKey.Number0);
                return true;
            }
            throw new NotImplementedException();
        }

        private bool ParseNumber(string input, out XmlFormulaTree numberNode, ref List<string> parsingErrors)
        {
            numberNode = XmlFormulaTreeFactory.CreateNumber(0);
            return false;
        }

        private bool ParseOperator(string input, out XmlFormulaTree operatorNode, ref List<string> parsingErrors)
        {
            operatorNode = XmlFormulaTreeFactory.CreateDefaultNode(FormulaEditorKey.MathExp);
            return false;
        }

    }
}
