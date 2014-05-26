using System;
using Catrobat.IDE.Core.Models.Formulas.Tree;

namespace Catrobat.IDE.Core.Formulas
{
    [Obsolete("Don't throw exceptions in FormulaInterpreter. ")]
    internal class SemanticsErrorException : Exception
    {
        public FormulaTree Node { get; private set; }

        public SemanticsErrorException(FormulaTree node, string message) : base(message)
        {
            Node = node;
        }
    }
}
