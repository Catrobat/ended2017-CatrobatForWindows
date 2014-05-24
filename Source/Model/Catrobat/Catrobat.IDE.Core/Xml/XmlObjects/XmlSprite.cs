using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;
using Catrobat.IDE.Core.Utilities.Helpers;
using Catrobat.IDE.Core.Xml.XmlObjects.Scripts;
using Catrobat.IDE.Core.Xml.XmlObjects.Variables;

namespace Catrobat.IDE.Core.Xml.XmlObjects
{
    public partial class XmlSprite : XmlObject
    {
        private XmlCostumeList _costumes;
        public XmlCostumeList Costumes
        {
            get { return _costumes; }
            set
            {
                if (_costumes == value)
                {
                    return;
                }

                _costumes = value;
                RaisePropertyChanged();
            }
        }

        private string _name;
        public string Name
        {
            get { return _name; }
            set
            {
                if (_name == value)
                {
                    return;
                }

                _name = value;
                RaisePropertyChanged();
            }
        }

        private XmlScriptList _scripts;
        public XmlScriptList Scripts
        {
            get { return _scripts; }
            set
            {
                if (_scripts == value)
                {
                    return;
                }

                _scripts = value;
                RaisePropertyChanged();
            }
        }

        private XmlSoundList _sounds;
        public XmlSoundList Sounds
        {
            get { return _sounds; }
            set
            {
                if (_sounds == value)
                {
                    return;
                }

                _sounds = value;
                RaisePropertyChanged();
            }
        }


        public XmlSprite()
        {
            _scripts = new XmlScriptList();
            _costumes = new XmlCostumeList();
            _sounds = new XmlSoundList();
        }

        public XmlSprite(XElement xElement)
        {
            LoadFromXml(xElement);
        }

        internal override void LoadFromXml(XElement xRoot)
        {
            if (xRoot.Element("lookList") != null)
            {
                _costumes = new XmlCostumeList(xRoot.Element("lookList"));
            }

            _name = xRoot.Element("name").Value;

            if (xRoot.Element("soundList") != null)
            {
                _sounds = new XmlSoundList(xRoot.Element("soundList"));
            }
            if (xRoot.Element("scriptList") != null)
            {
                _scripts = new XmlScriptList(xRoot.Element("scriptList"));
            }
        }

        internal override XElement CreateXml()
        {
            var xRoot = new XElement("object");

            if (_costumes != null)
            {
                xRoot.Add(_costumes.CreateXml());
            }

            xRoot.Add(new XElement("name")
            {
                Value = _name
            });

            if (_scripts != null)
            {
                xRoot.Add(_scripts.CreateXml());
            }

            if (_sounds != null)
            {
                xRoot.Add(_sounds.CreateXml());
            }

            return xRoot;
        }

        internal override void LoadReference()
        {
            foreach (var brick in Scripts.Scripts.SelectMany(script => script.Bricks.Bricks))
            {
                brick.LoadReference();
            }
        }

        public async Task<XmlObject> Copy()
        {
            var newSprite = new XmlSprite();

            newSprite._name = _name;
            if (_costumes != null)
            {
                newSprite._costumes = await _costumes.Copy() as XmlCostumeList;
            }
            if (_sounds != null)
            {
                newSprite._sounds = await _sounds.Copy() as XmlSoundList;
            }
            if (_scripts != null)
            {
                newSprite._scripts = _scripts.Copy() as XmlScriptList;
            }


            var entries = XmlParserTempProjectHelper.Project.VariableList.ObjectVariableList.ObjectVariableEntries;
            XmlObjectVariableEntry newEntry = null;

            foreach (var entry in entries)
                if (entry.Sprite == this)
                {
                    newEntry = entry.Copy(newSprite) as XmlObjectVariableEntry; //changes Spritereference to new Sprite
                }
            if (newEntry != null)
                entries.Add(newEntry);


            ReferenceHelper.UpdateReferencesAfterCopy(this, newSprite);

            return newSprite;
        }

        public override bool Equals(XmlObject other)
        {
            var otherSprite = other as XmlSprite;

            if (otherSprite == null)
                return false;

            if (Costumes != null && otherSprite.Costumes != null)
            {
                if (!Costumes.Equals(otherSprite.Costumes))
                    return false;
            }
            else if (!(Costumes == null && otherSprite.Costumes == null))
                return false;

            if (Sounds != null && otherSprite.Sounds != null)
            {
                if (!Sounds.Equals(otherSprite.Sounds))
                    return false;
            }
            else if (!(Sounds == null && otherSprite.Sounds == null))
                return false;

            if (Scripts != null && otherSprite.Scripts != null)
            {
                if (!Scripts.Equals(otherSprite.Scripts))
                    return false;
            }
            else if (!(Scripts == null && otherSprite.Scripts == null))
                return false;

            if (Name != otherSprite.Name)
                return false;

            return true;
        }
    }
}
