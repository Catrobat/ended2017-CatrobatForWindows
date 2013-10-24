namespace Catrobat.IDE.Core.CatrobatObjects.Formulas
{
    class ConstantFormulaTree : IFormulaTree
    {
        public IFormulaTree LeftChild
        {
            get { return null; }
        }

        public IFormulaTree RightChild
        {
            get { return null; }
        }
    }

    class ConstantFormulaTree<TValue> : ConstantFormulaTree
    {
        public TValue Value;
    }

}
