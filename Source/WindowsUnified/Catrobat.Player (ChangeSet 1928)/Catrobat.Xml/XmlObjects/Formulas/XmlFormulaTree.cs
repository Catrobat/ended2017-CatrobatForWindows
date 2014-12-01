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
            if(xRoot.Element("leftChild") != null)
                LeftChild = new XmlFormulaTree(xRoot.Element("leftChild"));
            if (xRoot.Element("rightChild") != null)
                RightChild = new XmlFormulaTree(xRoot.Element("rightChild"));

            if (xRoot.Element("type") != null)
            VariableType = xRoot.Element("type").Value;
            if (xRoot.Element("value") != null)
            VariableValue = xRoot.Element("value").Value;
        }

        internal override XElement CreateXml()
        {
            var xRoot = new XElement("formulaTree");

            if(LeftChild != null)
                xRoot.Add(LeftChild.CreateXML("leftChild"));
            if(RightChild != null)
                xRoot.Add(RightChild.CreateXML("rightChild"));
            if(VariableType != null)
                xRoot.Add(new XElement("type", VariableType));
            if(VariableValue != null)
                xRoot.Add(new XElement("value", VariableValue));

            return xRoot;
        }

        internal XElement CreateXML(string childName)
        {
            var xRoot = new XElement(childName);

            if (LeftChild != null)
                xRoot.Add(LeftChild.CreateXML("leftChild"));
            if (RightChild != null)
                xRoot.Add(RightChild.CreateXML("rightChild"));
            if (VariableType != null)
                xRoot.Add(new XElement("type", VariableType));
            if (VariableValue != null)
                xRoot.Add(new XElement("value", VariableValue));

            return xRoot;
        }
    }
}
