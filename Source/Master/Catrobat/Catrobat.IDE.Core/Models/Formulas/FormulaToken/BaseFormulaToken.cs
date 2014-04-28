namespace Catrobat.IDE.Core.Models.Formulas.FormulaToken
{
    public abstract partial class BaseFormulaToken : IFormulaToken
    {
        #region Overrides Equals

        public override bool Equals(object obj)
        {
            // auto-implemented by ReSharper
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((BaseFormulaToken) obj);
        }

        protected bool Equals(BaseFormulaToken other)
        {
            // auto-implemented by ReSharper
            return true;
        }

        public override int GetHashCode()
        {
            // auto-implemented by ReSharper
            return 0;
        }

        #endregion
    
        #region Implements ICloneable

        public virtual object Clone()
        {
            return MemberwiseClone();
        }

        #endregion
    }
}
