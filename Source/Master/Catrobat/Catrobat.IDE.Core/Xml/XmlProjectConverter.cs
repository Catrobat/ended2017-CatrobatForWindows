using System;
using System.Collections.Generic;
using System.Linq;
using Catrobat.IDE.Core.Models;
using Catrobat.IDE.Core.Models.Scripts;
using Catrobat.IDE.Core.Xml.Scripts;
using Catrobat.IDE.Core.Xml.Variables;
using Catrobat.IDE.Core.ExtensionMethods;
using Catrobat.IDE.Core.Models.Variables;

namespace Catrobat.IDE.Core.Xml
{
    internal class XmlProjectConverter
    {
        #region Covert

        public Project Convert(XmlProject project)
        {
            var globalVariables = project.VariableList.ProgramVariableList.UserVariables.ToDictionary(
                keySelector: variable => variable,
                elementSelector: variable => new GlobalVariable { Name = variable.Name });
            var localVariables = project.VariableList.ObjectVariableList.ObjectVariableEntries.ToDictionary(
                keySelector: entry => entry.Sprite,
                elementSelector: entry => entry.VariableList.UserVariables.ToDictionary(
                    keySelector: variable => variable,
                    elementSelector: variable => new LocalVariable { Name = variable.Name }));
            var sprites = project.SpriteList.Sprites.ToDictionary(
                keySelector: sprite => sprite,
                elementSelector: sprite =>
                    new Sprite
                    {
                        Name = sprite.Name, 
                        Costumes = sprite.Costumes.Costumes.Select(costume => new Costume
                        {
                            Name = costume.Name, 
                            FileName = costume.FileName
                        }).ToObservableCollection(), 
                        Sounds = sprite.Sounds.Sounds.Select(sound => new Sound
                        {
                            Name = sound.Name, 
                            FileName = sound.FileName
                        }).ToObservableCollection(), 
                        // Scripts = sprite.Scripts.Scripts.Select(script => script.ToModel()).ToObservableCollection(), 
                        LocalVariables = localVariables[sprite].Values.ToObservableCollection()
                    });
            return new Project
            {
                Name = project.ProjectHeader.ProgramName,
                Description = project.ProjectHeader.Description,
                GlobalVariables = globalVariables.Values.ToObservableCollection(), 
                Sprites = sprites.Values.ToObservableCollection()
            };
        }

        private Script Convert(XmlScript script, IDictionary<XmlUserVariable, LocalVariable> localVariables, IDictionary<XmlUserVariable, GlobalVariable> globalVariables)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region ConvertBack

        public XmlProject ConvertBack(Project project)
        {
            var globalVariables = project.GlobalVariables.ToDictionary(
                keySelector: variable => variable,
                elementSelector: variable => new XmlUserVariable { Name = variable.Name });
            var localVariables = project.Sprites.ToDictionary(
                keySelector: sprite => sprite,
                elementSelector: sprite => sprite.LocalVariables.ToDictionary(
                    keySelector: variable => variable,
                    elementSelector: variable => new XmlUserVariable { Name = variable.Name }));
            var sprites = project.Sprites.ToDictionary(
                keySelector: sprite => sprite,
                elementSelector: sprite => new XmlSprite
                {
                    Name = sprite.Name, 
                    Costumes  = new XmlCostumeList
                    {
                        Costumes = sprite.Costumes.Select(costume => new XmlCostume
                        {
                            Name = costume.Name, 
                            FileName = costume.FileName
                        }).ToObservableCollection()
                    }, 
                    Sounds = new XmlSoundList
                    {
                        Sounds = sprite.Sounds.Select(sound => new XmlSound
                        {
                            Name = sound.Name,
                            FileName = sound.FileName
                        }).ToObservableCollection()
                    }, 
                    Scripts = new XmlScriptList
                    {
                        Scripts = sprite.Scripts.Select(script => script.ToXml()).ToObservableCollection()
                    }
                });
            return new XmlProject
            {
                VariableList = new XmlVariableList
                {
                    ProgramVariableList = new XmlProgramVariableList
                    {
                        UserVariables = globalVariables.Values.ToObservableCollection()
                    },
                    ObjectVariableList = new XmlObjectVariableList
                    {
                        ObjectVariableEntries = localVariables.Select(entry => new XmlObjectVariableEntry
                        {
                            Sprite = sprites[entry.Key],
                            VariableList = new XmlUserVariableList
                            {
                                UserVariables = entry.Value.Values.ToObservableCollection()
                            }
                        }).ToObservableCollection()
                    }
                }
            };
        }

        #endregion
    }
}
