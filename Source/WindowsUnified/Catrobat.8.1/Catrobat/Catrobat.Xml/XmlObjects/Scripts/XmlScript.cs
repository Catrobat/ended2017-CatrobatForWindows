using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Catrobat.IDE.Core.Utilities.Helpers;
using Catrobat.IDE.Core.Xml.XmlObjects.Bricks;
using Catrobat_Player.NativeComponent;
using System.Linq;

namespace Catrobat.IDE.Core.Xml.XmlObjects.Scripts
{
    public abstract partial class XmlScript : XmlObjectNode, IScript
    {
        public XmlBrickList Bricks { get; set; }

        IList<IBrick> IScript.Bricks
        {
            get { return Bricks.Bricks.Cast<IBrick>().ToList(); }
            set { }
        }

        protected XmlScript()
        {
            Bricks = new XmlBrickList();
        }

        protected XmlScript(XElement xElement)
        {
            Bricks = new XmlBrickList();

            LoadFromCommonXML(xElement);
            LoadFromXml(xElement);
        }
        
        internal abstract override void LoadFromXml(XElement xRoot);

        private void LoadFromCommonXML(XElement xRoot)
        {
            XmlParserTempProjectHelper.Script = this;

            if (xRoot != null && xRoot.Element(XmlConstants.BrickList) != null)
            {
                Bricks = new XmlBrickList(xRoot.Element(XmlConstants.BrickList));
            }
        }

        internal abstract override XElement CreateXml();

        protected void CreateCommonXML(XElement xRoot)
        {
            if (Bricks != null)
            {
                XmlParserTempProjectHelper.Script = this;

                xRoot.Add(Bricks.CreateXml());
            }
        }
    }
}
