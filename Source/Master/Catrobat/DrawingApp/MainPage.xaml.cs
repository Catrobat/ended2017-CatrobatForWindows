using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;

namespace DrawingApp
{
    public partial class MainPage : PhoneApplicationPage
    {
        private Point currentPoint;
        private Point oldPoint;
        private Double linethikness = 8;
        // Constructor
        public MainPage()
        {
            InitializeComponent();
        }
        private void ContentPanelCanvas_MouseMove(object sender, MouseEventArgs e)
        {
            currentPoint = e.GetPosition(this.ContentPanelCanvas);
            Line line = new Line() { X1 = currentPoint.X, Y1 = currentPoint.Y, X2 = oldPoint.X, Y2 = oldPoint.Y };
            line.Stroke = new SolidColorBrush(Colors.Green);
            line.StrokeThickness = linethikness;
            this.ContentPanelCanvas.Children.Add(line);
            oldPoint = currentPoint;
        }
        private void ContentPanelCanvas_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            currentPoint = e.GetPosition(ContentPanelCanvas);
            oldPoint = currentPoint;
        }

        private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            linethikness = ((Slider)sender).Value;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (slider_thickness.Visibility == Visibility.Visible)
                slider_thickness.Visibility = Visibility.Collapsed;
            else
                slider_thickness.Visibility = Visibility.Visible;

        }
    }
}