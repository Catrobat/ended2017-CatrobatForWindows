using Catrobat.IDE.Core.CatrobatObjects;
using System;
using System.Collections.ObjectModel;

namespace Catrobat.IDE.Core.ExtensionMethods
{
    public static class ObservableCollectionExtension
    {
        public static IObservableCollection<TTarget> ObservableSelect<TSource, TTarget>(this ObservableCollection<TSource> source, Func<TSource, TTarget> selector, Func<TTarget, TSource> inverseSelector)
        {
            return new ObservableSelectCollection<TSource, TTarget>(source, selector, inverseSelector);
        }
    }
}
