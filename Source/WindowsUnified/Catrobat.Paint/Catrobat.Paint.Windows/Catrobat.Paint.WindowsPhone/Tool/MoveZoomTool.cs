using Catrobat.Paint.Phone;
using Catrobat.Paint.Phone.Tool;
using System;
using System.Windows;
using Windows.UI.Popups;
using Windows.UI.Xaml.Media;

namespace Catrobat.Paint.WindowsPhone.Tool
{
    class MoveZoomTool : ToolBase
    {
        private TransformGroup _transforms;


        public MoveZoomTool()
        {
            ToolType = ToolType.Move;
            ResetCanvas();
            if (PocketPaintApplication.GetInstance().PaintingAreaCheckeredGrid.RenderTransform != null)
            {
                _transforms = PocketPaintApplication.GetInstance().PaintingAreaCheckeredGrid.RenderTransform as TransformGroup;
            }
            if (_transforms == null)
            {
                PocketPaintApplication.GetInstance().PaintingAreaCheckeredGrid.RenderTransform = _transforms = new TransformGroup();
            }
        }
        public override void HandleDown(object arg)
        {

        }

        public override void HandleMove(object arg)
        {
            if (NeedToResetCanvas)
            {
                ResetCanvas();
            }

            if (arg is ScaleTransform)
            {
                var resize = (ScaleTransform)arg;
                resize.ScaleX = Math.Round(resize.ScaleX, 1);
                resize.ScaleY = Math.Round(resize.ScaleY, 1);

                var fixedaspection = 0.0;
                PocketPaintApplication.GetInstance().PaintData.min_max_resize = Math.Round(PocketPaintApplication.GetInstance().PaintData.min_max_resize, 1);
                fixedaspection = resize.ScaleX > resize.ScaleY ? resize.ScaleX : resize.ScaleY;

                fixedaspection = Math.Round(fixedaspection, 1);
                resize.ScaleX = Math.Round(0.0 + fixedaspection, 1);
                resize.ScaleY = Math.Round(0.0 + fixedaspection, 1);
                    
                _transforms.Children.Add(resize);
            }
            else if (arg is TranslateTransform)
            {
                var move = (TranslateTransform)arg;
                _transforms.Children.Add(move);
            }
            else
            {
                System.Diagnostics.Debug.WriteLine("MoveZoomTool Should Not Reach this!");
                return;
            }





            //            System.Diagnostics.Debug.WriteLine("MoveZoomTool Canvas: Actual " + PocketPaintApplication.GetInstance().PaintingAreaCanvas.ActualHeight + " " + PocketPaintApplication.GetInstance().PaintingAreaCanvas.ActualWidth + " Rendered " + PocketPaintApplication.GetInstance().PaintingAreaCanvas.RenderSize.Height + " " + PocketPaintApplication.GetInstance().PaintingAreaCanvas.RenderSize.Width);
            //            System.Diagnostics.Debug.WriteLine("MoveZoomTool Canvas2: Actual " + PocketPaintApplication.GetInstance().PaintingAreaCanvasUnderlaying.ActualHeight + " " + PocketPaintApplication.GetInstance().PaintingAreaCanvasUnderlaying.ActualWidth + " Rendered " + PocketPaintApplication.GetInstance().PaintingAreaCanvasUnderlaying.RenderSize.Height + " " + PocketPaintApplication.GetInstance().PaintingAreaCanvasUnderlaying.RenderSize.Width);
            //            System.Diagnostics.Debug.WriteLine("MoveZoomTool Grid: Actual " + PocketPaintApplication.GetInstance().PaintingAreaCheckeredGrid.ActualHeight + " " + PocketPaintApplication.GetInstance().PaintingAreaCheckeredGrid.ActualWidth + " Rendered " + PocketPaintApplication.GetInstance().PaintingAreaCheckeredGrid.RenderSize.Height + " " + PocketPaintApplication.GetInstance().PaintingAreaCheckeredGrid.RenderSize.Width);


        }

        public override void HandleUp(object arg)
        {

        }

        public override void Draw(object o)
        {
            throw new NotImplementedException();
        }

        public override void ResetDrawingSpace()
        {
            _transforms.Children.Clear();
        }
    }
}
