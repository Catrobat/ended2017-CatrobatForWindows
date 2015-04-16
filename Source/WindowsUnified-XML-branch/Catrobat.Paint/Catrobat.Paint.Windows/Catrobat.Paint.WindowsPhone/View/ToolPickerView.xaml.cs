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
                    PocketPaintApplication.GetInstance().ApplicationBarTop.BtnSelectedColorVisible(true);
                    NavigationService.GoBack();
                    break;
                case "Cursor":
                    PocketPaintApplication.GetInstance().SwitchTool(ToolType.Cursor);
                    PocketPaintApplication.GetInstance().ApplicationBarTop.BtnSelectedColorVisible(false);
                    NavigationService.GoBack();
                    break;
                case "Pipette":
                    PocketPaintApplication.GetInstance().SwitchTool(ToolType.Pipette);
                    PocketPaintApplication.GetInstance().ApplicationBarTop.BtnSelectedColorVisible(false);
                    NavigationService.GoBack();
                    break;
                case "Fill":
                    PocketPaintApplication.GetInstance().SwitchTool(ToolType.Fill);
                    PocketPaintApplication.GetInstance().ApplicationBarTop.BtnSelectedColorVisible(true);
                    NavigationService.GoBack();
                    break;
                case "Stamp":
                    PocketPaintApplication.GetInstance().SwitchTool(ToolType.Stamp);
                    PocketPaintApplication.GetInstance().ApplicationBarTop.BtnSelectedColorVisible(false);
                    NavigationService.GoBack();
                    break;
                case "Rectangle":
                    PocketPaintApplication.GetInstance().SwitchTool(ToolType.Rect);
                    PocketPaintApplication.GetInstance().ApplicationBarTop.BtnSelectedColorVisible(true);
                    NavigationService.GoBack();
                    break;
                case "Ellipse":
                    PocketPaintApplication.GetInstance().SwitchTool(ToolType.Ellipse);
                    PocketPaintApplication.GetInstance().ApplicationBarTop.BtnSelectedColorVisible(true);
                    NavigationService.GoBack();
                    break;
                case "ImportPicture":
                    PocketPaintApplication.GetInstance().SwitchTool(ToolType.ImportPng);
                    PocketPaintApplication.GetInstance().ApplicationBarTop.BtnSelectedColorVisible(false);
                    NavigationService.GoBack();
                    break;
                case "Crop":
                    PocketPaintApplication.GetInstance().SwitchTool(ToolType.Crop);
                    PocketPaintApplication.GetInstance().ApplicationBarTop.BtnSelectedColorVisible(false);
                    NavigationService.GoBack();
                    break;
                case "Eraser":
                    PocketPaintApplication.GetInstance().SwitchTool(ToolType.Eraser);
                    PocketPaintApplication.GetInstance().ApplicationBarTop.BtnSelectedColorVisible(false);
                    NavigationService.GoBack();
                    break;
                case "Flip":
                    PocketPaintApplication.GetInstance().SwitchTool(ToolType.Flip);
                    PocketPaintApplication.GetInstance().ApplicationBarTop.BtnSelectedColorVisible(false);
                    NavigationService.GoBack();
                    break;
                case "Move":
                    
                    PocketPaintApplication.GetInstance().SwitchTool(ToolType.Move);
                    PocketPaintApplication.GetInstance().ApplicationBarTop.BtnSelectedColorVisible(false);
                    NavigationService.GoBack();
                    break;
                case "Zoom":
                    PocketPaintApplication.GetInstance().SwitchTool(ToolType.Zoom);
                    PocketPaintApplication.GetInstance().ApplicationBarTop.BtnSelectedColorVisible(false);
                    NavigationService.GoBack();
                    break;
                case "Rotate":
                    PocketPaintApplication.GetInstance().SwitchTool(ToolType.Rotate);
                    PocketPaintApplication.GetInstance().ApplicationBarTop.BtnSelectedColorVisible(false);
                    NavigationService.GoBack();
                    break;
                case "Line":
                    PocketPaintApplication.GetInstance().SwitchTool((ToolType.Line));
                    PocketPaintApplication.GetInstance().ApplicationBarTop.BtnSelectedColorVisible(true);
                    NavigationService.GoBack();
                    break;

            }
        }

    }
}