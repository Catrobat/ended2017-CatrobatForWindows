using System.Xml.Linq;
using Catrobat.IDE.Core.Xml.XmlObjects.Formulas;

namespace Catrobat.IDE.Core.Xml.XmlObjects.Bricks.Sounds
{
    public partial class XmlPlaySoundBrick : XmlBrick
    {
        private XmlSoundReference _xmlSoundReference;
        internal XmlSoundReference XmlSoundReference
        {
            get { return _xmlSoundReference; }
            set
            {
                if (_xmlSoundReference == value)
                {
                    return;
                }

                _xmlSoundReference = value;
                RaisePropertyChanged();
                RaisePropertyChanged(() => Sound);
            }
        }

        public XmlSound Sound
        {
            get
            {
                if (_xmlSoundReference == null)
                {
                    return null;
                }

                return _xmlSoundReference.Sound;
            }
            set
            {
                if (_xmlSoundReference == null)
                    _xmlSoundReference = new XmlSoundReference();

                if (_xmlSoundReference.Sound == value)
                    return;

                _xmlSoundReference.Sound = value;

                if (value == null)
                    _xmlSoundReference = null;

                RaisePropertyChanged();
            }
        }


        public XmlPlaySoundBrick() { }

        public XmlPlaySoundBrick(XElement xElement) : base(xElement) { }

        internal override void LoadFromXml(XElement xRoot)
        {
            if (xRoot.Element("sound") != null)
            {
                _xmlSoundReference = new XmlSoundReference(xRoot.Element("sound"));
            }
        }

        internal override XElement CreateXml()
        {
            var xRoot = new XElement("playSoundBrick");

            if (_xmlSoundReference != null)
            {
                xRoot.Add(_xmlSoundReference.CreateXml());
            }

            ////CreateCommonXML(xRoot);

            return xRoot;
        }

        internal override void LoadReference()
        {
            if (_xmlSoundReference != null)
                _xmlSoundReference.LoadReference();
        }

        public override XmlObject Copy()
        {
            var newBrick = new XmlPlaySoundBrick();

            if (_xmlSoundReference != null)
                newBrick._xmlSoundReference = _xmlSoundReference.Copy() as XmlSoundReference;

            return newBrick;
        }

        public override bool Equals(XmlObject other)
        {
            var otherBrick = other as XmlPlaySoundBrick;

            if (otherBrick == null)
                return false;

            return XmlSoundReference.Equals(otherBrick.XmlSoundReference);
        }
    }
}