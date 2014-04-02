using System.Collections.Generic;
using Catrobat.IDE.Core.CatrobatObjects.Formulas.FormulaTree;

namespace Catrobat.IDE.Core.CatrobatObjects.Formulas.FormulaToken
{
    public abstract partial class BaseFormulaToken : IFormulaToken
    {
        #region overrides Equals

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
            return true;
        }

        public override int GetHashCode()
        {
            return 0;
        }

        #endregion
    }
}
