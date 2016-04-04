using Catrobat.Paint.WindowsPhone.Tool;
using System;
using Windows.Graphics.Imaging;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Shapes;

namespace Catrobat.Paint.WindowsPhone.Command
{
    class StampCommand : CommandBase
    {
        private uint _xCoordinateOnWorkingSpace = 0;
        private uint _yCoordinateOnWorkingSpace = 0;
        private Image _stampedImage;

        public StampCommand(uint xCoordinateOnWorkingSpace, uint yCoordinateOnWorkingSpace, Image stampedImage)
        {
            ToolType = ToolType.Stamp;
            _xCoordinateOnWorkingSpace = xCoordinateOnWorkingSpace;
            _yCoordinateOnWorkingSpace = yCoordinateOnWorkingSpace;
            _stampedImage = stampedImage;
        }

        public override bool ReDo()
        {
            try
            {
                StampTool stampTool = (StampTool)PocketPaintApplication.GetInstance().ToolCurrent;
                stampTool.StampPaste(_xCoordinateOnWorkingSpace, _yCoordinateOnWorkingSpace, _stampedImage);
                return true;
            }
            catch (Exception exception)
            {
                System.Diagnostics.Debug.WriteLine(exception.StackTrace);
                return false;
            }
        }

        public override bool UnDo()
        {
            if (PocketPaintApplication.GetInstance().PaintingAreaCanvas.Children.Contains(_stampedImage))
            {
                PocketPaintApplication.GetInstance().PaintingAreaCanvas.Children.Remove(_stampedImage);
                return true;
            }
            return false;           
        }

        public Windows.UI.Xaml.Media.ImageSource Url { get; set; }
    }
}
