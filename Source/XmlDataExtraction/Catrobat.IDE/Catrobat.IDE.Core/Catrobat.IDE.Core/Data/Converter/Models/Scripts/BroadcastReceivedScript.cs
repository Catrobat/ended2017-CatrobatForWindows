using Catrobat.Data.Xml.XmlObjects.Scripts;
using Catrobat.IDE.Core.ExtensionMethods;
using Context = Catrobat.IDE.Core.Xml.Converter.XmlProjectConverter.ConvertBackContext;

// ReSharper disable once CheckNamespace
namespace Catrobat.IDE.Core.Models.Scripts
{
    partial class BroadcastReceivedScript
    {
        protected internal override XmlScript ToXmlObject2(Context context)
        {
            return new XmlBroadcastScript
            {
                ReceivedMessage = Message == null ? string.Empty : Message.ToXmlObject()
            };
        }
    }
}
