using System.Xml.Linq;
using Catrobat.Core.Utilities.Helpers;

namespace Catrobat.Core.CatrobatObjects.Bricks
{
    public class IfLogicEndBrickReference : DataObject
    {
        internal string _reference;

        private IfLogicEndBrick _ifLogicEndBrick;
        public IfLogicEndBrick IfLogicEndBrick
        {
            get { return _ifLogicEndBrick; }
            set
            {
                if (_ifLogicEndBrick == value)
                    return;

                _ifLogicEndBrick = value;
                RaisePropertyChanged();
            }
        }


        public IfLogicEndBrickReference()
        {
        }

        public IfLogicEndBrickReference(XElement xElement)
        {
            LoadFromXML(xElement);
        }

        internal override void LoadFromXML(XElement xRoot)
        {
            _reference = xRoot.Attribute("reference").Value;
        }

        internal override XElement CreateXML()
        {
            var xRoot = new XElement("ifEndBrick");

            xRoot.Add(new XAttribute("reference", ReferenceHelper.GetReferenceString(this)));

            return xRoot;
        }

        internal override void LoadReference()
        {
            if(IfLogicEndBrick == null)
                IfLogicEndBrick = ReferenceHelper.GetReferenceObject(this, _reference) as IfLogicEndBrick;
            if (string.IsNullOrEmpty(_reference))
                _reference = ReferenceHelper.GetReferenceString(this);
        }

        public DataObject Copy()
        {
            var newIfLogicEndBrickRef = new IfLogicEndBrickReference();
            newIfLogicEndBrickRef.IfLogicEndBrick = _ifLogicEndBrick;

            return newIfLogicEndBrickRef;
        }

        public override bool Equals(DataObject other)
        {
            var otherReference = other as IfLogicEndBrickReference;

            if (otherReference == null)
                return false;

            if (_reference != otherReference._reference)
                return false;

            return true;
        }
    }
}