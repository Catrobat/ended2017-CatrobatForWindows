using System.Xml.Linq;

namespace Catrobat.IDE.Core.Xml.XmlObjects.Formulas
{
    public partial class XmlFormulaTree : XmlObject
    {
        private XmlFormulaTree _leftChild;
        public XmlFormulaTree LeftChild
        {
            get { return _leftChild; }
            set
            {
                if (_leftChild == value)
                {
                    return;
                }

                _leftChild = value;
                RaisePropertyChanged();
            }
        }

        private XmlFormulaTree _rightChild;
        public XmlFormulaTree RightChild
        {
            get { return _rightChild; }
            set
            {
                if (_rightChild == value)
                {
                    return;
                }

                _rightChild = value;
                RaisePropertyChanged();
            }
        }

        private string _variableType;
        public string VariableType
        {
            get { return _variableType; }
            set
            {
                if (_variableType == value)
                {
                    return;
                }

                _variableType = value;
                RaisePropertyChanged();
            }
        }

        private string _variableValue;
        public string VariableValue
        {
            get { return _variableValue; }
            set
            {
                if (_variableValue == value)
                {
                    return;
                }

                _variableValue = value;
                RaisePropertyChanged();
            }
        }


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
                _leftChild = new XmlFormulaTree(xRoot.Element("leftChild"));
            if (xRoot.Element("rightChild") != null)
                _rightChild = new XmlFormulaTree(xRoot.Element("rightChild"));

            if (xRoot.Element("type") != null)
            _variableType = xRoot.Element("type").Value;
            if (xRoot.Element("value") != null)
            _variableValue = xRoot.Element("value").Value;
        }

        internal override XElement CreateXml()
        {
            var xRoot = new XElement("formulaTree");

            if(_leftChild != null)
                xRoot.Add(_leftChild.CreateXML("leftChild"));
            if(_rightChild != null)
                xRoot.Add(_rightChild.CreateXML("rightChild"));
            if(_variableType != null)
                xRoot.Add(new XElement("type", _variableType));
            if(_variableValue != null)
                xRoot.Add(new XElement("value", _variableValue));

            return xRoot;
        }

        internal XElement CreateXML(string childName)
        {
            var xRoot = new XElement(childName);

            if (_leftChild != null)
                xRoot.Add(_leftChild.CreateXML("leftChild"));
            if (_rightChild != null)
                xRoot.Add(_rightChild.CreateXML("rightChild"));
            if (_variableType != null)
                xRoot.Add(new XElement("type", _variableType));
            if (_variableValue != null)
                xRoot.Add(new XElement("value", _variableValue));

            return xRoot;
        }

        public XmlObject Copy()
        {
            var newFormulaTree = new XmlFormulaTree();

            if (_leftChild != null)
                newFormulaTree.LeftChild = _leftChild.Copy() as XmlFormulaTree;
            if (_rightChild != null)
                newFormulaTree.RightChild = _rightChild.Copy() as XmlFormulaTree;
            newFormulaTree.VariableType = _variableType;
            newFormulaTree.VariableValue = _variableValue;

            return newFormulaTree;
        }

        public override bool Equals(XmlObject other)
        {
            var otherFormulaTree = other as XmlFormulaTree;

            if (otherFormulaTree == null)
                return false;

            if (LeftChild != null && otherFormulaTree.LeftChild != null)
            {
                if (!LeftChild.Equals(otherFormulaTree.LeftChild))
                    return false;
            }
            else if(!(LeftChild == null && otherFormulaTree.LeftChild == null))
                return false;

            if (RightChild != null && otherFormulaTree.RightChild != null)
            {
                if (!RightChild.Equals(otherFormulaTree.RightChild))
                    return false;
            }
            else if(!(RightChild == null && otherFormulaTree.RightChild == null))
                return false;

            if (VariableType != null && otherFormulaTree.VariableType != null)
            {
                if (VariableType != otherFormulaTree.VariableType)
                    return false;
            }
            else if(!(VariableType == null && otherFormulaTree.VariableType == null))
                return false;

            if (VariableValue != null && otherFormulaTree.VariableValue != null)
            {
                if (VariableValue != otherFormulaTree.VariableValue)
                    return false;
            }
            else if(!(VariableValue == null && otherFormulaTree.VariableValue == null))
                return false;

            return true;
        }
    }
}
