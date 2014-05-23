using System.Diagnostics;
using System.Threading.Tasks;
using Catrobat.IDE.Core.CatrobatObjects;
using GalaSoft.MvvmLight;

namespace Catrobat.IDE.Core.Models.Variables
{
    [DebuggerDisplay("Name = {Name}")]
    public class Variable : ObservableObject, IAsyncCloneable
    {
        #region Properties

        private string _name;
        public string Name
        {
            get { return _name; }
            set { Set(ref _name, value); }
        }

        #endregion

        #region Implements IAsyncCloneable

        public Task<object> CloneInstance()
        {
            return Task.Run(() => MemberwiseClone());
        }

        #endregion

        #region Overrides Equals

        public override bool Equals(object obj)
        {
            // auto-implemented by ReSharper
            return !ReferenceEquals(null, obj) &&
                   (ReferenceEquals(this, obj) || (obj.GetType() == GetType() && Equals((Variable) obj)));
        }

        protected bool Equals(Variable other)
        {
            // auto-implemented by ReSharper
            return string.Equals(_name, other._name);
        }

        public override int GetHashCode()
        {
            // No readonly fields
            // See http://blogs.msdn.com/b/ericlippert/archive/2011/02/28/guidelines-and-rules-for-gethashcode.aspx
            return 0;
        }

        #endregion
    }
}
