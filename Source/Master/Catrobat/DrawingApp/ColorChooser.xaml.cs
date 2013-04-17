using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System.Collections.ObjectModel;

namespace DrawingApp
{
    public partial class ColorChooser : PhoneApplicationPage
    {
        public ColorChooser()
        {
            InitializeComponent();
 
        }
 

        private void Pivot_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void Cancel_Click(object sender, EventArgs e)
        {
            NavigationService.GoBack();
        }

        private void Done_Click(object sender, EventArgs e)
        {

            //TODO save chosen color
            NavigationService.GoBack();
        }

        private void colorPicker1_ColorChanged(object sender, System.Windows.Media.Color color)
        {

        }
    }
 
}