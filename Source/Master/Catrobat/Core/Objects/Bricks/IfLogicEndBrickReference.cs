using System.Xml.Linq;
using Catrobat.Core.Misc.Helpers;

namespace Catrobat.Core.Objects.Bricks
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
            IfLogicEndBrick = ReferenceHelper.GetReferenceObject(this, _reference) as IfLogicEndBrick;
        }

        public DataObject Copy()
        {
            var newIfLogicEndBrickRef = new IfLogicEndBrickReference();
            newIfLogicEndBrickRef.IfLogicEndBrick = _ifLogicEndBrick;

            return newIfLogicEndBrickRef;
        }

        public override bool Equals(DataObject other)
        {
            throw new System.NotImplementedException();
        }
    }
}