using Catrobat.Paint.Phone;
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

// Die Elementvorlage "Benutzersteuerelement" ist unter http://go.microsoft.com/fwlink/?LinkId=234236 dokumentiert.

namespace Catrobat.Paint.WindowsPhone.Controls.UserControls
{
    public sealed partial class InfoBoxControl : UserControl
    {
        public InfoBoxControl()
        {
            this.InitializeComponent();
        }

        private void setCursorControlLayout()
        {
            double heightMultiplicator = PocketPaintApplication.GetInstance().size_width_multiplication;
            double widthMultiplicator = PocketPaintApplication.GetInstance().size_width_multiplication;

            GridMain.Width *= widthMultiplicator;
            GridMain.Height *= heightMultiplicator;

            foreach (Object obj in GridMain.Children)
            {
                if (obj.GetType() == typeof(Button))
                {
                    Button currentButton = (Button)obj;
                    currentButton.Height *= heightMultiplicator;
                    currentButton.Width *= widthMultiplicator;
                    Thickness currentButtonThickness = currentButton.BorderThickness;
                    currentButtonThickness.Bottom *= heightMultiplicator;
                    currentButtonThickness.Left *= heightMultiplicator;
                    currentButtonThickness.Right *= heightMultiplicator;
                    currentButtonThickness.Top *= heightMultiplicator;
                    currentButton.BorderThickness = currentButtonThickness;

                    //currentEllipse.Margin = new Thickness(
                    //                        currentEllipse.Margin.Left * widthMultiplicator,
                    //                        currentEllipse.Margin.Top * heightMultiplicator,
                    //                        currentEllipse.Margin.Right * widthMultiplicator,
                    //                        currentEllipse.Margin.Bottom * heightMultiplicator);
                }
                else if (obj.GetType() == typeof(TextBox))
                {
                    TextBox currentTextbox = (TextBox)obj;
                    currentTextbox.Height *= heightMultiplicator;
                    currentTextbox.Width *= widthMultiplicator;

                    //currentRectangle.Margin = new Thickness(
                    //                        currentRectangle.Margin.Left * widthMultiplicator,
                    //                        currentRectangle.Margin.Top * heightMultiplicator,
                    //                        currentRectangle.Margin.Right * widthMultiplicator,
                    //                        currentRectangle.Margin.Bottom * heightMultiplicator);
                }
            }
        }

        private void btnNewDrawingSpace_Click(object sender, RoutedEventArgs e)
        {
            if (PocketPaintApplication.GetInstance().PaintingAreaCanvas.Children.Count > 0)
            {
                messageBoxNewDrawingSpace_Click();
            }
            else
            {
                resetTools();
            }    
        }

        private void resetTools()
        {
            if (PocketPaintApplication.GetInstance().PaintingAreaCanvas.Children.Count > 0) 
            {
                PocketPaintApplication.GetInstance().PaintingAreaCanvas.Children.Clear();
            }

            PocketPaintApplication.GetInstance().PaintingAreaCanvas.RenderTransform = new TransformGroup();
            PocketPaintApplication.GetInstance().PaintingAreaCheckeredGrid.RenderTransform = new TransformGroup();
            PocketPaintApplication.GetInstance().PaintingAreaContentPanelGrid.RenderTransform = new TransformGroup();

            PocketPaintApplication.GetInstance().PaintingAreaView.disableToolbarsAndPaintingArea(false);
        }

        private async void messageBoxNewDrawingSpace_Click()
        { 
            // TODO: use dynamic text instead of static text

            var messageDialog = new MessageDialog("Änderungen speichern?", "Neues Bild");

            messageDialog.Commands.Add(new UICommand(
                "Speichern",
                new UICommandInvokedHandler(saveChanges)));
            messageDialog.Commands.Add(new UICommand(
                "Verwerfen",
                new UICommandInvokedHandler(deleteChanges)));

            messageDialog.DefaultCommandIndex = 0;
            messageDialog.CancelCommandIndex = 1;

            await messageDialog.ShowAsync();
        }

        private void saveChanges(IUICommand command)
        {
            // TODO save current Image
            
            resetTools();
        }

        private void deleteChanges(IUICommand command)
        {
            resetTools();
        }
    }
}
