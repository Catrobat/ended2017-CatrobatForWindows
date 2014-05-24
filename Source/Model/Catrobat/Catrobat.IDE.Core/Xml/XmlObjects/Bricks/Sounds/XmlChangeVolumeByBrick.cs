using System.Xml.Linq;
using Catrobat.IDE.Core.Xml.XmlObjects.Formulas;

namespace Catrobat.IDE.Core.Xml.XmlObjects.Bricks.Sounds
{
    public partial class XmlChangeVolumeByBrick : XmlBrick
    {
        protected XmlFormula _volume;
        public XmlFormula Volume
        {
            get { return _volume; }
            set
            {
                _volume = value;
                RaisePropertyChanged();
            }
        }


        public XmlChangeVolumeByBrick() {}

        public XmlChangeVolumeByBrick(XElement xElement) : base(xElement) {}

        internal override void LoadFromXml(XElement xRoot)
        {
            _volume = new XmlFormula(xRoot.Element("volume"));
        }

        internal override XElement CreateXml()
        {
            var xRoot = new XElement("changeVolumeByNBrick");

            var xVariable = new XElement("volume");
            xVariable.Add(_volume.CreateXml());
            xRoot.Add(xVariable);

            return xRoot;
        }

        internal override void LoadReference()
        {
            if (_volume != null)
                _volume.LoadReference();
        }

        public override XmlObject Copy()
        {
            var newBrick = new XmlChangeVolumeByBrick();
            newBrick._volume = _volume.Copy() as XmlFormula;

            return newBrick;
        }

        public override bool Equals(XmlObject other)
        {
            var otherBrick = other as XmlChangeVolumeByBrick;

            if (otherBrick == null)
                return false;

            return Volume.Equals(otherBrick.Volume);
        }
    }
}