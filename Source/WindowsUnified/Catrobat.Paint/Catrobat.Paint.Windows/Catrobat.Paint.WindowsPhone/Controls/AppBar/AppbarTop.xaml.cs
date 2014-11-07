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
using Windows.UI.Xaml.Shapes;

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
            setAppBarTopLayout();
        }
        private void setAppBarTopLayout()
        {
            double width_multiplicator = PocketPaintApplication.GetInstance().size_width_multiplication;
            double height_multiplicator = PocketPaintApplication.GetInstance().size_width_multiplication;
            GrdLayoutRoot.Width *= width_multiplicator;
            GrdLayoutRoot.Height *= height_multiplicator;

            foreach (Object obj in GrdLayoutRoot.Children)
            {
                if (obj.GetType() == typeof(AppBarButton))
                {
                    ((AppBarButton)obj).Height *= height_multiplicator;
                    ((AppBarButton)obj).Width *= width_multiplicator;

                    ((AppBarButton)obj).Margin = new Thickness(
                                            ((AppBarButton)obj).Margin.Left * width_multiplicator,
                                            ((AppBarButton)obj).Margin.Top * height_multiplicator,
                                            ((AppBarButton)obj).Margin.Right * width_multiplicator,
                                            ((AppBarButton)obj).Margin.Bottom * height_multiplicator);
                }
                else if (obj.GetType() == typeof(Button))
                {
                    ((Button)obj).Height *= height_multiplicator;
                    ((Button)obj).Width *= width_multiplicator;

                    ((Button)obj).Margin = new Thickness(
                                            ((Button)obj).Margin.Left * width_multiplicator,
                                            ((Button)obj).Margin.Top * height_multiplicator,
                                            ((Button)obj).Margin.Right * width_multiplicator,
                                            ((Button)obj).Margin.Bottom * height_multiplicator);
                }
                else if (obj.GetType() == typeof(Ellipse))
                {
                    ((Ellipse)obj).Height *= height_multiplicator;
                    ((Ellipse)obj).Width *= width_multiplicator;

                    ((Ellipse)obj).Margin = new Thickness(
                                            ((Ellipse)obj).Margin.Left * width_multiplicator,
                                            ((Ellipse)obj).Margin.Top * height_multiplicator,
                                            ((Ellipse)obj).Margin.Right * width_multiplicator,
                                            ((Ellipse)obj).Margin.Bottom * height_multiplicator);
                }
                else if (obj.GetType() == typeof(Rectangle))
                {
                    ((Rectangle)obj).Height *= height_multiplicator;
                    ((Rectangle)obj).Width *= width_multiplicator;

                    ((Rectangle)obj).Margin = new Thickness(
                                            ((Rectangle)obj).Margin.Left * width_multiplicator,
                                            ((Rectangle)obj).Margin.Top * height_multiplicator,
                                            ((Rectangle)obj).Margin.Right * width_multiplicator,
                                            ((Rectangle)obj).Margin.Bottom * height_multiplicator);
                }
            }

            ImgTransparence.Width *= width_multiplicator;
            ImgTransparence.Height *= height_multiplicator;
            ImgTransparence.Margin = new Thickness(
                                        ImgTransparence.Margin.Left * width_multiplicator,
                                        ImgTransparence.Margin.Top * height_multiplicator,
                                        ImgTransparence.Margin.Right * width_multiplicator,
                                        ImgTransparence.Margin.Bottom * height_multiplicator);

            RecSelectedColor.Width *= width_multiplicator;
            RecSelectedColor.Height *= height_multiplicator;
            RecSelectedColor.Margin = new Thickness(
                                        RecSelectedColor.Margin.Left * width_multiplicator,
                                        RecSelectedColor.Margin.Top * height_multiplicator,
                                        RecSelectedColor.Margin.Right * width_multiplicator,
                                        RecSelectedColor.Margin.Bottom * height_multiplicator);

            GrdBtnSelectedColor.Height *= height_multiplicator;
            GrdBtnSelectedColor.Width *= width_multiplicator;

            BtnSelectedColor.Background = PocketPaintApplication.GetInstance().PaintData.colorSelected;
            PocketPaintApplication.GetInstance().PaintData.colorChanged += ColorChangedHere;
            PocketPaintApplication.GetInstance().PaintData.toolCurrentChanged += ToolChangedHere;
            PocketPaintApplication.GetInstance().AppbarTop = this;

            BtnUndo.Click += PocketPaintApplication.GetInstance().ApplicationBarListener.BtnUndo_Click;
            BtnRedo.Click += PocketPaintApplication.GetInstance().ApplicationBarListener.BtnRedo_Click;
            BtnSelectedColor.Click += PocketPaintApplication.GetInstance().ApplicationBarListener.BtnColor_Click;
            //btnMoveScreen.Click += PocketPaintApplication.GetInstance().ApplicationBarListener.BtnMoveScreen_OnClick;
            BtnToolSelection.Click += PocketPaintApplication.GetInstance().ApplicationBarListener.BtnMoveScreenEllipse_OnClick;
            BtnUndo.IsEnabled = false;
            BtnRedo.IsEnabled = false;
        }
        private void ColorChangedHere(SolidColorBrush color)
        {
            if (color.Color != Colors.Transparent)
            {
                RecSelectedColor.Fill = color;
            }
            else
            {
                RecSelectedColor.Fill = new SolidColorBrush(Colors.Transparent);
            }
        }

        public void ToolChangedHere(ToolBase tool)
        {
            ImageBrush img_front = new ImageBrush();
            ImageBrush img_behind = new ImageBrush();
            img_behind.ImageSource = new BitmapImage(
                   new Uri("ms-resource:/Files/Assets/ToolMenu/icon_menu_move.png", UriKind.Absolute));

            Visibility currentStateOfGridThicknessControl = PocketPaintApplication.GetInstance().GrdThicknessControlState;
            PocketPaintApplication.GetInstance().PaintingAreaView.GrdThicknessControlVisibility = Visibility.Collapsed;

            if(tool.GetToolType() == ToolType.Eraser && PocketPaintApplication.GetInstance().isBrushEraser == true)
            {
                tool = new BrushTool();
            }
            else
            {
                if (PocketPaintApplication.GetInstance().isToolPickerUsed)
                {
                    PocketPaintApplication.GetInstance().isBrushEraser = false;
                }
            }

            switch (tool.GetToolType())
            {
                case ToolType.Brush:
                    img_front.ImageSource = new BitmapImage(GetToolImageUri(ToolType.Brush));
                    PocketPaintApplication.GetInstance().PaintingAreaView.GrdThicknessControlVisibility
                        = currentStateOfGridThicknessControl;
                    break;
                case ToolType.Crop:
                    img_front.ImageSource = new BitmapImage(GetToolImageUri(ToolType.Crop));
                    PocketPaintApplication.GetInstance().PaintingAreaView.GrdThicknessControlVisibility
                        = currentStateOfGridThicknessControl;
                    break;
                case ToolType.Cursor:
                    img_front.ImageSource = new BitmapImage(GetToolImageUri(ToolType.Cursor));
                    break;
                case ToolType.Ellipse:
                    img_front.ImageSource = new BitmapImage(GetToolImageUri(ToolType.Ellipse));
                    break;
                case ToolType.Eraser:
                    img_front.ImageSource = new BitmapImage(GetToolImageUri(ToolType.Eraser));
                    break;
                case ToolType.Fill:
                    img_front.ImageSource = new BitmapImage(GetToolImageUri(ToolType.Fill));
                    PocketPaintApplication.GetInstance().PaintingAreaView.GrdThicknessControlVisibility
                        = currentStateOfGridThicknessControl;
                    break;
                case ToolType.Flip:
                    img_front.ImageSource = new BitmapImage(GetToolImageUri(ToolType.Flip));
                    break;
                case ToolType.ImportPng:
                    img_front.ImageSource = new BitmapImage(GetToolImageUri(ToolType.ImportPng));
                    PocketPaintApplication.GetInstance().PaintingAreaView.GrdThicknessControlVisibility
                        = currentStateOfGridThicknessControl;
                    break;
                case ToolType.Line:
                    img_front.ImageSource = new BitmapImage(GetToolImageUri(ToolType.Line));
                    PocketPaintApplication.GetInstance().PaintingAreaView.GrdThicknessControlVisibility
                        = currentStateOfGridThicknessControl;
                    break;
                case ToolType.Move:
                    img_front.ImageSource = new BitmapImage(GetToolImageUri(ToolType.Move));
                    img_behind.ImageSource = new BitmapImage(
                     GetToolImageUri(PocketPaintApplication.GetInstance().ToolWhileMoveTool.GetToolType()));
                    break;
                case ToolType.Pipette:
                    img_front.ImageSource = new BitmapImage(GetToolImageUri(ToolType.Pipette));
                    break;
                case ToolType.Rect:
                    img_front.ImageSource = new BitmapImage(GetToolImageUri(ToolType.Rect));
                    break;
                case ToolType.Rotate:
                    img_front.ImageSource = new BitmapImage(GetToolImageUri(ToolType.Rotate));
                    break;
                case ToolType.Zoom:
                    img_front.ImageSource = new BitmapImage(GetToolImageUri(ToolType.Zoom));
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
            BtnSelectedColor.IsEnabled = enable;
        }

        private Uri GetToolImageUri(ToolType tooltype)
        { 
            switch (tooltype)
            {
                case ToolType.Brush:
                    return new Uri("ms-resource:/Files/Assets/ToolMenu/icon_menu_brush.png", UriKind.Absolute);
                case ToolType.Crop:
                    return new Uri("ms-resource:/Files/Assets/ToolMenu/icon_menu_crop.png", UriKind.Absolute);
                case ToolType.Cursor:
                    return new Uri("ms-resource:/Files/Assets/ToolMenu/icon_menu_cursor.png", UriKind.Absolute);
                case ToolType.Ellipse:
                    return new Uri("ms-resource:/Files/Assets/ToolMenu/icon_menu_ellipse.png", UriKind.Absolute);
                case ToolType.Eraser:
                    return new Uri("ms-resource:/Files/Assets/ToolMenu/icon_menu_eraser.png", UriKind.Absolute);
                case ToolType.Flip:
                    return new Uri("ms-resource:/Files/Assets/ToolMenu/icon_menu_flip_horizontal.png", UriKind.Absolute);
                case ToolType.ImportPng:
                    return new Uri("ms-resource:/Files/Assets/ToolMenu/icon_menu_import_image.png", UriKind.Absolute);
                case ToolType.Line:
                    return new Uri("ms-resource:/Files/Assets/ToolMenu/icon_menu_straight_line.png", UriKind.Absolute);
                case ToolType.Move:
                    return new Uri("ms-resource:/Files/Assets/ToolMenu/icon_menu_move.png", UriKind.Absolute);
                case ToolType.Pipette:
                    return new Uri("ms-resource:/Files/Assets/ToolMenu/icon_menu_pipette.png", UriKind.Absolute);
                case ToolType.Rect:
                    return new Uri("ms-resource:/Files/Assets/ToolMenu/icon_menu_rectangle.png", UriKind.Absolute);
                case ToolType.Rotate:
                    return new Uri("ms-resource:/Files/Assets/ToolMenu/icon_menu_rotate_left.png", UriKind.Absolute);
                case ToolType.Zoom:
                    return new Uri("ms-resource:/Files/Assets/ToolMenu/icon_menu_zoom.png", UriKind.Absolute);
                default:
                    return null;
            }
        }

        public bool BtnRedoEnable
        {
            get { return BtnRedo.IsEnabled; }
            set { BtnRedo.IsEnabled = value; }
        }

        public bool BtnUndoEnable
        {
            get { return BtnUndo.IsEnabled; }
            set { BtnUndo.IsEnabled = value; }
        }
    }
}
