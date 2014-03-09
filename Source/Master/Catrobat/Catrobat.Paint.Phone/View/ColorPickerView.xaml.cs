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
        }

        private void PaintDataOnColorChanged(SolidColorBrush color)
        {
            SelectedColorRectangle.Fill = color;
            if (color.Color == Colors.White || color.Color == Color.FromArgb(255, 173, 216, 230) ||
                color.Color == Color.FromArgb(255, 255, 255, 0) || color.Color == Color.FromArgb(255, 255, 173, 251) ||
                color.Color == Color.FromArgb(255, 196, 169, 169) || color.Color == Color.FromArgb(255, 138, 234, 57))

                BtnSelectedColor.Foreground = new SolidColorBrush(Colors.Black);
            else
            {
                BtnSelectedColor.Foreground = new SolidColorBrush(Colors.White);
            }
            if (color.Color == Colors.Transparent)
            {
                BtnSelectedColor.Background  = new ImageBrush
                {
                    ImageSource = new BitmapImage(new Uri("Assets/checkeredbgWXGA.png", UriKind.Relative))
                };
 
                BtnSelectedColor.Foreground = new SolidColorBrush(Colors.Black);
            }
            else
            {
                BtnSelectedColor.Background = color;
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
            NavigationService.GoBack();
        }
    }
}