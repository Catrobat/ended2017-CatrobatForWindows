using System.Collections.Generic;

namespace Catrobat.IDE.Core.CatrobatObjects.Formulas
{
    interface IFormulaTree
    {
        IEnumerable<IFormulaTree> Children { get; }
    }
}
