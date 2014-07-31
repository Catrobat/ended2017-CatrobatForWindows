using Catrobat.IDE.Core.CatrobatObjects;
using Catrobat.IDE.Core.ExtensionMethods;
using Catrobat.IDE.Core.Models.Formulas.Tree;

namespace Catrobat.IDE.Core.Models.Bricks
{
    public abstract partial class Brick : Model, IBrick
    {

        #region Implements ITestEquatable

        protected override bool TestEquals(Model other)
        {
            return base.TestEquals(other) && TestEquals((Brick) other);
        }

        protected virtual bool TestEquals(Brick other)
        {
            return true;
        }

        #endregion

        #region Implements ICloneable

        object ICloneable.CloneInstance()
        {
            return CloneInstance();
        }

        internal virtual object CloneInstance()
        {
            var result = (Brick) MemberwiseClone();
            result.IsAttached = true;
            return result;
        }

        internal void CloneMember(ref FormulaTree member)
        {
            if (member != null) member = member.Clone();
        }

        object ICloneable<CloneSpriteContext>.CloneInstance(CloneSpriteContext context)
        {
            // prevent endless loops
            Brick result;
            return context.Bricks.TryGetValue(this, out result) 
                ? result 
                : CloneInstance(context);
        }

        internal virtual object CloneInstance(CloneSpriteContext context)
        {
            var result = (Brick)MemberwiseClone();
            context.Bricks[this] = result;
            return result;
        }

        internal void CloneMember(ref FormulaTree member, CloneSpriteContext context)
        {
            if (member != null) member = member.Clone(context);
        }

        internal void CloneMember(ref Look member, CloneSpriteContext context)
        {
            if (member != null) member = context.Looks[member];
        }

        internal void CloneMember(ref Sound member, CloneSpriteContext context)
        {
            if (member != null) member = context.Sounds[member];
        }

        internal void CloneMember(ref Variable member, CloneSpriteContext context)
        {
            var localVariable = member as LocalVariable;
            if (localVariable != null) member = context.LocalVariables[localVariable];
        }

        internal void CloneMember<TBrick>(ref TBrick member, CloneSpriteContext context) where TBrick : Brick
        {
            if (member != null) member = member.Clone(context);
        }

        #endregion

        private bool _isAttached = true;
        public bool IsAttached
        {
            get { return _isAttached; }
            set { _isAttached = value; }
        }
    }
}
