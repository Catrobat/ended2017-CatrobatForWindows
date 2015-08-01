using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Catrobat.IDE.Core.Xml.XmlObjects.Formulas
{
    class XmlFormulaListFactory

    {
        public static XmlFormula getFormula(XElement xRoot, String formulaType)
        {
            if (xRoot != null && formulaType != "")
            {
                IEnumerable<XElement> elements = xRoot.Element(XmlConstants.FormulaList).Elements();
                foreach (XElement xElement in elements)
                {
                    //toDO: überprüfung wenn nur 1 formula element ist, keine schleife!!!
                    if (xElement.Attribute(XmlConstants.Category).Value == formulaType)
                    {
                        return new XmlFormula(xElement);
                    }
                }
            }
            return null;
        }


    }
}
