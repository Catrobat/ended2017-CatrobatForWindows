using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Xml.Linq;

namespace Catrobat.Core.Objects
{
    public class SpriteList : DataObject
    {
        private readonly Project project;

        public SpriteList(Project project)
        {
            this.project = project;
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

            foreach (XElement xSprite in xRoot.Elements("sprite"))
                Sprites.Add(new Sprite(project));

            IEnumerator<Sprite> enumerator = Sprites.GetEnumerator();
            foreach (XElement xSprite in xRoot.Elements("sprite"))
            {
                enumerator.MoveNext();
                enumerator.Current.LoadFromXML(xSprite);
            }
        }

        internal override XElement CreateXML()
        {
            var xRoot = new XElement("spriteList");

            foreach (Sprite sprite in Sprites)
                xRoot.Add(sprite.CreateXML());

            return xRoot;
        }
    }
}