using Catrobat.Paint.WindowsPhone.Tool;

namespace Catrobat.Paint.Phone.Command
{
    public abstract class CommandBase
    {
        public ToolType ToolType { get; protected set; }
        public abstract bool  ReDo();
        public abstract bool UnDo();
    }
}
