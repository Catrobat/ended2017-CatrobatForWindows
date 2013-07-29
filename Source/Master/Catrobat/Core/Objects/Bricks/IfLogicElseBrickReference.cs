using System.Xml.Linq;
using Catrobat.Core.Misc.Helpers;

namespace Catrobat.Core.Objects.Bricks
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
            IfLogicElseBrick = ReferenceHelper.GetReferenceObject(this, _reference) as IfLogicElseBrick;
        }

        public DataObject Copy()
        {
            var newIfLogicElseBrickRef = new IfLogicElseBrickReference();
            newIfLogicElseBrickRef.IfLogicElseBrick = _ifLogicElseBrick;

            return newIfLogicElseBrickRef;
        }

        public override bool Equals(DataObject other)
        {
            throw new System.NotImplementedException();
        }
    }
}