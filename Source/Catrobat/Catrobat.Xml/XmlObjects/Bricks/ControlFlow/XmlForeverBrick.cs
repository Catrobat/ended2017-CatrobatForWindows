using Catrobat_Player.NativeComponent;
using System.Xml.Linq;
using System;
using System.Collections.Generic;

namespace Catrobat.IDE.Core.Xml.XmlObjects.Bricks.ControlFlow
{
    public partial class XmlForeverBrick : XmlLoopBeginBrick, IForeverBrick
    {
        #region NativeInterface
        public IList<IBrick> Bricks
        {
            get
            {
                throw new NotImplementedException();
            }
            set { }
        }

        #endregion

        public XmlForeverBrick() {}

        public XmlForeverBrick(XElement xElement) : base(xElement) {}

        #region equals_and_gethashcode
        public override bool Equals(System.Object obj)
        {
            XmlForeverBrick b = obj as XmlForeverBrick;
            if ((object)b == null)
                return false;

            return this.Equals(b);
        }

        public bool Equals(XmlForeverBrick b)
        {
            return this.Equals((XmlBrick)b);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
        #endregion

        internal override void LoadFromXml(XElement xRoot)
        {
            if (xRoot != null)
            {
                base.LoadFromCommonXML(xRoot);
             }
            
        }

        internal override XElement CreateXml()
        {
            var xRoot = new XElement(XmlConstants.Brick);
            xRoot.SetAttributeValue(XmlConstants.Type, XmlConstants.XmlForeverBrickType);
            
            //base.CreateCommonXML(xRoot);

            return xRoot;
        }
    }
}
