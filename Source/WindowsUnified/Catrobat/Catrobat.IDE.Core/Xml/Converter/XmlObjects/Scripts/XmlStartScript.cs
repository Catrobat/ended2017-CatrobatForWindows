using Catrobat.IDE.Core.Models.Scripts;
using Context = Catrobat.IDE.Core.Xml.Converter.XmlProjectConverter.ConvertContext;

// ReSharper disable once CheckNamespace
namespace Catrobat.IDE.Core.Xml.XmlObjects.Scripts
{
    partial class XmlStartScript
    {
        protected internal override Script ToModel2(Context context)
        {
            return new StartScript();
        }
    }
}