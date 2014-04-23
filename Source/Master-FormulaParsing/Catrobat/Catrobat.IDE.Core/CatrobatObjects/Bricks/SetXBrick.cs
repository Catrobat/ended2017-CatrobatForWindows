using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Xml.Linq;
using Catrobat.IDE.Core.CatrobatObjects.Formulas;
using Catrobat.IDE.Core.CatrobatObjects.Variables;
using Catrobat.IDE.Core.FormulaEditor;
using Catrobat.IDE.Core.Utilities.Helpers;

namespace Catrobat.IDE.Core.CatrobatObjects.Bricks
{
    public class SetXBrick : Brick
    {
        protected Formula _xPosition;
        public Formula XPosition
        {
            get { return _xPosition; }
            set
            {
                _xPosition = value;
                RaisePropertyChanged();
            }
        }


        public SetXBrick() {}

        public SetXBrick(XElement xElement) : base(xElement) {}

        internal override void LoadFromXML(XElement xRoot)
        {
            _xPosition = new Formula(xRoot.Element("xPosition"));
        }

        internal override XElement CreateXML()
        {
            var xRoot = new XElement("setXBrick");

            var xVariable = new XElement("xPosition");
            xVariable.Add(_xPosition.CreateXML());
            xRoot.Add(xVariable);

            return xRoot;
        }

        internal override void LoadReference(XmlFormulaTreeConverter converter)
        {
            if (_xPosition != null)
                _xPosition.LoadReference(converter);
        }

        public override DataObject Copy()
        {
            var newBrick = new SetXBrick();
            newBrick._xPosition = _xPosition.Copy() as Formula;

            return newBrick;
        }

        public override bool Equals(DataObject other)
        {
            var otherBrick = other as SetXBrick;

            if (otherBrick == null)
                return false;

            return XPosition.Equals(otherBrick.XPosition);
        }
    }
}