using System.Collections.Generic;
using Catrobat.IDE.Core.CatrobatObjects.Formulas.FormulaNodes;

namespace Catrobat.IDE.Core.CatrobatObjects.Formulas
{
    public interface IFormulaTree : IFormulaToken
    {
        IEnumerable<IFormulaTree> Children { get; }
    }
}
