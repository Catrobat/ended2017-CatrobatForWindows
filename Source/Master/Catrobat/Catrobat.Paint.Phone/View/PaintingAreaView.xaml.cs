using System;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Navigation;
using Windows.Foundation.Metadata;
using Windows.Phone.Media.Capture;
using Catrobat.Paint.Phone.Command;
using Catrobat.Paint.Phone.Tool;
using Catrobat.Paint.Phone.Ui;
using Coding4Fun.Toolkit.Controls;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System.Windows.Media;
using System.Windows.Input;
using System.Windows.Controls;
using System.Windows.Shapes;

namespace Catrobat.Paint.Phone.View
{
    public partial class PaintingAreaView : PhoneApplicationPage
    {
        Int32 slider_thickness_textbox_last_value = 1;


        // Constructor
        public PaintingAreaView()
        {
            InitializeComponent();
            
            int result = 0;
            result = Convert.ToInt32(Application.Current.RootVisual.RenderSize.Width) % 2;
            //PaintingAreaContentPanelGrid.Width = Application.Current.RootVisual.RenderSize.Width - result;
            PaintingAreaContentPanelGrid.Width = Application.Current.RootVisual.RenderSize.Width - result;
            result = Convert.ToInt32(Application.Current.RootVisual.RenderSize.Height) % 2;
            //PaintingAreaContentPanelGrid.Height = Application.Current.RootVisual.RenderSize.Height - 200 - result;
            PaintingAreaContentPanelGrid.Height = Application.Current.RootVisual.RenderSize.Height - 200 - result;
            //MessageBox.Show(Application.Current.RootVisual.RenderSize.Height.ToString());
            //MessageBox.Show(Application.Current.RootVisual.RenderSize.Width.ToString());
            PocketPaintApplication.GetInstance().PaintingAreaCanvas = PaintingAreaCanvas;
            PocketPaintApplication.GetInstance().PaintingAreaLayoutRoot = LayoutRoot;
            PocketPaintApplication.GetInstance().PaintingAreaCanvasUnderlaying = PaintingAreaCanvasUnderlaying;
            PocketPaintApplication.GetInstance().PaintingAreaCheckeredGrid = PaintingAreaCheckeredGrid;
            PocketPaintApplication.GetInstance().PaintingAreaContentPanelGrid = PaintingAreaContentPanelGrid;
            PocketPaintApplication.GetInstance().PaintingAreaView = this;

            Spinner.SpinnerGrid = SpinnerGrid;
            Spinner.SpinnerStoryboard = SpinningStoryboard;

            PaintingAreaCheckeredGrid.ManipulationStarted += PocketPaintApplication.GetInstance().PaintingAreaManipulationListener.ManipulationStarted;
            PaintingAreaCheckeredGrid.ManipulationDelta += PocketPaintApplication.GetInstance().PaintingAreaManipulationListener.ManipulationDelta;
            PaintingAreaCheckeredGrid.ManipulationCompleted += PocketPaintApplication.GetInstance().PaintingAreaManipulationListener.ManipulationCompleted;
            PocketPaintApplication.GetInstance().PaintData.ToolCurrentChanged += ToolChangedHere;

            foreach (ApplicationBarIconButton btn in ApplicationBar.Buttons)
            {
                if (btn.Text.Contains("color"))
                {
                    btn.Click += PocketPaintApplication.GetInstance().ApplicationBarListener.BtnColor_Click;
                }
                else if (btn.Text.Contains("tools"))
                {
                    btn.Click += PocketPaintApplication.GetInstance().ApplicationBarListener.BtnTools_OnClick;
                }
            }
            
            SliderThickness.ValueChanged +=
                PocketPaintApplication.GetInstance().ApplicationBarListener.SliderThickness_ValueChanged;
            SliderThickness.Value = PocketPaintApplication.GetInstance().PaintData.ThicknessSelected;
            //BtnThickness.Click += BtnThickness_Click;
            btnBrushThickness.Click += PocketPaintApplication.GetInstance().ApplicationBarListener.BtnBrushThickness_OnClick;
            btnBrushThickness.Content = PocketPaintApplication.GetInstance().PaintData.ThicknessSelected;
            //BtnTools.Click += PocketPaintApplication.GetInstance().ApplicationBarListener.BtnTools_OnClick;
            //BtnTools.Click += BtnTools_Click;
            //BtnZoomIn.Click += BtnZoomIn_Click;
            //BtnZoomOut.Click += BtnZoomOut_Click;
            UndoRedoActionbarManager.GetInstance().ApplicationBarTop = ApplicationBarTopX;


        }

