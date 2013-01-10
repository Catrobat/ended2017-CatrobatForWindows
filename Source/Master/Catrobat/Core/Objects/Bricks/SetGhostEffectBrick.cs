using System.ComponentModel;
using System.Globalization;
using System.Xml.Linq;

namespace Catrobat.Core.Objects.Bricks
{
    public class SetGhostEffectBrick : Brick
    {
        protected double transparency = 0.0f;

        public SetGhostEffectBrick()
        {
        }

        public SetGhostEffectBrick(Sprite parent) : base(parent)
        {
        }

        public SetGhostEffectBrick(XElement xElement, Sprite parent) : base(xElement, parent)
        {
        }

        public double Transparency
        {
            get { return transparency; }
            set
            {
                transparency = value;
                OnPropertyChanged(new PropertyChangedEventArgs("Transparency"));
            }
        }

        internal override void LoadFromXML(XElement xRoot)
        {
            transparency = double.Parse(xRoot.Element("transparency").Value, CultureInfo.InvariantCulture);
        }

        internal override XElement CreateXML()
        {
            var xRoot = new XElement("setGhostEffectBrick");

            xRoot.Add(new XElement("transparency")
                {
                    Value = transparency.ToString()
                });

            //CreateCommonXML(xRoot);

            return xRoot;
        }

        public override DataObject Copy(Sprite parent)
        {
            var newBrick = new SetGhostEffectBrick(parent);
            newBrick.transparency = transparency;

            return newBrick;
        }
    }
}