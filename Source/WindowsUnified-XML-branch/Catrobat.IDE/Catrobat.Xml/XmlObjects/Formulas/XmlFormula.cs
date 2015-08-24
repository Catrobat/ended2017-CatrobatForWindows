using System.Xml.Linq;
using System;
using System.Globalization;
using Catrobat.IDE.Core.Xml.XmlObjects.Variables;
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
            //TODO: - old only "used" by NXT bricks
            LoadFromXml(xElement);
        }

        public XmlFormula(XElement xRoot, String formulaCategory)
        {
            if (xRoot != null && formulaCategory != String.Empty)
            {
                IEnumerable<XElement> elements = xRoot.Element(XmlConstants.FormulaList).Elements();

                XElement buffer = null;

                foreach (XElement xElement in elements)
                {
                    if (buffer != null) //to enshure, that only the first element is taken - which should be the only one
                    {
                        buffer = null;
                        break;
                    }

                    else if (xElement.Attribute(XmlConstants.Category).Value == formulaCategory)
                    {
                        buffer = xElement;
                    }
                }

                LoadFromXml(buffer); //to enshure, that only the first element is taken - which should be the only one
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
