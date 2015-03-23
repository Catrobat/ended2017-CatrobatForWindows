using Catrobat.Paint.WindowsPhone.Tool;
using Windows.UI.Xaml.Shapes;

namespace Catrobat.Paint.WindowsPhone.Command
{
    class FillCommand : CommandBase
    {
        private Path Path { get; set; }
        private byte[] _oldCanvas { get; set; }
        private byte[] _newCanvas { get; set; }

        public FillCommand(byte[] oldCanvas, byte[] newCanvas)
        {
            ToolType = ToolType.Fill;
            _oldCanvas = oldCanvas;
            _newCanvas = newCanvas;
        }

        public override bool ReDo()
        {
            if (!PocketPaintApplication.GetInstance().PaintingAreaCanvas.Children.Contains(Path))
            {
                PocketPaintApplication.GetInstance().PaintingAreaCanvas.Children.Add(Path);
                return true;
            }
            return false;
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
