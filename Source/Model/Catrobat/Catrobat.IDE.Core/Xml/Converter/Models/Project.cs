using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Catrobat.IDE.Core.ExtensionMethods;
using Catrobat.IDE.Core.Xml.Converter;
using Catrobat.IDE.Core.Xml.XmlObjects;
using Catrobat.IDE.Core.Xml.XmlObjects.Variables;
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
                        UserVariables = GlobalVariables == null ? null : GlobalVariables.Select(variable => globalVariables[variable]).ToObservableCollection()
                    },
                    ObjectVariableList = new XmlObjectVariableList
                    {
                        ObjectVariableEntries = Sprites.Select(sprite => new XmlObjectVariableEntry
                        {
                            Sprite = sprites[sprite],
                            VariableList = new XmlUserVariableList
                            {
                                UserVariables = sprite.LocalVariables == null 
                                    ? null 
                                    : sprite.LocalVariables.Select(variable => localVariables[sprite][variable]).ToObservableCollection()
                            }
                        }).ToObservableCollection()
                    }
                }, 
                SpriteList = new XmlSpriteList
                {
                    Sprites = Sprites.Select(sprite => sprites[sprite]).ToObservableCollection()
                }
            };

            return result;
        }
    }
}
