using System;

namespace Catrobat.IDE.Core.UI.Formula
{
    [Obsolete("Use bindings instead.", true)]
    public interface IPortableFormulaButton
    {
        CatrobatObjects.Formulas.Formula Formula { get; set; }
    }
}
