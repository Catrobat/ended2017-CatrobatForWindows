using System.Xml.Linq;

namespace Catrobat.IDE.Core.Xml.XmlObjects.Bricks.Looks
{
    public partial class XmlSetLookBrick : XmlBrick
    {
        internal XmlLookReference XmlLookReference { get; set; }

        public XmlLook Look
        {
            get
            {
                if (XmlLookReference == null)
                {
                    return null;
                }

                return XmlLookReference.Look;
            }
            set
            {
                if (XmlLookReference == null)
                    XmlLookReference = new XmlLookReference();

                if (XmlLookReference.Look == value)
                    return;

                XmlLookReference.Look = value;

                if (value == null)
                    XmlLookReference = null;
            }
        }

        public XmlSetLookBrick() { }

        public XmlSetLookBrick(XElement xElement) : base(xElement) { }

        internal override void LoadFromXml(XElement xRoot)
        {
            //if (xRoot.Element("look") != null)
            if (xRoot.Element(XmlConstants.Look) != null)
            {
                //XmlLookReference = new XmlLookReference(xRoot.Element("look"));
                XmlLookReference = new XmlLookReference(xRoot.Element(XmlConstants.Look));
            }
        }

        internal override XElement CreateXml()
        {
            //var xRoot = new XElement("setLookBrick");
            //var xRoot = new XElement("brick");
            //xRoot.SetAttributeValue("type", "setLookBrick");
            var xRoot = new XElement(XmlConstants.Brick);
            xRoot.SetAttributeValue(XmlConstants.Type, XmlConstants.XmlSetLookBrickType);

            if (XmlLookReference != null)
            {
                xRoot.Add(XmlLookReference.CreateXml());
            }

            return xRoot;
        }

        internal override void LoadReference()
        {
            if(XmlLookReference != null)
            XmlLookReference.LoadReference();
        }
    }
}