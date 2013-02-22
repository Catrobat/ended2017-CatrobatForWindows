using System.ComponentModel;
using System.Xml.Linq;
using Catrobat.Core.Objects.Costumes;
using Catrobat.Core.Objects.Sounds;

namespace Catrobat.Core.Objects
{
    public class Sprite : DataObject
    {
        private CostumeList costumes;
        private string name;

        private ScriptList scripts;

        private SoundList sounds;

        public Sprite()
        {
        }

        public Sprite(Project project)
        {
            Project = project;
            scripts = new ScriptList(this);
            costumes = new CostumeList(this);
            sounds = new SoundList(this);
        }

        public Sprite(XElement xElement, Project project)
        {
            Project = project;
            LoadFromXML(xElement);
        }

        public Project Project { get; set; }

        public string Name
        {
            get { return name; }
            set
            {
                if (name == value)
                    return;

                name = value;
                OnPropertyChanged(new PropertyChangedEventArgs("Name"));
            }
        }

        public ScriptList Scripts
        {
            get { return scripts; }
            set
            {
                if (scripts == value)
                    return;

                scripts = value;
                OnPropertyChanged(new PropertyChangedEventArgs("Sprites"));
            }
        }

        public CostumeList Costumes
        {
            get { return costumes; }
            set
            {
                if (costumes == value)
                    return;

                costumes = value;
                OnPropertyChanged(new PropertyChangedEventArgs("Costumes"));
            }
        }

        public SoundList Sounds
        {
            get { return sounds; }
            set
            {
                if (sounds == value)
                    return;

                sounds = value;
                OnPropertyChanged(new PropertyChangedEventArgs("Sounds"));
            }
        }

        internal override void LoadFromXML(XElement xRoot)
        {
            if (xRoot.Element("costumeDataList") != null)
                costumes = new CostumeList(xRoot.Element("costumeDataList"), this);

            name = xRoot.Element("name").Value;

            if (xRoot.Element("soundList") != null)
                sounds = new SoundList(xRoot.Element("soundList"), this);
            if (xRoot.Element("scriptList") != null)
                scripts = new ScriptList(xRoot.Element("scriptList"), this);
        }

        internal override XElement CreateXML()
        {
            var xRoot = new XElement("sprite");

            if (costumes != null)
                xRoot.Add(costumes.CreateXML());

            xRoot.Add(new XElement("name")
                {
                    Value = name
                });

            if (scripts != null)
                xRoot.Add(scripts.CreateXML());

            if (sounds != null)
                xRoot.Add(sounds.CreateXML());

            return xRoot;
        }

        public DataObject Copy()
        {
            var newSprite = new Sprite(Project);
            newSprite.name = name;
            if (costumes != null)
                newSprite.costumes = costumes.Copy(newSprite) as CostumeList;
            if (sounds != null)
                newSprite.sounds = sounds.Copy(newSprite) as SoundList;
            if (scripts != null)
                newSprite.scripts = scripts.Copy(newSprite) as ScriptList;

            if (scripts != null)
                newSprite.scripts.CopyReference(this, newSprite);

            return newSprite;
        }

        public void Delete()
        {
            costumes.Delete();
            sounds.Delete();
        }
    }
}