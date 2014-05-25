using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Catrobat.IDE.Core.ExtensionMethods;
using Catrobat.IDE.Core.Models;
using Catrobat.IDE.Core.Xml.Converter;
using Catrobat.IDE.Core.Xml.XmlObjects.Variables;
using ContextBase = Catrobat.IDE.Core.Xml.Converter.XmlProjectConverter.ConvertContextBase;
using Context = Catrobat.IDE.Core.Xml.Converter.XmlProjectConverter.ConvertContext;

// ReSharper disable once CheckNamespace
namespace Catrobat.IDE.Core.Xml.XmlObjects
{
    partial class XmlProject : IModelConvertible<Project>
    {
        Project IModelConvertible<Project>.ToModel()
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
                        sprite.Costumes == null || sprite.Costumes.Costumes == null
                            ? null
                            : sprite.Costumes.Costumes.ToReadOnlyDictionary(
                                keySelector: costume => costume,
                                elementSelector: costume => costume.ToModel()),
                        sprite.Sounds == null || sprite.Sounds.Sounds == null
                            ? null
                            : sprite.Sounds.Sounds.ToReadOnlyDictionary(
                                keySelector: sound => sound,
                                elementSelector: sound => sound.ToModel()),
                        localVariables2));
                });
            return new Project
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
