using System.ComponentModel;
using System.Xml.Linq;

namespace Catrobat.Core.Objects.Bricks
{
    public class ChangeXByBrick : Brick
    {
        protected int xMovement = 100;

        public ChangeXByBrick()
        {
        }

        public ChangeXByBrick(Sprite parent) : base(parent)
        {
        }

        public ChangeXByBrick(XElement xElement, Sprite parent) : base(xElement, parent)
        {
        }

        public int XMovement
        {
            get { return xMovement; }
            set
            {
                xMovement = value;
                OnPropertyChanged(new PropertyChangedEventArgs("XMovement"));
            }
        }

        internal override void LoadFromXML(XElement xRoot)
        {
            xMovement = int.Parse(xRoot.Element("xMovement").Value);
        }

        internal override XElement CreateXML()
        {
            var xRoot = new XElement("changeXByBrick");

            xRoot.Add(new XElement("xMovement")
                {
                    Value = xMovement.ToString()
                });

            //CreateCommonXML(xRoot);

            return xRoot;
        }

        public override DataObject Copy(Sprite parent)
        {
            var newBrick = new ChangeXByBrick(parent);
            newBrick.xMovement = xMovement;

            return newBrick;
        }
    }
}