        void BtnZoomOut_Click(object sender, EventArgs e)
        {
            MoveZoomTool tool = new MoveZoomTool();
            ScaleTransform scaletransform = new ScaleTransform();
            scaletransform.ScaleX = 0.9;
            scaletransform.ScaleY = 0.9;
            tool.HandleMove(scaletransform);
        }

        void BtnZoomIn_Click(object sender, EventArgs e)
        {
            
            MoveZoomTool tool = new MoveZoomTool();
            ScaleTransform scaletransform = new ScaleTransform();
            scaletransform.ScaleX = 1.1;
            scaletransform.ScaleY = 1.1;
            tool.HandleMove(scaletransform);
        }

        void BtnTools_Click(object sender, EventArgs e)
        {

            var phoneApplicationFrame = Application.Current.RootVisual as PhoneApplicationFrame; 
            phoneApplicationFrame.Navigate(new Uri("/Catrobat.Paint.Phone;component/View/ToolPickerView.xaml", UriKind.RelativeOrAbsolute));
        }

        void BtnThickness_Click(object sender, EventArgs e)
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

        private void ChangeIconBtnColor()
        {

        }

        // Sample code for building a localized ApplicationBar
        //private void BuildLocalizedApplicationBar()
        //{
        //    // Set the page's ApplicationBar to a new instance of ApplicationBar.
        //    ApplicationBar = new ApplicationBar();

        //    // Create a new button and set the text value to the localized string from AppResources.
        //    ApplicationBarIconButton appBarButton = new ApplicationBarIconButton(new Uri("/Assets/AppBar/appbar.add.rest.png", UriKind.Relative));
        //    appBarButton.Text = AppResources.AppBarButtonText;
        //    ApplicationBar.Buttons.Add(appBarButton);

        //    // Create a new menu item with the localized string from AppResources.
        //    ApplicationBarMenuItem appBarMenuItem = new ApplicationBarMenuItem(AppResources.AppBarMenuItemText);
        //    ApplicationBar.MenuItems.Add(appBarMenuItem);
        //}

        protected override void OnBackKeyPress(System.ComponentModel.CancelEventArgs e)
        {
            var result = MessageBoxResult.Cancel;

            if (PocketPaintApplication.GetInstance().UnsavedChangesMade)
            {
                result = MessageBox.Show("Are you sure you want to exit and discard unsaved changes?", "Confirm Exit?",
                                MessageBoxButton.OKCancel);

                if (result == MessageBoxResult.Cancel)
                {
                    e.Cancel = true;
                    return;

                }
            }
            Application.Current.Terminate();
        }

        private void ApplicationBarMenuItem_OnClick(object sender, EventArgs e)
        {

            //ACHTUNG: http://stackoverflow.com/questions/17477675/delete-an-image-in-the-medialibrary
            // sobald ein bild einmal in der medialibrary ist kann es programmatisch nicht mehr gelöscht oder ersetzt werden.
            // analog zu paintroid sollte mit dem Knopf "Speichern" pro Session immer das selbe Bild (bei uns anhand des DateTimeAppStarted definiert) überschrieben und
            // somit nur ein Bild in MediaLibrary landen
            // Think about it and change usecase for WP!

            PocketPaintApplication.GetInstance().SaveAsPng(PocketPaintApplication.GetInstance().DateTimeAppStarted);
        }

