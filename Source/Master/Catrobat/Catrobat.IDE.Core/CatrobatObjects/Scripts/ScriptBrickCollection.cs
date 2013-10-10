using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using Catrobat.IDE.Core.CatrobatObjects;
using Catrobat.IDE.Core.CatrobatObjects.Bricks;
using Catrobat.IDE.Core.CatrobatObjects.Scripts;

namespace Catrobat.IDE.Core.CatroatObjects.Scripts
{
    public class ScriptBrickCollection : IList, INotifyCollectionChanged
    {
        private ObservableCollection<Script> Scripts
        {
            get
            {
                if (_selectedSprite == null)
                {
                    return new ObservableCollection<Script>();
                }

                return _selectedSprite.Scripts.Scripts;
            }
        }
        private Sprite _selectedSprite;
        private Brick _lastDeletedBrick;
        private Brick _lastInsertedBrick;
        private int _lastInsertedIndex;

        public int LastDeletedIndex { get; private set; }

        public DataObject PreventIsertOfNext { get; set; }

        public void Update(Sprite selectedSprite)
        {
            _selectedSprite = selectedSprite;
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
                var brick = scriptBrick as Brick;

                var scriptEndIndex = -1;
                Script lastFullScript = null;
                foreach (Script script in Scripts)
                {
                    var scriptBeginIndex = scriptEndIndex + 1;
                    scriptEndIndex += script.Bricks.Bricks.Count + 1;

                    // what does that do?
                    //if (scriptEndIndex > lastViewIndex && scriptBeginIndex >= firstViewIndex)
                    //{
                    //    break;
                    //}

                    lastFullScript = script;
                }

                if (lastFullScript == null)
                {
                    var startScript = new StartScript();
                    Scripts.Add(startScript);
                    lastFullScript = startScript;

                    OnScriptAdded(startScript, IndexOf(startScript));
                }

                lastFullScript.Bricks.Bricks.Add(brick);

                //OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset)); // TODO: make faster and use method below instead
                OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, scriptBrick, IndexOf(scriptBrick)));
            }
            else if (scriptBrick is Script) // Add script at end of all
            {
                var script = scriptBrick as Script;
                Scripts.Add(script);

                //OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset)); // TODO: make faster and use method below instead
                OnScriptAdded((Script) scriptBrick, IndexOf(scriptBrick));
            }
        }

        private void InternalCollectionChanged(object sernder, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Remove || e.Action == NotifyCollectionChangedAction.Add)
            {
                if (e.Action == NotifyCollectionChangedAction.Remove)
                {
                    if (e.OldItems[0] is Script)
                    {
                        var args = new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset); // TODO: change this
                        OnCollectionChanged(args);
                    }
                    else
                    {
                        var args = new NotifyCollectionChangedEventArgs(e.Action, _lastDeletedBrick, LastDeletedIndex);
                        OnCollectionChanged(args);
                    }
                }
                else
                {
                    if (e.NewItems[0] is Script)
                    {
                        var args = new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset); // TODO: change this
                        OnCollectionChanged(args);
                    }
                    else
                    {
                        var args = new NotifyCollectionChangedEventArgs(e.Action, _lastInsertedBrick, _lastInsertedIndex);
                        OnCollectionChanged(args);
                    }
                }
            }
            else
            {
                var args = new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset);
                OnCollectionChanged(args);
            }
        }

        private void OnCollectionChanged(NotifyCollectionChangedEventArgs e)
        {
            if (CollectionChanged != null)
            {
                CollectionChanged.Invoke(this, e);
            }
        }

        private void OnScriptAdded(Script script, int index)
        {
            OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, script, index));

            var brickIndex = index;
            foreach (Brick brick in script.Bricks.Bricks)
            {
                brickIndex++;
                OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, brick, brickIndex));
            }
        }

        private void OnScriptRemoved(Script script, int index)
        {
            OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, script, index));

            var brickIndex = index;
            foreach (Brick brick in script.Bricks.Bricks)
            {
                //brickIndex++;
                OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, brick, brickIndex));
            }
        }

        public void RemoveAt(int index)
        {
            Script scriptToRemove = null;
            Brick brickToRemove = null;

            var count = 0;
            foreach (Script script in Scripts)
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

                        _lastDeletedBrick = brick;
                        LastDeletedIndex = index;

                        break;
                    }

                    count++;
                }

                if (brickToRemove != null)
                {
                    break;
                }
            }

            if (brickToRemove == null)
            {
                Scripts.Remove(scriptToRemove);

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
            Scripts.Clear();
        }

        public int Count
        {
            get
            {
                return Scripts.Sum(script => script.Bricks.Bricks.Count + 1);
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
                Remove((object) item);
                return true;
            }

            return false;
        }

        public IEnumerator<DataObject> GetEnumerator()
        {
            return new ScriptBrickIterator(Scripts) as IEnumerator<DataObject>;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return new ScriptBrickIterator(Scripts) as IEnumerator<DataObject>;
        }

        public int Add(object value)
        {
            if (value is Script)
            {
                Scripts.Add((Script) value);
            }

            if (value is Brick)
            {
                Scripts[Scripts.Count - 1].Bricks.Bricks.Add((Brick) value);
            }

            return 1; // TODO: should probably not be 1 ?
        }

        public bool Contains(object value)
        {
            if (Scripts.Contains(value as Script))
            {
                return true;
            }
            else
            {
                foreach (Script script in Scripts)
                {
                    if (script.Bricks.Bricks.Contains(value as Brick))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        public int ScriptIndexOf(Script value)
        {
            return Scripts.IndexOf(value);
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

            var enumerator = GetEnumerator();

            var count = 0;
            while (enumerator.MoveNext())
            {
                if (enumerator.Current == value)
                {
                    return count;
                }

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

            var count = 0;

            if (value is Script) // TODO: test me
            {
                var scriptIndex = 0;

                foreach (Script script in Scripts)
                {
                    if (count > index)
                    {
                        break;
                    }

                    count += script.Bricks.Bricks.Count + 1;
                    scriptIndex++;
                }

                Scripts.Insert(scriptIndex, (Script) value);
                OnScriptAdded((Script) value, count + 1);
            }

            if (value is Brick)
            {
                var brickCount = 0;
                _lastInsertedBrick = (Brick) value;
                _lastInsertedIndex = index;

                if (index == 0) // Cannot insert brick before first sprite
                {
                    index = 1;
                }

                foreach (Script script in Scripts)
                {
                    count++;
                    foreach (Brick brick in script.Bricks.Bricks)
                    {
                        if (count == index)
                        {
                            script.Bricks.Bricks.Insert(brickCount, (Brick) value);
                            OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, value, index));
                            return;
                        }

                        count++;
                        brickCount++;
                    }

                    if (count == index)
                    {
                        script.Bricks.Bricks.Insert(brickCount, (Brick) value);
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
            var index = IndexOf(value);

            if (value is Script)
            {
                var script = value as Script;
                if (Scripts.Contains(script))
                {
                    Scripts.Remove(script);

                    OnScriptRemoved(script, index);
                }
            }
            else if (value is Brick)
            {
                foreach (Script script in Scripts)
                {
                    if (script.Bricks.Bricks.Contains(value as Brick))
                    {
                        script.Bricks.Bricks.Remove(value as Brick);
                        OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, value, index));
                    }
                }
            }
        }

        object IList.this[int index]
        {
            get { return GetAtIndex(index); }
            set { throw new NotImplementedException(); }
        }

        private object GetAtIndex(int index)
        {
            var enumerator = GetEnumerator(); // TODO: make faster do not use enumerator

            var count = 0;
            while (enumerator.MoveNext())
            {
                if (count == index)
                {
                    return enumerator.Current;
                }

                count++;
            }

            return null;
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
            get { return Scripts; }
        }

        public event NotifyCollectionChangedEventHandler CollectionChanged;
    }
}