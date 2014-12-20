using Catrobat.Paint.WindowsPhone.Command;
using Catrobat.Paint.WindowsPhone.Tool;
// TODO: using Catrobat.Paint.Phone.Command;
using Catrobat.Paint.WindowsPhone.View;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Catrobat.Paint.WindowsPhone.Listener
{
    public class ApplicationBarListener
    {
        public void BtnUndo_Click(object sender, RoutedEventArgs e)
        {
            CommandManager.GetInstance().UnDo();

            // TODO: Maybe it would be better to swap out the following code to the paintingarea-file.
            if(PocketPaintApplication.GetInstance().PaintingAreaCanvas.Children.Count == 0)
            {
                PocketPaintApplication.GetInstance().PaintingAreaView.changeEnabledOfASecondaryAppbarButton("appBarButtonClearWorkingSpace", false);
                PocketPaintApplication.GetInstance().PaintingAreaView.changeEnabledOfASecondaryAppbarButton("appbarButtonSave", false);
            }
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
                PocketPaintApplication.GetInstance().is_border_color = false;
                rootFrame.Navigate(typeof(ViewColorPicker));
            }
        }

        public void BtnMoveScreenEllipse_OnClick(object sender, RoutedEventArgs e)
        {

            if (PocketPaintApplication.GetInstance().ToolCurrent.GetToolType() != ToolType.Move)
            {
                //PocketPaintApplication.GetInstance().PaintingAreaView.setVisibilityGrdSliderThickness(Visibility.Collapsed);
                PocketPaintApplication.GetInstance().SwitchTool(ToolType.Move);
                PocketPaintApplication.GetInstance().EllipseSelectionControl.IsHitTestVisible = false;
                PocketPaintApplication.GetInstance().RectangleSelectionControl.IsHitTestVisible = false;
            }
            else if (PocketPaintApplication.GetInstance().ToolCurrent.GetToolType() == ToolType.Move)
            {
                if (PocketPaintApplication.GetInstance().ToolWhileMoveTool == null)
                    return;
                PocketPaintApplication.GetInstance().SwitchTool(PocketPaintApplication.GetInstance().ToolWhileMoveTool.GetToolType());
                PocketPaintApplication.GetInstance().EllipseSelectionControl.IsHitTestVisible = true;
                PocketPaintApplication.GetInstance().RectangleSelectionControl.IsHitTestVisible = true;
            }
        }

        public void BtnTools_OnClick(object sender, RoutedEventArgs e)
        {
            Frame rootFrame = Window.Current.Content as Frame;
            
            if(rootFrame != null)
            {
                rootFrame.Navigate(typeof(ViewToolPicker));
            }
        }
    }
}
