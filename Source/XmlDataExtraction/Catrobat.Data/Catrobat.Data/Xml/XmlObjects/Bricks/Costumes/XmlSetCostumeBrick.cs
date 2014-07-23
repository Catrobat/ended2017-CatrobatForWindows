using System.Xml.Linq;

namespace Catrobat.Data.Xml.XmlObjects.Bricks.Costumes
{
    public partial class XmlSetCostumeBrick : XmlBrick
    {
        public XmlCostumeReference XmlCostumeReference { get; set; }

        public XmlCostume Costume
        {
            get
            {
                if (XmlCostumeReference == null)
                {
                    return null;
                }

                return XmlCostumeReference.Costume;
            }
            set
            {
                if (XmlCostumeReference == null)
                    XmlCostumeReference = new XmlCostumeReference();

                if (XmlCostumeReference.Costume == value)
                    return;

                XmlCostumeReference.Costume = value;

                if (value == null)
                    XmlCostumeReference = null;
            }
        }

        public XmlSetCostumeBrick() { }

        public XmlSetCostumeBrick(XElement xElement) : base(xElement) { }

        public override void LoadFromXml(XElement xRoot)
        {
            if (xRoot.Element("look") != null)
            {
                XmlCostumeReference = new XmlCostumeReference(xRoot.Element("look"));
            }
        }

        public override XElement CreateXml()
        {
            var xRoot = new XElement("setLookBrick");

            if (XmlCostumeReference != null)
            {
                xRoot.Add(XmlCostumeReference.CreateXml());
            }

            return xRoot;
        }

        public override void LoadReference()
        {
            if(XmlCostumeReference != null)
            XmlCostumeReference.LoadReference();
        }
    }
}