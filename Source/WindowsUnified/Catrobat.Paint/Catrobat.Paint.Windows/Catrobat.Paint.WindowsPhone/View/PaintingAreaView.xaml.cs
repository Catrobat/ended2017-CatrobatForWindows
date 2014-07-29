using Catrobat.Paint.Phone;
using Catrobat.Paint.Phone.Tool;
using Catrobat.Paint.Phone.Ui;
using Catrobat.Paint.WindowsPhone.Tool;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
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
    public sealed partial class PaintingAreaView : Page
    {
        Int32 slider_thickness_textbox_last_value = 1;
        static string current_appbar = "barStandard";
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
            PocketPaintApplication.GetInstance().PaintData.ToolCurrentChanged += ToolChangedHere;
            SliderThickness.ValueChanged += SliderThickness_ValueChanged;
            SliderThickness.Value = PocketPaintApplication.GetInstance().PaintData.ThicknessSelected;
            //BtnThickness.Click += BtnThickness_Click;
            // TODO: btnBrushThickness.Click += PocketPaintApplication.GetInstance().ApplicationBarListener.BtnBrushThickness_OnClick;
            // TODO: btnBrushThickness.Content = PocketPaintApplication.GetInstance().PaintData.ThicknessSelected;
            //BtnTools.Click += PocketPaintApplication.GetInstance().ApplicationBarListener.BtnTools_OnClick;
            //BtnTools.Click += BtnTools_Click;
            //BtnZoomIn.Click += BtnZoomIn_Click;
            //BtnZoomOut.Click += BtnZoomOut_Click;
            // TODO: UndoRedoActionbarManager.GetInstance().ApplicationBarTop = ApplicationBarTopX;


            checkPenLineCap(PocketPaintApplication.GetInstance().PaintData.CapSelected);
            createAppBarAndSwitchAppBarContent(current_appbar);
        }
        
        void checkPenLineCap(PenLineCap pen_line_cap)
        {
            if (pen_line_cap == PenLineCap.Round)
            {
                RoundRadioButton.IsChecked = true;
            }
            else if (pen_line_cap == PenLineCap.Square)
            {
                SquareRadioButton.IsChecked = true;
            }
            else
            {
                TriangleRadioButton.IsChecked = true;
            }
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

        public void createAppBarAndSwitchAppBarContent(string type)
        {
            CommandBar cmdBar = new CommandBar();
            if("barStandard" == type)
            {
                AppBarButton app_btnBrushThickness = new AppBarButton();
                AppBarButton app_btnColor = new AppBarButton();

                BitmapIcon thickness_icon = new BitmapIcon();
                thickness_icon.UriSource = new Uri("ms-resource:/Files/Assets/ColorPicker/icon_menu_strokes.png", UriKind.Absolute); 
                app_btnBrushThickness.Icon = thickness_icon;

                BitmapIcon color_icon = new BitmapIcon();
                color_icon.UriSource = new Uri("ms-resource:/Files/Assets/ColorPicker/icon_menu_color_palette.png", UriKind.Absolute);
                app_btnColor.Icon = color_icon;

                app_btnBrushThickness.Label = "Pinselstärke";
                app_btnColor.Label = "Farbe";

                app_btnBrushThickness.Click += btnThickness_Click;
                app_btnColor.Click += btnColor_Click;

                cmdBar.PrimaryCommands.Add(app_btnBrushThickness);
                cmdBar.PrimaryCommands.Add(app_btnColor);
           
            }
            else if("barPipette" == type)
            {
            

            }
            else if("barEraser" == type)
            {
                AppBarButton app_btnBrushThickness = new AppBarButton();

                BitmapIcon thickness_icon = new BitmapIcon();
                thickness_icon.UriSource = new Uri("ms-resource:/Files/Assets/ColorPicker/icon_menu_strokes.png", UriKind.Absolute);
                app_btnBrushThickness.Icon = thickness_icon;
               
                app_btnBrushThickness.Label = "Pinselstärke";

                app_btnBrushThickness.Click += btnThickness_Click;

                cmdBar.PrimaryCommands.Add(app_btnBrushThickness);

            }
            else if ("barMove" == type)
            {
                AppBarButton app_btnZoomIn = new AppBarButton();
                AppBarButton app_btnZoomOut = new AppBarButton();

                BitmapIcon zoom_in_icon = new BitmapIcon();
                zoom_in_icon.UriSource = new Uri("ms-resource:/Files/Assets/AppBar/icon_zoom_in.png", UriKind.Absolute);
                app_btnZoomIn.Icon = zoom_in_icon;

                BitmapIcon zoom_out_icon = new BitmapIcon();
                zoom_out_icon.UriSource = new Uri("ms-resource:/Files/Assets/AppBar/icon_zoom_out.png", UriKind.Absolute);
                app_btnZoomOut.Icon = zoom_out_icon;

                app_btnZoomIn.Label = "Vergrößern";
                app_btnZoomOut.Label = "Verkleinern";

                app_btnZoomIn.Click += BtnZoomIn_Click;
                app_btnZoomOut.Click += BtnZoomOut_Click;

                cmdBar.PrimaryCommands.Add(app_btnZoomIn);
                cmdBar.PrimaryCommands.Add(app_btnZoomOut);
            }
            else if("barRotate" == type)
            {
                AppBarButton app_btnRotate_left = new AppBarButton();
                AppBarButton app_btnRotate_right = new AppBarButton();

                BitmapIcon rotate_left_icon = new BitmapIcon();
                rotate_left_icon.UriSource = new Uri("ms-resource:/Files/Assets/AppBar/icon_menu_rotate_left.png", UriKind.Absolute);
                app_btnRotate_left.Icon = rotate_left_icon;
                
                BitmapIcon rotate_right_icon = new BitmapIcon();
                rotate_right_icon.UriSource = new Uri("ms-resource:/Files/Assets/AppBar/icon_menu_rotate_right.png", UriKind.Absolute);
                app_btnRotate_right.Icon = rotate_right_icon;

                app_btnRotate_left.Label = "rechts drehen";
                app_btnRotate_right.Label = "links drehen";

                app_btnRotate_left.Click += BtnLeft_OnClick;
                app_btnRotate_right.Click += BtnRight_OnClick;

                cmdBar.PrimaryCommands.Add(app_btnRotate_left);
                cmdBar.PrimaryCommands.Add(app_btnRotate_right);
            }
            else if("barFlip" == type)
            {
                AppBarButton app_btnHorizontal = new AppBarButton();
                AppBarButton app_btnVertical = new AppBarButton();

                BitmapIcon horizontal_icon = new BitmapIcon();
                horizontal_icon.UriSource = new Uri("ms-resource:/Files/Assets/AppBar/icon_menu_flip_horizontal.png", UriKind.Absolute);
                app_btnHorizontal.Icon = horizontal_icon;

                BitmapIcon vertical_icon = new BitmapIcon();
                vertical_icon.UriSource = new Uri("ms-resource:/Files/Assets/AppBar/icon_menu_flip_vertical.png", UriKind.Absolute);
                app_btnVertical.Icon = vertical_icon;

                app_btnHorizontal.Label = "horizontal";
                app_btnVertical.Label = "vertikal";

                app_btnHorizontal.Click += BtnLeft_OnClick;
                app_btnVertical.Click += BtnRight_OnClick;

                cmdBar.PrimaryCommands.Add(app_btnHorizontal);
                cmdBar.PrimaryCommands.Add(app_btnVertical);
            }
            else
            {
                return;
            }
            AppBarButton app_btnTools = new AppBarButton();
            AppBarButton app_btnSave = new AppBarButton();
            AppBarButton app_btnSaveCopy = new AppBarButton();
            AppBarButton app_btnNewPicture = new AppBarButton();
            AppBarButton app_btnLoad = new AppBarButton();
            AppBarButton app_btnFullScreen = new AppBarButton();
            AppBarButton app_btnAbout = new AppBarButton();

            BitmapIcon tools_icon = new BitmapIcon();
            tools_icon.UriSource = new Uri("ms-resource:/Files/Assets/AppBar/menu_tools_.png", UriKind.Absolute);
            app_btnTools.Icon = tools_icon;
            app_btnTools.Label = "Werkzeug";
            app_btnTools.Click += BtnTools_Click;

            app_btnSave.Label = "Speichern";
            app_btnSaveCopy.Label = "Kopie speichern";
            app_btnNewPicture.Label = "New Picture";
            app_btnLoad.Label = "Laden";
            app_btnFullScreen.Label = "Vollbild";
            app_btnAbout.Label = "Über";

            cmdBar.PrimaryCommands.Add(app_btnTools);

            cmdBar.SecondaryCommands.Add(app_btnSave);
            cmdBar.SecondaryCommands.Add(app_btnSaveCopy);
            cmdBar.SecondaryCommands.Add(app_btnNewPicture);
            cmdBar.SecondaryCommands.Add(app_btnLoad);
            cmdBar.SecondaryCommands.Add(app_btnFullScreen);

            BottomAppBar = cmdBar;
            current_appbar = type;
        }

        private void BtnLeft_OnClick(object sender, RoutedEventArgs e)
        {
            if (PocketPaintApplication.GetInstance().ToolCurrent.GetToolType() == ToolType.Rotate)
            {
                var rotateTool = (RotateTool)PocketPaintApplication.GetInstance().ToolCurrent;
                rotateTool.RotateLeft();
            }
            else
                return;

        }

        private void BtnRight_OnClick(object sender, RoutedEventArgs e)
        {
            if (PocketPaintApplication.GetInstance().ToolCurrent.GetToolType() == ToolType.Rotate)
            {
                var rotateTool = (RotateTool)PocketPaintApplication.GetInstance().ToolCurrent;
                rotateTool.RotateRight();
            }
            else
                return;
        }

        void BtnZoomOut_Click(object sender, RoutedEventArgs e)
        {
            MoveZoomTool tool = new MoveZoomTool();
            ScaleTransform scaletransform = new ScaleTransform();
            scaletransform.ScaleX = 0.9;
            scaletransform.ScaleY = 0.9;
            tool.HandleMove(scaletransform);
        }

        void BtnZoomIn_Click(object sender, RoutedEventArgs e )
        {

            MoveZoomTool tool = new MoveZoomTool();
            ScaleTransform scaletransform = new ScaleTransform();
            scaletransform.ScaleX = 1.1;
            scaletransform.ScaleY = 1.1;
            tool.HandleMove(scaletransform);
        }

        /// <summary>
        /// Wird aufgerufen, wenn diese Seite in einem Frame angezeigt werden soll.
        /// </summary>
        /// <param name="e">Ereignisdaten, die beschreiben, wie diese Seite erreicht wurde.
        /// Dieser Parameter wird normalerweise zum Konfigurieren der Seite verwendet.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
        }

        private void ToolChangedHere(ToolBase tool)
        {
            switch (tool.GetToolType())
            {
                case ToolType.Brush:
                case ToolType.Cursor:
                case ToolType.Line:
                    createAppBarAndSwitchAppBarContent("barStandard");
                    break;

                case ToolType.Pipette:
                    createAppBarAndSwitchAppBarContent("barPipette");
                    break;

                case ToolType.Eraser:
                    createAppBarAndSwitchAppBarContent("barEraser");
                    break;

                case ToolType.Move:
                case ToolType.Zoom:
                    createAppBarAndSwitchAppBarContent("barMove");
                    break;

                case ToolType.Crop:
                    // TODO: ApplicationBar = (IApplicationBar)this.Resources["barCrop"];
                    break;

                case ToolType.Rotate:
                    createAppBarAndSwitchAppBarContent("barRotate");
                    break;
                case ToolType.Flip:
                    createAppBarAndSwitchAppBarContent("barFlip");
                    break;
            }
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
                PocketPaintApplication.GetInstance().PaintData.ThicknessSelected = Convert.ToInt32(SliderThickness.Value);
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

        private void SliderThickness_ValueChanged_1(object sender, RangeBaseValueChangedEventArgs e)
        {

        }

    }
}
