using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Catrobat.IDE.Core.ExtensionMethods;
using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.Services.Common;
using Catrobat.IDE.Core.Xml.Converter;
using Catrobat.IDE.Core.Xml.XmlObjects;
using Catrobat.IDE.Core.Xml.XmlObjects.Variables;
using ContextBase = Catrobat.IDE.Core.Xml.Converter.XmlProjectConverter.ConvertBackContextBase;
using Context = Catrobat.IDE.Core.Xml.Converter.XmlProjectConverter.ConvertBackContext;

// ReSharper disable once CheckNamespace
namespace Catrobat.IDE.Core.Models
{
    partial class Program : IXmlObjectConvertible<XmlProgram>
    {
        XmlProgram IXmlObjectConvertible<XmlProgram>.ToXmlObject()
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
                        sprite.Looks == null
                            ? null
                            : sprite.Looks.ToReadOnlyDictionary(
                                keySelector: look => look,
                                elementSelector: look => look.ToXmlObject()),
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
            var result = new XmlProgram
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

            ServiceLocator.ContextService.UpdateProgramHeader(result);

            return result;
        }
    }
}
