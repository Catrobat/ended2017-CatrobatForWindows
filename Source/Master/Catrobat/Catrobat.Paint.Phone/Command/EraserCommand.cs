using System.Windows.Shapes;
using Catrobat.Paint.Phone.Tool;

namespace Catrobat.Paint.Phone.Command
{
    class EraserCommand : CommandBase
    {       
        private Path Path { get; set; }

        public EraserCommand(Path path)
        {
            ToolType = ToolType.Eraser;
            Path = path;
        }

        public override bool ReDo()
        {
            var e = new EraserTool();
            e.Draw(Path);
            return true;
        }

        public override bool UnDo()
        {
            return false;
        }
    }
}
