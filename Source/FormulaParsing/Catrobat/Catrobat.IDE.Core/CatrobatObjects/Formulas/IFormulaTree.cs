using System.Collections.Generic;

namespace Catrobat.IDE.Core.CatrobatObjects.Formulas
{
    public interface IFormulaTree : IFormulaToken
    {
        IEnumerable<IFormulaTree> Children { get; }
    }
}
