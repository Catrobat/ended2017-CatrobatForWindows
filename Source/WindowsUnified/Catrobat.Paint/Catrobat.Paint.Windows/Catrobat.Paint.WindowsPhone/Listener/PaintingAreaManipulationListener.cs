using System;
using System.Windows;
using System.Windows.Input;
using Catrobat.Paint.Phone.Tool;
using Catrobat.Paint.Phone.Ui;
using Windows.UI.Input;
using Windows.Foundation;
using Windows.UI.Xaml.Media;
using Catrobat.Paint.WindowsPhone.Tool;
using Windows.UI.Xaml.Input;
using Windows.UI.Popups;
using Windows.UI.Xaml.Shapes;
using Windows.UI.Xaml;

namespace Catrobat.Paint.Phone.Listener
{
    class PaintingAreaManipulationListener 
    {
        Point lastPoint = new Point(0.0, 0.0);
        int lastAngleValue = 0;

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
       

            // b
            //double b = point.X - centerPoint.X;
            // a
            //double a = point.Y - centerPoint.Y;
            // c
            //double c = Math.Sqrt(a * a + b * b);
            // angle between A and C or from A to B
            //double angleAB = Math.Asin(a / c);
            //PocketPaintApplication.GetInstance().PaintingAreaView.changeTbTestboxText(angleAB, 0.0);

            //double displayCenterX = Window.Current.Bounds.Width / 2.0;
            //double displayCenterY = Window.Current.Bounds.Width / 2.0;
            //rectRectangleForMovement.Width / 2.0, rectRectangleForMovement.Height / 2.0

            //if (!(lastPoint.X == 0.0 && lastPoint.Y == 0.0) && 
            //     (lastPoint.X != point.X || lastPoint.Y != point.Y))
            //{
            //    if (point.Y < displayCenterY)
            //    {
            //        if (point.X < displayCenterX)
            //        {

            //        }
            //        else if (point.X > displayCenterX)
            //        {

            //        }
            //    }
            //    else if (point.Y > displayCenterY)
            //    {
            //        if (point.X < displayCenterX)
            //        {

            //        }
            //        else if (point.X > displayCenterX)
            //        {

            //        }

            //    }
            //}

            //lastPoint = point;

            RotateTransform rotate = new RotateTransform();

            if (PocketPaintApplication.GetInstance().ToolCurrent.GetToolType() == ToolType.Rect)
            {
                rotate.CenterX = PocketPaintApplication.GetInstance().GridRectangleSelectionControl.Width / 2.0;
                rotate.CenterY = PocketPaintApplication.GetInstance().GridRectangleSelectionControl.Height / 2.0;
                //rectRectangleForMovement.Width / 2.0, rectRectangleForMovement.Height / 2.0

                Point centerPoint = PocketPaintApplication.GetInstance().RectangleSelectionControl.getCenterPointOfSelectionControl();  
                
                if (!(lastPoint.X == 0.0 && lastPoint.Y == 0.0) &&
                    (lastPoint.X != point.X || lastPoint.Y != point.Y))
                {
                    double currentXLength = point.X - centerPoint.X;
                    double currentYLength = point.Y - centerPoint.Y;

                    double previousXLength = lastPoint.X - centerPoint.X;
                    double previousYLength = lastPoint.Y - centerPoint.Y;

                    double deltaAngle = (Math.Atan(previousXLength / previousYLength) - Math.Atan(currentXLength / currentYLength));
                    double rotationAngle = deltaAngle * 180 / Math.PI;

                    rotate.Angle += rotationAngle;
                }
                lastPoint = point;

                // TODO:
                //int angleValue = 5;
                //int currentWinkel = (int)((point.X / 384) * 360 + point.Y / (Window.Current.Bounds.Height - 144.0) * 360.0) / 2;

                //if (point.Y < rotate.CenterY && point.X < rotate.CenterX)
                //{
                //    int distanceY = (int)Math.Abs(Math.Abs(lastPoint.Y) - Math.Abs(point.Y));
                //    int distanceX = (int)Math.Abs(Math.Abs(lastPoint.X) - Math.Abs(point.X));
                //    if(distanceX > distanceY)
                //    {
                //        angleValue = -2;
                //    }
                //}
                //else if (point.Y > rotate.CenterY)
                //{
                //    if (lastPoint.X < point.X)
                //    {
                //        angleValue = -2;
                //    }
                //    else if (lastPoint.X > point.X)
                //    {
                //        angleValue = 2;
                //    }
                //}

                //double result = (((e.Position.X / Window.Current.Bounds.Width) * 360.0) + (e.Position.Y / (Window.Current.Bounds.Height - 150.0)) * 360.0) / 2;
                //rotate.Angle = angleValue;

                //lastPoint = point;
                //lastAngleValue = currentWinkel;
            }
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
                case ToolType.Eraser:
                    PocketPaintApplication.GetInstance().ToolCurrent.HandleMove(point);
                    break;
                case ToolType.Cursor:
                    PocketPaintApplication.GetInstance().ToolCurrent.HandleMove(movezoom);
                    PocketPaintApplication.GetInstance().ToolCurrent.HandleMove(point);
                    break;
                case ToolType.Move:
                case ToolType.Zoom:
                    PocketPaintApplication.GetInstance().ToolCurrent.HandleMove(movezoom);
                    break;
                case ToolType.Line:
                    PocketPaintApplication.GetInstance().ToolCurrent.HandleMove(point);
                    break;
                case ToolType.Rect:
                    if (rotate.Angle != 0)
                        PocketPaintApplication.GetInstance().ToolCurrent.HandleMove(rotate);
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

        public void ResetDrawingSpace()
        {
            PocketPaintApplication.GetInstance().ToolCurrent.ResetDrawingSpace();
        }
    }
}
