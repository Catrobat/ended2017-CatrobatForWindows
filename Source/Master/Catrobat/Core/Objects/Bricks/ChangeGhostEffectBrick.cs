using System.ComponentModel;
using System.Globalization;
using System.Xml.Linq;

namespace Catrobat.Core.Objects
{
    public class ChangeGhostEffectBrick : Brick
    {
        protected double changeGhostEffect = 25.0f;

        public ChangeGhostEffectBrick()
        {
        }

        public ChangeGhostEffectBrick(Sprite parent) : base(parent)
        {
        }

        public ChangeGhostEffectBrick(XElement xElement, Sprite parent) : base(xElement, parent)
        {
        }

        public double ChangeGhostEffect
        {
            get { return changeGhostEffect; }
            set
            {
                changeGhostEffect = value;
                OnPropertyChanged(new PropertyChangedEventArgs("ChangeGhostEffect"));
            }
        }

        internal override void LoadFromXML(XElement xRoot)
        {
            changeGhostEffect = double.Parse(xRoot.Element("changeGhostEffect").Value, CultureInfo.InvariantCulture);
        }

        internal override XElement CreateXML()
        {
            var xRoot = new XElement("changeGhostEffectBrick");

            xRoot.Add(new XElement("changeGhostEffect")
                {
                    Value = changeGhostEffect.ToString()
                });

            //CreateCommonXML(xRoot);

            return xRoot;
        }

        public override DataObject Copy(Sprite parent)
        {
            var newBrick = new ChangeGhostEffectBrick(parent);
            newBrick.changeGhostEffect = changeGhostEffect;

            return newBrick;
        }
    }
}