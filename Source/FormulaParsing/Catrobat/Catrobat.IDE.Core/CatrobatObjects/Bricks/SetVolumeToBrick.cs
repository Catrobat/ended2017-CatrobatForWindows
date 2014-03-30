using System.ComponentModel;
using System.Globalization;
using System.Xml.Linq;
using Catrobat.IDE.Core.CatrobatObjects.Formulas;
using Catrobat.IDE.Core.FormulaEditor;

namespace Catrobat.IDE.Core.CatrobatObjects.Bricks
{
    public class SetVolumeToBrick : Brick
    {
        protected Formula _volume;
        public Formula Volume
        {
            get { return _volume; }
            set
            {
                _volume = value;
                RaisePropertyChanged();
            }
        }


        public SetVolumeToBrick() {}

        public SetVolumeToBrick(XElement xElement) : base(xElement) {}

        internal override void LoadFromXML(XElement xRoot)
        {
            _volume = new Formula(xRoot.Element("volume"));
        }

        internal override XElement CreateXML()
        {
            var xRoot = new XElement("setVolumeToBrick");

            var xVariable = new XElement("volume");
            xVariable.Add(_volume.CreateXML());
            xRoot.Add(xVariable);

            return xRoot;
        }

        internal override void LoadReference(XmlFormulaTreeConverter converter)
        {
            if (_volume != null)
                _volume.LoadReference(converter);
        }

        public override DataObject Copy()
        {
            var newBrick = new SetVolumeToBrick();
            newBrick._volume = _volume.Copy() as Formula;

            return newBrick;
        }

        public override bool Equals(DataObject other)
        {
            var otherBrick = other as SetVolumeToBrick;

            if (otherBrick == null)
                return false;

            return Volume.Equals(otherBrick.Volume);
        }
    }
}