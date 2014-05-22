using System;
using System.ComponentModel;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Catrobat.Paint.Phone.Tool;

namespace Catrobat.Paint.Phone.Controls.AppBar
{
    public partial class ApplicationBarTop : UserControl
    {
        public ApplicationBarTop()
        {
            InitializeComponent();

            if (!DesignerProperties.IsInDesignTool)
            {
                BtnSelectedColor.Background = PocketPaintApplication.GetInstance().PaintData.ColorSelected;
                PocketPaintApplication.GetInstance().PaintData.ColorChanged += ColorChangedHere;
                PocketPaintApplication.GetInstance().PaintData.ToolCurrentChanged += ToolChangedHere;


                BtnUndo.Click += PocketPaintApplication.GetInstance().ApplicationBarListener.BtnUndo_Click;
                BtnRedo.Click += PocketPaintApplication.GetInstance().ApplicationBarListener.BtnRedo_Click;
                BtnSelectedColor.Click += PocketPaintApplication.GetInstance().ApplicationBarListener.BtnColBtnSelectedColor_OnClick;
                BtnMoveScreen.Click += PocketPaintApplication.GetInstance().ApplicationBarListener.BtnMoveScreen_OnClick;
            }
        }

        /// <summary>
        /// testing EventHandler
        /// </summary>
        /// <param name="color"></param>
        private void ColorChangedHere(SolidColorBrush color)
        {
            // System.Diagnostics.Debug.WriteLine("ColorChangedHere called: " + color.Color.ToString());
            BtnSelectedColor.Background = color;
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
                    BtnMoveScreen.ImageSource = new BitmapImage(GetToolImageUri(ToolType.Brush));
                    BtnMoveScreen.Background = new ImageBrush
                    {
                        ImageSource =
                            new BitmapImage(GetToolImageUri(ToolType.Move)),
                        Opacity = 0.2
                    };
                    break;
                case ToolType.Eraser:
                    BtnMoveScreen.ImageSource = new BitmapImage(GetToolImageUri(ToolType.Eraser));
                    BtnMoveScreen.Background = new ImageBrush
                    {
                        ImageSource =
                            new BitmapImage(GetToolImageUri(ToolType.Move)),
                        Opacity = 0.2
                    };
                    break;
                case ToolType.Move:
                    BtnMoveScreen.ImageSource = new BitmapImage(GetToolImageUri(ToolType.Move));
                    BtnMoveScreen.Background = ib;
                    break;
                case ToolType.Zoom:
                    BtnMoveScreen.ImageSource = new BitmapImage(GetToolImageUri(ToolType.Zoom));
                    BtnMoveScreen.Background = ib;
                    break;
                case ToolType.Pipette:
                    BtnMoveScreen.ImageSource = new BitmapImage(GetToolImageUri(ToolType.Pipette));
                    BtnMoveScreen.Background = new ImageBrush
                    {
                        ImageSource =
                            new BitmapImage(GetToolImageUri(ToolType.Move)),
                        Opacity = 0.2
                    };
                    break;
                case ToolType.Rotate:
                    BtnMoveScreen.ImageSource = new BitmapImage(GetToolImageUri(ToolType.Rotate));
                    BtnMoveScreen.Background = new ImageBrush
                    {
                        ImageSource =
                            new BitmapImage(GetToolImageUri(ToolType.Move)),
                        Opacity = 0.2
                    };
                    break;
                case ToolType.Line:
                    BtnMoveScreen.ImageSource = new BitmapImage(GetToolImageUri(ToolType.Line));
                    BtnMoveScreen.Background = new ImageBrush
                    {
                        ImageSource =
                            new BitmapImage(GetToolImageUri(ToolType.Move)),
                        Opacity = 0.2
                    };
                    break;
                case ToolType.Flip:
                    BtnMoveScreen.ImageSource = new BitmapImage(GetToolImageUri(ToolType.Flip));
                    BtnMoveScreen.Background = new ImageBrush
                    {
                        ImageSource =
                            new BitmapImage(GetToolImageUri(ToolType.Move)),
                        Opacity = 0.2
                    };
                    break;
                default:
                    BtnMoveScreen.ImageSource = null;
                    BtnMoveScreen.Background = null;
                    break;
            }
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
