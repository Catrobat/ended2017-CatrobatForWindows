using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Xml.Linq;
using Catrobat.IDE.Core.CatrobatObjects.Formulas;
using Catrobat.IDE.Core.CatrobatObjects.Variables;
using Catrobat.IDE.Core.FormulaEditor;

namespace Catrobat.IDE.Core.CatrobatObjects.Bricks
{
    public abstract class Brick : DataObject
    {
        protected Brick() {}

        protected Brick(XElement xElement)
        {
            LoadFromXML(xElement);

            //LoadFromCommonXML(xElement); 
        }

        internal abstract override void LoadFromXML(XElement xRoot);

        protected virtual void LoadFromCommonXML(XElement xRoot)
        {
            //  if (xRoot.Element("object") != null)
            //    sprite = new SpriteReference(xRoot.Element("object"));
        }

        internal abstract override XElement CreateXML();

        protected virtual void CreateCommonXML(XElement xRoot)
        {
            //  //if (sprite != null)
            //  //  xRoot.Add(sprite.CreateXML());
        }

        [Obsolete("Use overload with converter instead. ", true)]
        internal new void LoadReference() { base.LoadReference(); }

        /// <param name="converter">Passed to all <see cref="Formula" /> properties. See <see cref="Formula.LoadReference(XmlFormulaTreeConverter)"/>. </param>
        internal virtual void LoadReference(XmlFormulaTreeConverter converter) { base.LoadReference(); }

        public abstract DataObject Copy();
    }
}
