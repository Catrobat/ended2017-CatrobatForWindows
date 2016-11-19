using System.Xml.Linq;
using Catrobat.IDE.Core.Xml.XmlObjects.Formulas;
using Catrobat_Player.NativeComponent;

namespace Catrobat.IDE.Core.Xml.XmlObjects.Bricks.Properties
{
    public partial class XmlGoNStepsBackBrick : XmlBrick, IGoNStepsBackBrick
    {
        #region NativeInterface
        public IFormulaTree Steps
        {
            get
            {
                return StepsXML == null ? null: StepsXML.FormulaTree;
            }
            set { }
        }
        #endregion
        public XmlFormula StepsXML { get; set; }

        public override bool Equals(System.Object obj)
        {
            XmlGoNStepsBackBrick b = obj as XmlGoNStepsBackBrick;
            if ((object)b == null)
                return false;

            return this.Equals(b) && this.StepsXML.Equals(b.StepsXML);
        }

        public bool Equals(XmlGoNStepsBackBrick b)
        {
            return this.Equals((XmlBrick)b) && this.StepsXML.Equals(b.StepsXML);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode() ^ StepsXML.GetHashCode();
        }

        public XmlGoNStepsBackBrick()
        {
        }

        public XmlGoNStepsBackBrick(XElement xElement) : base(xElement)
        {
        }

        internal override void LoadFromXml(XElement xRoot)
        {
            if (xRoot != null)
            {
                StepsXML = new XmlFormula(xRoot, XmlConstants.Steps);
            }
        }

        internal override XElement CreateXml()
        {
            var xRoot = new XElement(XmlConstants.Brick);
            xRoot.SetAttributeValue(XmlConstants.Type, XmlConstants.XmlGoNStepsBackBrickType);
            
            var xElement = StepsXML.CreateXml();
            xElement.SetAttributeValue(XmlConstants.Category, XmlConstants.Steps);

            var xFormulalist = new XElement(XmlConstants.FormulaList);
            xFormulalist.Add(xElement);

            xRoot.Add(xFormulalist);

            return xRoot;
        }

        public override void LoadReference()
        {
            if (StepsXML != null)
                StepsXML.LoadReference();
        }
    }
}
