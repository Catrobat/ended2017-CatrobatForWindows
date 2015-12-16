using System.Collections.Generic;
using System.Linq;
using Catrobat.IDE.Core.Models.Formulas.Tree;

namespace Catrobat.IDE.Core.ExtensionMethods
{
    public static class FormulaTreeExtensions
    {
        internal static IEnumerable<FormulaTree> AsEnumerable(this FormulaTree node)
        {
            return node == null
                ? Enumerable.Empty<FormulaTree>()
                : Enumerable.Repeat(node, 1).Concat(node.Children.SelectMany(child => child.AsEnumerable()));
        }
    }
}
