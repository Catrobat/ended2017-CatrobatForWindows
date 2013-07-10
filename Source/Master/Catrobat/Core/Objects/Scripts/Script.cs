using System.Xml.Linq;
using Catrobat.Core.Objects.Bricks;

namespace Catrobat.Core.Objects.Scripts
{
    public abstract class Script : DataObject
    {
        protected BrickList _bricks;
        public BrickList Bricks
        {
            get { return _bricks; }
            set
            {
                if (_bricks == value)
                {
                    return;
                }

                _bricks = value;
                RaisePropertyChanged();
            }
        }

        protected Sprite _sprite;
        public Sprite Sprite
        {
            get { return _sprite; }
            set
            {
                if (_sprite == value)
                {
                    return;
                }

                _sprite = value;
                _bricks.Sprite = value;

                RaisePropertyChanged();
            }
        }

        protected Script()
        {
            Bricks = new BrickList(null);
        }

        protected Script(Sprite parent)
        {
            Bricks = new BrickList(parent);
            _sprite = parent;
        }

        protected Script(XElement xElement, Sprite parent)
        {
            Bricks = new BrickList(parent);
            _sprite = parent;

            LoadFromCommonXML(xElement);
            LoadFromXML(xElement);
        }

        
        internal abstract override void LoadFromXML(XElement xRoot);

        private void LoadFromCommonXML(XElement xRoot)
        {
            if (xRoot.Element("brickList") != null)
            {
                _bricks = new BrickList(xRoot.Element("brickList"), _sprite);
            }
        }

        internal abstract override XElement CreateXML();

        protected void CreateCommonXML(XElement xRoot)
        {
            if (_bricks != null)
            {
                xRoot.Add(_bricks.CreateXML());
            }
        }

        public abstract DataObject Copy(Sprite parent);

        public void CopyReference(Script copiedFrom, Sprite parent)
        {
            if (copiedFrom.Bricks != null)
            {
                _bricks.CopyReference(copiedFrom.Bricks, parent);
            }
        }
    }
}