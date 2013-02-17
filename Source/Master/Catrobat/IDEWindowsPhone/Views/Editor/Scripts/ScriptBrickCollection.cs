using System;
using System.Collections.ObjectModel;
using Catrobat.Core.Objects;
using Catrobat.Core.Objects.Bricks;
using System.Collections.Specialized;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;

namespace Catrobat.IDEWindowsPhone.Views.Editor.Scripts
{
  public class ScriptBrickCollection : IList, ICollection, IEnumerable, INotifyCollectionChanged, INotifyPropertyChanged
  {
    private ObservableCollection<Script> scripts 
    { 
      get 
      {
        if (selectedSprite == null)
          return new ObservableCollection<Script>();

        return selectedSprite.Scripts.Scripts;
      } 
    }

    private Sprite selectedSprite;

    private Brick lastDeletedBrick;
    private Brick lastInsertedBrick;
    private int lastDeletedIndex;
    private int lastInsertedIndex;

    public int LastDeletedIndex
    {
      get { return lastDeletedIndex; }
    }

    public DataObject PreventIsertOfNext {get; set;}

    public void Update(Sprite selectedSprite)
    {
      this.selectedSprite = selectedSprite;
      OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
    }

    public void AddScriptBrick(DataObject scriptBrick, int firstViewIndex, int lastViewIndex)
    {
      //if (this.Count == lastViewIndex + 1 && GetAtIndex(lastViewIndex) is Script && )
      //{
      //  lastViewIndex++;
      //}

      if (scriptBrick is Brick) // Add brick at last visible end of a script
      {
        Brick brick = scriptBrick as Brick;

        int scriptEndIndex = -1;
        int scriptBeginIndex = 0;
        Script lastFullScript = null;
        foreach (Script script in scripts)
        {
          scriptBeginIndex = scriptEndIndex + 1;
          scriptEndIndex += script.Bricks.Bricks.Count + 1;

          if (scriptEndIndex > lastViewIndex && scriptBeginIndex >= firstViewIndex)
            break;

          lastFullScript = script;
        }

        if (lastFullScript == null)
        {
          StartScript startScript = new StartScript(selectedSprite);
          scripts.Add(startScript);
          lastFullScript = startScript;

          OnScriptAdded(startScript, IndexOf(startScript));
        }

        lastFullScript.Bricks.Bricks.Add(brick);

        //OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset)); // TODO: make faster and use method below instead
        OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, scriptBrick, IndexOf(scriptBrick)));
      }
      else if (scriptBrick is Script) // Add script at end of all
      {
        Script script = scriptBrick as Script;
        scripts.Add(script);

        //OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset)); // TODO: make faster and use method below instead
        OnScriptAdded((Script)scriptBrick, IndexOf(scriptBrick));
      }

       
      //
    }

    private void InternalCollectionChanged(object sernder, NotifyCollectionChangedEventArgs e)
    {
      if (e.Action == NotifyCollectionChangedAction.Remove || e.Action == NotifyCollectionChangedAction.Add)
      {

        if (e.Action == NotifyCollectionChangedAction.Remove)
        {
          if (e.OldItems[0] is Script)
          {
            NotifyCollectionChangedEventArgs args = new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset); // TODO: change this
            OnCollectionChanged(args);
          }
          else
          {
            NotifyCollectionChangedEventArgs args = new NotifyCollectionChangedEventArgs(e.Action, lastDeletedBrick, lastDeletedIndex);
            OnCollectionChanged(args);
          }
        }
        else
        {
          if (e.NewItems[0] is Script)
          {
            NotifyCollectionChangedEventArgs args = new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset); // TODO: change this
            OnCollectionChanged(args);
          }
          else
          {
            NotifyCollectionChangedEventArgs args = new NotifyCollectionChangedEventArgs(e.Action, lastInsertedBrick, lastInsertedIndex);
            OnCollectionChanged(args);
          }
        }
      }
      else
      {
        NotifyCollectionChangedEventArgs args = new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset);
        OnCollectionChanged(args);
      }
    }

    private void OnCollectionChanged(NotifyCollectionChangedEventArgs e)
    {
      if(CollectionChanged != null)
        CollectionChanged.Invoke(this, e);
    }

    private void OnScriptAdded(Script script, int index)
    {
      OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, script, index));

      int brickIndex = index;
      foreach (Brick brick in script.Bricks.Bricks)
      {
        brickIndex++;
        OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, brick, brickIndex));
      }
    }

    private void OnScriptRemoved(Script script, int index)
    {
      OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, script, index));

      int brickIndex = index;
      foreach (Brick brick in script.Bricks.Bricks)
      {
        //brickIndex++;
        OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, brick, brickIndex));
      }
    }

    private void OnPropertyChanged(PropertyChangedEventArgs e)
    {
      if (PropertyChanged != null)
        PropertyChanged.Invoke(this, e);
    }

    public event NotifyCollectionChangedEventHandler CollectionChanged;
    public event PropertyChangedEventHandler PropertyChanged;

    public void RemoveAt(int index)
    {
      Script scriptToRemove = null;
      Brick brickToRemove = null;

      int count = 0;
      foreach (Script script in scripts)
      {
        if (count == index)
        {
          scriptToRemove = script;
          break;
        }

        count++;
        foreach (Brick brick in script.Bricks.Bricks)
        {
          if (count == index)
          {
            scriptToRemove = script;
            brickToRemove = brick;

            lastDeletedBrick = brick;
            lastDeletedIndex = index;

            break;
          }

          count++;
        }

        if (brickToRemove != null)
          break;
      }

      if (brickToRemove == null)
      {
        scripts.Remove(scriptToRemove);

        OnScriptRemoved(scriptToRemove, index);

      }
      else
      {
        if (scriptToRemove != null)
        {
          scriptToRemove.Bricks.Bricks.Remove(brickToRemove);
          OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, brickToRemove, index));
        }
      }
    }

    public void Clear()
    {
      scripts.Clear();
    }

    public int Count
    {
      get 
      { 
        int count = 0;
        foreach (Script script in scripts)
          count += script.Bricks.Bricks.Count + 1;
        return count;
      }
    }

    public bool IsReadOnly
    {
      get { return false; }
    }

    public bool Remove(DataObject item)
    {
      if (item is DataObject)
      {
        Remove((object)item);
        return true;
      }

      return false;
    }

    public IEnumerator<DataObject> GetEnumerator()
    {
      return new ScriptBrickIterator(scripts) as IEnumerator<DataObject>;
    }

    System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
    {
      return new ScriptBrickIterator(scripts) as IEnumerator<DataObject>;
    }

    public int Add(object value)
    {
      if (value is Script)
        scripts.Add((Script)value);

      if (value is Brick)
        scripts[scripts.Count - 1].Bricks.Bricks.Add((Brick)value);

      return 1; // TODO: should probably not be 1 ?
    }

    public bool Contains(object value)
    {
      if (scripts.Contains(value as Script))
        return true;
      else
        foreach (Script script in scripts)
        {
          if (script.Bricks.Bricks.Contains(value as Brick))
            return true;
        }

      return false;
    }

    public int ScriptIndexOf(Script value)
    {
      return scripts.IndexOf(value);
    }

    public int IndexOf(object value)
    {      
      //int counter = 0;
      //foreach (Script script in scripts)
      //{
      //  counter++;

      //  if (value is Brick)
      //  {
      //    if (script.Bricks.Bricks.Contains((Brick)value))
      //    {
      //      counter += script.Bricks.Bricks.IndexOf((Brick)value);
      //      return counter;
      //    }
      //  }
      //  else
      //  {
      //    if (value == script)
      //      return counter;

      //    counter += script.Bricks.Bricks.Count;
      //  }
      //}

      //return -1;

      IEnumerator<DataObject> enumerator = GetEnumerator();

      int count = 0;
      while (enumerator.MoveNext())
      {
        if (enumerator.Current == value)
          return count;

        count++;
      }

      return -1;
    }

    public void Insert(int index, object value)
    {
      if (PreventIsertOfNext != null && PreventIsertOfNext == value)
      {
        PreventIsertOfNext = null;
        return;
      }

      int count = 0;

      if (value is Script) // TODO: test me
      {
        int scriptIndex = 0;

        foreach (Script script in scripts)
        {
          if (count > index)
            break;

          count += script.Bricks.Bricks.Count + 1;
          scriptIndex++;
        }

        scripts.Insert(scriptIndex, (Script)value);
        OnScriptAdded((Script)value, count + 1);
      }

      if (value is Brick)
      {
        int brickCount = 0;
        lastInsertedBrick = (Brick)value;
        lastInsertedIndex = index;

        if (index == 0) // Cannot insert brick before first sprite
          index = 1;

        foreach (Script script in scripts)
        {
          count++;
          foreach (Brick brick in script.Bricks.Bricks)
          {
            if (count == index)
            {
              script.Bricks.Bricks.Insert(brickCount, (Brick)value);
              OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, value, index));
              return;
            }

            count++;
            brickCount++;
          }

          if (count == index)
          {
            script.Bricks.Bricks.Insert(brickCount, (Brick)value);
            OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, value, index));
            return;
          }

          brickCount = 0;
        }
      }
    }

    public bool IsFixedSize
    {
      get { return false; }
    }

    public void Remove(object value)
    {
      int index = IndexOf(value);

      if (value is Script)
      {
        Script script = value as Script;
        if (scripts.Contains(script))
        {
          scripts.Remove(script);

          OnScriptRemoved(script, index);
        }
      }
      else if (value is Brick)
      {
        foreach(Script script in scripts)
          if (script.Bricks.Bricks.Contains(value as Brick))
          {
            script.Bricks.Bricks.Remove(value as Brick);
            OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, value, index));
          }
      }
    }

    object IList.this[int index]
    {
      get
      {
        return GetAtIndex(index);
      }
      set
      {
        throw new NotImplementedException();
      }
    }

    private object GetAtIndex(int index)
    {
      IEnumerator<DataObject> enumerator = GetEnumerator(); // TODO: make faster do not use enumerator

      int count = 0;
      while (enumerator.MoveNext())
      {
        if (count == index)
          return enumerator.Current;

        count++;
      }

      return null;
      //throw new IndexOutOfRangeException();
    }

    public void CopyTo(Array array, int index)
    {
      throw new NotImplementedException();
    }

    public bool IsSynchronized
    {
      // TODO: synchroize me
      get { return false; }
    }

    public object SyncRoot
    {
      get { return scripts; }
    }
  }
}
