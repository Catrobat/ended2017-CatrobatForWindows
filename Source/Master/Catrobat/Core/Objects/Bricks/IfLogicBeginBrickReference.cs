using System.Xml.Linq;
using Catrobat.Core.Misc.Helpers;

namespace Catrobat.Core.Objects.Bricks
{
    public class IfLogicBeginBrickReference : DataObject
    {
        internal string _reference;

        private IfLogicBeginBrick _ifLogicBeginBrick;
        public IfLogicBeginBrick IfLogicBeginBrick
        {
            get { return _ifLogicBeginBrick; }
            set
            {
                if (_ifLogicBeginBrick == value)
                    return;

                _ifLogicBeginBrick = value;
                RaisePropertyChanged();
            }
        }


        public IfLogicBeginBrickReference()
        {
        }

        public IfLogicBeginBrickReference(XElement xElement)
        {
            LoadFromXML(xElement);
        }

        internal override void LoadFromXML(XElement xRoot)
        {
            _reference = xRoot.Attribute("reference").Value;
        }

        internal override XElement CreateXML()
        {
            var xRoot = new XElement("ifBeginBrick");

            xRoot.Add(new XAttribute("reference", ReferenceHelper.GetReferenceString(this)));

            return xRoot;
        }

        internal override void LoadReference()
        {
            if(IfLogicBeginBrick == null)
                IfLogicBeginBrick = ReferenceHelper.GetReferenceObject(this, _reference) as IfLogicBeginBrick;
            if (string.IsNullOrEmpty(_reference))
                _reference = ReferenceHelper.GetReferenceString(this);
        }

        public DataObject Copy()
        {
            var newIfLogicBeginBrickRef = new IfLogicBeginBrickReference();
            newIfLogicBeginBrickRef.IfLogicBeginBrick = _ifLogicBeginBrick;

            return newIfLogicBeginBrickRef;
        }

        public override bool Equals(DataObject other)
        {
            var otherReference = other as IfLogicBeginBrickReference;

            if (otherReference == null)
                return false;

            if (_reference != otherReference._reference)
                return false;

            return true;
        }
    }
}