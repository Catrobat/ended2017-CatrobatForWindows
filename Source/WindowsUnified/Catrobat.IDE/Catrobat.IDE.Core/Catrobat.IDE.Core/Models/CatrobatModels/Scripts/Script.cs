using System.Collections.ObjectModel;
using System.Linq;
using Catrobat.IDE.Core.CatrobatObjects;
using Catrobat.IDE.Core.ExtensionMethods;
using Catrobat.IDE.Core.Models.Bricks;
using Catrobat.IDE.Core.Models.CatrobatModels;

namespace Catrobat.IDE.Core.Models.Scripts
{
    public abstract partial class Script : CatrobatModelBase, IBrick
    {
        #region Properties

        private ObservableCollection<Brick> _bricks = new ObservableCollection<Brick>();

        public ObservableCollection<Brick> Bricks
        {
            get { return _bricks; }
            set { Set(ref _bricks, value); }
        }

        #endregion

        #region Implements ITestEquatable

        protected override bool TestEquals(ModelBase other)
        {
            return base.TestEquals(other) && TestEquals((Script) other);
        }

        protected virtual bool TestEquals(Script other)
        {
            return CollectionExtensions.TestEquals(_bricks, other._bricks);
        }

        #endregion

        #region Implements ICloneable

        object ICloneable.CloneInstance()
        {
            var result = CloneInstance();
            result.Bricks = Bricks == null ? null : Bricks
                .Select(brick => brick.Clone())
                .ToObservableCollection();
            result.IsAttached = true;
            return result;
        }

        object ICloneable<CloneSpriteContext>.CloneInstance(CloneSpriteContext context)
        {
            var result = CloneInstance();
            result.Bricks = Bricks == null ? null : Bricks
                .Select(brick => brick.Clone(context))
                .ToObservableCollection();
            return result;
        }

        protected internal virtual Script CloneInstance()
        {
            return (Script) MemberwiseClone();
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
