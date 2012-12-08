using System.ComponentModel;
using System.Xml.Linq;

namespace Catrobat.Core.Objects
{
    public class SetYBrick : Brick
    {
        protected int yPosition = 0;

        public SetYBrick()
        {
        }

        public SetYBrick(Sprite parent) : base(parent)
        {
        }

        public SetYBrick(XElement xElement, Sprite parent) : base(xElement, parent)
        {
        }

        public int YPosition
        {
            get { return yPosition; }
            set
            {
                yPosition = value;
                OnPropertyChanged(new PropertyChangedEventArgs("YPosition"));
            }
        }

        internal override void LoadFromXML(XElement xRoot)
        {
            yPosition = int.Parse(xRoot.Element("yPosition").Value);
        }

        internal override XElement CreateXML()
        {
            var xRoot = new XElement("setYBrick");

            xRoot.Add(new XElement("yPosition")
                {
                    Value = yPosition.ToString()
                });

            //CreateCommonXML(xRoot);

            return xRoot;
        }

        public override DataObject Copy(Sprite parent)
        {
            var newBrick = new SetYBrick(parent);
            newBrick.yPosition = yPosition;

            return newBrick;
        }
    }
}