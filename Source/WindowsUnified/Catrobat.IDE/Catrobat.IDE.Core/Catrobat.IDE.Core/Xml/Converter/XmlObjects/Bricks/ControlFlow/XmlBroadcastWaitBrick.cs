using Catrobat.IDE.Core.Models;
using Catrobat.IDE.Core.Models.Bricks;
using Context = Catrobat.IDE.Core.Xml.Converter.XmlProjectConverter.ConvertContext;

// ReSharper disable once CheckNamespace
namespace Catrobat.IDE.Core.Xml.XmlObjects.Bricks.ControlFlow
{
    partial class XmlBroadcastWaitBrick
    {
        protected override Brick ToModel2(Context context)
        {
            BroadcastMessage message = null;
            if (BroadcastMessage != null)
            {
                if (!context.BroadcastMessages.TryGetValue(BroadcastMessage, out message))
                {
                    message = new BroadcastMessage
                    {
                        Content = BroadcastMessage
                    };
                    context.BroadcastMessages.Add(BroadcastMessage, message);
                }
            }
            return new BroadcastSendBlockingBrick
            {
                Message = message
            };
        }
    }
}