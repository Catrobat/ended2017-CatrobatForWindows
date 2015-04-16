using System.Collections.Generic;
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

            combineControlBricks(result.Bricks);
            
            return result;
        }

        private void combineControlBricks(ObservableCollection<Brick> bricks)
        {
            List<ForeverBrick> beginnForeverBricks = new List<ForeverBrick>();
            List<RepeatBrick> beginRepeatBricks = new List<RepeatBrick>();
            List<IfBrick> beginIfBricks = new List<IfBrick>();


            for (int i = 0; i < bricks.Count; i++)
            {
                if (bricks[i] is ForeverBrick)
                {
                    beginnForeverBricks.Add(bricks[i] as ForeverBrick);
                }
                else if (bricks[i] is RepeatBrick)
                {
                    beginRepeatBricks.Add(bricks[i] as RepeatBrick);
                }
                else if (bricks[i] is IfBrick)
                {
                    beginIfBricks.Add(bricks[i] as IfBrick);
                }
                else if (bricks[i] is EndForeverBrick)
                {
                    var tmp = beginnForeverBricks[beginnForeverBricks.Count - 1];
                    beginnForeverBricks.RemoveAt(beginnForeverBricks.Count - 1);
                    tmp.End = bricks[i] as EndForeverBrick;
                    (bricks[i] as EndForeverBrick).Begin = tmp;
                }
                else if (bricks[i] is EndRepeatBrick)
                {
                    var tmp = beginRepeatBricks[beginRepeatBricks.Count - 1];
                    beginRepeatBricks.RemoveAt(beginRepeatBricks.Count - 1);
                    tmp.End = bricks[i] as EndRepeatBrick;
                    (bricks[i] as EndRepeatBrick).Begin = tmp;
                }
                else if (bricks[i] is ElseBrick)
                {
                    var tmp = beginIfBricks[beginIfBricks.Count - 1];
                    tmp.Else = bricks[i] as ElseBrick;
                    (bricks[i] as ElseBrick).Begin = tmp;
                }
                else if (bricks[i] is EndIfBrick)
                {
                    var tmp = beginIfBricks[beginIfBricks.Count - 1];
                    beginIfBricks.RemoveAt(beginIfBricks.Count - 1);
                    tmp.End = bricks[i] as EndIfBrick;
                    (bricks[i] as EndIfBrick).Begin = tmp;
                    tmp.Else.End = bricks[i] as EndIfBrick;
                    (bricks[i] as EndIfBrick).Else = tmp.Else;
                }
            }
        }

        object ICloneable<CloneSpriteContext>.CloneInstance(CloneSpriteContext context)
        {
            var result = CloneInstance();
            result.Bricks = Bricks == null ? null : Bricks
                .Select(brick => brick.Clone(context))
                .ToObservableCollection();

            combineControlBricks(result.Bricks);

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
