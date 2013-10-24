namespace Catrobat.IDE.Core.CatrobatObjects.Formulas
{
    abstract class BinaryFormulaTree : IFormulaTree
    {
        public IFormulaTree LeftChild { get; set; }

        public IFormulaTree RightChild { get; set; }
    }
}
