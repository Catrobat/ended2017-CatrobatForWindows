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


        protected Script()
        {
            Bricks = new BrickList();
        }

        protected Script(XElement xElement)
        {
            Bricks = new BrickList();

            LoadFromCommonXML(xElement);
            LoadFromXML(xElement);
        }

        
        internal abstract override void LoadFromXML(XElement xRoot);

        private void LoadFromCommonXML(XElement xRoot)
        {
            if (xRoot.Element("brickList") != null)
            {
                _bricks = new BrickList(xRoot.Element("brickList"));
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

        internal override void LoadReference()
        {
            foreach (var brick in Bricks.Bricks)
                brick.LoadReference();
        }

        public abstract DataObject Copy();

        public void CopyReference(Script copiedFrom)
        {
            if (copiedFrom.Bricks != null)
                _bricks.CopyReference(copiedFrom.Bricks);
        }
    }
}