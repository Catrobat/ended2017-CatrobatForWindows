using Catrobat.Paint.WindowsPhone.Controls.UserControls;
using Catrobat.Paint.WindowsPhone.Tool;
using System;
using Windows.Phone.UI.Input;
using Windows.UI;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
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
        private bool isOneToolButtonHolding;
        public ViewToolPicker()
        {
            this.InitializeComponent();

            setToolPickerLayout();
            isOneToolButtonHolding = false;
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
            if (isOneToolButtonHolding)
            {
                isOneToolButtonHolding = false;
            }
            else
            {
                PocketPaintApplication pocketPaintApplication = PocketPaintApplication.GetInstance();
                if (pocketPaintApplication != null)
                {
                    pocketPaintApplication.AppbarTop.BtnSelectedColorVisible(false);
                    pocketPaintApplication.isBrushEraser = false;
                    pocketPaintApplication.isBrushTool = false;
                    pocketPaintApplication.isToolPickerUsed = true;
                    bool enableEdgeTypes = false;
                    PocketPaintApplication.GetInstance().PaintingAreaView.changeBackgroundColorAndOpacityOfPaintingAreaCanvas(Colors.Transparent, 1.0);
                    pocketPaintApplication.PaintingAreaView.resetControls();
                    switch (((Button)sender).Name)
                    {
                        case "BtnBrush":
                            pocketPaintApplication.SwitchTool(ToolType.Brush);
                            pocketPaintApplication.AppbarTop.BtnSelectedColorVisible(true);
                            pocketPaintApplication.isBrushTool = true;
                            break;
                        case "BtnCrop":
                            pocketPaintApplication.SwitchTool(ToolType.Crop);
                            pocketPaintApplication.GridCropControl.Visibility = Visibility.Visible;
                            pocketPaintApplication.GridCropControl.Children.Clear();
                            pocketPaintApplication.GridCropControl.Children.Add(new CropControl());
                            break;
                        case "BtnCursor":
                            pocketPaintApplication.SwitchTool(ToolType.Cursor);
                            pocketPaintApplication.AppbarTop.BtnSelectedColorVisible(true);
                            break;
                        case "BtnEllipse":
                            pocketPaintApplication.SwitchTool(ToolType.Ellipse);
                            enableEdgeTypes = false;
                            pocketPaintApplication.BarRecEllShape.setIsEnabledOfEdgeType(enableEdgeTypes, enableEdgeTypes, enableEdgeTypes);
                            pocketPaintApplication.BarRecEllShape.setForgroundOfLabelEdgeType(Colors.White);
                            pocketPaintApplication.ToolCurrent.ResetDrawingSpace();
                            pocketPaintApplication.EllipseSelectionControl.Visibility = Visibility.Visible;
                            PocketPaintApplication.GetInstance().PaintingAreaView.changeBackgroundColorAndOpacityOfPaintingAreaCanvas(Colors.Black, 0.5);
                            break;
                        case "BtnEraser":
                            pocketPaintApplication.SwitchTool(ToolType.Eraser);
                            break;
                        case "BtnFill":
                            pocketPaintApplication.SwitchTool(ToolType.Fill);
                            pocketPaintApplication.AppbarTop.BtnSelectedColorVisible(true);
                            break;
                        case "BtnFlip":
                            pocketPaintApplication.SwitchTool(ToolType.Flip);
                            break;
                        case "BtnImportPicture":
                            pocketPaintApplication.SwitchTool(ToolType.ImportPng);
                            pocketPaintApplication.GridImportImageSelectionControl.Children.Clear();
                            pocketPaintApplication.GridImportImageSelectionControl.Children.Add(new ImportImageSelectionControl());
                            pocketPaintApplication.BarRecEllShape.setBtnHeightValue = 160.0;
                            pocketPaintApplication.BarRecEllShape.setBtnWidthValue = 160.0;

                            pocketPaintApplication.GridImportImageSelectionControl.Visibility = Visibility.Visible;
                            pocketPaintApplication.InfoBoxActionControl.Visibility = Visibility.Visible;
                            pocketPaintApplication.AppbarTop.Visibility = Visibility.Collapsed;
                            pocketPaintApplication.PaintingAreaView.BottomAppBar.Visibility = Visibility.Collapsed;
                            // TODO: Write a function with the following three sentences and put it in the paintingareaview.cs.
                            // TODO: Implement this functionality
                            //PocketPaintApplication.GetInstance().PaintingAreaView.BottomAppBar.Visibility = Visibility.Collapsed;
                            //PocketPaintApplication.GetInstance().PaintingAreaView.changeBackgroundColorAndOpacityOfPaintingAreaCanvas(Colors.Black, 0.5);
                            enableEdgeTypes = true;
                            pocketPaintApplication.BarRecEllShape.setIsEnabledOfEdgeType(enableEdgeTypes, enableEdgeTypes, enableEdgeTypes);
                            pocketPaintApplication.BarRecEllShape.setForgroundOfLabelEdgeType(Colors.White);
                            pocketPaintApplication.PaintingAreaView.changeBackgroundColorAndOpacityOfPaintingAreaCanvas(Colors.Black, 0.5);
                            break;
                        case "BtnLine":
                            pocketPaintApplication.SwitchTool((ToolType.Line));
                            pocketPaintApplication.AppbarTop.BtnSelectedColorVisible(true);
                            break;
                        case "BtnMove":
                            pocketPaintApplication.SwitchTool(ToolType.Move);
                            break;
                        case "BtnPipette":
                            pocketPaintApplication.SwitchTool(ToolType.Pipette);
                            break;
                        case "BtnRectangle":
                            pocketPaintApplication.SwitchTool(ToolType.Rect);
                            enableEdgeTypes = true;
                            pocketPaintApplication.BarRecEllShape.setIsEnabledOfEdgeType(enableEdgeTypes, enableEdgeTypes, enableEdgeTypes);
                            pocketPaintApplication.BarRecEllShape.setForgroundOfLabelEdgeType(Colors.White);
                            pocketPaintApplication.ToolCurrent.ResetDrawingSpace();
                            pocketPaintApplication.RectangleSelectionControl.Visibility = Visibility.Visible;
                            PocketPaintApplication.GetInstance().PaintingAreaView.changeBackgroundColorAndOpacityOfPaintingAreaCanvas(Colors.Black, 0.5);
                            break;
                        case "BtnRotate":
                            pocketPaintApplication.SwitchTool(ToolType.Rotate);
                            break;
                        case "BtnStamp":
                            pocketPaintApplication.SwitchTool(ToolType.Stamp);
                            break;
                        case "BtnZoom":
                            pocketPaintApplication.SwitchTool(ToolType.Zoom);
                            break;
                    }
                    navigateToPaintingAreaView();
                }
            }
        }

        public void navigateToPaintingAreaView()
        {
            Frame frame = this.Frame;
            if (frame == null)
            {
                frame = new Frame();
            }

            frame.Navigate(typeof(PaintingAreaView));
        }

        private async void showToolMessage(string message)
        {
            isOneToolButtonHolding = true;

            MessageDialog msg = new MessageDialog(message);
            

            // TODO: Aktuell tritt eine Exception auf, da die asynchrone Programmierung nicht korrekt implementiert wurde.
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
