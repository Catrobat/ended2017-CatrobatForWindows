using System;
using System.Xml.Linq;
using Catrobat.IDE.Core.Xml.XmlObjects.Formulas;
using Catrobat_Player.NativeComponent;

namespace Catrobat.IDE.Core.Xml.XmlObjects.Bricks.ControlFlow
{
    public partial class XmlWaitBrick : XmlBrick, IWaitBrick
    {
        #region NativeInterface
        IFormulaTree IWaitBrick.TimeToWaitInSeconds
        {
            get
            {
                return TimeToWaitInSeconds == null ? null : TimeToWaitInSeconds.FormulaTree;
            }
            set { }
        }

        #endregion

        public XmlFormula TimeToWaitInSeconds { get; set; }

        public XmlWaitBrick() { }

        public XmlWaitBrick(XElement xElement) : base(xElement) { }

        #region equals_and_gethashcode
        public override bool Equals(System.Object obj)
        {
            XmlWaitBrick b = obj as XmlWaitBrick;
            if ((object)b == null)
                return false;

            return this.Equals(b) && this.TimeToWaitInSeconds.Equals(b.TimeToWaitInSeconds);
        }

        public bool Equals(XmlWaitBrick b)
        {
            return this.Equals((XmlBrick)b) && this.TimeToWaitInSeconds.Equals(b.TimeToWaitInSeconds);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode() ^ TimeToWaitInSeconds.GetHashCode();
        }
        #endregion


        internal override void LoadFromXml(XElement xRoot)
        {
            if (xRoot != null)
            {
                TimeToWaitInSeconds = new XmlFormula(xRoot, XmlConstants.TimeToWaitInSeconds);
            }
        }

        internal override XElement CreateXml()
        {
            var xRoot = new XElement(XmlConstants.Brick);
            xRoot.SetAttributeValue(XmlConstants.Type, XmlConstants.XmlWaitBrickType);

            var xElement = TimeToWaitInSeconds.CreateXml();
            xElement.SetAttributeValue(XmlConstants.Category, XmlConstants.TimeToWaitInSeconds);

            var xFormulalist = new XElement(XmlConstants.FormulaList);
            xFormulalist.Add(xElement);

            xRoot.Add(xFormulalist);

            return xRoot;
        }

        public override void LoadReference()
        {
            if (TimeToWaitInSeconds != null)
                TimeToWaitInSeconds.LoadReference();
        }
    }
}
