using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Catrobat.IDE.Core.Annotations;
using Catrobat.IDE.Core.Models;
using Catrobat.Core.Resources.Localization;

namespace Catrobat.IDE.Core.UI
{


    public sealed class NullItemCollection : IList
  {
      private static readonly Look NullLook = new Look {Name = AppResourcesHelper.Get("Editor_NoLookSelected") };
      private static readonly Sound NullSound = new Sound { Name = AppResourcesHelper.Get("Editor_NoSoundSelected") };
      private static readonly BroadcastMessage NullBroadcastMessage = new BroadcastMessage { Content = AppResourcesHelper.Get("Editor_NoMessageSelected") };
      private static readonly Sprite NullSprite = new Sprite {Name = AppResourcesHelper.Get("Editor_NoSpriteSelected") };

    public object NullObject { get; set; }

    private IList _sourceCollection;
    public IList SourceCollection 
    { 
      get { return _sourceCollection; }
      set
      {
        _sourceCollection = value;

        if (_sourceCollection != null)
        {
          if (_sourceCollection is IList<Look>)
          {
            NullObject = NullLook;
          }

          if (_sourceCollection is IList<Sound>)
          {
            NullObject = NullSound;
          }

          if (_sourceCollection is IList<Sprite>)
          {
            NullObject = NullSprite;
          }

          if (_sourceCollection is IList<BroadcastMessage>)
          {
              NullObject = NullBroadcastMessage;
          }
        }
      }
    }

    #region Implementation of IEnumerable

    public IEnumerator GetEnumerator()
    {
      throw new NotImplementedException();
    }

    #endregion

    #region Implementation of ICollection

    public void CopyTo(Array array, int index)
    {
      throw new NotImplementedException();
    }

    public int Count
    {
      get
      {
        return SourceCollection.Count + 1;
      } private set { }
    }
    public bool IsSynchronized { get { return SourceCollection.IsSynchronized; } private set { } }
    public object SyncRoot { get { return SourceCollection.SyncRoot; } private set { } }

    #endregion

    #region Implementation of IList

    public int Add(object value)
    {
      return SourceCollection.Add(value) + 1;
    }

    public void Clear()
    {
      SourceCollection.Clear();
    }

    public bool Contains(object value)
    {
      return value == NullObject || SourceCollection.Contains(value);
    }

    public int IndexOf(object value)
    {
      return value == NullObject || value == null ? 0 : SourceCollection.IndexOf(value) + 1;
    }

    public void Insert(int index, object value)
    {
      if(index == 0)
        throw new Exception("Cannot insert before NullObject");
      else
        SourceCollection.Insert(index-1, value);
    }

    public void Remove(object value)
    {
      if (value == NullObject)
        throw new Exception("Cannot remove null object");
      else
        SourceCollection.Remove(value);
    }

    public void RemoveAt(int index)
    {
      if(index == 0)
        throw new Exception("Cannot remove null object");
      else
        SourceCollection.RemoveAt(index - 1);
    }

    public bool IsFixedSize { get { return SourceCollection.IsFixedSize; } private set { } }
    public bool IsReadOnly { get { return SourceCollection.IsReadOnly; } private set { } }

    public object this[int index]
    {
      get {
        if (index != 0 && index > SourceCollection.Count)
          Debugger.Break();

        return index == 0 ? NullObject : SourceCollection[index - 1];
      }
      set
      {
        if (index == 0)
          NullObject = value;
        else
          SourceCollection[index - 1] = value;
      }
    }

    #endregion

    #region PropertyChanged
    public event PropertyChangedEventHandler PropertyChanged;
    [NotifyPropertyChangedInvocator]
    private void RaisePropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChangedEventHandler handler = PropertyChanged;
        if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
    }
    #endregion

  }
}
