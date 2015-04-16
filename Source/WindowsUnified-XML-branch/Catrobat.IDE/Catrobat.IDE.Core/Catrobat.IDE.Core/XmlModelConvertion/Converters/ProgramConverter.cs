using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Catrobat.IDE.Core.Models;
using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.Xml.XmlObjects;
using Catrobat.IDE.Core.ExtensionMethods;
using Catrobat.IDE.Core.Xml.XmlObjects.Variables;

namespace Catrobat.IDE.Core.XmlModelConvertion.Converters
{
    public class ProgramConverter : XmlModelConverter<XmlProgram, Program>
    {
        public ProgramConverter() { }

        public override Program Convert(XmlProgram o, XmlModelConvertContext c)
        {
            var spriteConverter = new SpriteConverter();
            var lookConverter = new LookConverter();
            var soundConverter = new SoundConverter();
            var localVariableConverter = new VariableConverter<LocalVariable>();
            var globalVariableConverter = new VariableConverter<GlobalVariable>();
            var uploadHeaderConverter = new UploadHeaderConverter();

            var localVariables = o.VariableList.ObjectVariableList.ObjectVariableEntries.ToReadOnlyDictionary(
                keySelector: entry => entry.Sprite,
                elementSelector: entry => entry.VariableList.UserVariables.ToReadOnlyDictionary(
                    keySelector: variable => variable,
                    elementSelector: variable => (LocalVariable)localVariableConverter.Convert(variable, c)));
            var globalVariables = o.VariableList.ProgramVariableList.UserVariables.ToReadOnlyDictionary(
                keySelector: variable => variable,
                elementSelector: variable => (GlobalVariable)globalVariableConverter.Convert(variable, c));
            var contextBase = new XmlModelConvertContextBase(o, globalVariables);
            var sprites = o.SpriteList.Sprites.ToReadOnlyDictionary(
                keySelector: sprite => sprite,
                elementSelector: sprite =>
                {
                    ReadOnlyDictionary<XmlUserVariable, LocalVariable> localVariables2;
                    if (!localVariables.TryGetValue(sprite, out localVariables2))
                    {
                        localVariables2 = new ReadOnlyDictionary<XmlUserVariable, LocalVariable>(new Dictionary<XmlUserVariable, LocalVariable>());
                    }
                    return (Sprite)spriteConverter.Convert(sprite, new XmlModelConvertContext(contextBase, sprite,
                        sprite.Looks == null || sprite.Looks.Looks == null
                            ? null
                            : sprite.Looks.Looks.ToReadOnlyDictionary(
                                keySelector: look => look,
                                elementSelector: look => (Look)lookConverter.Convert(look, c)),
                        sprite.Sounds == null || sprite.Sounds.Sounds == null
                            ? null
                            : sprite.Sounds.Sounds.ToReadOnlyDictionary(
                                keySelector: sound => sound,
                                elementSelector: sound => (Sound)soundConverter.Convert(sound, c)),
                        localVariables2));
                });
            return new Program
            {
                Name = o.ProgramHeader.ProgramName,
                Description = o.ProgramHeader.Description,
                UploadHeader = (UploadHeader)uploadHeaderConverter.Convert(o.ProgramHeader, c),
                GlobalVariables = o.VariableList.ProgramVariableList.UserVariables.Select(variable => globalVariables[variable]).ToObservableCollection(),
                /*BroadcastMessages = contextBase.BroadcastMessages.Values.ToObservableCollection(),*/
                Sprites = o.SpriteList.Sprites.Select(sprite => sprites[sprite]).ToObservableCollection()
            };
        }

        public override XmlProgram Convert(Program m, XmlModelConvertBackContext c)
        {
            var spriteConverter = new SpriteConverter();
            var lookConverter = new LookConverter();
            var soundConverter = new SoundConverter();
            var localVariableConverter = new VariableConverter<LocalVariable>();
            var globalVariableConverter = new VariableConverter<GlobalVariable>();
            var uploadHeaderConverter = new UploadHeaderConverter();

            var localVariables = m.Sprites.ToReadOnlyDictionary(
                keySelector: sprite => sprite,
                elementSelector: sprite => sprite.LocalVariables.ToReadOnlyDictionary(
                    keySelector: variable => variable,
                    elementSelector: variable => (XmlUserVariable)localVariableConverter.Convert(variable, c)));
            var globalVariables = m.GlobalVariables.ToReadOnlyDictionary(
                keySelector: variable => variable,
                elementSelector: variable => (XmlUserVariable)globalVariableConverter.Convert(variable, c));
            var contextBase = new XmlModelConvertBackContextBase(m, globalVariables);
            var sprites = m.Sprites.ToReadOnlyDictionary(
                keySelector: sprite => sprite,
                elementSelector: sprite =>
                {
                    ReadOnlyDictionary<LocalVariable, XmlUserVariable> localVariables2;
                    if (!localVariables.TryGetValue(sprite, out localVariables2))
                    {
                        localVariables2 = new ReadOnlyDictionary<LocalVariable, XmlUserVariable>(new Dictionary<LocalVariable, XmlUserVariable>());
                    }
                    return (XmlSprite)spriteConverter.Convert(sprite, new XmlModelConvertBackContext(contextBase, sprite,
                        sprite.Looks == null
                            ? null
                            : sprite.Looks.ToReadOnlyDictionary(
                                keySelector: look => look,
                                elementSelector: look => (XmlLook)lookConverter.Convert(look, c)),
                        sprite.Sounds == null
                            ? null
                            : sprite.Sounds.ToReadOnlyDictionary(
                                keySelector: sound => sound,
                                elementSelector: sound => (XmlSound)soundConverter.Convert(sound, c)),
                        localVariables2));
                });
            var header = (XmlProjectHeader)uploadHeaderConverter.Convert(m.UploadHeader, c);
            header.ProgramName = m.Name;
            header.Description = m.Description;
            var result = new XmlProgram
            {
                ProgramHeader = header,
                VariableList = new XmlVariableList
                {
                    ProgramVariableList = new XmlProgramVariableList
                    {
                        UserVariables = m.GlobalVariables == null ? new List<XmlUserVariable>() : m.GlobalVariables.Select(variable => globalVariables[variable]).ToList()
                    },
                    ObjectVariableList = new XmlObjectVariableList
                    {
                        ObjectVariableEntries = m.Sprites.Select(sprite => new XmlObjectVariableEntry
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
                    Sprites = m.Sprites.Select(sprite => sprites[sprite]).ToList()
                }
            };
            ServiceLocator.ContextService.UpdateProgramHeader(result);

            return result;
        }

        public Program Convert(XmlProgram xmlProgram)
        {
            return Convert(xmlProgram, null);
        }

        public XmlProgram Convert(Program program)
        {
            return Convert(program, null);
        }
    }
}
