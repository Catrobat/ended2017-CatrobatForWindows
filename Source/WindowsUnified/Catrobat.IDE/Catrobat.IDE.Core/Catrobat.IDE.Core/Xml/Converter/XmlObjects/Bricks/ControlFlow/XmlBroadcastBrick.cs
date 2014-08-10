using Catrobat.IDE.Core.Models;
using Catrobat.IDE.Core.Models.Bricks;
using Context = Catrobat.IDE.Core.Xml.Converter.XmlProgramConverter.ConvertContext;

// ReSharper disable once CheckNamespace
namespace Catrobat.IDE.Core.Xml.XmlObjects.Bricks.ControlFlow
{
    partial class XmlBroadcastBrick
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
            return new BroadcastSendBrick
            {
                Message = message
            };
        }
    }
}