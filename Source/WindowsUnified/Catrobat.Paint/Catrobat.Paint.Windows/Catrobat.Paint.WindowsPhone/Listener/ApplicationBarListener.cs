using System;
using System.Windows;
// TODO: using Catrobat.Paint.Phone.Command;
using Catrobat.Paint.Phone.Tool;
using Catrobat.Paint.WindowsPhone.View;
using Catrobat.Paint.WindowsPhone.Tool;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls.Primitives;
using Catrobat.Paint.Phone.Command;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Catrobat.IDE.WindowsShared.Common;

namespace Catrobat.Paint.Phone.Listener
{
    public class ApplicationBarListener
    {
        public void BtnUndo_Click(object sender, RoutedEventArgs e)
        {
            CommandManager.GetInstance().UnDo();
        }

        public void BtnRedo_Click(object sender, RoutedEventArgs e)
        {
            CommandManager.GetInstance().ReDo();
        }

        public void BtnColor_Click(object sender, RoutedEventArgs e)
        {
            Frame rootFrame = Window.Current.Content as Frame;

            if (rootFrame != null)
            {
                Visibility visibility = Visibility.Collapsed;
                PocketPaintApplication.GetInstance().PaintingAreaView.setVisibilityOFSliderThicknessControl(Visibility.Collapsed);
                PocketPaintApplication.GetInstance().PaintingAreaView.setVisibilityOFThicknessKeyboard(visibility);

                PocketPaintApplication.GetInstance().is_border_color = false;
                rootFrame.Navigate(typeof(ViewColorPicker));
            }
        }


        // TODO: public void SliderThickness_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        /*public void SliderThickness_ValueChanged(object sender, RangeBaseValueChangedEventArgs e)
        {
            
        }*/

        public void BtnColBtnSelectedColor_OnClick(object sender, RoutedEventArgs e)
        {

        }
        public void BtnThickness_OnClick(object sender, RoutedEventArgs e)
        {
             if (PocketPaintApplication.GetInstance().PaintingAreaView.getVisibilityOFSliderThicknessControl() == Visibility.Collapsed)
              {
                    PocketPaintApplication.GetInstance().PaintingAreaView.setVisibilityOFSliderThicknessControl(Visibility.Visible);
                    PocketPaintApplication.GetInstance().PaintingAreaView.setSliderThicknessControlMargin(new Thickness(0.0, 0.0, 0.0, 0.0));
              }
              else
              {
                  PocketPaintApplication.GetInstance().PaintingAreaView.setVisibilityOFSliderThicknessControl(Visibility.Collapsed);
              }
        }

        public void BtnBrushThickness_OnClick(object sender, RoutedEventArgs e)
        {
           PocketPaintApplication.GetInstance().PaintingAreaView.checkIfThicknessWasEntered();
           if (PocketPaintApplication.GetInstance().PaintingAreaView.getVisibilityOFThicknessKeyboard() == Visibility.Collapsed)
            {
                PocketPaintApplication.GetInstance().PaintingAreaView.setSliderThicknessControlMargin(new Thickness(0.0, -324.0, 0.0, 287.0));
                PocketPaintApplication.GetInstance().PaintingAreaView.setVisibilityOFThicknessKeyboard(Visibility.Visible);
            }
            else
            {
                PocketPaintApplication.GetInstance().PaintingAreaView.setVisibilityOFThicknessKeyboard(Visibility.Collapsed);
                PocketPaintApplication.GetInstance().PaintingAreaView.setSliderThicknessControlMargin(new Thickness(0.0, 0.0, 0.0, 0.0));
            }
        }
        public void BtnMoveScreen_OnClick(object sender, RoutedEventArgs e)
        {
            
            if (PocketPaintApplication.GetInstance().ToolCurrent.GetToolType() != ToolType.Move)
            {
                PocketPaintApplication.GetInstance().PaintingAreaView.setVisibilityOFSliderThicknessControl(Visibility.Collapsed);
                PocketPaintApplication.GetInstance().SwitchTool(ToolType.Move);
            }
            else if (PocketPaintApplication.GetInstance().ToolCurrent.GetToolType() == ToolType.Move)
            {
                if (PocketPaintApplication.GetInstance().ToolWhileMoveTool == null)
                    return;
                PocketPaintApplication.GetInstance().SwitchTool(PocketPaintApplication.GetInstance().ToolWhileMoveTool.GetToolType());
            }
        }

        public void BtnMoveScreenEllipse_OnClick(object sender, RoutedEventArgs e)
        {

            if (PocketPaintApplication.GetInstance().ToolCurrent.GetToolType() != ToolType.Move)
            {
                PocketPaintApplication.GetInstance().PaintingAreaView.setVisibilityOFSliderThicknessControl(Visibility.Collapsed);
                PocketPaintApplication.GetInstance().SwitchTool(ToolType.Move);
            }
            else if (PocketPaintApplication.GetInstance().ToolCurrent.GetToolType() == ToolType.Move)
            {
                if (PocketPaintApplication.GetInstance().ToolWhileMoveTool == null)
                    return;
                PocketPaintApplication.GetInstance().SwitchTool(PocketPaintApplication.GetInstance().ToolWhileMoveTool.GetToolType());
            }
        }

        public void BtnTools_OnClick(object sender, RoutedEventArgs e)
        {
            Frame rootFrame = Window.Current.Content as Frame;
            
            if(rootFrame != null)
            {
                Visibility visibility = Visibility.Collapsed;
                PocketPaintApplication.GetInstance().PaintingAreaView.setVisibilityOFSliderThicknessControl(visibility);
                PocketPaintApplication.GetInstance().PaintingAreaView.setVisibilityOFThicknessKeyboard(visibility);
                PocketPaintApplication.GetInstance().PaintingAreaView.setVisibilityOFRectEllUserControl(visibility);

                rootFrame.Navigate(typeof(ViewToolPicker));
            }
        }

        public void TriangleButton_OnClick(object sender, RoutedEventArgs e)
        {
           PocketPaintApplication.GetInstance().PaintData.CapSelected = PenLineCap.Triangle;
           PocketPaintApplication.GetInstance().PaintingAreaView.checkPenLineCap(PocketPaintApplication.GetInstance().PaintData.CapSelected);
        }

        public void RoundButton_OnClick(object sender, RoutedEventArgs e)
        {
            PocketPaintApplication.GetInstance().PaintData.CapSelected = PenLineCap.Round;
            PocketPaintApplication.GetInstance().PaintingAreaView.checkPenLineCap(PocketPaintApplication.GetInstance().PaintData.CapSelected);
        }

        public void SquareButton_OnClick(object sender, RoutedEventArgs e)
        {
            PocketPaintApplication.GetInstance().PaintData.CapSelected = PenLineCap.Square;
            PocketPaintApplication.GetInstance().PaintingAreaView.checkPenLineCap(PocketPaintApplication.GetInstance().PaintData.CapSelected);
        }
    }
}
