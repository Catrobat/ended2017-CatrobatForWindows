using Catrobat_Player.NativeComponent;
using System.Xml.Linq;
using System;

namespace Catrobat.IDE.Core.Xml.XmlObjects.Bricks.Sounds
{
    public partial class XmlPlaySoundBrick : XmlBrick, IPlaySoundBrick
    {
        #region NativeInterface
        public string Name
        {
            get
            {
                return (XmlSoundReference == null || XmlSoundReference.Sound == null) ? null : XmlSoundReference.Sound.Name;
            }
            set { }
        }

        public string FileName
        {
            get
            {
                return (XmlSoundReference == null || XmlSoundReference.Sound == null) ? null : XmlSoundReference.Sound.FileName;
            }
            set { }
        }

        #endregion

        internal XmlSoundReference XmlSoundReference { get; set; }

        public XmlSound Sound
        {
            get
            {
                if (XmlSoundReference == null)
                {
                    return null;
                }

                return XmlSoundReference.Sound;
            }
            set
            {
                if (XmlSoundReference == null)
                    XmlSoundReference = new XmlSoundReference();

                if (XmlSoundReference.Sound == value)
                    return;

                XmlSoundReference.Sound = value;

                if (value == null)
                    XmlSoundReference = null;
            }
        }

        public XmlPlaySoundBrick() { }

        public XmlPlaySoundBrick(XElement xElement) : base(xElement) { }

        internal override void LoadFromXml(XElement xRoot)
        {
            if (xRoot != null && xRoot.Element(XmlConstants.Sound) != null)
            {
                XmlSoundReference = new XmlSoundReference(xRoot.Element(XmlConstants.Sound));
            }
        }

        internal override XElement CreateXml()
        {
            var xRoot = new XElement(XmlConstants.Brick);
            xRoot.SetAttributeValue(XmlConstants.Type, XmlConstants.XmlPlaySoundBrickType);

            if (XmlSoundReference != null)
            {
                xRoot.Add(XmlSoundReference.CreateXml());
            }

            ////CreateCommonXML(xRoot);

            return xRoot;
        }

        public override void LoadReference()
        {
            if (XmlSoundReference != null)
                XmlSoundReference.LoadReference();
        }
    }
}
