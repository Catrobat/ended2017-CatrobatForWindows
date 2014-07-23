using System.Xml.Linq;
using Catrobat.Data.Xml.XmlObjects.Bricks;

namespace Catrobat.Data.Xml.XmlObjects.Scripts
{
    public abstract partial class XmlScript : XmlObject
    {
        public XmlBrickList Bricks { get; set; }

        protected XmlScript()
        {
            Bricks = new XmlBrickList();
        }

        protected XmlScript(XElement xElement)
        {
            Bricks = new XmlBrickList();

            LoadFromCommonXML(xElement);
            LoadFromXml(xElement);
        }
        
        public abstract override void LoadFromXml(XElement xRoot);

        private void LoadFromCommonXML(XElement xRoot)
        {
            if (xRoot.Element("brickList") != null)
            {
                Bricks = new XmlBrickList(xRoot.Element("brickList"));
            }
        }

        public abstract override XElement CreateXml();

        protected void CreateCommonXML(XElement xRoot)
        {
            if (Bricks != null)
            {
                xRoot.Add(Bricks.CreateXml());
            }
        }
    }
}