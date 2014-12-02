using System.Collections.Generic;
using System.Collections.Specialized;

namespace Catrobat.IDE.Core.CatrobatObjects
{
    public interface IObservableCollection<T> : IList<T>, INotifyCollectionChanged
    {
    }
}
