using System;
using System.Collections;
using System.Collections.Generic;
using Catrobat.IDE.Core.Models;
using Catrobat.IDE.Core.Models.Bricks;
using Catrobat.IDE.Core.Models.Scripts;

namespace Catrobat.IDE.Core.UI
{
    [Obsolete("Replace with Scripts.SelectMany(script => Enumerable.Repeat<Model>(script, 1).Concat(script.Bricks)). ")]
    public class ScriptBrickIterator : IEnumerator<ModelBase>
    {
        private readonly IEnumerator<Script> _scriptEnumerator;
        private IEnumerator<Brick> _brickEnumerator;

        public ScriptBrickIterator(IEnumerable<Script> scripts)
        {
            // TODO: check synchronization
            _scriptEnumerator = scripts.GetEnumerator();
        }

        public ModelBase Current
        {
            get
            {
                if (_brickEnumerator != null && _brickEnumerator.Current != null)
                {
                    return _brickEnumerator.Current;
                }
                else
                {
                    var script = _scriptEnumerator.Current;

                    //if (script == null)
                    //    return new EmptyDummyBrick();


                    return script;
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
                    var script = _scriptEnumerator.Current;

                    //if (script == null)
                    //    return new EmptyDummyBrick();


                    return script;
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

                    _scriptEnumerator.MoveNext();

                    if (_scriptEnumerator.Current != null)
                        _brickEnumerator = _scriptEnumerator.Current.Bricks.GetEnumerator();

                    if (_scriptEnumerator.Current == null)
                    {
                        //var isLastEmpty = !WasEmptyItem;

                        //if (isLastEmpty)
                        //{
                        //    WasEmptyItem = true;
                        //    return true;
                        //}

                        return false;
                    }
                    _brickEnumerator = _scriptEnumerator.Current.Bricks.GetEnumerator();
                }
            }
            else
            {
                _scriptEnumerator.MoveNext();

                if (_scriptEnumerator.Current != null)
                {
                    _brickEnumerator = _scriptEnumerator.Current.Bricks.GetEnumerator();
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

        //public bool WasEmptyItem { get; set; }
    }
}