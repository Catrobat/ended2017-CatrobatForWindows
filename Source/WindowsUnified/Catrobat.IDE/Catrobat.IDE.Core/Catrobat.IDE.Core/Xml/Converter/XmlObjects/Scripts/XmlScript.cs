using Catrobat.IDE.Core.ExtensionMethods;
using Catrobat.IDE.Core.Models.Scripts;
using Catrobat.IDE.Core.Xml.Converter;
using System.Linq;
using Context = Catrobat.IDE.Core.Xml.Converter.XmlProgramConverter.ConvertContext;

// ReSharper disable once CheckNamespace
namespace Catrobat.IDE.Core.Xml.XmlObjects.Scripts
{
    partial class XmlScript : IModelConvertible<Script, Context>
    {
        Script IModelConvertible<Script, Context>.ToModel(Context context)
        {
            var result = ToModel2(context);
            result.Bricks = Bricks == null || Bricks.Bricks == null
                ? null 
                : Bricks.Bricks.Select(brick => brick.ToModel(context)).ToObservableCollection();
            return result;
        }

        protected internal abstract Script ToModel2(Context context);
    }
}