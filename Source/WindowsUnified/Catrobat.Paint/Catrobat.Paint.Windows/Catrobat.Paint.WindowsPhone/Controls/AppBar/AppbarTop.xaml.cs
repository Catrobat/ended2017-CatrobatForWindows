using Catrobat.Paint.Phone;
using Catrobat.Paint.Phone.Tool;
using Catrobat.Paint.WindowsPhone.Tool;
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
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// Die Elementvorlage "Leere Seite" ist unter http://go.microsoft.com/fwlink/?LinkID=390556 dokumentiert.

namespace Catrobat.Paint.WindowsPhone.Controls.AppBar
{
    /// <summary>
    /// Eine leere Seite, die eigenständig verwendet werden kann oder auf die innerhalb eines Rahmens navigiert werden kann.
    /// </summary>
    public sealed partial class AppbarTop
    {
        public AppbarTop()
        {
            this.InitializeComponent();
            //if (!DesignerProperties.IsInDesignTool)
            {
                btnSelectedColor.Background = PocketPaintApplication.GetInstance().PaintData.ColorSelected;
                PocketPaintApplication.GetInstance().PaintData.ColorChanged += ColorChangedHere;
                PocketPaintApplication.GetInstance().PaintData.ToolCurrentChanged += ToolChangedHere;
                PocketPaintApplication.GetInstance().AppbarTop = this;

                btnUndo.Click += PocketPaintApplication.GetInstance().ApplicationBarListener.BtnUndo_Click;
                btnRedo.Click += PocketPaintApplication.GetInstance().ApplicationBarListener.BtnRedo_Click;
                btnSelectedColor.Click += PocketPaintApplication.GetInstance().ApplicationBarListener.BtnColBtnSelectedColor_OnClick;
                //btnMoveScreen.Click += PocketPaintApplication.GetInstance().ApplicationBarListener.BtnMoveScreen_OnClick;
                ellipseTool_front.PointerEntered += PocketPaintApplication.GetInstance().ApplicationBarListener.BtnMoveScreenEllipse_OnClick;
                ellipseTool_behind.PointerEntered += PocketPaintApplication.GetInstance().ApplicationBarListener.BtnMoveScreenEllipse_OnClick;
                btnUndo.IsEnabled = false;
                btnRedo.IsEnabled = false;               
                
            }
        }
        private void ColorChangedHere(SolidColorBrush color)
        {
            // System.Diagnostics.Debug.WriteLine("ColorChangedHere called: " + color.Color.ToString());
            if (color.Color != Colors.Transparent)
            {
                btnSelectedColor.Background = color;
            }
            else
            {
                btnSelectedColor.Background = new SolidColorBrush(Colors.Transparent);
            }
        }

        public void ToolChangedHere(ToolBase tool)
        {
            ImageBrush img_front = new ImageBrush();
            ImageBrush img_behind = new ImageBrush();
            img_behind.ImageSource = new BitmapImage(
                   new Uri("ms-resource:/Files/Assets/ToolMenu/icon_menu_move.png", UriKind.Absolute));

            switch (tool.GetToolType())
            {
                case ToolType.Brush:
                    img_front.ImageSource = new BitmapImage(GetToolImageUri(ToolType.Brush));                  
                    break;
                case ToolType.Eraser:
                    img_front.ImageSource = new BitmapImage(GetToolImageUri(ToolType.Eraser));
                    break;
                case ToolType.Move:
                    img_front.ImageSource = new BitmapImage(GetToolImageUri(ToolType.Move));
                    img_behind.ImageSource = new BitmapImage(
                     GetToolImageUri(PocketPaintApplication.GetInstance().ToolWhileMoveTool.GetToolType()));
                    break;
                case ToolType.Zoom:
                    img_front.ImageSource = new BitmapImage(GetToolImageUri(ToolType.Zoom));
                    break;
                case ToolType.Pipette:
                    img_front.ImageSource = new BitmapImage(GetToolImageUri(ToolType.Pipette));
                    break;
                case ToolType.Rotate:
                    img_front.ImageSource = new BitmapImage(GetToolImageUri(ToolType.Rotate));
                    break;
                case ToolType.Line:
                    img_front.ImageSource = new BitmapImage(GetToolImageUri(ToolType.Line));
                    break;
                case ToolType.Flip:
                    img_front.ImageSource = new BitmapImage(GetToolImageUri(ToolType.Flip));
                    break;
                default:
                    // TODO: BtnMoveScreen.ImageSource = null;
                    // TODO: BtnMoveScreen.Background = null;
                    break;
            }

            ellipseTool_behind.Opacity = 0.1;
            ellipseTool_behind.Fill = img_behind;
            ellipseTool_front.Fill = img_front;
        }

        public void BtnSelectedColorVisible(bool enable)
        {
            btnSelectedColor.IsEnabled = enable;
            if (enable == true)
            {
               rectTransparence.Visibility = Visibility.Visible;
            }
            else
            {
               rectTransparence.Visibility = Visibility.Collapsed;
            }
        }

        private Uri GetToolImageUri(ToolType tooltype)
        { 
            switch (tooltype)
            {
                case ToolType.Brush:
                    return new Uri("ms-resource:/Files/Assets/ToolMenu/icon_menu_brush.png", UriKind.Absolute);
                case ToolType.Eraser:
                    return new Uri("ms-resource:/Files/Assets/ToolMenu/icon_menu_eraser.png", UriKind.Absolute);
                case ToolType.Move:
                    return new Uri("ms-resource:/Files/Assets/ToolMenu/icon_menu_move.png", UriKind.Absolute);
                case ToolType.Zoom:
                    return new Uri("ms-resource:/Files/Assets/ToolMenu/icon_menu_zoom.png", UriKind.Absolute);
                case ToolType.Pipette:
                    return new Uri("ms-resource:/Files/Assets/ToolMenu/icon_menu_pipette.png", UriKind.Absolute);
                case ToolType.Rotate:
                    return new Uri("ms-resource:/Files/Assets/ToolMenu/icon_menu_rotate_left.png", UriKind.Absolute);
                case ToolType.Line:
                    return new Uri("ms-resource:/Files/Assets/ToolMenu/icon_menu_straight_line.png", UriKind.Absolute);
                case ToolType.Flip:
                    return new Uri("ms-resource:/Files/Assets/ToolMenu/icon_menu_flip_horizontal.png", UriKind.Absolute);

                default:
                    return null;
            }
        }

        public bool BtnRedoEnable
        {
            get { return btnRedo.IsEnabled; }
            set { btnRedo.IsEnabled = value; }
        }

        public bool BtnUndoEnable
        {
            get { return btnUndo.IsEnabled; }
            set { btnUndo.IsEnabled = value; }
        }

        private void ellipseTool_PointerEntered(object sender, PointerRoutedEventArgs e)
        {

        } 
    }
}
