using Catrobat.Paint.Phone;
using Catrobat.Paint.WindowsPhone.Tool;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// Die Elementvorlage "Leere Seite" ist unter http://go.microsoft.com/fwlink/?LinkID=390556 dokumentiert.

namespace Catrobat.Paint.WindowsPhone.View
{
    /// <summary>
    /// Eine leere Seite, die eigenständig verwendet werden kann oder auf die innerhalb eines Rahmens navigiert werden kann.
    /// </summary>
    public sealed partial class ViewToolPicker : Page
    {
        public ViewToolPicker()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Wird aufgerufen, wenn diese Seite in einem Frame angezeigt werden soll.
        /// </summary>
        /// <param name="e">Ereignisdaten, die beschreiben, wie diese Seite erreicht wurde.
        /// Dieser Parameter wird normalerweise zum Konfigurieren der Seite verwendet.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
        }

        private void Button_OnClick(object sender, RoutedEventArgs e)
        {
            PocketPaintApplication.GetInstance().RecDrawingRectangle.Visibility = Visibility.Collapsed;
            PocketPaintApplication.GetInstance().AppbarTop.BtnSelectedColorVisible(false);
            switch (((Button)sender).Name)
            {
                case "Brush":
                    PocketPaintApplication.GetInstance().SwitchTool(ToolType.Brush);
                    PocketPaintApplication.GetInstance().AppbarTop.BtnSelectedColorVisible(true);
                    break;
                case "Cursor":
                    PocketPaintApplication.GetInstance().SwitchTool(ToolType.Cursor);
                    break;
                case "Pipette":
                    PocketPaintApplication.GetInstance().SwitchTool(ToolType.Pipette);
                    break;
                case "Fill":
                    PocketPaintApplication.GetInstance().SwitchTool(ToolType.Fill);
                    PocketPaintApplication.GetInstance().AppbarTop.BtnSelectedColorVisible(true);
                    break;
                case "Stamp":
                    PocketPaintApplication.GetInstance().SwitchTool(ToolType.Stamp);
                    break;
                case "Rectangle":
                    PocketPaintApplication.GetInstance().RecDrawingRectangle.Margin = new Thickness(171, 263, 0, 0);
                    PocketPaintApplication.GetInstance().RecDrawingRectangle.Visibility = Visibility.Visible;
                    PocketPaintApplication.GetInstance().SwitchTool(ToolType.Rect);
                    break;
                case "Ellipse":
                    PocketPaintApplication.GetInstance().RecDrawingRectangle.Margin = new Thickness(171, 263, 0, 0);
                    PocketPaintApplication.GetInstance().RecDrawingRectangle.Height = 50;
                    PocketPaintApplication.GetInstance().RecDrawingRectangle.Width = 50;
                    PocketPaintApplication.GetInstance().RecDrawingRectangle.Visibility = Visibility.Visible;
                    PocketPaintApplication.GetInstance().SwitchTool(ToolType.Ellipse);
                    break;
                case "ImportPicture":
                    PocketPaintApplication.GetInstance().SwitchTool(ToolType.ImportPng);
                    break;
                case "Crop":
                    PocketPaintApplication.GetInstance().SwitchTool(ToolType.Crop);
                    break;
                case "Eraser":
                    PocketPaintApplication.GetInstance().SwitchTool(ToolType.Eraser);
                    break;
                case "Flip":
                    PocketPaintApplication.GetInstance().SwitchTool(ToolType.Flip);
                    break;
                case "Move":
                    PocketPaintApplication.GetInstance().SwitchTool(ToolType.Move);
                    break;
                case "Zoom":
                    PocketPaintApplication.GetInstance().SwitchTool(ToolType.Zoom);
                    break;
                case "Rotate":
                    PocketPaintApplication.GetInstance().SwitchTool(ToolType.Rotate);
                    break;
                case "Line":
                    PocketPaintApplication.GetInstance().SwitchTool((ToolType.Line));
                    PocketPaintApplication.GetInstance().AppbarTop.BtnSelectedColorVisible(true);
                    break;
            }

            Frame frame = this.Frame;
            if(frame == null)
            {
                frame = new Frame();
            }

            frame.Navigate(typeof(PaintingAreaView));
        }
    }
}
