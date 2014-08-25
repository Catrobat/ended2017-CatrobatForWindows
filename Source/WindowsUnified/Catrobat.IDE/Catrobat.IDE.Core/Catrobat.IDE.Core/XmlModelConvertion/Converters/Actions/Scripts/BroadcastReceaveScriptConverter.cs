using Catrobat.IDE.Core.Models;
using Catrobat.IDE.Core.Models.Scripts;
using Catrobat.IDE.Core.Xml.XmlObjects.Scripts;

namespace Catrobat.IDE.Core.XmlModelConvertion.Converters.Actions.Scripts
{
    public class BroadcastReceaveScriptConverter : ScriptConverterBase<XmlBroadcastScript, BroadcastReceivedScript>
    {
        public override BroadcastReceivedScript Convert1(XmlBroadcastScript o, XmlModelConvertContext c)
        {
            BroadcastMessage message = null;
            if (o.ReceivedMessage != null)
            {
                if (!c.BroadcastMessages.TryGetValue(o.ReceivedMessage, out message))
                {
                    message = new BroadcastMessage
                    {
                        Content = o.ReceivedMessage
                    };
                    c.BroadcastMessages.Add(o.ReceivedMessage, message);
                }
            }
            return new BroadcastReceivedScript
            {
                Message = message
            };
        }

        public override XmlBroadcastScript Convert1(BroadcastReceivedScript m, XmlModelConvertBackContext c)
        {
            return new XmlBroadcastScript
            {
                ReceivedMessage = m.Message == null ?
                string.Empty : m.Message.Content
            };
        }
    }
}
