using System.ComponentModel;
using System.Xml.Linq;
using Catrobat.Core.Misc.Helpers;

namespace Catrobat.Core.Objects.Bricks
{
    public class LoopEndBrickReference : DataObject
    {
        internal string _reference;

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
            _reference = xRoot.Attribute("reference").Value;
        }

        internal override XElement CreateXML()
        {
            var xRoot = new XElement("loopEndBrick");
            xRoot.Add(new XAttribute("reference", ReferenceHelper.GetReferenceString(this)));

            return xRoot;
        }

        internal override void LoadReference()
        {
            if(LoopEndBrick == null)
                LoopEndBrick = ReferenceHelper.GetReferenceObject(this, _reference) as LoopEndBrick;
            if (_reference == "")
                _reference = ReferenceHelper.GetReferenceString(LoopEndBrick);
        }

        public DataObject Copy()
        {
            var newLoopEndBrickRef = new LoopEndBrickReference();
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