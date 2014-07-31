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
            switch (((Button)sender).Name)
            {
                case "Brush":
                    PocketPaintApplication.GetInstance().SwitchTool(ToolType.Brush);
                    // PocketPaintApplication.GetInstance().ApplicationBarTop.BtnSelectedColorVisible(true);
                    break;
                case "Cursor":
                    PocketPaintApplication.GetInstance().SwitchTool(ToolType.Cursor);
                    // TODO: PocketPaintApplication.GetInstance().ApplicationBarTop.BtnSelectedColorVisible(false);
                    break;
                case "Pipette":
                    PocketPaintApplication.GetInstance().SwitchTool(ToolType.Pipette);
                    // TODO: PocketPaintApplication.GetInstance().ApplicationBarTop.BtnSelectedColorVisible(false);
                    break;
                case "Fill":
                    PocketPaintApplication.GetInstance().SwitchTool(ToolType.Fill);
                    // TODO: PocketPaintApplication.GetInstance().ApplicationBarTop.BtnSelectedColorVisible(true);
                    break;
                case "Stamp":
                    PocketPaintApplication.GetInstance().SwitchTool(ToolType.Stamp);
                    // TODO: PocketPaintApplication.GetInstance().ApplicationBarTop.BtnSelectedColorVisible(false);
                    break;
                case "Rectangle":
                    PocketPaintApplication.GetInstance().SwitchTool(ToolType.Rect);
                    // TODO: PocketPaintApplication.GetInstance().ApplicationBarTop.BtnSelectedColorVisible(true);
                    break;
                case "Ellipse":
                    PocketPaintApplication.GetInstance().SwitchTool(ToolType.Ellipse);
                    // TODO: PocketPaintApplication.GetInstance().ApplicationBarTop.BtnSelectedColorVisible(true);
                    break;
                case "ImportPicture":
                    PocketPaintApplication.GetInstance().SwitchTool(ToolType.ImportPng);
                    // TODO: PocketPaintApplication.GetInstance().ApplicationBarTop.BtnSelectedColorVisible(false);
                    break;
                case "Crop":
                    PocketPaintApplication.GetInstance().SwitchTool(ToolType.Crop);
                    // TODO: PocketPaintApplication.GetInstance().ApplicationBarTop.BtnSelectedColorVisible(false);
                    break;
                case "Eraser":
                    PocketPaintApplication.GetInstance().SwitchTool(ToolType.Eraser);
                    // TODO: PocketPaintApplication.GetInstance().ApplicationBarTop.BtnSelectedColorVisible(false);
                    break;
                case "Flip":
                    PocketPaintApplication.GetInstance().SwitchTool(ToolType.Flip);
                    // TODO: PocketPaintApplication.GetInstance().ApplicationBarTop.BtnSelectedColorVisible(false);
                    break;
                case "Move":
                    PocketPaintApplication.GetInstance().SwitchTool(ToolType.Move);
                    // TODO: PocketPaintApplication.GetInstance().ApplicationBarTop.BtnSelectedColorVisible(false);
                    break;
                case "Zoom":
                    PocketPaintApplication.GetInstance().SwitchTool(ToolType.Zoom);
                    // TODO: PocketPaintApplication.GetInstance().ApplicationBarTop.BtnSelectedColorVisible(false);
                    break;
                case "Rotate":
                    PocketPaintApplication.GetInstance().SwitchTool(ToolType.Rotate);
                    // TODO: PocketPaintApplication.GetInstance().ApplicationBarTop.BtnSelectedColorVisible(false);
                    break;
                case "Line":
                    PocketPaintApplication.GetInstance().SwitchTool((ToolType.Line));
                    // TODO: PocketPaintApplication.GetInstance().ApplicationBarTop.BtnSelectedColorVisible(true);
                    break;
            }
            if (this.Frame != null)
            {
                this.Frame.GoBack();
            }
        }
    }
}
