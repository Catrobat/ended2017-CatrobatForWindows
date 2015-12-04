using Catrobat.IDE.Core.Xml.XmlObjects.Formulas;
using System.Xml.Linq;

namespace Catrobat.IDE.Core.Xml.XmlObjects.Bricks.Sounds
{
    public partial class XmlSpeakBrick : XmlBrick
    {
        public string Text { get; set; }

        public XmlFormula FText { get; set; }

        public XmlSpeakBrick() {}

        public XmlSpeakBrick(XElement xElement) : base(xElement) {}

        internal override void LoadFromXml(XElement xRoot)
        {
            if (xRoot != null)
            {
                FText = new XmlFormula(xRoot, XmlConstants.Speak);

                Text = FText.FormulaTree.VariableValue;
            }
        }

        internal override XElement CreateXml()
        {
            var xRoot = new XElement(XmlConstants.Brick);
            xRoot.SetAttributeValue(XmlConstants.Type, XmlConstants.XmlSpeakBrickType);

            if ((FText == null) && (Text != null))
            {
                //necessary as there is right now no gui support for formaulas in this brick
                FText = new XmlFormula
                {
                    FormulaTree = new XmlFormulaTree
                    {
                        VariableType = "STRING",
                        VariableValue = Text
                    }
                };
            }

            if (Text != null)
            {
                var xElement = FText.CreateXml();
                xElement.SetAttributeValue(XmlConstants.Category, XmlConstants.Speak);

                var xFormulalist = new XElement(XmlConstants.FormulaList);
                xFormulalist.Add(xElement);

                xRoot.Add(xFormulalist);
            }

            return xRoot;
        }
    }
}