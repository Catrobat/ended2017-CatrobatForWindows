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
            /*// TODO: if (!DesignerProperties.IsInDesignTool)
            {
                BtnSelectedColor.Background = PocketPaintApplication.GetInstance().PaintData.ColorSelected;
                PocketPaintApplication.GetInstance().PaintData.ColorChanged += ColorChangedHere;
                PocketPaintApplication.GetInstance().PaintData.ToolCurrentChanged += ToolChangedHere;
                PocketPaintApplication.GetInstance().ApplicationBarTop = this;

                BtnUndo.Click += PocketPaintApplication.GetInstance().ApplicationBarListener.BtnUndo_Click;
                BtnRedo.Click += PocketPaintApplication.GetInstance().ApplicationBarListener.BtnRedo_Click;
                BtnSelectedColor.Click += PocketPaintApplication.GetInstance().ApplicationBarListener.BtnColBtnSelectedColor_OnClick;
                BtnMoveScreen.Click += PocketPaintApplication.GetInstance().ApplicationBarListener.BtnMoveScreen_OnClick;
            }*/
        }
        private void ColorChangedHere(SolidColorBrush color)
        {
            // System.Diagnostics.Debug.WriteLine("ColorChangedHere called: " + color.Color.ToString());
            if (color.Color != Colors.Transparent)
            {
                // TODO: BtnSelectedColor.Background = color;
            }
            else
            {
                // TODO: BtnSelectedColor.Background = new SolidColorBrush(Colors.Transparent);
            }
        }

        private void ToolChangedHere(ToolBase tool)
        {
            ImageBrush ib = null;
            if (PocketPaintApplication.GetInstance().ToolWhileMoveTool != null)
            {
                ib = new ImageBrush
                {
                    ImageSource =
                        new BitmapImage(GetToolImageUri(PocketPaintApplication.GetInstance().ToolWhileMoveTool.GetToolType())),
                    Opacity = 0.2
                };
            }


            switch (tool.GetToolType())
            {
                case ToolType.Brush:
                    // TODO: BtnMoveScreen.ImageSource = new BitmapImage(GetToolImageUri(ToolType.Brush));
                    // TODO: BtnMoveScreen.Background = new ImageBrush
                    /* TODO: {
                        ImageSource =
                            new BitmapImage(GetToolImageUri(ToolType.Move)),
                        Opacity = 0.2
                    };*/
                    break;
                case ToolType.Eraser:
                    // TODO: BtnMoveScreen.ImageSource = new BitmapImage(GetToolImageUri(ToolType.Eraser));
                    // TODO: BtnMoveScreen.Background = new ImageBrush
                    {
                        /*TODO: ImageSource =
                            new BitmapImage(GetToolImageUri(ToolType.Move)),
                        Opacity = 0.2*/
                    };
                    break;
                case ToolType.Move:
                    // TODO: BtnMoveScreen.ImageSource = new BitmapImage(GetToolImageUri(ToolType.Move));
                    // TODO: BtnMoveScreen.Background = ib;
                    break;
                case ToolType.Zoom:
                    // TODO: BtnMoveScreen.ImageSource = new BitmapImage(GetToolImageUri(ToolType.Zoom));
                    // TODO: BtnMoveScreen.Background = ib;
                    break;
                case ToolType.Pipette:
                    // TODO: BtnMoveScreen.ImageSource = new BitmapImage(GetToolImageUri(ToolType.Pipette));
                    // TODO: BtnMoveScreen.Background = new ImageBrush
                    /* TODO: {
                        ImageSource =
                            new BitmapImage(GetToolImageUri(ToolType.Move)),
                        Opacity = 0.2
                    }; */
                    break;
                case ToolType.Rotate:
                    // TODO: BtnMoveScreen.ImageSource = new BitmapImage(GetToolImageUri(ToolType.Rotate));
                    /* TODO: BtnMoveScreen.Background = new ImageBrush
                    {
                        ImageSource =
                            new BitmapImage(GetToolImageUri(ToolType.Move)),
                        Opacity = 0.2
                    };*/
                    break;
                case ToolType.Line:
                    /*BtnMoveScreen.ImageSource = new BitmapImage(GetToolImageUri(ToolType.Line));
                    BtnMoveScreen.Background = new ImageBrush
                    {
                        ImageSource =
                            new BitmapImage(GetToolImageUri(ToolType.Move)),
                        Opacity = 0.2
                    };*/
                    break;
                case ToolType.Flip:
                    /*BtnMoveScreen.ImageSource = new BitmapImage(GetToolImageUri(ToolType.Flip));
                    BtnMoveScreen.Background = new ImageBrush
                    {
                        ImageSource =
                            new BitmapImage(GetToolImageUri(ToolType.Move)),
                        Opacity = 0.2
                    };*/
                    break;
                default:
                    // TODO: BtnMoveScreen.ImageSource = null;
                    // TODO: BtnMoveScreen.Background = null;
                    break;
            }
        }

        public void BtnSelectedColorVisible(bool enable)
        {
            // TODO: BtnSelectedColor.IsEnabled = enable;
            // TODO: object ellipseTransparence = LayoutRoot.FindName("ellipseTransparence");
            /* TODO: if(ellipseTransparence != null)
            {
                if (enable == true)
                {
                    ((Ellipse)ellipseTransparence).Visibility = Visibility.Visible;
                }
                else
                {
                    ((Ellipse)ellipseTransparence).Visibility = Visibility.Collapsed;
                };
            }*/
        }

        private Uri GetToolImageUri(ToolType tooltype)
        {
            switch (tooltype)
            {
                case ToolType.Brush:
                    return new Uri("/Assets/ToolMenu/icon_menu_brush.png", UriKind.Relative);
                case ToolType.Eraser:
                    return new Uri("/Assets/ToolMenu/icon_menu_eraser.png", UriKind.Relative);
                case ToolType.Move:
                    return new Uri("/Assets/ToolMenu/icon_menu_move.png", UriKind.Relative);
                case ToolType.Zoom:
                    return new Uri("/Assets/ToolMenu/icon_menu_zoom.png", UriKind.Relative);
                case ToolType.Pipette:
                    return new Uri("/Assets/ToolMenu/icon_menu_pipette.png", UriKind.Relative);
                case ToolType.Rotate:
                    return new Uri("/Assets/ToolMenu/icon_menu_rotate_left.png", UriKind.Relative);
                case ToolType.Line:
                    return new Uri("/Assets/ToolMenu/icon_menu_straight_line.png", UriKind.Relative);
                case ToolType.Flip:
                    return new Uri("/Assets/ToolMenu/icon_menu_flip_horizontal.png", UriKind.Relative);

                default:
                    return null;
            }
        }
    }
}
