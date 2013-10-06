using System.Xml.Linq;
using Catrobat.Core.Misc.Helpers;

namespace Catrobat.Core.CatrobatObjects.Bricks
{
    public class IfLogicElseBrickReference : DataObject
    {
        internal string _reference;

        private IfLogicElseBrick _ifLogicElseBrick;
        public IfLogicElseBrick IfLogicElseBrick
        {
            get { return _ifLogicElseBrick; }
            set
            {
                if (_ifLogicElseBrick == value)
                    return;

                _ifLogicElseBrick = value;
                RaisePropertyChanged();
            }
        }


        public IfLogicElseBrickReference()
        {
        }

        public IfLogicElseBrickReference(XElement xElement)
        {
            LoadFromXML(xElement);
        }

        internal override void LoadFromXML(XElement xRoot)
        {
            _reference = xRoot.Attribute("reference").Value;
        }

        internal override XElement CreateXML()
        {
            var xRoot = new XElement("ifElseBrick");

            xRoot.Add(new XAttribute("reference", ReferenceHelper.GetReferenceString(this)));

            return xRoot;
        }

        internal override void LoadReference()
        {
            if(IfLogicElseBrick == null)
                IfLogicElseBrick = ReferenceHelper.GetReferenceObject(this, _reference) as IfLogicElseBrick;
            if (string.IsNullOrEmpty(_reference))
                _reference = ReferenceHelper.GetReferenceString(this);
        }

        public DataObject Copy()
        {
            var newIfLogicElseBrickRef = new IfLogicElseBrickReference();
            newIfLogicElseBrickRef.IfLogicElseBrick = _ifLogicElseBrick;

            return newIfLogicElseBrickRef;
        }

        public override bool Equals(DataObject other)
        {
            var otherReference = other as IfLogicElseBrickReference;

            if (otherReference == null)
                return false;

            if (_reference != otherReference._reference)
                return false;

            return true;
        }
    }
}