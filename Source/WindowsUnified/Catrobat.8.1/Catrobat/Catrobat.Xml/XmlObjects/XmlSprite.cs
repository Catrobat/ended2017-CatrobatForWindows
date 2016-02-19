using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using Catrobat.IDE.Core.Utilities.Helpers;
using Catrobat.IDE.Core.Xml.XmlObjects.Scripts;
using Catrobat_Player.NativeComponent;
using Catrobat.IDE.Core.Xml.XmlObjects.Variables;

namespace Catrobat.IDE.Core.Xml.XmlObjects
{
    public partial class XmlSprite : XmlObjectNode, IObject
    {
        #region NativeInterface
        public IList<IUserVariable> UserVariables
    {
            get {
                if (Variables != null)
                    return Variables.UserVariables.Cast<IUserVariable>().ToList();
                else
                    return new List<IUserVariable>();
            }
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

        /// <summary>
        /// WTF the xml has the object variables in a global list ...
        /// </summary>
        public XmlUserVariableList Variables { get; set; }

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

            if (xRoot.Element(XmlConstants.XmlLookListType) != null)
            {
                 Looks = new XmlLookList(xRoot.Element(XmlConstants.XmlLookListType));
            }

            Name = xRoot.Attribute(XmlConstants.Name).Value;

            if (xRoot.Element(XmlConstants.XmlSoundList) != null)
            {
                Sounds = new XmlSoundList(xRoot.Element(XmlConstants.XmlSoundList));
            }
            if (xRoot.Element(XmlConstants.ScriptList) != null)
            {
                Scripts = new XmlScriptList(xRoot.Element(XmlConstants.ScriptList));
            }
        }

        internal override XElement CreateXml()
        {
            XmlParserTempProjectHelper.Sprite = this;

            var xRoot = new XElement(XmlConstants.Object);
            xRoot.SetAttributeValue(XmlConstants.Name, Name.ToString());

            if (Looks != null)
            {
                xRoot.Add(Looks.CreateXml());
            }

            //xRoot.Add(new XElement("name")
            //{
            //    Value = Name
            //});

            //if (Scripts != null)
            //{
            //    xRoot.Add(Scripts.CreateXml());
            //}

            if (Sounds != null)
            {
                xRoot.Add(Sounds.CreateXml());
            }

            if (Scripts != null)
            {
                xRoot.Add(Scripts.CreateXml());
            }

            return xRoot;
        }

        public override void LoadReference()
        {
            XmlParserTempProjectHelper.Sprite = this;

            foreach(var script in Scripts.Scripts)
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
