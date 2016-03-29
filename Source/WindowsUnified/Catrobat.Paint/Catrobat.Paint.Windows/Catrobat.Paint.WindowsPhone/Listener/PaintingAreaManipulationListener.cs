using Catrobat.Paint.WindowsPhone.Ui;
using Catrobat.Paint.WindowsPhone.Tool;
using System;
using Windows.Foundation;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;

namespace Catrobat.Paint.WindowsPhone.Listener
{
    class PaintingAreaManipulationListener 
    {
        Point _lastPoint = new Point(0.0, 0.0);
        RotateTransform rotate = new RotateTransform();

        public void ManipulationStarting(object sender, ManipulationStartingRoutedEventArgs e)
        {
            e.Mode.ToString();
        }
        
        public void ManipulationStarted(object sender, ManipulationStartedRoutedEventArgs e)
        {
            var point = new Point(Convert.ToInt32(e.Position.X), Convert.ToInt32(e.Position.Y));

            // TODO some bubbling? issue here, fast multiple applicationbartop undos result in triggering this event
            if (point.X < 0 || point.Y < 0 || Spinner.SpinnerActive || e.Handled)
            {
                return;
            }

            PocketPaintApplication.GetInstance().ToolCurrent.HandleDown(point);
        }

        public void ManipulationDelta(object sender, ManipulationDeltaRoutedEventArgs e)
        {
            var point = new Point(Convert.ToInt32(e.Position.X), Convert.ToInt32(e.Position.Y));

            // TODO some bubbling? issue here, fast multiple applicationbartop undos result in triggering this event
            if (point.X < 0 || point.Y < 0 || Spinner.SpinnerActive || e.Handled)
            {
                return;
            }


            object movezoom = null;

            RotateTransform rotate = new RotateTransform();
            Point rotateCenterPoint = new Point();
            if (PocketPaintApplication.GetInstance().ToolCurrent.GetToolType() == ToolType.Rect)
            {
                // TODO: @Karl: implement rotation for the rectangle tool
                //rotateCenterPoint.X = PocketPaintApplication.GetInstance().RectangleSelectionControl.MainGrid.Width / 2.0;
                //rotateCenterPoint.Y = PocketPaintApplication.GetInstance().RectangleSelectionControl.MainGrid.Height / 2.0;

                //rotate.CenterX = rotateCenterPoint.X;
                //rotate.CenterY = rotateCenterPoint.Y;

                //Point centerPoint = PocketPaintApplication.GetInstance().RectangleSelectionControl.getCenterCoordinateOfGridMain();

                //if (!(lastPoint.X == 0.0 && lastPoint.Y == 0.0) &&
                //    (lastPoint.X != point.X || lastPoint.Y != point.Y))
                //{
                //    double currentXLength = point.X - centerPoint.X;
                //    double currentYLength = point.Y - centerPoint.Y;
                //    double normalCurrentX = currentXLength / (Math.Sqrt(currentXLength * currentXLength + currentYLength * currentYLength));
                //    double normalCurrentY = currentYLength / (Math.Sqrt(currentXLength * currentXLength + currentYLength * currentYLength));

                //    double previousXLength = lastPoint.X - centerPoint.X;
                //    double previousYLength = lastPoint.Y - centerPoint.Y;
                //    double normalPreviousX = previousXLength / (Math.Sqrt(previousXLength * previousXLength + previousYLength * previousYLength));
                //    double normalPreviousY = previousYLength / (Math.Sqrt(previousXLength * previousXLength + previousYLength * previousYLength));

                //    double deltaAngle = (Math.Atan(normalPreviousX / normalPreviousY) - Math.Atan(normalCurrentX / normalCurrentY));
                //    double rotationAngle = deltaAngle * 360.0 / Math.PI;

                //    rotate.Angle = rotationAngle;
                //}
                lastPoint = point;
            }
            else if(PocketPaintApplication.GetInstance().ToolCurrent.GetToolType() == ToolType.Ellipse)
            {
                // TODO: @Karl: implement rotation for the ellipse tool
                //rotateCenterPoint.X = PocketPaintApplication.GetInstance().EllipseSelectionControl.gridMain.Width / 2.0;
                //rotateCenterPoint.Y = PocketPaintApplication.GetInstance().EllipseSelectionControl.gridMain.Height / 2.0;

                //rotate.CenterX = rotateCenterPoint.X;
                //rotate.CenterY = rotateCenterPoint.Y;

                //Point centerPoint = PocketPaintApplication.GetInstance().EllipseSelectionControl.getCenterCoordinateOfGridMain();

                //if (!(lastPoint.X == 0.0 && lastPoint.Y == 0.0) &&
                //    (lastPoint.X != point.X || lastPoint.Y != point.Y))
                //{
                //    double currentXLength = point.X - centerPoint.X;
                //    double currentYLength = point.Y - centerPoint.Y;
                //    double normalCurrentX = currentXLength / (Math.Sqrt(currentXLength * currentXLength + currentYLength * currentYLength));
                //    double normalCurrentY = currentYLength / (Math.Sqrt(currentXLength * currentXLength + currentYLength * currentYLength));

                //    double previousXLength = lastPoint.X - centerPoint.X;
                //    double previousYLength = lastPoint.Y - centerPoint.Y;
                //    double normalPreviousX = previousXLength / (Math.Sqrt(previousXLength * previousXLength + previousYLength * previousYLength));
                //    double normalPreviousY = previousYLength / (Math.Sqrt(previousXLength * previousXLength + previousYLength * previousYLength));

                //    double deltaAngle = (Math.Atan(normalPreviousX / normalPreviousY) - Math.Atan(normalCurrentX / normalCurrentY));
                //    double rotationAngle = deltaAngle * 180.0 / Math.PI;

                //    rotate.Angle = rotationAngle;
                //}
                //lastPoint = point;
            }
            else if(PocketPaintApplication.GetInstance().ToolCurrent.GetToolType() == ToolType.ImportPng)
            {
                // TODO: @Karl: implement rotation for the import picture tool
                //rotateCenterPoint.X = PocketPaintApplication.GetInstance().RectangleSelectionControl.gridMain.Width / 2.0;
                //rotateCenterPoint.Y = PocketPaintApplication.GetInstance().RectangleSelectionControl.gridMain.Height / 2.0;

                //rotate.CenterX = rotateCenterPoint.X;
                //rotate.CenterY = rotateCenterPoint.Y;

                //Point centerPoint = PocketPaintApplication.GetInstance().ImportImageSelectionControl.getCenterPointOfSelectionControl();

                //if (!(lastPoint.X == 0.0 && lastPoint.Y == 0.0) &&
                //    (lastPoint.X != point.X || lastPoint.Y != point.Y))
                //{
                //    double currentXLength = point.X - centerPoint.X;
                //    double currentYLength = point.Y - centerPoint.Y;
                //    double normalCurrentX = currentXLength / (Math.Sqrt(currentXLength * currentXLength + currentYLength * currentYLength));
                //    double normalCurrentY = currentYLength / (Math.Sqrt(currentXLength * currentXLength + currentYLength * currentYLength));

                //    double previousXLength = lastPoint.X - centerPoint.X;
                //    double previousYLength = lastPoint.Y - centerPoint.Y;
                //    double normalPreviousX = previousXLength / (Math.Sqrt(previousXLength * previousXLength + previousYLength * previousYLength));
                //    double normalPreviousY = previousYLength / (Math.Sqrt(previousXLength * previousXLength + previousYLength * previousYLength));

                //    double deltaAngle = (Math.Atan(normalPreviousX / normalPreviousY) - Math.Atan(normalCurrentX / normalCurrentY));
                //    double rotationAngle = deltaAngle * 360.0 / Math.PI;

                //    rotate.Angle = rotationAngle;
                //}
                //lastPoint = point;

            }
            // TODO: @Karl: implement rotation for the stamp tool
            else
            {
                if (e.Delta.Scale != 1.0)
                {
                    movezoom = new ScaleTransform();
                    if (e.Delta.Scale > 0)
                    {
                        System.Diagnostics.Debug.WriteLine("Scale " + e.Delta.Scale);
                        ((ScaleTransform)movezoom).ScaleX *= e.Delta.Scale;
                        ((ScaleTransform)movezoom).ScaleY *= e.Delta.Scale;
                    }
                }
                else
                {
                    movezoom = new TranslateTransform();

                    ((TranslateTransform)movezoom).X += e.Delta.Translation.X;
                    ((TranslateTransform)movezoom).Y += e.Delta.Translation.Y;
                }
            }
            

            switch (PocketPaintApplication.GetInstance().ToolCurrent.GetToolType())
            {
                case ToolType.Brush:
                    PocketPaintApplication.GetInstance().ToolCurrent.HandleMove(point);
                    break;
                case ToolType.Cursor:
                    PocketPaintApplication.GetInstance().ToolCurrent.HandleMove(movezoom);
                    PocketPaintApplication.GetInstance().ToolCurrent.HandleMove(point);
                    break;
                case ToolType.Ellipse:
                    if (rotate.Angle != 0.0)
                    {
                        PocketPaintApplication.GetInstance().ToolCurrent.HandleMove(rotate);
                    }
                    break;
                case ToolType.Eraser:
                    PocketPaintApplication.GetInstance().ToolCurrent.HandleMove(point);
                    break;
                case ToolType.ImportPng:
                    if (rotate.Angle != 0.0)
                    {
                        PocketPaintApplication.GetInstance().ToolCurrent.HandleMove(rotate);
                    }
                    break;
                case ToolType.Line:
                    PocketPaintApplication.GetInstance().ToolCurrent.HandleMove(point);
                    break;
                case ToolType.Move:
                    PocketPaintApplication.GetInstance().ToolCurrent.HandleMove(movezoom);
                    break;
                case ToolType.Rect:
                    if (rotate.Angle != 0.0)
                    {
                        PocketPaintApplication.GetInstance().ToolCurrent.HandleMove(rotate);
                    }
                    break;
                case ToolType.Zoom:
                    PocketPaintApplication.GetInstance().ToolCurrent.HandleMove(movezoom);
                    break;
            }
        }


        public void ManipulationCompleted(object sender, ManipulationCompletedRoutedEventArgs e)
        {
            var point = new Point(Convert.ToInt32(e.Position.X), Convert.ToInt32(e.Position.Y));

            // TODO some bubbling? issue here, fast multiple applicationbartop undos result in triggering this event
            if (point.X < 0 || point.Y < 0 || Spinner.SpinnerActive || e.Handled)
            {
                return;
            }

            PocketPaintApplication.GetInstance().ToolCurrent.HandleUp(point);        
        }

        public Point lastPoint
        {
            get
            {
                return _lastPoint;
            }
            set
            {
                _lastPoint = value;
            }
        }

        public void ResetDrawingSpace()
        {
            PocketPaintApplication.GetInstance().ToolCurrent.ResetDrawingSpace();
        }
    }
}
