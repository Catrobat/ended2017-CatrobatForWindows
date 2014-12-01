using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;

namespace Catrobat.IDE.Core.CatrobatObjects
{
    internal class ObservableSelectCollection<TSource, TTarget> : List<TTarget>, IObservableCollection<TTarget>
    {
        #region Implements IObservableCollection

        public event NotifyCollectionChangedEventHandler CollectionChanged;
        private void RaiseCollectionChanged(NotifyCollectionChangedEventArgs e)
        {
            if (CollectionChanged != null) CollectionChanged(this, e);
        }

        #endregion

        private readonly Func<TSource, TTarget> _selector;
        private readonly Func< TTarget, TSource> _inverseSelector;

        public ObservableSelectCollection(ObservableCollection<TSource> source, Func<TSource, TTarget> selector, Func<TTarget, TSource> inverseSelector)
            : base(source.Select(selector).ToList())
        {
            if (source == null) throw new ArgumentNullException("source");
            if (selector == null) throw new ArgumentNullException("selector");
            if (inverseSelector == null) throw new ArgumentNullException("inverseSelector");

            _selector = selector;
            _inverseSelector = inverseSelector;
            _source = source;
            _source.CollectionChanged += Source_OnCollectionChanged;
        }

        #region Inherits List

        public new void Add(TTarget item)
        {
            _suppressSourceCollectionChanged = true;
            Source.Add(_inverseSelector(item));
            _suppressSourceCollectionChanged = false;
            base.Add(item);
            RaiseCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, item, Count - 1));
        }

        public new void Clear()
        {
            _suppressSourceCollectionChanged = true;
            Source.Clear();
            _suppressSourceCollectionChanged = false;
            base.Clear();
            RaiseCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
        }

        public new void Insert(int index, TTarget item)
        {
            _suppressSourceCollectionChanged = true;
            Source.Insert(index, _inverseSelector(item));
            _suppressSourceCollectionChanged = false;
            base.Insert(index, item);
            RaiseCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, item, index));
        }

        public new bool Remove(TTarget item)
        {
            var index = IndexOf(item);
            if (index == -1) return false;
            RemoveAt(index);
            return true;
        }

        public new void RemoveAt(int index)
        {
            _suppressSourceCollectionChanged = true;
            Source.RemoveAt(index);
            _suppressSourceCollectionChanged = false;
            var oldItem = this[index];
            base.RemoveAt(index);
            RaiseCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, oldItem, index));
        }

        public new TTarget this[int index]
        {
            get { return base[index]; }
            set
            {
                _suppressSourceCollectionChanged = true;
                Source[index] = _inverseSelector(value);
                _suppressSourceCollectionChanged = false;
                var oldItem = this[index];
                base[index] = value;
                RaiseCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Replace, oldItem, value, index));
            }
        }

        #endregion

        #region Source

        private bool _suppressSourceCollectionChanged = false;

        private readonly ObservableCollection<TSource> _source;
        public ObservableCollection<TSource> Source 
        {
            get { return _source; }
        }
        /// <remarks>See <see cref="http://msdn.microsoft.com/en-us/library/system.collections.specialized.notifycollectionchangedeventargs.aspx"/>. </remarks>
        private void Source_OnCollectionChanged(object sender, NotifyCollectionChangedEventArgs eSource)
        {
            if (_suppressSourceCollectionChanged) return;

            NotifyCollectionChangedEventArgs eTarget;
            switch (eSource.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    eTarget = new NotifyCollectionChangedEventArgs(
                        action: NotifyCollectionChangedAction.Add, 
                        changedItems: eSource.NewItems.Cast<TSource>().Select(_selector).ToList(), 
                        startingIndex: eSource.NewStartingIndex);
                    for (var relativeIndex = 0; relativeIndex < eTarget.NewItems.Count; relativeIndex++)
                    {
                        var absolueIndex = eTarget.NewStartingIndex + relativeIndex;
                        var targetItem = (TTarget) eTarget.NewItems[relativeIndex];
                        base.Insert(absolueIndex, targetItem);

                    }
                    break;
                case NotifyCollectionChangedAction.Move:
                    eTarget = new NotifyCollectionChangedEventArgs(
                        action: NotifyCollectionChangedAction.Move, 
                        changedItems: eSource.OldItems.Cast<TSource>().Select(_selector).ToList(), 
                        index: eSource.NewStartingIndex, 
                        oldIndex: eSource.OldStartingIndex);
                    for (var relativeIndex = eTarget.OldItems.Count - 1; relativeIndex >= 0; relativeIndex--)
                    {
                        base.RemoveAt(eTarget.OldStartingIndex + relativeIndex);
                    }
                    for (var relativeIndex = 0; relativeIndex < eTarget.NewItems.Count; relativeIndex++)
                    {
                        var absoluteIndex = eTarget.NewStartingIndex + relativeIndex;
                        var targetItem = (TTarget) eTarget.NewItems[relativeIndex];
                        base.Insert(absoluteIndex, targetItem);
                    }
                    break;
                case NotifyCollectionChangedAction.Remove:
                    eTarget = new NotifyCollectionChangedEventArgs(
                        action: NotifyCollectionChangedAction.Remove,
                        changedItems: eSource.OldItems.Cast<TSource>().Select(_selector).ToList(), 
                        startingIndex: eSource.OldStartingIndex);
                    for (var relativeIndex = eTarget.OldItems.Count - 1; relativeIndex >= 0; relativeIndex--)
                    {
                        var absoluteIndex = eTarget.OldStartingIndex + relativeIndex;
                        base.RemoveAt(absoluteIndex);
                    }
                    break;
                case NotifyCollectionChangedAction.Replace:
                    eTarget = new NotifyCollectionChangedEventArgs(
                        action: NotifyCollectionChangedAction.Replace,
                        newItems: eSource.NewItems.Cast<TSource>().Select(_selector).ToList(),
                        oldItems: eSource.OldItems.Cast<TSource>().Select(_selector).ToList(),
                        startingIndex: eSource.OldStartingIndex);
                    for (var relativeIndex = 0; relativeIndex < eTarget.OldItems.Count; relativeIndex++)
                    {
                        var absoluteIndex = eTarget.OldStartingIndex + relativeIndex;
                        var targetItem = (TTarget) eTarget.NewItems[relativeIndex];
                        base[absoluteIndex] = targetItem;
                    }
                    break;
                case NotifyCollectionChangedAction.Reset:
                    eTarget = new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset);
                    base.Clear();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            RaiseCollectionChanged(eTarget);
        }

        #endregion
    }
}
