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
            // TODO: var phoneApplicationFrame = Application.Current.RootVisual as PhoneApplicationFrame;
            // TODO: if (phoneApplicationFrame != null)
            {
            // TODO: PocketPaintApplication.GetInstance().PaintingAreaView.setVisibilityOFSliderThicknessControl(Visibility.Collapsed);
            // TODO:    phoneApplicationFrame.Navigate(new Uri("/Catrobat.Paint.Phone;component/View/ColorPickerView.xaml", UriKind.RelativeOrAbsolute));
            }
        }


        // TODO: public void SliderThickness_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        /*public void SliderThickness_ValueChanged(object sender, RangeBaseValueChangedEventArgs e)
        {
            
        }*/

        public void BtnColBtnSelectedColor_OnClick(object sender, RoutedEventArgs e)
        {
            // TODO: var phoneApplicationFrame = Application.Current.RootVisual as PhoneApplicationFrame;
            // TODO: if (phoneApplicationFrame != null)
            {
                PocketPaintApplication.GetInstance().PaintingAreaView.setVisibilityOFSliderThicknessControl(Visibility.Collapsed);
                PocketPaintApplication.GetInstance().PaintingAreaView.NavigatedTo(typeof(ViewColorPicker));
            }
        }
        public void BtnThickness_OnClick(object sender, EventArgs e)
        {
             /* if (PocketPaintApplication.GetInstance().PaintingAreaView.getVisibilityOFSliderThicknessControl() == Visibility.Collapsed)
              {
                    PocketPaintApplication.GetInstance().PaintingAreaView.setVisibilityOFSliderThicknessControl(Visibility.Visible);
                    PocketPaintApplication.GetInstance().PaintingAreaView.setSliderThicknessControlMargin(new Thickness(0.0, 0.0, 0.0, 0.0));
              }
              else
              {
                  PocketPaintApplication.GetInstance().PaintingAreaView.setVisibilityOFSliderThicknessControl(Visibility.Collapsed);
              }
            */
        }

        public void BtnBrushThickness_OnClick(object sender, EventArgs e)
        {
            /* TODO:
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
            }*/
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

        public void BtnTools_OnClick(object sender, EventArgs e)
        {
            /*
            var phoneApplicationFrame = Application.Current.RootVisual as PhoneApplicationFrame;
            if (phoneApplicationFrame != null)
            {
                PocketPaintApplication.GetInstance().PaintingAreaView.setVisibilityOFSliderThicknessControl(Visibility.Collapsed);
                phoneApplicationFrame.Navigate(new Uri("/Catrobat.Paint.Phone;component/View/ToolPickerView.xaml", UriKind.RelativeOrAbsolute));

                ToolType tool_type = PocketPaintApplication.GetInstance().ToolCurrent.GetToolType();
            }*/
        }

    }
}
