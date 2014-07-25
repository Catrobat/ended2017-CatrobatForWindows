using Catrobat.Paint.Phone;
using Catrobat.Paint.Phone.Ui;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
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
    public sealed partial class PaintingAreaView : Page
    {
        Int32 slider_thickness_textbox_last_value = 1;
        public PaintingAreaView()
        {
            this.InitializeComponent();

           /*TODO: int result = 0;
            result = Convert.ToInt32(Application.Current.RootVisual.RenderSize.Width) % 2;
            //PaintingAreaContentPanelGrid.Width = Application.Current.RootVisual.RenderSize.Width - result;
            PaintingAreaContentPanelGrid.Width = Application.Current.RootVisual.RenderSize.Width - result;
            result = Convert.ToInt32(Application.Current.RootVisual.RenderSize.Height) % 2;
            //PaintingAreaContentPanelGrid.Height = Application.Current.RootVisual.RenderSize.Height - 200 - result;
            PaintingAreaContentPanelGrid.Height = Application.Current.RootVisual.RenderSize.Height - 200 - result;
            //MessageBox.Show(Application.Current.RootVisual.RenderSize.Height.ToString());
            //MessageBox.Show(Application.Current.RootVisual.RenderSize.Width.ToString());*/
            PocketPaintApplication.GetInstance().PaintingAreaCanvas = PaintingAreaCanvas;
            PocketPaintApplication.GetInstance().PaintingAreaLayoutRoot = LayoutRoot;
            PocketPaintApplication.GetInstance().PaintingAreaCanvasUnderlaying = PaintingAreaCanvasUnderlaying;
            PocketPaintApplication.GetInstance().PaintingAreaCheckeredGrid = PaintingAreaCheckeredGrid;
            PocketPaintApplication.GetInstance().PaintingAreaContentPanelGrid = PaintingAreaContentPanelGrid;
            PocketPaintApplication.GetInstance().PaintingAreaView = this;

            Spinner.SpinnerGrid = SpinnerGrid;
            // TODO: Spinner.SpinnerStoryboard = SpinningStoryboard;

            PaintingAreaCheckeredGrid.ManipulationStarted += PocketPaintApplication.GetInstance().PaintingAreaManipulationListener.ManipulationStarted;
            PaintingAreaCheckeredGrid.ManipulationDelta += PaintingAreaCheckeredGrid_ManipulationDelta;
            PaintingAreaCheckeredGrid.ManipulationCompleted += PaintingAreaCheckeredGrid_ManipulationCompleted;
            PocketPaintApplication.GetInstance().PaintData.ToolCurrentChanged += PaintData_ToolCurrentChanged;
            // TODO: SliderThickness.ValueChanged += PocketPaintApplication.GetInstance().ApplicationBarListener.SliderThickness_ValueChanged;
            // TODO: SliderThickness.Value = PocketPaintApplication.GetInstance().PaintData.ThicknessSelected;
            //BtnThickness.Click += BtnThickness_Click;
            // TODO: btnBrushThickness.Click += PocketPaintApplication.GetInstance().ApplicationBarListener.BtnBrushThickness_OnClick;
            // TODO: btnBrushThickness.Content = PocketPaintApplication.GetInstance().PaintData.ThicknessSelected;
            //BtnTools.Click += PocketPaintApplication.GetInstance().ApplicationBarListener.BtnTools_OnClick;
            //BtnTools.Click += BtnTools_Click;
            //BtnZoomIn.Click += BtnZoomIn_Click;
            //BtnZoomOut.Click += BtnZoomOut_Click;
            // TODO: UndoRedoActionbarManager.GetInstance().ApplicationBarTop = ApplicationBarTopX;
        }

        void PaintData_ToolCurrentChanged(Phone.Tool.ToolBase tool)
        {
            throw new NotImplementedException();
        }

        void PaintingAreaCheckeredGrid_ManipulationCompleted(object sender, ManipulationCompletedRoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        void PaintingAreaCheckeredGrid_ManipulationDelta(object sender, ManipulationDeltaRoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        void PaintingAreaCheckeredGrid_ManipulationStarted(object sender, ManipulationStartedRoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Wird aufgerufen, wenn diese Seite in einem Frame angezeigt werden soll.
        /// </summary>
        /// <param name="e">Ereignisdaten, die beschreiben, wie diese Seite erreicht wurde.
        /// Dieser Parameter wird normalerweise zum Konfigurieren der Seite verwendet.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
        }

        private void btnAccept_Click(object sender, RoutedEventArgs e)
        {
            SliderThicknessControl.Margin = new Thickness(0.0, 0.0, 0.0, 0.0);

            checkIfThicknessWasEntered();
            checkIfValueIsInRange(true);
            uctrlOwnKeyboard.Visibility = Visibility.Collapsed;
        }

        private void checkIfValueIsInRange(bool pressed_accept)
        {
            
            if (btnBrushThickness.Content.ToString() == "")
            {
                btnValue0.IsEnabled = true;
                btnValue1.IsEnabled = true;
                btnValue2.IsEnabled = true;
                btnValue3.IsEnabled = true;
                btnValue4.IsEnabled = true;
                btnValue5.IsEnabled = true;
                btnValue6.IsEnabled = true;
                btnValue7.IsEnabled = true;
                btnValue8.IsEnabled = true;
                btnValue9.IsEnabled = true;
                btnValue0.IsEnabled = false;
            }
            else
            {
                Int32 input = Convert.ToInt32(btnBrushThickness.Content);
                if (input > 5 && input < 10)
                {
                    btnValue0.IsEnabled = false;
                    btnValue1.IsEnabled = false;
                    btnValue2.IsEnabled = false;
                    btnValue3.IsEnabled = false;
                    btnValue4.IsEnabled = false;
                    btnValue5.IsEnabled = false;
                    btnValue6.IsEnabled = false;
                    btnValue7.IsEnabled = false;
                    btnValue8.IsEnabled = false;
                    btnValue9.IsEnabled = false;
                }
                else if (input == 5)
                {
                    btnValue0.IsEnabled = true;
                    btnValue1.IsEnabled = false;
                    btnValue2.IsEnabled = false;
                    btnValue3.IsEnabled = false;
                    btnValue4.IsEnabled = false;
                    btnValue5.IsEnabled = false;
                    btnValue6.IsEnabled = false;
                    btnValue7.IsEnabled = false;
                    btnValue8.IsEnabled = false;
                    btnValue9.IsEnabled = false;
                }
                else if (input < 5)
                {
                    btnValue0.IsEnabled = true;
                    btnValue1.IsEnabled = true;
                    btnValue2.IsEnabled = true;
                    btnValue3.IsEnabled = true;
                    btnValue4.IsEnabled = true;
                    btnValue5.IsEnabled = true;
                    btnValue6.IsEnabled = true;
                    btnValue7.IsEnabled = true;
                    btnValue8.IsEnabled = true;
                    btnValue9.IsEnabled = true;
                }
                else
                {
                    btnValue0.IsEnabled = false;
                    btnValue1.IsEnabled = true;
                    btnValue2.IsEnabled = true;
                    btnValue3.IsEnabled = true;
                    btnValue4.IsEnabled = true;
                    btnValue5.IsEnabled = true;
                    btnValue6.IsEnabled = true;
                    btnValue7.IsEnabled = true;
                    btnValue8.IsEnabled = true;
                    btnValue9.IsEnabled = true;
                }

                SliderThickness.Value = Convert.ToDouble(input);
            }
        }

        public void checkIfThicknessWasEntered()
        {
            if (uctrlOwnKeyboard.Visibility == Visibility.Visible)
            {
                string slider_thickness_text_box_value = btnBrushThickness.Content.ToString();
                Int32 slider_thickness_text_box_int_value;

                if (!slider_thickness_text_box_value.Equals(""))
                {
                    slider_thickness_text_box_int_value = Convert.ToInt32(slider_thickness_text_box_value);

                    if (!(slider_thickness_text_box_int_value >= 1 && slider_thickness_text_box_int_value <= 50))
                    {
                        btnBrushThickness.Content = slider_thickness_textbox_last_value.ToString();
                    }
                    else
                    {
                        slider_thickness_textbox_last_value = slider_thickness_text_box_int_value;
                        SliderThickness.Value = slider_thickness_text_box_int_value;
                    }
                }
                else
                {
                    btnBrushThickness.Content = slider_thickness_textbox_last_value.ToString();
                }

                btnBrushThickness.Foreground = new SolidColorBrush(Colors.White);
            }
        }

        private void btnDeleteNumbers_Click(object sender, RoutedEventArgs e)
        {
            if (btnBrushThickness.Content.ToString() != "" && Convert.ToInt32(btnBrushThickness.Content.ToString()) > 0)
            {
                btnBrushThickness.Content = btnBrushThickness.Content.ToString().Remove(btnBrushThickness.Content.ToString().Length - 1);
            }

            //  if(btnSliderThickness.Content.ToString() != "")
            {
                checkIfValueIsInRange(false);
            }
        }

        private void ButtonNumbers_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            if (button != null)
            {
                string get_clicked_button_number = button.Name.Substring(8);

                if (btnBrushThickness.Content.ToString().Length < 2)
                {
                    btnBrushThickness.Content += get_clicked_button_number;
                }
                else if (btnBrushThickness.Content.ToString().Length == 2)
                {
                    btnBrushThickness.Content = "";
                    btnBrushThickness.Content += get_clicked_button_number;
                }
                checkIfValueIsInRange(false);
            }
        }

        private void TriangleRadioButon_OnClick(object sender, RoutedEventArgs e)
        {
            PocketPaintApplication.GetInstance().PaintData.CapSelected = PenLineCap.Triangle;
        }

        public void RoundRadioButon_OnClick(object sender, RoutedEventArgs e)
        {
            PocketPaintApplication.GetInstance().PaintData.CapSelected = PenLineCap.Round;
        }

        public void SquareRadioButon_OnClick(object sender, RoutedEventArgs e)
        {
            PocketPaintApplication.GetInstance().PaintData.CapSelected = PenLineCap.Square;
        }

        private void RoundImage_Click(object sender, RoutedEventArgs e)
        {
            RoundRadioButton.IsChecked = true;
            RoundRadioButon_OnClick(sender, e);
        }

        private void SquareImage_Click(object sender, RoutedEventArgs e)
        {
            SquareRadioButton.IsChecked = true;
            SquareRadioButon_OnClick(sender, e);
        }

        private void TriangleImage_Click(object sender, RoutedEventArgs e)
        {
            TriangleRadioButton.IsChecked = true;
            TriangleRadioButon_OnClick(sender, e);
        }

        private void SliderThickness_ValueChanged(object sender, RangeBaseValueChangedEventArgs e)
        {
            if (SliderThickness != null)
            {
                btnBrushThickness.Content = Convert.ToInt32(SliderThickness.Value).ToString();
                slider_thickness_textbox_last_value = Convert.ToInt32(SliderThickness.Value);
            }
        }

        private void btnThickness_Click(object sender, RoutedEventArgs e)
        {
            if (getVisibilityOFSliderThicknessControl() == Visibility.Collapsed)
            {
                setVisibilityOFSliderThicknessControl(Visibility.Visible);
                setSliderThicknessControlMargin(new Thickness(0.0, 0.0, 0.0, 0.0));
            }
            else
            {
                setVisibilityOFSliderThicknessControl(Visibility.Collapsed);
            }
        }

        public Visibility getVisibilityOFSliderThicknessControl()
        {
            return SliderThicknessControl.Visibility;
        }

        public void setVisibilityOFSliderThicknessControl(Visibility visibility)
        {
            SliderThicknessControl.Visibility = visibility;
            if (Visibility.Collapsed == visibility)
            {
                setVisibilityOFThicknessKeyboard(visibility);
            }
        }
        public void setVisibilityOFThicknessKeyboard(Visibility visibility)
        {
            uctrlOwnKeyboard.Visibility = visibility;
        }
        public void setSliderThicknessControlMargin(Thickness margin)
        {
            SliderThicknessControl.Margin = margin;
        }

        private void btnBrushThickness_Click(object sender, RoutedEventArgs e)
        {
            checkIfThicknessWasEntered();
            if (getVisibilityOFThicknessKeyboard() == Visibility.Collapsed)
            {
                setSliderThicknessControlMargin(new Thickness(0.0, -324.0, 0.0, 287.0));
                setVisibilityOFThicknessKeyboard(Visibility.Visible);
            }
            else
            {
                setVisibilityOFThicknessKeyboard(Visibility.Collapsed);
                setSliderThicknessControlMargin(new Thickness(0.0, 0.0, 0.0, 0.0));
            }
        }
        public Visibility getVisibilityOFThicknessKeyboard()
        {
            return uctrlOwnKeyboard.Visibility;
        }

        private void BtnTools_Click(object sender, RoutedEventArgs e)
        {
            if (this.Frame != null)
            {
                this.Frame.Navigate(typeof(ViewToolPicker));
            }
        }

        private void btnColor_Click(object sender, RoutedEventArgs e)
        {
            if (this.Frame != null)
            {
                this.Frame.Navigate(typeof(ViewColorPicker));
            }
        }

    }
}
