using Catrobat.IDE.Core.Xml.Scripts;

// ReSharper disable once CheckNamespace
namespace Catrobat.IDE.Core.Models.Scripts
{
    public partial class BroadcastReceivedScript
    {
        protected override XmlScript ToXml2()
        {
            return new XmlBroadcastScript
            {
                ReceivedMessage = Message
            };
        }
    }
}
