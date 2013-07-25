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
            IfLogicBeginBrick = ReferenceHelper.GetReferenceObject(this, _reference) as IfLogicBeginBrick;
        }

        public DataObject Copy()
        {
            var newIfLogicBeginBrickRef = new IfLogicBeginBrickReference();
            newIfLogicBeginBrickRef.IfLogicBeginBrick = _ifLogicBeginBrick;

            return newIfLogicBeginBrickRef;
        }        
    }
}