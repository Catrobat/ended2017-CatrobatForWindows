using System.Xml.Linq;

namespace Catrobat.IDE.Core.Xml.XmlObjects.Formulas
{
    public partial class XmlFormulaTree : XmlObjectNode
    {
        public XmlFormulaTree LeftChild { get; set; }

        public XmlFormulaTree RightChild { get; set; }

        public string VariableType { get; set; }

        public string VariableValue { get; set; }

        public XmlFormulaTree()
        {
        }

        public XmlFormulaTree(XElement xElement)
        {
            LoadFromXml(xElement);
        }

        internal override void LoadFromXml(XElement xRoot)
        {
            if(xRoot.Element(XmlConstants.LeftChild)) != null)
                LeftChild = new XmlFormulaTree(xRoot.Element(XmlConstants.LeftChild));
            if (xRoot.Element(XmlConstants.RightChild)) != null)
                RightChild = new XmlFormulaTree(xRoot.Element(XmlConstants.RightChild)));

            if (xRoot.Element(XmlConstants.Type) != null)
            VariableType = xRoot.Element(XmlConstants.Type).Value;
            if (xRoot.Element(XmlConstants.Value) != null)
            VariableValue = xRoot.Element(XmlConstants.Value).Value;
        }

        internal override XElement CreateXml()
        {
            var xRoot = new XElement(XmlConstants.Formula);

            if(LeftChild != null)
                xRoot.Add(LeftChild.CreateXML(XmlConstants.LeftChild)));
            if(RightChild != null)
                xRoot.Add(RightChild.CreateXML(XmlConstants.RightChild)));
            if(VariableType != null)
                xRoot.Add(new XElement(XmlConstants.Type), VariableType));
            if(VariableValue != null)
                xRoot.Add(new XElement(XmlConstants.Value), VariableValue));

            return xRoot;
        }

        internal XElement CreateXML(string childName)
        {
            var xRoot = new XElement(childName);

            if (LeftChild != null)
                xRoot.Add(LeftChild.CreateXML(XmlConstants.LeftChild));
            if (RightChild != null)
                xRoot.Add(RightChild.CreateXML(XmlConstants.RightChild));
            if (VariableType != null)
                xRoot.Add(new XElement(XmlConstants.Type, VariableType));
            if (VariableValue != null)
                xRoot.Add(new XElement(XmlConstants.Value, VariableValue));

            return xRoot;
        }
    }
}
