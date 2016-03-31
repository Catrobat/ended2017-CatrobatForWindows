using Catrobat_Player.NativeComponent;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Xml.Linq;
using System;

namespace Catrobat.IDE.Core.Xml.XmlObjects
{
    public class XmlLookList : XmlObjectNode
    {
        public List<XmlLook> Looks { get; set; }

        public override bool Equals(System.Object obj)
        {
            XmlLookList l = obj as XmlLookList;
            if ((object)l == null)
            {
                return false;
            }

            return this.Equals(l);
        }

        public bool Equals(XmlLookList l)
        {
            return this.Looks.Equals(l.Looks);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode() ^ Looks.GetHashCode();
        }

        public XmlLookList()
        {
            Looks = new List<XmlLook>();
        }

        public XmlLookList(XElement xElement)
        {
            LoadFromXml(xElement);
        } 

        internal override void LoadFromXml(XElement xRoot)
        {
            Looks = new List<XmlLook>();
           foreach (var element in xRoot.Elements(XmlConstants.Look))
            {
                Looks.Add(new XmlLook(element));
            }
        }

        internal override XElement CreateXml()
        {
            var xRoot = new XElement(XmlConstants.XmlLookListType);

            foreach (XmlLook look in Looks)
            {
                xRoot.Add(look.CreateXml());
            }

            return xRoot;
        }
    }
}
