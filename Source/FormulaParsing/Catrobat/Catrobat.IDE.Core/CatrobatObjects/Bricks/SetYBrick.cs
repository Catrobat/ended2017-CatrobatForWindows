using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Xml.Linq;
using Catrobat.IDE.Core.CatrobatObjects.Formulas;
using Catrobat.IDE.Core.CatrobatObjects.Formulas.FormulaTree;
using Catrobat.IDE.Core.CatrobatObjects.Variables;
using Catrobat.IDE.Core.FormulaEditor;
using Catrobat.IDE.Core.Utilities.Helpers;

namespace Catrobat.IDE.Core.CatrobatObjects.Bricks
{
    public class SetYBrick : Brick
    {
        protected Formula _yPosition;
        public Formula YPosition
        {
            get { return _yPosition; }
            set
            {
                _yPosition = value;
                RaisePropertyChanged();
            }
        }


        public SetYBrick() {}

        public SetYBrick(XElement xElement) : base(xElement) {}

        internal override void LoadFromXML(XElement xRoot)
        {
            _yPosition = new Formula(xRoot.Element("yPosition"));
        }

        internal override XElement CreateXML()
        {
            var xRoot = new XElement("setYBrick");

            var xVariable = new XElement("yPosition");
            xVariable.Add(_yPosition.CreateXML());
            xRoot.Add(xVariable);

            return xRoot;
        }

        internal override void LoadReference(XmlFormulaTreeConverter converter)
        {
            if (_yPosition != null)
                _yPosition.LoadReference(converter);
        }

        public override DataObject Copy()
        {
            var newBrick = new SetYBrick();
            newBrick._yPosition = _yPosition.Copy() as Formula;

            return newBrick;
        }

        public override bool Equals(DataObject other)
        {
            var otherBrick = other as SetYBrick;

            if (otherBrick == null)
                return false;

            return YPosition.Equals(otherBrick.YPosition);
        }
    }
}