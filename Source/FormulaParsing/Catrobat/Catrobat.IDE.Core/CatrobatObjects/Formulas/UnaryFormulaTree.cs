namespace Catrobat.IDE.Core.CatrobatObjects.Formulas
{
    abstract class UnaryFormulaTree : IFormulaTree
    {
        public IFormulaTree LeftChild
        {
            get { return Child; }
        }

        public IFormulaTree RightChild
        {
            get { return null; }
        }

        public IFormulaTree Child { get; set; }
    }
}
