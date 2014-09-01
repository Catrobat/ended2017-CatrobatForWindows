using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Catrobat.IDE.Core.Models;
using Catrobat.IDE.Core.Models.Scripts;
using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.Xml.XmlObjects;
using Catrobat.IDE.Core.Xml.XmlObjects.Scripts;
using Catrobat.IDE.Core.ExtensionMethods;
using System.Collections.ObjectModel;
using Catrobat.IDE.Core.XmlModelConvertion.Converters.Actions.Scripts;

namespace Catrobat.IDE.Core.XmlModelConvertion.Converters
{
    public class SpriteConverter : XmlModelConverter<XmlSprite, Sprite>
    {
        public SpriteConverter() { }

        public override Sprite Convert(XmlSprite o, XmlModelConvertContext c)
        {
            return Convert(o, c, false);
        }

        public override Sprite Convert(XmlSprite o, XmlModelConvertContext c, bool pointerOnly)
        {
            var scriptConverterCollection = new XmlModelConverterCollection<IScriptConverter>();

            // prevents endless loops
            Sprite result;
            if (!c.Sprites.TryGetValue(o, out result))
            {
                result = new Sprite { Name = o.Name };
                c.Sprites[o] = result;
            }
            if (pointerOnly) return result;

            var localVariables = c.Program.VariableList.ObjectVariableList.ObjectVariableEntries.FirstOrDefault(entry => entry.Sprite == o);
            result.Looks = c.Looks == null
                ? new ObservableCollection<Look>()
                : c.Looks.Values.ToObservableCollection();
            result.Sounds = c.Sounds == null
                ? new ObservableCollection<Sound>()
                : c.Sounds.Values.ToObservableCollection();
            result.LocalVariables = localVariables == null || localVariables.VariableList == null || localVariables.VariableList.UserVariables == null
                ? new ObservableCollection<LocalVariable>()
                : localVariables.VariableList.UserVariables.Select(variable => c.LocalVariables[variable]).ToObservableCollection();
            c.Sprites[o] = result;
            result.Scripts = o.Scripts == null || o.Scripts.Scripts == null
                ? new ObservableCollection<Script>()
                : o.Scripts.Scripts.Select(script => (Script)scriptConverterCollection.Convert(script, c)).ToObservableCollection();
            return result;
        }

        public override XmlSprite Convert(Sprite m, XmlModelConvertBackContext c)
        {
            return Convert(m, c, false);
        }
        public override XmlSprite Convert(Sprite m, XmlModelConvertBackContext c, bool pointerOnly)
        {
            var scriptConverterCollection = new XmlModelConverterCollection<IScriptConverter>();

            // prevents endless loops
            XmlSprite result;
            if (!c.Sprites.TryGetValue(m, out result))
            {
                result = new XmlSprite { Name = m.Name };
                c.Sprites[m] = result;
            }
            if (pointerOnly) return result;

            result.Looks = new XmlLookList
            {
                Looks = c.Looks == null ? new List<XmlLook>() : c.Looks.Values.ToList()
            };
            result.Sounds = new XmlSoundList
            {
                Sounds = c.Sounds == null ? new List<XmlSound>() : c.Sounds.Values.ToList()
            };
            result.Scripts = new XmlScriptList
            {
                Scripts = m.Scripts == null
                    ? new List<XmlScript>()
                    : m.Scripts.Select(script => (XmlScript)scriptConverterCollection.Convert(script, c)).ToList()
            };
            return result;
        }
    }
}
