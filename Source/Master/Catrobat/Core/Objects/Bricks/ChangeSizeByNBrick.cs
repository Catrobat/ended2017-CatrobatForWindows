using System.ComponentModel;
using System.Globalization;
using System.Xml.Linq;

namespace Catrobat.Core.Objects
{
    public class ChangeSizeByNBrick : Brick
    {
        protected double size = 20.0f;

        public ChangeSizeByNBrick()
        {
        }

        public ChangeSizeByNBrick(Sprite parent) : base(parent)
        {
        }

        public ChangeSizeByNBrick(XElement xElement, Sprite parent) : base(xElement, parent)
        {
        }

        public double Size
        {
            get { return size; }
            set
            {
                size = value;
                OnPropertyChanged(new PropertyChangedEventArgs("Size"));
            }
        }

        internal override void LoadFromXML(XElement xRoot)
        {
            size = double.Parse(xRoot.Element("size").Value, CultureInfo.InvariantCulture);
        }

        internal override XElement CreateXML()
        {
            var xRoot = new XElement("changeSizeByNBrick");

            xRoot.Add(new XElement("size")
                {
                    Value = size.ToString()
                });

            //CreateCommonXML(xRoot);

            return xRoot;
        }

        public override DataObject Copy(Sprite parent)
        {
            var newBrick = new ChangeSizeByNBrick(parent);
            newBrick.size = size;

            return newBrick;
        }
    }
}