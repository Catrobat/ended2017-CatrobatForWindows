using System.Windows.Shapes;
using Catrobat.Paint.Phone.Tool;

namespace Catrobat.Paint.Phone.Command
{
    class BrushCommand : CommandBase
    {
        private Path Path { get; set; }

        public BrushCommand(Path path)
        {
            ToolType = ToolType.Brush;
            Path = path;
        }

        public override bool ReDo()
        {
            PocketPaintApplication.GetInstance().PaintingAreaCanvas.Children.Add(Path);
            return true;
        }

        public override bool UnDo()
        {
            if (PocketPaintApplication.GetInstance().PaintingAreaCanvas.Children.Contains(Path))
            {
                PocketPaintApplication.GetInstance().PaintingAreaCanvas.Children.Remove(Path);
                return true;                
            }
            return false;
        }
    }
}
