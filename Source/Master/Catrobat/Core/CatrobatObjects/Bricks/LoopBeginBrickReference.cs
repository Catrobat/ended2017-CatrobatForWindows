using System.ComponentModel;
using System.Xml.Linq;
using Catrobat.Core.Misc.Helpers;

namespace Catrobat.Core.CatrobatObjects.Bricks
{
    public class LoopBeginBrickReference : DataObject
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

        private LoopBeginBrick _loopBeginBrick;
        public LoopBeginBrick LoopBeginBrick
        {
            get { return _loopBeginBrick; }
            set
            {
                if (_loopBeginBrick == value)
                    return;

                _loopBeginBrick = value;
                RaisePropertyChanged();
            }
        }


        public LoopBeginBrickReference() 
        {
        }


        public LoopBeginBrickReference(XElement xElement)
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
            var xRoot = new XElement("loopBeginBrick");
            xRoot.Add(new XAttribute("class", _classField));
            xRoot.Add(new XAttribute("reference", ReferenceHelper.GetReferenceString(this)));

            return xRoot;
        }

        internal override void LoadReference()
        {
            if(LoopBeginBrick == null)
                LoopBeginBrick = ReferenceHelper.GetReferenceObject(this, _reference) as LoopBeginBrick;
            if (string.IsNullOrEmpty(_reference))
                _reference = ReferenceHelper.GetReferenceString(this);
        }

        public DataObject Copy()
        {
            var newLoopBeginBrickRef = new LoopBeginBrickReference();
            newLoopBeginBrickRef.Class = _classField;
            newLoopBeginBrickRef.LoopBeginBrick = _loopBeginBrick;

            return newLoopBeginBrickRef;
        }

        public override bool Equals(DataObject other)
        {
            var otherReference = other as LoopBeginBrickReference;

            if (otherReference == null)
                return false;

            if (_reference != otherReference._reference)
                return false;

            return true;
        }
    }
}