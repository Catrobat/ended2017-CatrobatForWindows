using System.Collections.Generic;
using Catrobat.Core.Objects;
using Catrobat.Core.Objects.Bricks;
using System.Collections.ObjectModel;

namespace Catrobat.IDEWindowsPhone.Views.Editor.Scripts
{
    public class ScriptBrickIterator : IEnumerator<DataObject>
    {
        ObservableCollection<Script> scripts;
        IEnumerator<Script> scriptEnumerator;
        IEnumerator<Brick> brickEnumerator;

        public ScriptBrickIterator(ObservableCollection<Script> scripts)
        {
            // TODO: check synchronization
            this.scripts = scripts;
            scriptEnumerator = scripts.GetEnumerator();
        }

        public DataObject Current
        {
            get
            {
                if (brickEnumerator != null && brickEnumerator.Current != null)
                    return brickEnumerator.Current;
                else
                    return scriptEnumerator.Current;
            }
        }

        public void Dispose()
        {
            scriptEnumerator.Dispose();
            brickEnumerator.Dispose();
        }

        object System.Collections.IEnumerator.Current
        {
            get
            {
                if (brickEnumerator != null && brickEnumerator.Current != null)
                    return brickEnumerator.Current;
                else
                    return scriptEnumerator.Current;
            }
        }

        public bool MoveNext()
        {
            if (brickEnumerator != null)
            {
                brickEnumerator.MoveNext();

                if (brickEnumerator.Current == null)
                {
                    brickEnumerator.Dispose();
                    brickEnumerator = scriptEnumerator.Current.Bricks.Bricks.GetEnumerator();
                    scriptEnumerator.MoveNext();

                    if (scriptEnumerator.Current == null)
                        return false;
                    else
                        brickEnumerator = scriptEnumerator.Current.Bricks.Bricks.GetEnumerator();
                }
            }
            else
            {
                scriptEnumerator.MoveNext();

                if (scriptEnumerator.Current != null)
                {
                    brickEnumerator = scriptEnumerator.Current.Bricks.Bricks.GetEnumerator();
                }
            }

            return true;
        }

        public void Reset()
        {
            scriptEnumerator.Reset();
            brickEnumerator.Dispose();
            brickEnumerator = null;
        }
    }
}
