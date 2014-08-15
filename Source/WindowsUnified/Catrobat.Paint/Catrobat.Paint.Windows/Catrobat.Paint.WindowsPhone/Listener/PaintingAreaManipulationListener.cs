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

namespace Catrobat.Paint.Phone.Listener
{
    class PaintingAreaManipulationListener 
    {
        public void ManipulationStarting(object sender, ManipulationStartingRoutedEventArgs e)
        {
            
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


            object movezoom;
            /*if (PinchManipulation != null)
            {

                movezoom = new ScaleTransform();
                if (e.Delta.Scale > 0)
                {
                    System.Diagnostics.Debug.WriteLine("Scale " + e.Delta.Scale);
                     ((ScaleTransform)movezoom).ScaleX *= e.Delta.Scale;
                     ((ScaleTransform)movezoom).ScaleY *= e.Delta.Scale;
                }
            }*/
            //else
            {
                movezoom = new TranslateTransform();
                
                ((TranslateTransform)movezoom).X += e.Delta.Translation.X;
                ((TranslateTransform)movezoom).Y += e.Delta.Translation.Y;
            }

            switch (PocketPaintApplication.GetInstance().ToolCurrent.GetToolType())
            {
                case ToolType.Brush:
                case ToolType.Eraser:
                    PocketPaintApplication.GetInstance().ToolCurrent.HandleMove(point);
                    break;
               case ToolType.Move:
               case ToolType.Zoom:
                    PocketPaintApplication.GetInstance().ToolCurrent.HandleMove(movezoom);
                    break;
               case ToolType.Line:
                    PocketPaintApplication.GetInstance().ToolCurrent.HandleMove(point);
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
