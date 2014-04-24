using System.Collections.Generic;
using Catrobat.IDE.Core.CatrobatObjects.Formulas.FormulaToken;

namespace Catrobat.IDE.Core.CatrobatObjects.Formulas.FormulaTree
{
    public partial interface IFormulaTree : ICloneable, IFormulaToken
    {
        IEnumerable<IFormulaTree> Children { get; }
    }
}
