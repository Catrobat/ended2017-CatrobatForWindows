using Catrobat.IDE.Core.Models;
using Catrobat.IDE.Core.Models.Scripts;
using Context = Catrobat.IDE.Core.Xml.Converter.XmlProgramConverter.ConvertContext;

// ReSharper disable once CheckNamespace
namespace Catrobat.IDE.Core.Xml.XmlObjects.Scripts
{
    partial class XmlBroadcastScript
    {
        protected internal override Script ToModel2(Context context)
        {
            BroadcastMessage message = null;
            if (ReceivedMessage != null)
            {
                if (!context.BroadcastMessages.TryGetValue(ReceivedMessage, out message))
                {
                    message = new BroadcastMessage
                    {
                        Content = ReceivedMessage
                    };
                    context.BroadcastMessages.Add(ReceivedMessage, message);
                }
            }
            return new BroadcastReceivedScript
            {
                Message = message
            };
        }
    }
}