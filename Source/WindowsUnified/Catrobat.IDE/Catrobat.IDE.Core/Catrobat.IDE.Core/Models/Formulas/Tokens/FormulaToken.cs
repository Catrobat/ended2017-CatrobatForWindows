namespace Catrobat.IDE.Core.Models.Formulas.Tokens
{
    public abstract partial class FormulaToken : Model, IFormulaToken
    {
        #region Implements ITestEquatable

        bool ITestEquatable<IFormulaToken>.TestEquals(IFormulaToken other)
        {
            return other != null && other.GetType() == GetType() && TestEquals((FormulaToken) other);
        }

        protected override bool TestEquals(Model other)
        {
            return base.TestEquals(other) && TestEquals((FormulaToken) other);
        }

        protected virtual bool TestEquals(FormulaToken other)
        {
            return true;
        }

        #endregion
    
        #region Implements ICloneable

        public virtual object CloneInstance()
        {
            return MemberwiseClone();
        }

        #endregion
    }
}
