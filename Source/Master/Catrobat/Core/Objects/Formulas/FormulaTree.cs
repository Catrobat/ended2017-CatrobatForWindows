using System;
using System.Xml.Linq;

namespace Catrobat.Core.Objects.Formulas
{
    public class FormulaTree : DataObject
    {
        internal string _childName;

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
            _childName = "formulaTree";
        }

        public FormulaTree(XElement xElement)
        {
            _childName = "formulaTree";
            LoadFromXML(xElement);
        }

        internal override void LoadFromXML(XElement xRoot)
        {
            if(xRoot.Element("leftChild") != null)
                _leftChild = new LeftChild(xRoot.Element("leftChild"));
            if (xRoot.Element("rightChild") != null)
            _rightChild = new RightChild(xRoot.Element("rightChild"));

            if (xRoot.Element("type") != null)
            _variableType = xRoot.Element("type").Value;
            if (xRoot.Element("value") != null)
            _variableValue = xRoot.Element("value").Value;
        }

        internal override XElement CreateXML()
        {
            var xRoot = new XElement(_childName);

            if(_leftChild != null)
                xRoot.Add(_leftChild.CreateXML());
            if(_rightChild != null)
                xRoot.Add(_rightChild.CreateXML());
            if(_variableType != null)
                xRoot.Add(new XElement("type", _variableType));
            if(_variableValue != null)
                xRoot.Add(new XElement("value", _variableValue));

            return xRoot;
        }

        public DataObject Copy()
        {
            var newFormulaTree = new FormulaTree();

            if (_leftChild != null)
                newFormulaTree.LeftChild = _leftChild.Copy() as LeftChild;
            if (_rightChild != null)
                newFormulaTree.RightChild = _rightChild.Copy() as RightChild;
            newFormulaTree.VariableType = _variableType;
            newFormulaTree.VariableValue = _variableValue;

            return newFormulaTree;
        }
    }
}
