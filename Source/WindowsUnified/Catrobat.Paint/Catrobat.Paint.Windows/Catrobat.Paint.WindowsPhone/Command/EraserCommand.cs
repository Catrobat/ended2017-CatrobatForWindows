using Catrobat.Paint.WindowsPhone.Tool;
using System.Collections.Generic;
using Windows.Foundation;
using Windows.UI.Xaml.Shapes;

namespace Catrobat.Paint.WindowsPhone.Command
{
    class EraserCommand : CommandBase
    {
        private List<Point> _points = null;
        public EraserCommand(List<Point> points)
        {
            ToolType = ToolType.Eraser;
            _points = points;
        }

        public override bool ReDo()
        {
            PocketPaintApplication.GetInstance().ToolCurrent.Draw(_points);
            return true;
        }

        public override bool UnDo()
        {
            return true;
        }
    }
}
