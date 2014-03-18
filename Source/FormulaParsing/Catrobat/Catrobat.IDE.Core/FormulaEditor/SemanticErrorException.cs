using System;
using Catrobat.IDE.Core.CatrobatObjects.Formulas.FormulaTree;

namespace Catrobat.IDE.Core.FormulaEditor
{
    internal class SemanticErrorException : Exception
    {
        public IFormulaTree Node { get; set; }

        public SemanticErrorException(IFormulaTree node)
        {
            Node = node;
        }

        public SemanticErrorException(IFormulaTree node, string message) : base(message)
        {
            Node = node;
        }
    }
}
