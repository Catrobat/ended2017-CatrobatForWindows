using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;

namespace Catrobat.Paint.Phone.View
{
    public partial class ColorPickerView : PhoneApplicationPage
    {
        public ColorPickerView()
        {

            InitializeComponent();
            SelectedColorRectangle.Fill = PocketPaintApplication.GetInstance().PaintData.ColorSelected;
            PocketPaintApplication.GetInstance().PaintData.ColorChanged += PaintDataOnColorChanged;

            
            if (PocketPaintApplication.GetInstance().PaintData.ColorSelected != null)
            {
                Color selected_color = ((SolidColorBrush)PocketPaintApplication.GetInstance().PaintData.ColorSelected).Color;
                int reference_color = (selected_color.R + selected_color.G + selected_color.B) / 3;
                if (reference_color <= 128 && selected_color.A > 5)
                {
                    BtnSelectedColor.Foreground = new SolidColorBrush(Colors.White);
                }
                else
                {
                    BtnSelectedColor.Foreground = new SolidColorBrush(Colors.Black);
                }

                if (selected_color == Colors.Transparent)
                {
                    btnTransparence.Visibility = Visibility.Visible;
                    SelectedColorRectangle.Visibility = Visibility.Collapsed;
                    BtnSelectedColor.Background = new SolidColorBrush(Colors.Transparent);
                }
            }            
        }

        private void PaintDataOnColorChanged(SolidColorBrush color)
        {
            SelectedColorRectangle.Fill = color;
            Color current_color = ((SolidColorBrush)color).Color;
            int reference_color = (current_color.R + current_color.G + current_color.B) / 3;
            if (reference_color <= 128 && current_color.A > 5)
            {
                BtnSelectedColor.Foreground = new SolidColorBrush(Colors.White);
            }
            else
            {
                BtnSelectedColor.Foreground = new SolidColorBrush(Colors.Black);
            }

            if (color.Color == Colors.Transparent)
            {
                //BtnSelectedColor.Background = new SolidColorBrush(Colors.Transparent);
                /*SelectedColorRectangle.Fill = new ImageBrush {
                    ImageSource = new BitmapImage(new Uri("Assets/ColorPicker/btn_checkeredbg.png", UriKind.Relative)),
                    Stretch = Stretch.None
                };*/ 
                //ImgBtnTransparence.Visibility = Visibility.Visible;
                
               // BitmapImage imageSource = new BitmapImage(new Uri("Assets/checkeredbgWXGA.png", UriKind.Relative));
                //BtnSelectedColor.Background = new SolidColorBrush(Colors.Blue);
                /*BtnSelectedColor.Background = new ImageBrush {
                    ImageSource = new BitmapImage(new Uri("Assets/checkeredbgWXGA.png", UriKind.Relative)),
                    Stretch = Stretch.None
                };*/
               /* BtnSelectedColor.Background  = new ImageBrush
                {
                    ImageSource = new BitmapImage(new Uri("Assets/checkeredbgWXGA.png", UriKind.Relative))
                };*/
                btnTransparence.Visibility = Visibility.Visible;
                SelectedColorRectangle.Visibility = Visibility.Collapsed;
                BtnSelectedColor.Background = new SolidColorBrush(Colors.Transparent);

            }
            else
            {
                BtnSelectedColor.Background = color;
                btnTransparence.Visibility = Visibility.Collapsed;
                SelectedColorRectangle.Visibility = Visibility.Visible;
            }

        }


        private void ColorChangedN(object sender, RoutedEventArgs routedEventArgs)
        {
            var colorBrush = new SolidColorBrush(Colors.Black);
            if ((((Rectangle) sender).Fill) is ImageBrush)
            {
                colorBrush.Color = Colors.Transparent;

            }
            else
            {
                colorBrush = (SolidColorBrush)((Rectangle)sender).Fill;

            }
            PocketPaintApplication.GetInstance().PaintData.ColorSelected = colorBrush;

        }

        private void ColorChanged(object sender, Color color)
        {
            var colorBrush = new SolidColorBrush(color);
            PocketPaintApplication.GetInstance().PaintData.ColorSelected = colorBrush;
        }

        private void BtnSelectedColor_OnClick(object sender, RoutedEventArgs e)
        {
            Color current_color = ((SolidColorBrush)PocketPaintApplication.GetInstance().PaintData.ColorSelected).Color;
            if( current_color == Colors.Transparent)
            {
                PocketPaintApplication.GetInstance().SwitchTool(Catrobat.Paint.Phone.Tool.ToolType.Eraser);
            }
            else
            {
                if (PocketPaintApplication.GetInstance().ToolCurrent.GetToolType() == Catrobat.Paint.Phone.Tool.ToolType.Eraser)
                {
                    PocketPaintApplication.GetInstance().SwitchTool(Catrobat.Paint.Phone.Tool.ToolType.Brush);
                }
            }
            
            NavigationService.GoBack();
        }
    }
}