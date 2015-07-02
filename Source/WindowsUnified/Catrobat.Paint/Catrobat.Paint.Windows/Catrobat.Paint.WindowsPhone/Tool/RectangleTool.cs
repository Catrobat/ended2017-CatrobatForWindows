using Catrobat.Paint.WindowsPhone.Command;
using Windows.Foundation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Shapes;

namespace Catrobat.Paint.WindowsPhone.Tool
{
    class RectangleTool : ToolBase
    {
        private Path _path;

        public RectangleTool()
        {
            this.ToolType = ToolType.Rect;
        }

        public override void HandleDown(object arg)
        {

        }

        public override void HandleMove(object arg)
        {
        }

        public override void HandleUp(object arg)
        {
        }

        public override void Draw(object o)
        {
        }

        public override void ResetDrawingSpace()
        {
        }
    }
}
