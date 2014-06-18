using System.Collections.Generic;
using Catrobat.IDE.Core.CatrobatObjects;
using Catrobat.IDE.Core.ExtensionMethods;
using Catrobat.IDE.Core.Models.Formulas.Tokens;

namespace Catrobat.IDE.Core.Models.Formulas.Tree
{
    public abstract partial class FormulaTree : Model, IFormulaToken, ICloneable, ICloneable<CloneSpriteContext>
    {
        public abstract IEnumerable<FormulaTree> Children { get; }

        #region Implements ITestEquatable
        bool ITestEquatable<IFormulaToken>.TestEquals(IFormulaToken other)
        {
            return other != null && other.GetType() == GetType() && TestEquals((FormulaTree) other);
        }

        protected override bool TestEquals(Model other)
        {
            return base.TestEquals(other) && TestEquals((FormulaTree) other);
        }

        protected abstract bool TestEquals(FormulaTree other);

        #endregion

        #region Implements ICloneable

        object ICloneable.CloneInstance()
        {
            return CloneInstance();
        }

        internal virtual object CloneInstance()
        {
            return MemberwiseClone();
        }

        internal void CloneMember(ref FormulaTree member)
        {
            if (member != null) member = member.Clone();
        }

        object ICloneable<CloneSpriteContext>.CloneInstance(CloneSpriteContext context)
        {
            return CloneInstance();
        }

        internal virtual object CloneInstance(CloneSpriteContext context)
        {
            return MemberwiseClone();
        }

        internal void CloneMember(ref FormulaTree member, CloneSpriteContext context)
        {
            if (member != null) member = member.Clone(context);
        }

        internal void CloneMember(ref Variable member, CloneSpriteContext context)
        {
            var localVariable = member as LocalVariable;
            if (localVariable != null) member = context.LocalVariables[localVariable];
        }

        #endregion
    }
}