        static void OnBackKeyPressed(object sender, CancelEventArgs e)
        {
            if (PocketPaintApplication.GetInstance().UnsavedChangesMade)
            {
                var result = MessageBox.Show("Nicht gespeicherte Änderungen verwerfen und beenden?", "Beenden",
                                              MessageBoxButton.OKCancel);


                if (result == MessageBoxResult.OK)
                {
                    return;
                }
                e.Cancel = true;
            }
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            /*  if (e.NavigationMode == NavigationMode.Back)
              {
                  //e.Uri = "";
              }*/

            base.OnNavigatedFrom(e);
            PaintingAreaCanvas.CaptureMouse();
        }

        private void TriangleRadioButon_OnClick(object sender, EventArgs e)
        {
            PocketPaintApplication.GetInstance().PaintData.CapSelected = PenLineCap.Triangle;
        }

        public void RoundRadioButon_OnClick(object sender, EventArgs e)
        {
            PocketPaintApplication.GetInstance().PaintData.CapSelected = PenLineCap.Round;
        }

        public void SquareRadioButon_OnClick(object sender, EventArgs e)
        {
            PocketPaintApplication.GetInstance().PaintData.CapSelected = PenLineCap.Square;
        }

        private void SliderThickness_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (SliderThickness != null)
            {
                btnBrushThickness.Content = Convert.ToInt32(SliderThickness.Value).ToString();
                slider_thickness_textbox_last_value = Convert.ToInt32(SliderThickness.Value);
            }
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

        private void ToolChangedHere(ToolBase tool)
        {
            switch (tool.GetToolType())
            {
                case ToolType.Brush:
                case ToolType.Cursor:
                case ToolType.Line:
                    ApplicationBar = (IApplicationBar)this.Resources["barStandard"];
                    break;

                case ToolType.Pipette:
                    ApplicationBar = (IApplicationBar)this.Resources["barPipette"];
                    break;

                case ToolType.Eraser:
                    ApplicationBar = (IApplicationBar)this.Resources["barEraser"];;
                    break;

                case ToolType.Move:
                case ToolType.Zoom:
                    ApplicationBar = (IApplicationBar)this.Resources["barMove"];
                    break;

                case ToolType.Crop:
                    ApplicationBar = (IApplicationBar)this.Resources["barCrop"];
                    break;

                case ToolType.Rotate:
                    ApplicationBar = (IApplicationBar)this.Resources["barRotate"];
                    break;
                case ToolType.Flip:
                    ApplicationBar = (IApplicationBar)this.Resources["barFlip"];
                    break;
            }





        }


        // TODO defining this handler solves issue that first tap after toolpicker page was open is not recognized by 
        // PaintingAreaCanvas Eventhandler... 
        // PaintingAreaCheckeredGrid handles now and this seems to be resolved.
        //        private void PaintingAreaContentPanelGrid_OnManipulationStarted(object sender, ManipulationStartedEventArgs e)
        //        {
        //            //System.Diagnostics.Debug.WriteLine("--PaintingAreaContentPanelGrid--");
        //        }
        private void BtnLeft_OnClick(object sender, EventArgs e)
        {
            if (PocketPaintApplication.GetInstance().ToolCurrent.GetToolType() == ToolType.Rotate)
            {
                var rotateTool = (RotateTool)PocketPaintApplication.GetInstance().ToolCurrent;
                rotateTool.RotateLeft();
            }
            else
                return;

        }

        private void BtnRight_OnClick(object sender, EventArgs e)
        {
            if (PocketPaintApplication.GetInstance().ToolCurrent.GetToolType() == ToolType.Rotate)
            {
                var rotateTool = (RotateTool)PocketPaintApplication.GetInstance().ToolCurrent;
                rotateTool.RotateRight();
            }
            else
                return;
        }

