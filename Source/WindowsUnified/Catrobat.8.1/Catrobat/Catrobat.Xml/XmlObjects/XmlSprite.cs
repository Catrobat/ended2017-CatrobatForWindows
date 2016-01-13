using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using Catrobat.IDE.Core.Utilities.Helpers;
using Catrobat.IDE.Core.Xml.XmlObjects.Scripts;
using Catrobat_Player.NativeComponent;

namespace Catrobat.IDE.Core.Xml.XmlObjects
{
    public partial class XmlSprite : XmlObjectNode, IObject
    {
        #region NativeInterface
        public IList<IUserVariable> UserVariables
        {
            get { return UserVariables.Cast<IUserVariable>().ToList(); }
            set { }
        }

        IList<IScript> IObject.Scripts
        {
            get { return Scripts.Scripts.Cast<IScript>().ToList(); }
            set { }
        }

        IList<ILook> IObject.Looks
        {
            get { return Looks.Looks.Cast<ILook>().ToList(); }
            set { }
        }
        #endregion

        public XmlLookList Looks { get; set; }

        public string Name { get; set; }

        public XmlScriptList Scripts { get; set; }

        public XmlSoundList Sounds { get; set; }

        public XmlSprite()
        {
            Scripts = new XmlScriptList();
            Looks = new XmlLookList();
            Sounds = new XmlSoundList();
        }

        public XmlSprite(XElement xElement)
        {
            LoadFromXml(xElement);
        }

        internal override void LoadFromXml(XElement xRoot)
        {
            XmlParserTempProjectHelper.Sprite = this;

            if (xRoot.Element("lookList") != null)
            {
                Looks = new XmlLookList(xRoot.Element("lookList"));
            }

            Name = xRoot.Element("name").Value;

            if (xRoot.Element("soundList") != null)
            {
                Sounds = new XmlSoundList(xRoot.Element("soundList"));
            }
            if (xRoot.Element("scriptList") != null)
            {
                Scripts = new XmlScriptList(xRoot.Element("scriptList"));
            }
        }

        internal override XElement CreateXml()
        {
            XmlParserTempProjectHelper.Sprite = this;

            var xRoot = new XElement("object");

            if (Looks != null)
            {
                xRoot.Add(Looks.CreateXml());
            }

            xRoot.Add(new XElement("name")
            {
                Value = Name
            });

            if (Scripts != null)
            {
                xRoot.Add(Scripts.CreateXml());
            }

            if (Sounds != null)
            {
                xRoot.Add(Sounds.CreateXml());
            }

            return xRoot;
        }

        internal override void LoadReference()
        {
            XmlParserTempProjectHelper.Sprite = this;

            foreach (var script in Scripts.Scripts)
            {
                XmlParserTempProjectHelper.Script = script;

                foreach (var brick in script.Bricks.Bricks)
                {
                    brick.LoadReference();
                }
            }


        }
    }
}
