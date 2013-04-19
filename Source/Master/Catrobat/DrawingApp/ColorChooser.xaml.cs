using System;
using System.Windows;
using System.Windows.Media;
using Microsoft.Phone.Controls;


namespace DrawingApp
{
    public partial class ColorChooser : PhoneApplicationPage
    {

        private Color color_;

        public ColorChooser()
        {
            InitializeComponent();
            color_ = (App.Current as App).ColorValue;
            SelectedColorRectangle.Fill = new SolidColorBrush(color_);
            CheckColorAndSetSelectedColorFontVisible();
        }

        
        private void Pivot_Loaded(object sender, RoutedEventArgs e)
        {
            ColorPicker.Color = color_; 
        }

        private void Cancel_Click(object sender, EventArgs e)
        {
            NavigationService.GoBack();
        }

        private void Done_Click(object sender, EventArgs e)
        {

            (App.Current as App).ColorValue = color_;
            NavigationService.GoBack();
        }

        private void colorPicker1_ColorChanged(object sender, Color color)
        {
            color_ = color;
            SelectedColorRectangle.Fill = new SolidColorBrush(color);
        }

        private void Rectangle_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            SelectedColorRectangle.Fill = ((System.Windows.Shapes.Rectangle)sender).Fill;
            Brush brush = ((System.Windows.Shapes.Rectangle)sender).Fill;
            var colorBrush = brush as SolidColorBrush;
            if (colorBrush != null)
            {
                color_ = colorBrush.Color;
                ColorPicker.Color = color_;
            }
            else //todo mind transparent color!
            {
                color_ = Colors.Transparent;
                ColorPicker.Color = color_;
            }
            CheckColorAndSetSelectedColorFontVisible();
        }

        private void CheckColorAndSetSelectedColorFontVisible()
        {
            SelectedColorBoxLabel.Foreground = color_.Equals(Colors.White) || color_.Equals(Colors.Transparent) ? new SolidColorBrush(Colors.Black) : new SolidColorBrush(Colors.White);
        }

        private void colorPicker1_Loaded(object sender, RoutedEventArgs e)
        {
            ColorPicker.Color = color_; 
        }
    }
 
}