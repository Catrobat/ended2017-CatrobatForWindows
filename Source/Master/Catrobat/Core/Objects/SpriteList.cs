using System.Collections.ObjectModel;
using System.Xml.Linq;

namespace Catrobat.Core.Objects
{
    public class SpriteList : DataObject
    {
        private readonly Project _project;

        public SpriteList(Project project)
        {
            _project = project;
            Sprites = new ObservableCollection<Sprite>();
        }

        public ObservableCollection<Sprite> Sprites { get; set; }

        //public SpriteList(XElement xElement, Project project)
        //{ 
        //  this.project = project;
        //  LoadFromXML(xElement);
        //}

        internal override void LoadFromXML(XElement xRoot)
        {
            Sprites = new ObservableCollection<Sprite>();

            foreach (XElement xSprite in xRoot.Elements("object"))
            {
                Sprites.Add(new Sprite(_project));
            }

            var enumerator = Sprites.GetEnumerator();
            foreach (XElement xSprite in xRoot.Elements("object"))
            {
                enumerator.MoveNext();
                enumerator.Current.LoadFromXML(xSprite);
            }
        }

        internal override XElement CreateXML()
        {
            var xRoot = new XElement("objectList");

            foreach (Sprite sprite in Sprites)
            {
                xRoot.Add(sprite.CreateXML());
            }

            return xRoot;
        }
    }
}