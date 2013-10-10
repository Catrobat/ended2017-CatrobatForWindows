using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Catrobat.IDE.Core.CatrobatObjects;
using Catrobat.IDE.Core.CatrobatObjects.Bricks;
using Catrobat.IDE.Core.CatrobatObjects.Scripts;

namespace Catrobat.IDE.Core.CatroatObjects.Scripts
{
    public class ScriptBrickIterator : IEnumerator<DataObject>
    {
        private ObservableCollection<Script> _scripts;
        private readonly IEnumerator<Script> _scriptEnumerator;
        private IEnumerator<Brick> _brickEnumerator;

        public ScriptBrickIterator(ObservableCollection<Script> scripts)
        {
            // TODO: check synchronization
            this._scripts = scripts;
            _scriptEnumerator = scripts.GetEnumerator();
        }

        public DataObject Current
        {
            get
            {
                if (_brickEnumerator != null && _brickEnumerator.Current != null)
                {
                    return _brickEnumerator.Current;
                }
                else
                {
                    return _scriptEnumerator.Current;
                }
            }
        }

        public void Dispose()
        {
            _scriptEnumerator.Dispose();
            _brickEnumerator.Dispose();
        }

        object IEnumerator.Current
        {
            get
            {
                if (_brickEnumerator != null && _brickEnumerator.Current != null)
                {
                    return _brickEnumerator.Current;
                }
                else
                {
                    return _scriptEnumerator.Current;
                }
            }
        }

        public bool MoveNext()
        {
            if (_brickEnumerator != null)
            {
                _brickEnumerator.MoveNext();

                if (_brickEnumerator.Current == null)
                {
                    _brickEnumerator.Dispose();
                    _brickEnumerator = _scriptEnumerator.Current.Bricks.Bricks.GetEnumerator();
                    _scriptEnumerator.MoveNext();

                    if (_scriptEnumerator.Current == null)
                    {
                        return false;
                    }
                    else
                    {
                        _brickEnumerator = _scriptEnumerator.Current.Bricks.Bricks.GetEnumerator();
                    }
                }
            }
            else
            {
                _scriptEnumerator.MoveNext();

                if (_scriptEnumerator.Current != null)
                {
                    _brickEnumerator = _scriptEnumerator.Current.Bricks.Bricks.GetEnumerator();
                }
            }

            return true;
        }

        public void Reset()
        {
            _scriptEnumerator.Reset();
            _brickEnumerator.Dispose();
            _brickEnumerator = null;
        }
    }
}