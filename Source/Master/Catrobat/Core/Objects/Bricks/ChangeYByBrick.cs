using System.ComponentModel;
using System.Xml.Linq;

namespace Catrobat.Core.Objects.Bricks
{
    public class ChangeYByBrick : Brick
    {
        protected int _yMovement = 100;
        public int YMovement
        {
            get { return _yMovement; }
            set
            {
                _yMovement = value;
                RaisePropertyChanged();
            }
        }


        public ChangeYByBrick() {}

        public ChangeYByBrick(Sprite parent) : base(parent) {}

        public ChangeYByBrick(XElement xElement, Sprite parent) : base(xElement, parent) {}

        internal override void LoadFromXML(XElement xRoot)
        {
            _yMovement = int.Parse(xRoot.Element("yMovement").Value);
        }

        internal override XElement CreateXML()
        {
            var xRoot = new XElement("changeYByNBrick");

            xRoot.Add(new XElement("yMovement")
            {
                Value = _yMovement.ToString()
            });

            //CreateCommonXML(xRoot);

            return xRoot;
        }

        public override DataObject Copy(Sprite parent)
        {
            var newBrick = new ChangeYByBrick(parent);
            newBrick._yMovement = _yMovement;

            return newBrick;
        }
    }
}