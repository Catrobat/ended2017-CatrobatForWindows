using System;
using System.Xml.Linq;

namespace Catrobat.Core.Objects.Formulas
{
    public class RightChild : FormulaTree
    {
        public RightChild()
        {
            _childName = "rightChild";
        }

        public RightChild(XElement xElement)
        {
            _childName = "rightChild";
            base.LoadFromXML(xElement);
        }
    }
}
