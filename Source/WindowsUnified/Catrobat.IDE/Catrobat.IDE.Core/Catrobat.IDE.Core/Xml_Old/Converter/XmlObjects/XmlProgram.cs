using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Catrobat.IDE.Core.ExtensionMethods;
using Catrobat.IDE.Core.Models;
using Catrobat.IDE.Core.Xml.Converter;
using Catrobat.IDE.Core.Xml.XmlObjects.Variables;
using ContextBase = Catrobat.IDE.Core.Xml.Converter.XmlProgramConverter.ConvertContextBase;
using Context = Catrobat.IDE.Core.Xml.Converter.XmlProgramConverter.ConvertContext;

// ReSharper disable once CheckNamespace
namespace Catrobat.IDE.Core.Xml.XmlObjects
{
    partial class XmlProgram : IModelConvertible<Program>
    {
        Program IModelConvertible<Program>.ToModel()
        {
            var localVariables = VariableList.ObjectVariableList.ObjectVariableEntries.ToReadOnlyDictionary(
                keySelector: entry => entry.Sprite,
                elementSelector: entry => entry.VariableList.UserVariables.ToReadOnlyDictionary(
                    keySelector: variable => variable,
                    elementSelector: variable => variable.ToModel<LocalVariable>()));
            var globalVariables = VariableList.ProgramVariableList.UserVariables.ToReadOnlyDictionary(
                keySelector: variable => variable,
                elementSelector: variable => variable.ToModel<GlobalVariable>());
            var contextBase = new ContextBase(this, globalVariables);
            var sprites = SpriteList.Sprites.ToReadOnlyDictionary(
                keySelector: sprite => sprite,
                elementSelector: sprite =>
                {
                    ReadOnlyDictionary<XmlUserVariable, LocalVariable> localVariables2;
                    if (!localVariables.TryGetValue(sprite, out localVariables2))
                    {
                        localVariables2 = new ReadOnlyDictionary<XmlUserVariable, LocalVariable>(new Dictionary<XmlUserVariable, LocalVariable>());
                    }
                    return sprite.ToModel(new Context(contextBase, sprite,
                        sprite.Looks == null || sprite.Looks.Looks == null
                            ? null
                            : sprite.Looks.Looks.ToReadOnlyDictionary(
                                keySelector: look => look,
                                elementSelector: look => look.ToModel()),
                        sprite.Sounds == null || sprite.Sounds.Sounds == null
                            ? null
                            : sprite.Sounds.Sounds.ToReadOnlyDictionary(
                                keySelector: sound => sound,
                                elementSelector: sound => sound.ToModel()),
                        localVariables2));
                });
            return new Program
            {
                Name = ProjectHeader.ProgramName,
                Description = ProjectHeader.Description,
                UploadHeader = ProjectHeader.ToModel(),
                GlobalVariables = VariableList.ProgramVariableList.UserVariables.Select(variable => globalVariables[variable]).ToObservableCollection(),
                BroadcastMessages = contextBase.BroadcastMessages.Values.ToObservableCollection(),
                Sprites = SpriteList.Sprites.Select(sprite => sprites[sprite]).ToObservableCollection()
            };
        }
    }
}
