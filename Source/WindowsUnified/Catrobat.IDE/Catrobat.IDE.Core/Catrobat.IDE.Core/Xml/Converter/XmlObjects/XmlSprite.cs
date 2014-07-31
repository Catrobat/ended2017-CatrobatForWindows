using System.Collections.ObjectModel;
using Catrobat.IDE.Core.ExtensionMethods;
using Catrobat.IDE.Core.Models;
using Catrobat.IDE.Core.Models.Scripts;
using Catrobat.IDE.Core.Xml.Converter;
using System.Linq;
using Context = Catrobat.IDE.Core.Xml.Converter.XmlProjectConverter.ConvertContext;

// ReSharper disable once CheckNamespace
namespace Catrobat.IDE.Core.Xml.XmlObjects
{
    partial class XmlSprite : IModelConvertibleCyclic<Sprite, Context>
    {
        Sprite IModelConvertibleCyclic<Sprite, Context>.ToModel(Context context, bool pointerOnly)
        {
            // prevents endless loops
            Sprite result;
            if (!context.Sprites.TryGetValue(this, out result))
            {
                result = new Sprite {Name = Name};
                context.Sprites[this] = result;
            }
            if (pointerOnly) return result;

            var localVariables = context.Project.VariableList.ObjectVariableList.ObjectVariableEntries.FirstOrDefault(entry => entry.Sprite == this);
            result.Looks = context.Looks == null 
                ? new ObservableCollection<Look>() 
                : context.Looks.Values.ToObservableCollection();
            result.Sounds = context.Sounds == null 
                ? new ObservableCollection<Sound>() 
                : context.Sounds.Values.ToObservableCollection();
            result.LocalVariables = localVariables == null || localVariables.VariableList == null || localVariables.VariableList.UserVariables == null 
                ? new ObservableCollection<LocalVariable>() 
                : localVariables.VariableList.UserVariables.Select(variable => context.LocalVariables[variable]).ToObservableCollection();
            context.Sprites[this] = result;
            result.Scripts = Scripts == null || Scripts.Scripts == null
                ? new ObservableCollection<Script>()
                : Scripts.Scripts.Select(script => script.ToModel(context)).ToObservableCollection();
            return result;
        }
    }
}
