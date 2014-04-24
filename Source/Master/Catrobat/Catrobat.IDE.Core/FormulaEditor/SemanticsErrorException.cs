using System;
using Catrobat.IDE.Core.CatrobatObjects.Formulas.FormulaTree;

namespace Catrobat.IDE.Core.FormulaEditor
{
    internal class SemanticsErrorException : Exception
    {
        public IFormulaTree Node { get; private set; }

        public SemanticsErrorException(IFormulaTree node, string message) : base(message)
        {
            Node = node;
        }
    }
}
