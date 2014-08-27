using Catrobat.IDE.Core.Models;
using Catrobat.IDE.Core.Models.Bricks;
using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.Xml.XmlObjects.Bricks.ControlFlow;

namespace Catrobat.IDE.Core.XmlModelConvertion.Converters.Actions.Bricks
{
    public class BroadcastSendBrickConverter : BrickConverterBase<XmlBroadcastBrick, BroadcastSendBrick>
    {
        public BroadcastSendBrickConverter(IXmlModelConversionService converter)  
            : base(converter) { }

        public override BroadcastSendBrick Convert1(XmlBroadcastBrick o, XmlModelConvertContext c)
        {
            BroadcastMessage message = null;
            if (o.BroadcastMessage != null)
            {
                if (!c.BroadcastMessages.TryGetValue(o.BroadcastMessage, out message))
                {
                    message = new BroadcastMessage
                    {
                        Content = o.BroadcastMessage
                    };
                    c.BroadcastMessages.Add(o.BroadcastMessage, message);
                }
            }
            return new BroadcastSendBrick
            {
                Message = message
            };
        }

        public override XmlBroadcastBrick Convert1(BroadcastSendBrick m, XmlModelConvertBackContext c)
        {
            return new XmlBroadcastBrick
            {
                BroadcastMessage = m.Message == null ? string.Empty : m.Message.Content
            };
        }
    }
}
