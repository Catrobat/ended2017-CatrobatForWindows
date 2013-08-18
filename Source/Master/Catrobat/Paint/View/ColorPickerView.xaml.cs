using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Navigation;
using Catrobat.Paint.ViewModel;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

namespace Catrobat.Paint.View
{
    public partial class ColorPickerView : PhoneApplicationPage
    {
        public ColorPickerView()
        {
            InitializeComponent();
        }

        private void ColorChanged(object sender, Color color)
        {
            SolidColorBrush _colorBrush = new SolidColorBrush();
            _colorBrush.Color = color;
            
            ((ColorPickerViewModel)DataContext).SelectColorValue.Execute(_colorBrush);

        }
    }
}