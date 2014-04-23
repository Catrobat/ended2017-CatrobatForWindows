using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;
using Catrobat.IDE.Core.FormulaEditor;
using Catrobat.IDE.Core.Utilities.Helpers;
using Catrobat.IDE.Core.CatrobatObjects.Costumes;
using Catrobat.IDE.Core.CatrobatObjects.Scripts;
using Catrobat.IDE.Core.CatrobatObjects.Sounds;
using Catrobat.IDE.Core.CatrobatObjects.Variables;

namespace Catrobat.IDE.Core.CatrobatObjects
{
    public class Sprite : DataObject
    {
        private CostumeList _costumes;
        public CostumeList Costumes
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

        private ScriptList _scripts;
        public ScriptList Scripts
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

        private SoundList _sounds;
        public SoundList Sounds
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


        public Sprite()
        {
            _scripts = new ScriptList();
            _costumes = new CostumeList();
            _sounds = new SoundList();
        }

        public Sprite(XElement xElement)
        {
            LoadFromXML(xElement);
        }

        internal override void LoadFromXML(XElement xRoot)
        {
            if (xRoot.Element("lookList") != null)
            {
                _costumes = new CostumeList(xRoot.Element("lookList"));
            }

            _name = xRoot.Element("name").Value;

            if (xRoot.Element("soundList") != null)
            {
                _sounds = new SoundList(xRoot.Element("soundList"));
            }
            if (xRoot.Element("scriptList") != null)
            {
                _scripts = new ScriptList(xRoot.Element("scriptList"));
            }
        }

        internal override XElement CreateXML()
        {
            var xRoot = new XElement("object");

            if (_costumes != null)
            {
                xRoot.Add(_costumes.CreateXML());
            }

            xRoot.Add(new XElement("name")
            {
                Value = _name
            });

            if (_scripts != null)
            {
                xRoot.Add(_scripts.CreateXML());
            }

            if (_sounds != null)
            {
                xRoot.Add(_sounds.CreateXML());
            }

            return xRoot;
        }

        internal override void LoadReference()
        {
            var localVariables = VariableHelper.GetLocalVariableList(XmlParserTempProjectHelper.Project, this);
            var globalVariables = XmlParserTempProjectHelper.Project.VariableList.ProgramVariableList.UserVariables;
            var converter = new XmlFormulaTreeConverter(localVariables, globalVariables);
            foreach (var brick in Scripts.Scripts.SelectMany(script => script.Bricks.Bricks))
            {
                brick.LoadReference(converter);
            }
        }

        public async Task<DataObject> Copy()
        {
            var newSprite = new Sprite();

            newSprite._name = _name;
            if (_costumes != null)
            {
                newSprite._costumes = await _costumes.Copy() as CostumeList;
            }
            if (_sounds != null)
            {
                newSprite._sounds = await _sounds.Copy() as SoundList;
            }
            if (_scripts != null)
            {
                newSprite._scripts = _scripts.Copy() as ScriptList;
            }


            var entries = XmlParserTempProjectHelper.Project.VariableList.ObjectVariableList.ObjectVariableEntries;
            ObjectVariableEntry newEntry = null;

            foreach (var entry in entries)
                if (entry.Sprite == this)
                {
                    newEntry = entry.Copy(newSprite) as ObjectVariableEntry; //changes Spritereference to new Sprite
                }
            if (newEntry != null)
                entries.Add(newEntry);


            ReferenceHelper.UpdateReferencesAfterCopy(this, newSprite);

            return newSprite;
        }

        public async Task Delete()
        {
            await _costumes.Delete();
            await _sounds.Delete();
        }

        public override bool Equals(DataObject other)
        {
            var otherSprite = other as Sprite;

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
