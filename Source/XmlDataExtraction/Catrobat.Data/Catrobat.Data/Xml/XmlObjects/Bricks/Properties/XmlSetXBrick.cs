﻿using System.Xml.Linq;
using Catrobat.Data.Xml.XmlObjects.Formulas;

namespace Catrobat.Data.Xml.XmlObjects.Bricks.Properties
{
    public partial class XmlSetXBrick : XmlBrick
    {
        public XmlFormula XPosition { get; set; }

        public XmlSetXBrick() {}

        public XmlSetXBrick(XElement xElement) : base(xElement) {}

        public override void LoadFromXml(XElement xRoot)
        {
            XPosition = new XmlFormula(xRoot.Element("xPosition"));
        }

        public override XElement CreateXml()
        {
            var xRoot = new XElement("setXBrick");

            var xVariable = new XElement("xPosition");
            xVariable.Add(XPosition.CreateXml());
            xRoot.Add(xVariable);

            return xRoot;
        }

        public override void LoadReference()
        {
            if (XPosition != null)
                XPosition.LoadReference();
        }
    }
}