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

            Color color = Colors.Orange;
            if ((App.Current as App).ColorValue != null)
            {
                color = (App.Current as App).ColorValue;
            }
            line.Stroke = new SolidColorBrush(color);

            line.StrokeThickness = linethikness;
            //            Rectangle rec = new Rectangle();
            //            rec.Height = linethikness;
            //            rec.Width = linethikness;            
            //            rec.Fill = new SolidColorBrush((App.Current as App).ColorValue);
            //            Canvas.SetTop(rec, currentPoint.Y - linethikness/2);
            //            Canvas.SetLeft(rec, currentPoint.X - linethikness/2);
            //      line.Opacity = 0.1;
            Ellipse elli = new Ellipse();
            elli.Height = linethikness;
            elli.Width = linethikness;
            elli.Fill = new SolidColorBrush((App.Current as App).ColorValue);
            //    elli.Opacity = 0.3;
            Canvas.SetTop(elli, currentPoint.Y - linethikness / 2);
            Canvas.SetLeft(elli, currentPoint.X - linethikness / 2);

            this.ContentPanelCanvas.Children.Add(line);
            this.ContentPanelCanvas.Children.Add(elli);
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

        private void ApplicationBarIconButton_Click(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/ColorChooser.xaml", UriKind.Relative));

        }

        private void MAX_Click(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/DrawingSurface.xaml", UriKind.Relative));
        }
    }
}