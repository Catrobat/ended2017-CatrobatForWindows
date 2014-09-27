using Catrobat.Paint.Phone;
using Catrobat.Paint.WindowsPhone.Tool;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
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

            setToolPickerLayout();
            //MessageDialog msg = new MessageDialog(BtnBrush.Width.ToString());
            //msg.ShowAsync();
        }

        private void setToolPickerLayout()
        {
            var height = PocketPaintApplication.GetInstance().size_height_multiplication;
            var width = PocketPaintApplication.GetInstance().size_width_multiplication;

            GrdToolPickerButtonInner.Width *= width;
            GrdToolPickerButtonOuter.Width *= width;
            foreach (Object obj in GrdToolPickerButtonInner.Children)
            {
                if (obj.GetType() == typeof(Button))
                {
                    ((Button)obj).Height *= height;
                    ((Button)obj).Width *= width;

                    ((Button)obj).Margin = new Thickness(
                                            ((Button)obj).Margin.Left * width,
                                            ((Button)obj).Margin.Top * height,
                                            ((Button)obj).Margin.Right * width,
                                            ((Button)obj).Margin.Bottom * height);

                    ((Button)obj).Padding = new Thickness(
                                            ((Button)obj).Padding.Left * width,
                                            ((Button)obj).Padding.Top * height,
                                            ((Button)obj).Padding.Right * width,
                                            ((Button)obj).Padding.Bottom * height);

                    ((Button)obj).FontSize *= height;

                    
                }
            }
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
            PocketPaintApplication.GetInstance().isBrushEraser = false;
            PocketPaintApplication.GetInstance().isBrushTool = false;
            PocketPaintApplication.GetInstance().isToolPickerUsed = true;

            switch (((Button)sender).Name)
            {
                case "BtnBrush":
                    PocketPaintApplication.GetInstance().SwitchTool(ToolType.Brush);
                    PocketPaintApplication.GetInstance().AppbarTop.BtnSelectedColorVisible(true);
                    PocketPaintApplication.GetInstance().isBrushTool = true;
                    break;
                case "BtnCursor":
                    PocketPaintApplication.GetInstance().SwitchTool(ToolType.Cursor);
                    break;
                case "BtnPipette":
                    PocketPaintApplication.GetInstance().SwitchTool(ToolType.Pipette);
                    break;
                case "BtnFill":
                    PocketPaintApplication.GetInstance().SwitchTool(ToolType.Fill);
                    PocketPaintApplication.GetInstance().AppbarTop.BtnSelectedColorVisible(true);
                    break;
                case "BtnStamp":
                    PocketPaintApplication.GetInstance().SwitchTool(ToolType.Stamp);
                    break;
                case "BtnRectangle":
                    PocketPaintApplication.GetInstance().RecDrawingRectangle.Margin = new Thickness(171, 263, 0, 0);
                    PocketPaintApplication.GetInstance().RecDrawingRectangle.Visibility = Visibility.Visible;
                    PocketPaintApplication.GetInstance().SwitchTool(ToolType.Rect);
                    break;
                case "BtnEllipse":
                    PocketPaintApplication.GetInstance().RecDrawingRectangle.Margin = new Thickness(171, 263, 0, 0);
                    PocketPaintApplication.GetInstance().RecDrawingRectangle.Height = 50;
                    PocketPaintApplication.GetInstance().RecDrawingRectangle.Width = 50;
                    PocketPaintApplication.GetInstance().RecDrawingRectangle.Visibility = Visibility.Visible;
                    PocketPaintApplication.GetInstance().SwitchTool(ToolType.Ellipse);
                    break;
                case "BtnImportPicture":
                    PocketPaintApplication.GetInstance().SwitchTool(ToolType.ImportPng);
                    break;
                case "BtnCrop":
                    PocketPaintApplication.GetInstance().SwitchTool(ToolType.Crop);
                    break;
                case "BtnEraser":
                    PocketPaintApplication.GetInstance().SwitchTool(ToolType.Eraser);
                    break;
                case "BtnFlip":
                    PocketPaintApplication.GetInstance().SwitchTool(ToolType.Flip);
                    break;
                case "BtnMove":
                    PocketPaintApplication.GetInstance().SwitchTool(ToolType.Move);
                    break;
                case "BtnZoom":
                    PocketPaintApplication.GetInstance().SwitchTool(ToolType.Zoom);
                    break;
                case "BtnRotate":
                    PocketPaintApplication.GetInstance().SwitchTool(ToolType.Rotate);
                    break;
                case "BtnLine":
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
