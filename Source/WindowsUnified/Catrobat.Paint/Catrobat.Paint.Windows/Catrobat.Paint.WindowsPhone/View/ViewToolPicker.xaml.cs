using Catrobat.Paint.Phone;
using Catrobat.Paint.WindowsPhone.Controls.UserControls;
using Catrobat.Paint.WindowsPhone.Tool;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
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
        private bool isHolding;
        public ViewToolPicker()
        {
            this.InitializeComponent();

            setToolPickerLayout();
            isHolding = false;
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
            if (isHolding)
            {
                isHolding = false;
            }
            else
            {
                // PocketPaintApplication.GetInstance().RecDrawingRectangle.Visibility = Visibility.Collapsed;
                PocketPaintApplication.GetInstance().AppbarTop.BtnSelectedColorVisible(false);
                PocketPaintApplication.GetInstance().isBrushEraser = false;
                PocketPaintApplication.GetInstance().isBrushTool = false;
                PocketPaintApplication.GetInstance().isToolPickerUsed = true; ;
                PocketPaintApplication.GetInstance().GridEllipseSelectionControl.Visibility = Visibility.Collapsed;
                PocketPaintApplication.GetInstance().GridRectangleSelectionControl.Visibility = Visibility.Collapsed;
                PocketPaintApplication.GetInstance().GridCutControl.Visibility = Visibility.Collapsed;
                PocketPaintApplication.GetInstance().GridImportImageSelectionControl.Visibility = Visibility.Collapsed;
                PocketPaintApplication.GetInstance().GridRectangleSelectionControl.IsHitTestVisible = true;
                PocketPaintApplication.GetInstance().GridEllipseSelectionControl.IsHitTestVisible = true;
                // TODO: RectangleSelctionControl should be reseted if the rectangle-tool is selected.
                // PocketPaintApplication.GetInstance().RectangleSelectionControl.resetRectangleSelectionControl();

                switch (((Button)sender).Name)
                {
                    case "BtnBrush":
                        PocketPaintApplication.GetInstance().AppbarTop.BtnSelectedColorVisible(true);
                        PocketPaintApplication.GetInstance().isBrushTool = true;
                        PocketPaintApplication.GetInstance().SwitchTool(ToolType.Brush);
                        break;
                    case "BtnCursor":
                        PocketPaintApplication.GetInstance().AppbarTop.BtnSelectedColorVisible(true);
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
                        PocketPaintApplication.GetInstance().SwitchTool(ToolType.Rect);
                        
                        PocketPaintApplication.GetInstance().GridRectangleSelectionControl.Children.Clear();
                        PocketPaintApplication.GetInstance().GridRectangleSelectionControl.Children.Add(new RectangleSelectionControl());
                        PocketPaintApplication.GetInstance().BarRecEllShape.setContentHeightValue = 160.0;
                        PocketPaintApplication.GetInstance().BarRecEllShape.setTbWidthValue = 160.0;

                        PocketPaintApplication.GetInstance().GridRectangleSelectionControl.Visibility = Visibility.Visible;
                        bool enableEdgeTypes = true;
                        PocketPaintApplication.GetInstance().BarRecEllShape.setIsEnabledOfEdgeType(enableEdgeTypes, enableEdgeTypes, enableEdgeTypes);
                        PocketPaintApplication.GetInstance().BarRecEllShape.setForgroundOfLabelEdgeType(Colors.White);
                        break;
                    case "BtnEllipse":
                        PocketPaintApplication.GetInstance().SwitchTool(ToolType.Ellipse);
                        
                        PocketPaintApplication.GetInstance().GridEllipseSelectionControl.Children.Clear();
                        PocketPaintApplication.GetInstance().GridEllipseSelectionControl.Children.Add(new EllipseSelectionControl());
                        PocketPaintApplication.GetInstance().BarRecEllShape.setContentHeightValue = 160.0;
                        PocketPaintApplication.GetInstance().BarRecEllShape.setTbWidthValue = 160.0;

                        PocketPaintApplication.GetInstance().GridEllipseSelectionControl.Visibility = Visibility.Visible;
                        enableEdgeTypes = false;
                        PocketPaintApplication.GetInstance().BarRecEllShape.setIsEnabledOfEdgeType(enableEdgeTypes, enableEdgeTypes, enableEdgeTypes);
                        PocketPaintApplication.GetInstance().BarRecEllShape.setForgroundOfLabelEdgeType(Colors.Gray);
                        break;
                    case "BtnImportPicture":
                        PocketPaintApplication.GetInstance().SwitchTool(ToolType.ImportPng);

                        PocketPaintApplication.GetInstance().GridImportImageSelectionControl.Children.Clear();
                        PocketPaintApplication.GetInstance().GridImportImageSelectionControl.Children.Add(new ImportImageSelectionControl());
                        PocketPaintApplication.GetInstance().BarRecEllShape.setContentHeightValue = 160.0;
                        PocketPaintApplication.GetInstance().BarRecEllShape.setTbWidthValue = 160.0;

                        PocketPaintApplication.GetInstance().GridImportImageSelectionControl.Visibility = Visibility.Visible;
                        enableEdgeTypes = true;
                        PocketPaintApplication.GetInstance().BarRecEllShape.setIsEnabledOfEdgeType(enableEdgeTypes, enableEdgeTypes, enableEdgeTypes);
                        PocketPaintApplication.GetInstance().BarRecEllShape.setForgroundOfLabelEdgeType(Colors.White);
                        break;
                    case "BtnCrop":
                        PocketPaintApplication.GetInstance().SwitchTool(ToolType.Crop);
                        PocketPaintApplication.GetInstance().GridCutControl.Visibility = Visibility.Visible;
                        PocketPaintApplication.GetInstance().GridCutControl.Children.Clear();
                        PocketPaintApplication.GetInstance().GridCutControl.Children.Add(new CutControl());
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
                if (frame == null)
                {
                    frame = new Frame();
                }

                frame.Navigate(typeof(PaintingAreaView));
            }
        }

        private async void showToolMessage(string message)
        {
            isHolding = true;

            MessageDialog msg = new MessageDialog(message);

            try
            {
                await msg.ShowAsync();
            }
            catch (Exception error)
            {

            }
        }

        private void BtnBrush_Holding(object sender, HoldingRoutedEventArgs e)
        {
            showToolMessage("Wähle die Farbe und Strichstärke auf der unteren Leiste.");
        }


        private void BtnCursorx_Holding(object sender, HoldingRoutedEventArgs e)
        {
            showToolMessage("Positioniere den Zeiger wo du zeichnen willst. " + Environment.NewLine
                + "Klicke einmal um den Zeichenmodus zu aktivieren. Bewege den Finger um zu zeichnen. " + Environment.NewLine
                + "Klicke noch einmal um den Zeichenmodus zu deaktivkieren.");
        }

        private void BtnPipette_Holding(object sender, HoldingRoutedEventArgs e)
        {
            showToolMessage("Klicke auf eine Farbe im Bild um diese Farbe auszuwählen.");
        }

        private void BtnFill_Holding(object sender, HoldingRoutedEventArgs e)
        {
            showToolMessage("Klicke in das Bild um einen Bereich zu füllen.");
        }

        private void BtnStamp_Holding(object sender, HoldingRoutedEventArgs e)
        {
            showToolMessage("Bewege und verändere das Rechteck bis es über dem Bereich liegt " + Environment.NewLine
                + "den du gerne aufnehmen würdest. Klicke in das Rechteck um den Bereich auszuwählen. Bewege das " + Environment.NewLine
                + "Rechteck und klicke um zu stempeln.");        
        }

        private void BtnRectangle_Holding(object sender, HoldingRoutedEventArgs e)
        {
            showToolMessage("Bewege und verändere das Rechteck. Klicke um das Rechteck auf das Bild zu zeichnen.");
        }

        private void BtnEllipse_Holding(object sender, HoldingRoutedEventArgs e)
        {
            showToolMessage("Bewege und verändere den Kreis" + Environment.NewLine
                + "oder forme ihn in eine Ellipse. Klicke um das Ergebnis auf das Bild zu zeichnen");
        }

        private void BtnImportPicture_Holding(object sender, HoldingRoutedEventArgs e)
        {
            showToolMessage("Wähle ein Bild aus der Galerie aus und importiere" + Environment.NewLine
                + "es in das Stempel Werkzeug");
        }

        private void BtnZuschneiden_Holding(object sender, HoldingRoutedEventArgs e)
        {
            showToolMessage("Bewege und verändere das Rechteck und schneide das Bild zu.");
        }

        private void BtnEraser_Holding(object sender, HoldingRoutedEventArgs e)
        {
            showToolMessage("Lösche wie mit einem Radierer Bereiche des Bildes.");
        }

        private void BtnFlip_Holding(object sender, HoldingRoutedEventArgs e)
        {
            showToolMessage("Klicke auf die Symbole auf der unteren Leiste." + Environment.NewLine
                + "um das ganze Bild zu spiegeln.");
        }

        private void BtnMove_Holding(object sender, HoldingRoutedEventArgs e)
        {
            showToolMessage("Bewege das Bild mit deinem Finger.");
        }

        private void BtnZoom_Holding(object sender, HoldingRoutedEventArgs e)
        {
            showToolMessage("Klicke auf die Symbole auf der unteren Leiste um das Bild" + Environment.NewLine
                + "zu vergrößern oder zu verkleinern.");
        }

        private void BtnRotate_Holding(object sender, HoldingRoutedEventArgs e)
        {
            showToolMessage("Klicke auf die Symbole auf der unteren Leist um das ganze Bild zu drehen.");
        }

        private void BtnLine_Holding(object sender, HoldingRoutedEventArgs e)
        {
            showToolMessage("Zeichne eine gerade Linie.");
        }
    }
}
