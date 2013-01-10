using System.ComponentModel;
using System.Xml.Linq;
using Catrobat.Core.Objects.Bricks;

namespace Catrobat.Core.Objects
{
    public abstract class Script : DataObject
    {
        protected BrickList bricks;

        protected Sprite sprite;

        public Script()
        {
            Bricks = new BrickList(null);
        }

        public Script(Sprite parent)
        {
            Bricks = new BrickList(parent);
            sprite = parent;
        }

        public Script(XElement xElement, Sprite parent)
        {
            Bricks = new BrickList(parent);
            sprite = parent;

            LoadFromCommonXML(xElement);
            LoadFromXML(xElement);
        }

        public BrickList Bricks
        {
            get { return bricks; }
            set
            {
                if (bricks == value)
                    return;

                bricks = value;
                OnPropertyChanged(new PropertyChangedEventArgs("Bricks"));
            }
        }

        public Sprite Sprite
        {
            get { return sprite; }
            set
            {
                if (sprite == value)
                    return;

                sprite = value;
                bricks.Sprite = value;

                OnPropertyChanged(new PropertyChangedEventArgs("Sprite"));
            }
        }

        internal abstract override void LoadFromXML(XElement xRoot);

        private void LoadFromCommonXML(XElement xRoot)
        {
            if (xRoot.Element("brickList") != null)
                bricks = new BrickList(xRoot.Element("brickList"), sprite);
        }

        internal abstract override XElement CreateXML();

        protected void CreateCommonXML(XElement xRoot)
        {
            if (bricks != null)
                xRoot.Add(bricks.CreateXML());
        }

        public abstract DataObject Copy(Sprite parent);

        public void CopyReference(Script copiedFrom, Sprite parent)
        {
            if (copiedFrom.Bricks != null)
                bricks.CopyReference(copiedFrom.Bricks, parent);
        }
    }
}