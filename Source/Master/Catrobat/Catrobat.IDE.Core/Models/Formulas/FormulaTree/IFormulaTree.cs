using System.Collections.Generic;
using Catrobat.IDE.Core.CatrobatObjects.Formulas;
using Catrobat.IDE.Core.Models.Formulas.FormulaToken;

namespace Catrobat.IDE.Core.Models.Formulas.FormulaTree
{
    public partial interface IFormulaTree : ICloneable, IFormulaToken
    {
        IEnumerable<IFormulaTree> Children { get; }
    }
}
