namespace Catrobat.IDE.Core.CatrobatObjects.Formulas.FormulaTree
{
    public partial interface IFormulaOperator : IFormulaTree
    {
        /// Defines operator precedence like * before +
        int Order { get; }
    }
}