        private void BtnHorizotal_OnClick(object sender, EventArgs e)
        {
            if (PocketPaintApplication.GetInstance().ToolCurrent.GetToolType() == ToolType.Flip)
            {
                var flipTool = (FlipTool)PocketPaintApplication.GetInstance().ToolCurrent;
                flipTool.FlipHorizontal();
            }
            else
                return;
        }

        private void BtnVertical_OnClick(object sender, EventArgs e)
        {
            if (PocketPaintApplication.GetInstance().ToolCurrent.GetToolType() == ToolType.Flip)
            {
                var flipTool = (FlipTool)PocketPaintApplication.GetInstance().ToolCurrent;
                flipTool.FlipVertical();
            }
            else
                return;
        }

        private void btnAccept_Click(object sender, RoutedEventArgs e)
        {
            SliderThicknessControl.Margin = new Thickness(0.0, 0.0, 0.0, 0.0);

            checkIfThicknessWasEntered();
            checkIfValueIsInRange(true);
            uctrlOwnKeyboard.Visibility = Visibility.Collapsed;
        }

        private void btnDeleteNumbers_Click(object sender, RoutedEventArgs e)
        {
            if (btnBrushThickness.Content.ToString() != "" && Convert.ToInt32(btnBrushThickness.Content.ToString()) > 0)
            {
                btnBrushThickness.Content = btnBrushThickness.Content.ToString().Remove(btnBrushThickness.Content.ToString().Length - 1);
            }

          //  if(btnBrushThickness.Content.ToString() != "")
            {
                checkIfValueIsInRange(false);
            }
        }



        private void btnSliderThickness_Click(object sender, RoutedEventArgs e)
        {
            
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
                if(input > 5 && input < 10)
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
                else if(input == 5)
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
                else if(input < 5)
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
                /*
                Int32 input = Convert.ToInt32(btnSliderThickness.Content);
                if (input > 50)
                {
                    btnSliderThickness.Content = "50";
                }
                else if (pressed_accept && (btnSliderThickness.Content.ToString() == ""))
                {
                    btnSliderThickness.Content = "1";
                }

                if (btnSliderThickness.Content.ToString() != "")
                {
                    SliderThickness.Value = Convert.ToInt32(btnSliderThickness.Content);
                }

                if(btnValue0.IsEnabled == false && btnSliderThickness.Content.ToString().Length < 2)
                {
                    btnValue0.IsEnabled = true;
                }*/
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
                 else if(btnBrushThickness.Content.ToString().Length == 2)
                 {
                     btnBrushThickness.Content = "";
                     btnBrushThickness.Content += get_clicked_button_number;
                 }
                 checkIfValueIsInRange(false);
             }
        }

        private void PaintingAreaCheckeredGrid_MouseEnter(object sender, MouseEventArgs e)
        {
            checkIfThicknessWasEntered();
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

        public void setVisibilityOFSliderThicknessControl(Visibility visibility)
        {
            SliderThicknessControl.Visibility = visibility;
            if (Visibility.Collapsed == visibility)
            {
                setVisibilityOFThicknessKeyboard(visibility);
            }
        }

        public Visibility getVisibilityOFSliderThicknessControl()
        {
            return SliderThicknessControl.Visibility;
        }

        public void setSliderThicknessControlMargin(Thickness margin)
        {
            SliderThicknessControl.Margin = margin;
        }

        public void setVisibilityOFThicknessKeyboard(Visibility visibility)
        {
            uctrlOwnKeyboard.Visibility = visibility;
        }

        public Visibility getVisibilityOFThicknessKeyboard()
        {
            return uctrlOwnKeyboard.Visibility;
        }

        private void PhoneApplicationPage_MouseMove(object sender, MouseEventArgs e)
        {
            if (PocketPaintApplication.GetInstance().ToolCurrent.GetToolType() == ToolType.Move)
            {
                ManipulationStartedEventArgs args = new ManipulationStartedEventArgs();
                PocketPaintApplication.GetInstance().PaintingAreaManipulationListener.ManipulationStarted(sender, args);
            }
        }
    }
}