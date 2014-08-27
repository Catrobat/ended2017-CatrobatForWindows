//using System;
//using System.Collections.Generic;
//using System.Collections.ObjectModel;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using Catrobat.IDE.Core.Models;
//using Catrobat.IDE.Core.Services;
//using Catrobat.IDE.Core.Xml.XmlObjects;
//using Catrobat.IDE.Core.ExtensionMethods;
//using Catrobat.IDE.Core.Xml.XmlObjects.Variables;
//using ContextBase = Catrobat.IDE.Core.XmlModelConvertion.XmlModelConvertContextBase;
//using Context = Catrobat.IDE.Core.XmlModelConvertion.XmlModelConvertContext;
//using BackContextBase = Catrobat.IDE.Core.XmlModelConvertion.XmlModelConvertBackContextBase;
//using BackContext = Catrobat.IDE.Core.XmlModelConvertion.XmlModelConvertBackContext;
//// TODO replace ToModel / ToXmlObject

//namespace Catrobat.IDE.Core.XmlModelConvertion.Converters
//{
//    public class ProgramConverter : XmlModelConverter<XmlProgram, Program>
//    {
//        public ProgramConverter(IXmlModelConversionService converter) : base(converter)
//        {
//        }

//        public override Program Convert(XmlProgram o, XmlModelConvertContext c)
//        {
//            //var localVariables = o.VariableList.ObjectVariableList.ObjectVariableEntries.ToReadOnlyDictionary(
//            //    keySelector: entry => entry.Sprite,
//            //    elementSelector: entry => entry.VariableList.UserVariables.ToReadOnlyDictionary(
//            //        keySelector: variable => variable,
//            //        elementSelector: variable => variable.ToModel<LocalVariable>()));
//            //var globalVariables = o.VariableList.ProgramVariableList.UserVariables.ToReadOnlyDictionary(
//            //    keySelector: variable => variable,
//            //    elementSelector: variable => variable.ToModel<GlobalVariable>());
//            //var contextBase = new ContextBase(o, globalVariables);
//            //var sprites = o.SpriteList.Sprites.ToReadOnlyDictionary(
//            //    keySelector: sprite => sprite,
//            //    elementSelector: sprite =>
//            //    {
//            //        ReadOnlyDictionary<XmlUserVariable, LocalVariable> localVariables2;
//            //        if (!localVariables.TryGetValue(sprite, out localVariables2))
//            //        {
//            //            localVariables2 = new ReadOnlyDictionary<XmlUserVariable, LocalVariable>(new Dictionary<XmlUserVariable, LocalVariable>());
//            //        }
//            //        return sprite.ToModel(new Context(contextBase, sprite,
//            //            sprite.Looks == null || sprite.Looks.Looks == null
//            //                ? null
//            //                : sprite.Looks.Looks.ToReadOnlyDictionary(
//            //                    keySelector: look => look,
//            //                    elementSelector: look => look.ToModel()),
//            //            sprite.Sounds == null || sprite.Sounds.Sounds == null
//            //                ? null
//            //                : sprite.Sounds.Sounds.ToReadOnlyDictionary(
//            //                    keySelector: sound => sound,
//            //                    elementSelector: sound => sound.ToModel()),
//            //            localVariables2));
//            //    });
//            //return new Program
//            //{
//            //    Name = o.ProjectHeader.ProgramName,
//            //    Description = o.ProjectHeader.Description,
//            //    UploadHeader = o.ProjectHeader.ToModel(),
//            //    GlobalVariables = o.VariableList.ProgramVariableList.UserVariables.Select(variable => globalVariables[variable]).ToObservableCollection(),
//            //    BroadcastMessages = contextBase.BroadcastMessages.Values.ToObservableCollection(),
//            //    Sprites = o.SpriteList.Sprites.Select(sprite => sprites[sprite]).ToObservableCollection()
//            //};
//            throw new NotImplementedException();
//        }

//        public override XmlProgram Convert(Program m, XmlModelConvertBackContext c)
//        {
//            //var localVariables = m.Sprites.ToReadOnlyDictionary(
//            //    keySelector: sprite => sprite,
//            //    elementSelector: sprite => sprite.LocalVariables.ToReadOnlyDictionary(
//            //        keySelector: variable => variable,
//            //        elementSelector: variable => variable.ToXmlObject()));
//            //var globalVariables = m.GlobalVariables.ToReadOnlyDictionary(
//            //    keySelector: variable => variable,
//            //    elementSelector: variable => variable.ToXmlObject());
//            //var contextBase = new BackContextBase(m, globalVariables);
//            //var sprites = m.Sprites.ToReadOnlyDictionary(
//            //    keySelector: sprite => sprite,
//            //    elementSelector: sprite =>
//            //    {
//            //        ReadOnlyDictionary<LocalVariable, XmlUserVariable> localVariables2;
//            //        if (!localVariables.TryGetValue(sprite, out localVariables2))
//            //        {
//            //            localVariables2 = new ReadOnlyDictionary<LocalVariable, XmlUserVariable>(new Dictionary<LocalVariable, XmlUserVariable>());
//            //        }
//            //        return sprite.ToXmlObject(new BackContext(contextBase, sprite,
//            //            sprite.Looks == null
//            //                ? null
//            //                : sprite.Looks.ToReadOnlyDictionary(
//            //                    keySelector: look => look,
//            //                    elementSelector: look => look.ToXmlObject()),
//            //            sprite.Sounds == null
//            //                ? null
//            //                : sprite.Sounds.ToReadOnlyDictionary(
//            //                    keySelector: sound => sound,
//            //                    elementSelector: sound => sound.ToXmlObject()),
//            //            localVariables2));
//            //    });
//            //var header = UploadHeader.ToXmlObject();
//            //header.ProgramName = m.Name;
//            //header.Description = m.Description;
//            //var result = new XmlProgram
//            //{
//            //    ProjectHeader = header,
//            //    VariableList = new XmlVariableList
//            //    {
//            //        ProgramVariableList = new XmlProgramVariableList
//            //        {
//            //            UserVariables = m.GlobalVariables == null ? new List<XmlUserVariable>() : m.GlobalVariables.Select(variable => globalVariables[variable]).ToList()
//            //        },
//            //        ObjectVariableList = new XmlObjectVariableList
//            //        {
//            //            ObjectVariableEntries = m.Sprites.Select(sprite => new XmlObjectVariableEntry
//            //            {
//            //                Sprite = sprites[sprite],
//            //                VariableList = new XmlUserVariableList
//            //                {
//            //                    UserVariables = sprite.LocalVariables == null
//            //                        ? new List<XmlUserVariable>()
//            //                        : sprite.LocalVariables.Select(variable => localVariables[sprite][variable]).ToList()
//            //                }
//            //            }).ToList()
//            //        }
//            //    },
//            //    SpriteList = new XmlSpriteList
//            //    {
//            //        Sprites = m.Sprites.Select(sprite => sprites[sprite]).ToList()
//            //    }
//            //};

//            //ServiceLocator.ContextService.UpdateProgramHeader(result);

//            //return result;
//            throw new NotImplementedException();
//        }
//    }
//}
