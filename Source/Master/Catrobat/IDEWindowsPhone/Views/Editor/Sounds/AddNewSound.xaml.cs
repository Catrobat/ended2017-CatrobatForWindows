using System;
using System.Windows;
using Microsoft.Phone.Controls;

namespace Catrobat.IDEWindowsPhone.Views.Editor.Sounds
{
  public partial class AddNewSound : PhoneApplicationPage
  {
    public AddNewSound()
    {
      InitializeComponent();
    }

    private void btnMediaLibrary_Click(object sender, RoutedEventArgs e)
    {
      NavigationService.Navigate(new Uri("/Views/Editor/Sounds/AudioLibrary.xaml", UriKind.Relative));
    }

    private void btnRecorder_Click(object sender, RoutedEventArgs e)
    {
      NavigationService.Navigate(new Uri("/Views/Editor/Sounds/RecorderLauncher.xaml", UriKind.Relative));
    }
  }
}
