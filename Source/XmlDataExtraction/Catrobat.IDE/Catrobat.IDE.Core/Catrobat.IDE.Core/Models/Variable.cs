using System.Diagnostics;
using Catrobat.IDE.Core.CatrobatObjects;

namespace Catrobat.IDE.Core.Models
{
    [DebuggerDisplay("Name = {Name}")]
    public abstract partial class Variable : Model
    {
        #region Properties

        private string _name;
        public string Name
        {
            get { return _name; }
            set { Set(ref _name, value); }
        }

        #endregion

        #region Implements ITestEquatable

        protected override bool TestEquals(Model other)
        {
            return base.TestEquals(other) && TestEquals((Variable) other);
        }

        protected bool TestEquals(Variable other)
        {
            return string.Equals(_name, other._name);
        }

        #endregion
    }

    #region Implementations

    public partial class GlobalVariable : Variable
    {
    }

    public partial class LocalVariable : Variable, ICloneable
    {
        #region Implements ICloneable

        object ICloneable.CloneInstance()
        {
            return MemberwiseClone();
        }

        #endregion
    }

    #endregion
}
