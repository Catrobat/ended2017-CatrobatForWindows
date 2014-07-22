using System.Collections.Generic;
using System.Linq;
using Catrobat.Data.Xml.XmlObjects;
using Catrobat.Data.Xml.XmlObjects.Scripts;
using Catrobat.IDE.Core.ExtensionMethods;
using Catrobat.IDE.Core.Xml.Converter;
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

            result.Costumes = new XmlCostumeList
            {
                Costumes = context.Costumes == null ? new List<XmlCostume>() : context.Costumes.Values.ToList()
            };
            result.Sounds = new XmlSoundList
            {
                Sounds = context.Sounds == null ? new List<XmlSound>() : context.Sounds.Values.ToList()
            };
            result.Scripts = new XmlScriptList
            {
                Scripts = Scripts == null
                    ? new List<XmlScript>()
                    : Scripts.Select(script => script.ToXmlObject(context)).ToList()
            };
            return result;
        }
    }
}
