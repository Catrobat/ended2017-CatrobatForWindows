using System;
using System.Windows;
using Catrobat.IDEWindowsPhone.Misc;
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
      Navigation.NavigateTo(typeof(AudioLibrary));
    }

    private void btnRecorder_Click(object sender, RoutedEventArgs e)
    {
      Navigation.NavigateTo(typeof(SoundRecorderView));
    }
  }
}
