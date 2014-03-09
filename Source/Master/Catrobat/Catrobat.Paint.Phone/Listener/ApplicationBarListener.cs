using System;
using System.Windows;
using Catrobat.Paint.Phone.Command;
using Catrobat.Paint.Phone.Tool;
using Microsoft.Phone.Controls;

namespace Catrobat.Paint.Phone.Listener
{
    public class ApplicationBarListener
    {
        public void BtnUndo_Click(object sender, EventArgs e)
        {
            CommandManager.GetInstance().UnDo();
        }

        public void BtnRedo_Click(object sender, EventArgs e)
        {
            CommandManager.GetInstance().ReDo();
        }

        public void BtnColor_Click(object sender, EventArgs e)
        {
            var phoneApplicationFrame = Application.Current.RootVisual as PhoneApplicationFrame;
            if (phoneApplicationFrame != null)
                phoneApplicationFrame.Navigate(new Uri("/Catrobat.Paint.Phone;component/View/ColorPickerView.xaml", UriKind.RelativeOrAbsolute));
        }

 

        public void SliderThickness_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            PocketPaintApplication.GetInstance().PaintData.ThicknessSelected = Convert.ToInt32(e.NewValue);

        }

        public void BtnColBtnSelectedColor_OnClick(object sender, EventArgs e)
        {
            var phoneApplicationFrame = Application.Current.RootVisual as PhoneApplicationFrame;
            if (phoneApplicationFrame != null)
                phoneApplicationFrame.Navigate(new Uri("/Catrobat.Paint.Phone;component/View/ColorPickerView.xaml", UriKind.RelativeOrAbsolute));
        }

        public void BtnMoveScreen_OnClick(object sender, EventArgs e)
        {
            if (PocketPaintApplication.GetInstance().ToolCurrent.GetToolType() != ToolType.Move)
            {
                PocketPaintApplication.GetInstance().SwitchTool(ToolType.Move);
            }
            else if (PocketPaintApplication.GetInstance().ToolCurrent.GetToolType() == ToolType.Move)
            {
                if (PocketPaintApplication.GetInstance().ToolWhileMoveTool == null)
                    return;
                PocketPaintApplication.GetInstance().SwitchTool(PocketPaintApplication.GetInstance().ToolWhileMoveTool.GetToolType());
            }
         
        }
    }
}
