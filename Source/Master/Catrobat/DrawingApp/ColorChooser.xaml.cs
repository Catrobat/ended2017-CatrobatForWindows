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

            this.DataContext = _photos;
            _photos.Add(new Photo() { ImageUrl = "/Toolkit.Content/ApplicationBar.Cancel.png" });
            _photos.Add(new Photo() { ImageUrl = "/Toolkit.Content/ApplicationBar.Cancel.png" });
        }
 
        public ObservableCollection<Photo>  _photos = new ObservableCollection<Photo>();

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
    }
    public class Photo
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string ImageUrl { get; set; }
    }
}