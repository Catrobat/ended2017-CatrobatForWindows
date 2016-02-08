using System.Xml.Linq;
using Catrobat.IDE.Core.Xml.XmlObjects.Formulas;
using System;

namespace Catrobat.IDE.Core.Xml.XmlObjects.Bricks
{
    public partial class XmlNoteBrick : XmlBrick
    {
        public string Note { get; set; }

        public XmlFormula FNote { get; set; }

        public XmlNoteBrick() {}

        public XmlNoteBrick(XElement xElement) : base(xElement) {}

        internal override void LoadFromXml(XElement xRoot)
        {
            if (xRoot != null)
            {
                FNote = new XmlFormula(xRoot, XmlConstants.Note);

                Note = FNote.FormulaTree.VariableValue;
            }
        }

        internal override XElement CreateXml()
        {
            var xRoot = new XElement(XmlConstants.Brick);
            xRoot.SetAttributeValue(XmlConstants.Type, XmlConstants.XmlNoteBrickType);

            if((FNote == null) && (Note != null))
            {
                //necessary as there is right now no gui support for formaulas in this brick
                FNote = new XmlFormula
                {
                    FormulaTree = new XmlFormulaTree
                    {
                        VariableType = "STRING",
                        VariableValue = Note
                    }
                };
            }

            if (Note != null)
            {
                var xElement = FNote.CreateXml();
                xElement.SetAttributeValue(XmlConstants.Category, XmlConstants.Note);

                var xFormulalist = new XElement(XmlConstants.FormulaList);
                xFormulalist.Add(xElement);

                xRoot.Add(xFormulalist);
            }
            return xRoot;
        }
    }
}