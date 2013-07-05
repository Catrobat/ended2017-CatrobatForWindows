using System.ComponentModel;
using System.Xml.Linq;
using Catrobat.Core.Objects.Costumes;
using Catrobat.Core.Objects.Sounds;

namespace Catrobat.Core.Objects
{
    public class Sprite : DataObject
    {
        private CostumeList _costumes;
        private string _name;
        private ScriptList _scripts;
        private SoundList _sounds;

        public Sprite() {}

        public Sprite(Project project)
        {
            Project = project;
            _scripts = new ScriptList(this);
            _costumes = new CostumeList(this);
            _sounds = new SoundList(this);
        }

        public Sprite(XElement xElement, Project project)
        {
            Project = project;
            LoadFromXML(xElement);
        }

        public Project Project { get; set; }

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
                OnPropertyChanged(new PropertyChangedEventArgs("Name"));
            }
        }

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
                OnPropertyChanged(new PropertyChangedEventArgs("Sprites"));
            }
        }

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
                OnPropertyChanged(new PropertyChangedEventArgs("Costumes"));
            }
        }

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
                OnPropertyChanged(new PropertyChangedEventArgs("Sounds"));
            }
        }

        internal override void LoadFromXML(XElement xRoot)
        {
            if (xRoot.Element("costumeDataList") != null)
            {
                _costumes = new CostumeList(xRoot.Element("costumeDataList"), this);
            }

            _name = xRoot.Element("name").Value;

            if (xRoot.Element("soundList") != null)
            {
                _sounds = new SoundList(xRoot.Element("soundList"), this);
            }
            if (xRoot.Element("scriptList") != null)
            {
                _scripts = new ScriptList(xRoot.Element("scriptList"), this);
            }
        }

        internal override XElement CreateXML()
        {
            var xRoot = new XElement("sprite");

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

        public DataObject Copy()
        {
            var newSprite = new Sprite(Project);
            newSprite._name = _name;
            if (_costumes != null)
            {
                newSprite._costumes = _costumes.Copy(newSprite) as CostumeList;
            }
            if (_sounds != null)
            {
                newSprite._sounds = _sounds.Copy(newSprite) as SoundList;
            }
            if (_scripts != null)
            {
                newSprite._scripts = _scripts.Copy(newSprite) as ScriptList;
            }

            if (_scripts != null)
            {
                newSprite._scripts.CopyReference(this, newSprite);
            }

            return newSprite;
        }

        public void Delete()
        {
            _costumes.Delete();
            _sounds.Delete();
        }
    }
}