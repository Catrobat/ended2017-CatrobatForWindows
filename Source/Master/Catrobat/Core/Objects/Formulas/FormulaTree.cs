using System;
using System.Xml.Linq;

namespace Catrobat.Core.Objects.Formulas
{
    public class FormulaTree : DataObject
    {
        private FormulaTree _leftChild;
        public FormulaTree LeftChild
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

        private FormulaTree _rightChild;
        public FormulaTree RightChild
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


        public FormulaTree()
        {
        }

        public FormulaTree(XElement xElement)
        {
            LoadFromXML(xElement);
        }

        internal override void LoadFromXML(XElement xRoot)
        {
            if(xRoot.Element("leftChild") != null)
                _leftChild = new FormulaTree(xRoot.Element("leftChild"));
            if (xRoot.Element("rightChild") != null)
                _rightChild = new FormulaTree(xRoot.Element("rightChild"));

            if (xRoot.Element("type") != null)
            _variableType = xRoot.Element("type").Value;
            if (xRoot.Element("value") != null)
            _variableValue = xRoot.Element("value").Value;
        }

        internal override XElement CreateXML()
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

        public DataObject Copy()
        {
            var newFormulaTree = new FormulaTree();

            if (_leftChild != null)
                newFormulaTree.LeftChild = _leftChild.Copy() as FormulaTree;
            if (_rightChild != null)
                newFormulaTree.RightChild = _rightChild.Copy() as FormulaTree;
            newFormulaTree.VariableType = _variableType;
            newFormulaTree.VariableValue = _variableValue;

            return newFormulaTree;
        }

        public override bool Equals(DataObject other)
        {
            var otherFormulaTree = other as FormulaTree;

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
