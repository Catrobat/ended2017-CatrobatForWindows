using System.ComponentModel;
using System.Xml.Linq;
using Catrobat.Core.Misc.Helpers;

namespace Catrobat.Core.Objects.Bricks
{
    public class LoopEndBrickReference : DataObject
    {
        internal string _reference;

        protected string _classField;
        public string Class
        {
            get { return _classField; }
            set
            {
                if (_classField == value)
                {
                    return;
                }

                _classField = value;
                RaisePropertyChanged();
            }
        }

        private LoopEndBrick _loopEndBrick;
        public LoopEndBrick LoopEndBrick
        {
            get { return _loopEndBrick; }
            set
            {
                if (_loopEndBrick == value)
                    return;

                _loopEndBrick = value;
                RaisePropertyChanged();
            }
        }


        public LoopEndBrickReference()
        {
        }

        public LoopEndBrickReference(XElement xElement)
        {
            LoadFromXML(xElement);
        }

        internal override void LoadFromXML(XElement xRoot)
        {
            _classField = xRoot.Attribute("class").Value;
            _reference = xRoot.Attribute("reference").Value;
        }

        internal override XElement CreateXML()
        {
            var xRoot = new XElement("loopEndBrick");
            xRoot.Add(new XAttribute("class", _classField));
            xRoot.Add(new XAttribute("reference", ReferenceHelper.GetReferenceString(this)));

            return xRoot;
        }

        internal override void LoadReference()
        {
            if(LoopEndBrick == null)
                LoopEndBrick = ReferenceHelper.GetReferenceObject(this, _reference) as LoopEndBrick;
            if (string.IsNullOrEmpty(_reference))
                _reference = ReferenceHelper.GetReferenceString(this);
        }

        public DataObject Copy()
        {
            var newLoopEndBrickRef = new LoopEndBrickReference();
            newLoopEndBrickRef.Class = _classField;
            newLoopEndBrickRef.LoopEndBrick = _loopEndBrick;

            return newLoopEndBrickRef;
        }

        public override bool Equals(DataObject other)
        {
            var otherReference = other as LoopEndBrickReference;

            if (otherReference == null)
                return false;

            if (_reference != otherReference._reference)
                return false;

            return true;
        }
    }
}