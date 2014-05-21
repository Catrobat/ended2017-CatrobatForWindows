using System;
using Catrobat.IDE.Core.Models.Formulas.FormulaTree;

namespace Catrobat.IDE.Core.Formulas
{
    [Obsolete("Don't throw exceptions in FormulaInterpreter. ")]
    internal class SemanticsErrorException : Exception
    {
        public IFormulaTree Node { get; private set; }

        public SemanticsErrorException(IFormulaTree node, string message) : base(message)
        {
            Node = node;
        }
    }
}
