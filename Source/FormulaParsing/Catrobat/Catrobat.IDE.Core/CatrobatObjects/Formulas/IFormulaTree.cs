namespace Catrobat.IDE.Core.CatrobatObjects.Formulas
{
    interface IFormulaTree
    {
        IFormulaTree LeftChild { get; }

        IFormulaTree RightChild { get; }
    }
}
