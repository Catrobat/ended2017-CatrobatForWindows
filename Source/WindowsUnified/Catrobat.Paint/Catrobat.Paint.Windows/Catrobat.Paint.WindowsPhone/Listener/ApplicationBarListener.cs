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
                PocketPaintApplication.GetInstance().PaintingAreaView.GrdThicknessControlVisibility = Visibility.Collapsed;

                PocketPaintApplication.GetInstance().is_border_color = false;
                rootFrame.Navigate(typeof(ViewColorPicker));
            }
        }

        public void BtnColBtnSelectedColor_OnClick(object sender, RoutedEventArgs e)
        {

        }

        public void BtnMoveScreen_OnClick(object sender, RoutedEventArgs e)
        {
            
            if (PocketPaintApplication.GetInstance().ToolCurrent.GetToolType() != ToolType.Move)
            {
                //PocketPaintApplication.GetInstance().PaintingAreaView.setVisibilityGrdSliderThickness(Visibility.Collapsed);
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
                //PocketPaintApplication.GetInstance().PaintingAreaView.setVisibilityGrdSliderThickness(Visibility.Collapsed);
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
                PocketPaintApplication.GetInstance().PaintingAreaView.GrdThicknessControlVisibility = visibility;
                //pplication.GetInstance().PaintingAreaView.setVisibilityOFRectEllUserControl(visibility);

                rootFrame.Navigate(typeof(ViewToolPicker));
            }
        }
    }
}
