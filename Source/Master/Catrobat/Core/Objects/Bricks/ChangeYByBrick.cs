using System.ComponentModel;
using System.Xml.Linq;

namespace Catrobat.Core.Objects
{
    public class ChangeYByBrick : Brick
    {
        protected int yMovement = 100;

        public ChangeYByBrick()
        {
        }

        public ChangeYByBrick(Sprite parent) : base(parent)
        {
        }

        public ChangeYByBrick(XElement xElement, Sprite parent) : base(xElement, parent)
        {
        }

        public int YMovement
        {
            get { return yMovement; }
            set
            {
                yMovement = value;
                OnPropertyChanged(new PropertyChangedEventArgs("YMovement"));
            }
        }

        internal override void LoadFromXML(XElement xRoot)
        {
            yMovement = int.Parse(xRoot.Element("yMovement").Value);
        }

        internal override XElement CreateXML()
        {
            var xRoot = new XElement("changeYByBrick");

            xRoot.Add(new XElement("yMovement")
                {
                    Value = yMovement.ToString()
                });

            //CreateCommonXML(xRoot);

            return xRoot;
        }

        public override DataObject Copy(Sprite parent)
        {
            var newBrick = new ChangeYByBrick(parent);
            newBrick.yMovement = yMovement;

            return newBrick;
        }
    }
}