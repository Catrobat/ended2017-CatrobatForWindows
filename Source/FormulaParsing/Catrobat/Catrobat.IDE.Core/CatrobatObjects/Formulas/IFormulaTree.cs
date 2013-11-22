using System.Collections.Generic;

namespace Catrobat.IDE.Core.CatrobatObjects.Formulas
{
    interface IFormulaTree : IFormulaToken
    {
        IEnumerable<IFormulaTree> Children { get; }
    }
}
