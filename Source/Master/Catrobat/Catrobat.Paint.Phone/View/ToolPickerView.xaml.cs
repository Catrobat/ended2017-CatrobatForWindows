using System.Windows;
using System.Windows.Controls;
using Catrobat.Paint.Phone.Tool;
using Microsoft.Phone.Controls;

namespace Catrobat.Paint.Phone.View
{
    public partial class ToolPickerView : PhoneApplicationPage
    {
        public ToolPickerView()
        {
            InitializeComponent();
        }

        private void Button_OnClick(object sender, RoutedEventArgs e)
        {
            switch (((Button) sender).Name)
            {
                case "Brush":
                    PocketPaintApplication.GetInstance().SwitchTool(ToolType.Brush);
                    NavigationService.GoBack();
                    break;
                case "Cursor":
                    PocketPaintApplication.GetInstance().SwitchTool(ToolType.Cursor);
                    NavigationService.GoBack();
                    break;
                case "Pipette":
                    PocketPaintApplication.GetInstance().SwitchTool(ToolType.Pipette);
                    NavigationService.GoBack();
                    break;
                case "Fill":
                    PocketPaintApplication.GetInstance().SwitchTool(ToolType.Fill);
                    NavigationService.GoBack();
                    break;
                case "Stamp":
                    PocketPaintApplication.GetInstance().SwitchTool(ToolType.Stamp);
                    NavigationService.GoBack();
                    break;
                case "Rectangle":
                    PocketPaintApplication.GetInstance().SwitchTool(ToolType.Rect);
                    NavigationService.GoBack();
                    break;
                case "Ellipse":
                    PocketPaintApplication.GetInstance().SwitchTool(ToolType.Ellipse);
                    NavigationService.GoBack();
                    break;
                case "ImportPicture":
                    PocketPaintApplication.GetInstance().SwitchTool(ToolType.ImportPng);
                    NavigationService.GoBack();
                    break;
                case "Crop":
                    PocketPaintApplication.GetInstance().SwitchTool(ToolType.Crop);
                    NavigationService.GoBack();
                    break;
                case "Eraser":
                    PocketPaintApplication.GetInstance().SwitchTool(ToolType.Eraser);
                    NavigationService.GoBack();
                    break;
                case "Flip":
                    PocketPaintApplication.GetInstance().SwitchTool(ToolType.Flip);
                    NavigationService.GoBack();
                    break;
                case "Move":
                    PocketPaintApplication.GetInstance().SwitchTool(ToolType.Move);
                    NavigationService.GoBack();
                    break;
                case "Zoom":
                    PocketPaintApplication.GetInstance().SwitchTool(ToolType.Zoom);
                    NavigationService.GoBack();
                    break;
                case "Rotate":
                    PocketPaintApplication.GetInstance().SwitchTool(ToolType.Rotate);
                    NavigationService.GoBack();
                    break;
                case "Line":
                    PocketPaintApplication.GetInstance().SwitchTool((ToolType.Line));
                    NavigationService.GoBack();
                    break;


            }
        }

    }
}