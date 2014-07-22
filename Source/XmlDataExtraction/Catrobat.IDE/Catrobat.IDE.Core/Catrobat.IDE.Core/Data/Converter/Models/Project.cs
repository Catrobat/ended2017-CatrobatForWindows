using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Catrobat.Data.Xml.XmlObjects;
using Catrobat.Data.Xml.XmlObjects.Variables;
using Catrobat.IDE.Core.ExtensionMethods;
using Catrobat.IDE.Core.Xml.Converter;
using ContextBase = Catrobat.IDE.Core.Xml.Converter.XmlProjectConverter.ConvertBackContextBase;
using Context = Catrobat.IDE.Core.Xml.Converter.XmlProjectConverter.ConvertBackContext;

// ReSharper disable once CheckNamespace
namespace Catrobat.IDE.Core.Models
{
    partial class Project : IXmlObjectConvertible<XmlProject>
    {
        XmlProject IXmlObjectConvertible<XmlProject>.ToXmlObject()
        {
            var localVariables = Sprites.ToReadOnlyDictionary(
                keySelector: sprite => sprite,
                elementSelector: sprite => sprite.LocalVariables.ToReadOnlyDictionary(
                    keySelector: variable => variable,
                    elementSelector: variable => variable.ToXmlObject()));
            var globalVariables = GlobalVariables.ToReadOnlyDictionary(
                keySelector: variable => variable,
                elementSelector: variable => variable.ToXmlObject());
            var contextBase = new ContextBase(this, globalVariables);
            var sprites = Sprites.ToReadOnlyDictionary(
                keySelector: sprite => sprite,
                elementSelector: sprite =>
                {
                    ReadOnlyDictionary<LocalVariable, XmlUserVariable> localVariables2;
                    if (!localVariables.TryGetValue(sprite, out localVariables2))
                    {
                        localVariables2 = new ReadOnlyDictionary<LocalVariable, XmlUserVariable>(new Dictionary<LocalVariable, XmlUserVariable>());
                    }
                    return sprite.ToXmlObject(new Context(contextBase, sprite,
                        sprite.Costumes == null
                            ? null
                            : sprite.Costumes.ToReadOnlyDictionary(
                                keySelector: costume => costume,
                                elementSelector: costume => costume.ToXmlObject()),
                        sprite.Sounds == null
                            ? null
                            : sprite.Sounds.ToReadOnlyDictionary(
                                keySelector: sound => sound,
                                elementSelector: sound => sound.ToXmlObject()),
                        localVariables2));
                });
            var header = UploadHeader.ToXmlObject();
            header.ProgramName = Name;
            header.Description = Description;
            var result = new XmlProject
            {
                ProjectHeader = header, 
                VariableList = new XmlVariableList
                {
                    ProgramVariableList = new XmlProgramVariableList
                    {
                        UserVariables = GlobalVariables == null ? new List<XmlUserVariable>() : GlobalVariables.Select(variable => globalVariables[variable]).ToList()
                    },
                    ObjectVariableList = new XmlObjectVariableList
                    {
                        ObjectVariableEntries = Sprites.Select(sprite => new XmlObjectVariableEntry
                        {
                            Sprite = sprites[sprite],
                            VariableList = new XmlUserVariableList
                            {
                                UserVariables = sprite.LocalVariables == null 
                                    ? new List<XmlUserVariable>() 
                                    : sprite.LocalVariables.Select(variable => localVariables[sprite][variable]).ToList()
                            }
                        }).ToList()
                    }
                }, 
                SpriteList = new XmlSpriteList
                {
                    Sprites = Sprites.Select(sprite => sprites[sprite]).ToList()
                }
            };

            return result;
        }
    }
}
