using Catrobat.IDE.Core.Xml.XmlObjects.Formulas;
using System.Xml.Linq;

namespace Catrobat.IDE.Core.Xml.XmlObjects.Bricks.Sounds
{
    public partial class XmlSpeakBrick : XmlBrick
    {
        public string Text { get; set; } //TODO: necessary as there is right now no gui support for formaulas in this brick

        public XmlFormula FText { get; set; }

        public XmlSpeakBrick() {}

        public XmlSpeakBrick(XElement xElement) : base(xElement) {}

        #region equals_and_gethashcode
        public override bool Equals(System.Object obj)
        {
            XmlSpeakBrick b = obj as XmlSpeakBrick;
            if ((object)b == null)
                return false;

            return this.Equals(b) && this.FText.Equals(b.FText);
        }

        public bool Equals(XmlSpeakBrick b)
        {
            return this.Equals((XmlBrick)b) && this.FText.Equals(b.FText);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode() ^ FText.GetHashCode();
        }
        #endregion

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

            //TODO: only for tests needed as long as upper "necessary as there is right now no gui support for formaulas in this brick" isn't fixed
            if ((FText != null) && (Text == null))
                Text = FText.FormulaTree.VariableValue;

            if ((FText != null) && (Text != null))
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
