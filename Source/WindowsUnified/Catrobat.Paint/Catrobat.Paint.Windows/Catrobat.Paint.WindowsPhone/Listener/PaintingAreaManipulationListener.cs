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
namespace Catrobat.Paint.Phone.Listener
{
    class PaintingAreaManipulationListener 
    {
        public void ManipulationStarted(object sender, ManipulationStartedEventArgs e)
        {
            //var point = new Point(Convert.ToInt32(e.Position.X), Convert.ToInt32(e.Position.Y));
            // TODO some bubbling? issue here, fast multiple applicationbartop undos result in triggering this event
           // if (point.X < 0 || point.Y < 0 || Spinner.SpinnerActive ) // TODO: e.Handled
            {
                return;
            }
            
            //PocketPaintApplication.GetInstance().ToolCurrent.HandleDown(point);
        }

        public void ManipulationDelta(object sender, ManipulationDelta e)
        {
            var point = new Point(Convert.ToInt32(e.Translation.X), Convert.ToInt32(e.Translation.Y));

            // TODO some bubbling? issue here, fast multiple applicationbartop undos result in triggering this event
            if (point.X < 0 || point.Y < 0 || Spinner.SpinnerActive) // TODO: || e.Handled
            {
                return;
            }


            object movezoom;
            // TODO: if (e.PinchManipulation != null)
            {

                movezoom = new ScaleTransform();
                // TODO: if (e.DeltaManipulation.Scale.X > 0 && e.DeltaManipulation.Scale.Y > 0)
                {
                    // TODO: System.Diagnostics.Debug.WriteLine("Scale.X " + e.DeltaManipulation.Scale.X + " Scale.Y " +
                    // TODO:                                e.DeltaManipulation.Scale.Y);
                    // TODO: ((ScaleTransform)movezoom).ScaleX *= e.DeltaManipulation.Scale.X;
                    // TODO: ((ScaleTransform)movezoom).ScaleY *= e.DeltaManipulation.Scale.Y;
                }
            }
            // TODO: else
            {
                movezoom = new TranslateTransform();
                // TODO: int right_left = PocketPaintApplication.GetInstance().PaintData.max_right_left;
                // TODO: int difference = right_left + Convert.ToInt32(e.DeltaManipulation.Translation.X);

                bool move_allowed = false;
                int move_x = 0;



                    /*((TranslateTransform)movezoom).X += Convert.ToInt32(e.DeltaManipulation.Translation.X);
                    ((TranslateTransform)movezoom).Y += e.DeltaManipulation.Translation.Y;
                    PocketPaintApplication.GetInstance().PaintData.max_right_left = PocketPaintApplication.GetInstance().PaintData.max_right_left + Convert.ToInt32(e.DeltaManipulation.Translation.X);*/
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
       

        public void ManipulationCompleted(object sender, ManipulationCompletedEventArgs e)
        {
            var point = new Point(Convert.ToInt32(e.Position.X), Convert.ToInt32(e.Position.Y));

            // TODO some bubbling? issue here, fast multiple applicationbartop undos result in triggering this event
            if (point.X < 0 || point.Y < 0 || Spinner.SpinnerActive) // TODO:|| e.Handled
            {
                return;
            }

            PocketPaintApplication.GetInstance().ToolCurrent.HandleUp(point);        
        }
    }
}
