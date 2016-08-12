using Catrobat_Player.NativeComponent;
using System.Xml.Linq;

namespace Catrobat.IDE.Core.Xml.XmlObjects.Bricks.ControlFlow
{
    public partial class XmlIfLogicEndBrick : XmlBrick, IIfEndBrick
    {
        internal XmlIfLogicBeginBrickReference IfLogicBeginBrickReference { get; set; }

        public XmlIfLogicBeginBrick IfLogicBeginBrick
        {
            get
            {
                if (IfLogicBeginBrickReference == null)
                    return null;

                return IfLogicBeginBrickReference.IfLogicBeginBrick;
            }
            set
            {
                if (IfLogicBeginBrickReference == null)
                    IfLogicBeginBrickReference = new XmlIfLogicBeginBrickReference();

                if (IfLogicBeginBrickReference.IfLogicBeginBrick == value)
                    return;

                IfLogicBeginBrickReference.IfLogicBeginBrick = value;

                if (value == null)
                    IfLogicBeginBrickReference = null;
            }
        }

        internal XmlIfLogicElseBrickReference IfLogicElseBrickReference { get; set; }

        public XmlIfLogicElseBrick IfLogicElseBrick
        {
            get
            {
                if (IfLogicElseBrickReference == null)
                    return null;

                return IfLogicElseBrickReference.IfLogicElseBrick;
            }
            set
            {
                if (IfLogicElseBrickReference == null)
                    IfLogicElseBrickReference = new XmlIfLogicElseBrickReference();

                if (IfLogicElseBrickReference.IfLogicElseBrick == value)
                    return;

                IfLogicElseBrickReference.IfLogicElseBrick = value;

                if (value == null)
                    IfLogicElseBrickReference = null;
            }
        }

        public XmlIfLogicEndBrick() {}

        public XmlIfLogicEndBrick(XElement xElement) : base(xElement) { }

        #region equals_and_gethashcode
        public override bool Equals(System.Object obj)
        {
            XmlIfLogicEndBrick b = obj as XmlIfLogicEndBrick;
            if ((object)b == null)
                return false;

            return this.Equals(b);
        }

        public bool Equals(XmlIfLogicEndBrick b)
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
         /* welcome to V93
            if (xRoot.Element("ifBeginBrick") != null)
            {
                IfLogicBeginBrickReference = new XmlIfLogicBeginBrickReference(xRoot.Element("ifBeginBrick"));
            }
            if (xRoot.Element("ifElseBrick") != null)
            {
                IfLogicElseBrickReference = new XmlIfLogicElseBrickReference(xRoot.Element("ifElseBrick"));
            }
          */
        }

        internal override XElement CreateXml()
        {
            var xRoot = new XElement(XmlConstants.Brick);
            xRoot.SetAttributeValue(XmlConstants.Type, XmlConstants.XmlIfLogicEndBrick);

            /* welcome to v93
                xRoot.Add(IfLogicBeginBrickReference.CreateXml());

                xRoot.Add(IfLogicElseBrickReference.CreateXml());
             */

            return xRoot;
        }

        public override void LoadReference()
        {
            if (IfLogicBeginBrickReference != null)
                IfLogicBeginBrickReference.LoadReference();
            if(IfLogicElseBrickReference != null)
                IfLogicElseBrickReference.LoadReference();
        }
    }
}
