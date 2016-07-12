using System.Xml.Linq;
using Catrobat.IDE.Core.Utilities.Helpers;
using Catrobat_Player.NativeComponent;

namespace Catrobat.IDE.Core.Xml.XmlObjects
{
    public partial class XmlLook : XmlObjectNode, ILook
    {
        #region NativeInterface
        public string FileName { get; set; }

        public string Name { get; set; }

        #endregion

        public override bool Equals(System.Object obj)
        {
            XmlLook l = obj as XmlLook;
            if ((object)l == null)
            {
                return false;
            }

            return this.Equals(l);
        }

        public bool Equals(XmlLook l)
        {
            return this.FileName.Equals(l.FileName) && this.Name.Equals(l.Name);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode() ^ FileName.GetHashCode() ^ Name.GetHashCode();
        }

        public XmlLook() { }

        public XmlLook(string name)
        {
            Name = name;
            FileName = FileNameGenerationHelper.Generate() + Name;
        }

        internal XmlLook(XElement xElement)
        {
            LoadFromXml(xElement);
        }

        internal override void LoadFromXml(XElement xRoot)
        {
            FileName = xRoot.Element(XmlConstants.FileName).Value;
            Name = xRoot.Attribute(XmlConstants.Name).Value;
            
        }

        internal override XElement CreateXml()
        {
            var xRoot = new XElement(XmlConstants.Look);

            xRoot.Add(new XElement(XmlConstants.FileName, FileName));

            xRoot.SetAttributeValue(XmlConstants.Name, Name.ToString());

            return xRoot;
        }
    }
}
