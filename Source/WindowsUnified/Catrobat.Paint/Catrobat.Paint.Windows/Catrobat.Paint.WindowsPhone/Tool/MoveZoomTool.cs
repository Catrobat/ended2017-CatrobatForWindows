using Catrobat.Paint.Phone.Tool;
using System;
using System.Windows;
using Windows.UI.Xaml.Media;

namespace Catrobat.Paint.WindowsPhone.Tool
{
    class MoveZoomTool : ToolBase
    {
        private readonly TransformGroup _transforms;


        public MoveZoomTool()
        {
            ToolType = ToolType.Move;
            ResetCanvas();
            // TODO: if (PocketPaintApplication.GetInstance().PaintingAreaCheckeredGrid.RenderTransform != null)
            {
                // TODO: _transforms = PocketPaintApplication.GetInstance().PaintingAreaCheckeredGrid.RenderTransform as TransformGroup;
            }
            if (_transforms == null)
            {
                // TODO: PocketPaintApplication.GetInstance().PaintingAreaCheckeredGrid.RenderTransform = _transforms = new TransformGroup();
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
                bool scale_allowed = false;
                // MessageBox.Show(resize.ScaleX.ToString() + ", " + resize.ScaleY.ToString());
                //Point point = PocketPaintApplication.GetInstance().PaintData.min_max_resize;

                var fixedaspection = 0.0;
                // TODO: double min_max_resize = PocketPaintApplication.GetInstance().PaintData.min_max_resize;
                double min_max_resize = 0.0;
                double boundary = 5.0;
                double boundary_zoom_out = -2.5;

                // TODO: PocketPaintApplication.GetInstance().PaintData.min_max_resize = Math.Round(PocketPaintApplication.GetInstance().PaintData.min_max_resize, 1);
                //MessageBox.Show(PocketPaintApplication.GetInstance().PaintData.min_max_resize.ToString());
                fixedaspection = resize.ScaleX > resize.ScaleY ? resize.ScaleX : resize.ScaleY;

                if (fixedaspection > 1.00)
                {

                    if (min_max_resize < boundary)
                    {
                        if (min_max_resize + fixedaspection > boundary)
                        {
                            fixedaspection = (boundary - min_max_resize) > 1.0 ?
                                (boundary - min_max_resize) : 1.0 + (boundary - min_max_resize);
                            // TODO: PocketPaintApplication.GetInstance().PaintData.min_max_resize = boundary;
                        }
                        else
                        {
                            // TODO: PocketPaintApplication.GetInstance().PaintData.min_max_resize += fixedaspection - 1.0;
                        }
                        scale_allowed = true;
                    }
                }
                else
                {
                    if (min_max_resize > boundary_zoom_out)
                    {
                        double value = (((1 - fixedaspection)) * -1);
                        if (min_max_resize + value < boundary_zoom_out)
                        {
                            double result = ((boundary_zoom_out * -1) - (min_max_resize * -1));
                            /*fixedaspection =  result < 1.0 ?
                                (1 - result) : (1.0 + (-boundary_zoom_out - min_max_resize) * -1);*/
                            fixedaspection = (1 - result);
                            // TODO: PocketPaintApplication.GetInstance().PaintData.min_max_resize = boundary_zoom_out;
                        }
                        else
                        {
                            double merke = (1.0 - fixedaspection);
                            //double merke_2 = (1.0 + merke);
                            double merke_3 = merke * -1;
                            // TODO: PocketPaintApplication.GetInstance().PaintData.min_max_resize += merke_3;
                        }
                        scale_allowed = true;
                    }
                }

                if (scale_allowed)
                {
                    fixedaspection = Math.Round(fixedaspection, 1);
                    resize.ScaleX = Math.Round(0.0 + fixedaspection, 1);
                    resize.ScaleY = Math.Round(0.0 + fixedaspection, 1);
                    
                    _transforms.Children.Add(resize);
                }

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
    }
}
