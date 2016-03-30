using System;
using System.Xml.Linq;
using Catrobat.IDE.Core.Xml.XmlObjects.Formulas;
using Catrobat_Player.NativeComponent;

namespace Catrobat.IDE.Core.Xml.XmlObjects.Bricks.Properties
{
    public partial class XmlMoveNStepsBrick : XmlBrick, IMoveNStepsBrick
    {
        #region NativeInterface
        IFormulaTree IMoveNStepsBrick.Steps
        {
            get
            {
                return Steps == null ? null : Steps.FormulaTree;
            }
            set { }
        }

        #endregion

        public XmlFormula Steps { get; set; }

        public override bool Equals(System.Object obj)
        {
            XmlMoveNStepsBrick b = obj as XmlMoveNStepsBrick;
            if ((object)b == null)
                return false;

            return this.Equals(b) && this.Steps.Equals(b.Steps);
        }

        public bool Equals(XmlMoveNStepsBrick b)
        {
            return this.Equals((XmlBrick)b) && this.Steps.Equals(b.Steps);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode() ^ Steps.GetHashCode();
        }

        public XmlMoveNStepsBrick() {}

        public XmlMoveNStepsBrick(XElement xElement) : base(xElement) {}

        internal override void LoadFromXml(XElement xRoot)
        {
            if (xRoot != null)
            {
                Steps = new XmlFormula(xRoot, XmlConstants.Steps);
            }
        }

        internal override XElement CreateXml()
        {
            var xRoot = new XElement(XmlConstants.Brick);
            xRoot.SetAttributeValue(XmlConstants.Type, XmlConstants.XmlMoveNStepsBrickType);

            var xElement = Steps.CreateXml();
            xElement.SetAttributeValue(XmlConstants.Category, XmlConstants.Steps);

            var xFormulalist = new XElement(XmlConstants.FormulaList);
            xFormulalist.Add(xElement);

            xRoot.Add(xFormulalist);

            return xRoot;
        }

        public override void LoadReference()
        {
            if (Steps != null)
                Steps.LoadReference();
        }
    }
}
