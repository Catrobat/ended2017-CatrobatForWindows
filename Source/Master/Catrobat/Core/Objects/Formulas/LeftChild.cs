using System;
using System.Xml.Linq;

namespace Catrobat.Core.Objects.Formulas
{
    public class LeftChild : FormulaTree
    {
        public LeftChild()
        {
            _childName = "leftChild";
        }

        public LeftChild(XElement xElement)
        {
            _childName = "leftChild";
            base.LoadFromXML(xElement);
        }
    }
}
