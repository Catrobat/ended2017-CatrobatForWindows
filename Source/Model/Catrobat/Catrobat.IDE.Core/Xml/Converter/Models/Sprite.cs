using System.Linq;
using Catrobat.IDE.Core.ExtensionMethods;
using Catrobat.IDE.Core.Xml.Converter;
using Catrobat.IDE.Core.Xml.XmlObjects;
using Catrobat.IDE.Core.Xml.XmlObjects.Scripts;
using Context = Catrobat.IDE.Core.Xml.Converter.XmlProjectConverter.ConvertBackContext;

// ReSharper disable once CheckNamespace
namespace Catrobat.IDE.Core.Models
{
    partial class Sprite : IXmlObjectConvertibleCyclic<XmlSprite, Context>
    {
        XmlSprite IXmlObjectConvertibleCyclic<XmlSprite, Context>.ToXmlObject(Context context, bool pointerOnly)
        {
            // prevents endless loops
            XmlSprite result;
            if (!context.Sprites.TryGetValue(this, out result))
            {
                result = new XmlSprite {Name = Name};
                context.Sprites[this] = result;
            }
            if (pointerOnly) return result;

            result.Costumes = context.Costumes == null ? null : new XmlCostumeList
            {
                Costumes = context.Costumes.Values.ToObservableCollection()
            };
            result.Sounds = context.Sounds == null ? null : new XmlSoundList
            {
                Sounds = context.Sounds.Values.ToObservableCollection()
            };
            result.Scripts = new XmlScriptList
            {
                Scripts = Scripts == null
                    ? null
                    : Scripts.Select(script => script.ToXmlObject(context)).ToObservableCollection()
            };
            return result;
        }
    }
}
