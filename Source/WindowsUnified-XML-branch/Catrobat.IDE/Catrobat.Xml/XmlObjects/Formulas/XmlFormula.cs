using System.Xml.Linq;
using System;
using System.Globalization;
using Catrobat.IDE.Core.Xml.XmlObjects.Variables;
using System.Xml.Linq;
using System;
using System.Collections.Generic;

namespace Catrobat.IDE.Core.Xml.XmlObjects.Formulas
{
    public partial class XmlFormula : XmlObjectNode
    {
        public XmlFormulaTree FormulaTree { get; set; }

        public XmlFormula()
        {
        }

        public XmlFormula(XElement xElement)
        {
            LoadFromXml(xElement);
        }

        public XmlFormula(XElement xRoot, String formulaCategory)
        {
            if (xRoot != null && formulaCategory != String.Empty)
            {
                IEnumerable<XElement> elements = xRoot.Element(XmlConstants.FormulaList).Elements();
                foreach (XElement xElement in elements)
                {
                    //TODO: überprüfung wenn nur 1 formula element ist, keine schleife!!!
                    if (xElement.Attribute(XmlConstants.Category).Value == formulaCategory)
                    {
                        LoadFromXml(xElement);
                    }
                }
            }
        }

        internal override void LoadFromXml(XElement xRoot)
        {
            //TODO: notwendig? oder doch nur  if (xRoot.Element(XmlConstants.Formula) != null)
            
            //if (xRoot.Element(XmlConstants.Formula) != null)
            if (xRoot != null)
            {
                if (xRoot.Name.LocalName == XmlConstants.Formula)
                {
                    FormulaTree = new XmlFormulaTree(xRoot);//.Element(XmlConstants.Formula));
                }
            }
            
        }

        internal override XElement CreateXml()
        {
            return (FormulaTree ?? new XmlFormulaTree()).CreateXml();
        }
    }
}
