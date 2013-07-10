using System.ComponentModel;
using System.Xml.Linq;

namespace Catrobat.Core.Objects.Bricks
{
    public class ChangeXByBrick : Brick
    {
        protected int _xMovement = 100;
        public int XMovement
        {
            get { return _xMovement; }
            set
            {
                _xMovement = value;
                RaisePropertyChanged();
            }
        }


        public ChangeXByBrick() {}

        public ChangeXByBrick(Sprite parent) : base(parent) {}

        public ChangeXByBrick(XElement xElement, Sprite parent) : base(xElement, parent) {}

        internal override void LoadFromXML(XElement xRoot)
        {
            _xMovement = int.Parse(xRoot.Element("xMovement").Value);
        }

        internal override XElement CreateXML()
        {
            var xRoot = new XElement("changeXByNBrick");

            xRoot.Add(new XElement("xMovement")
            {
                Value = _xMovement.ToString()
            });

            //CreateCommonXML(xRoot);

            return xRoot;
        }

        public override DataObject Copy(Sprite parent)
        {
            var newBrick = new ChangeXByBrick(parent);
            newBrick._xMovement = _xMovement;

            return newBrick;
        }
    }
}