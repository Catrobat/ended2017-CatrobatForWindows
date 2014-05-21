using System.Collections.Generic;
using System.Linq;
using Catrobat.IDE.Core.Models.Formulas.FormulaTree;

namespace Catrobat.IDE.Core.ExtensionMethods
{
    public static class FormulaTreeExtensions
    {
        internal static IEnumerable<IFormulaTree> AsEnumerable(this IFormulaTree node)
        {
            return node == null
                ? Enumerable.Empty<IFormulaTree>()
                : Enumerable.Repeat(node, 1).Concat(node.Children.SelectMany(child => child.AsEnumerable()));
        }
    }
}
