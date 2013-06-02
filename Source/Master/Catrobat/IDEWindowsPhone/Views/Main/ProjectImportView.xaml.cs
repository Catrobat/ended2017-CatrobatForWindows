using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Catrobat.IDEWindowsPhone.Misc;
using GalaSoft.MvvmLight.Messaging;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

namespace Catrobat.IDEWindowsPhone.Views.Main
{
  public partial class ProjectImportView : PhoneApplicationPage
  {
    public ProjectImportView()
    {
      InitializeComponent();
      Loaded += OnLoaded;
    }

    private async void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
    {
      var fileToken = string.Empty;
      if (NavigationContext.QueryString.TryGetValue("fileToken", out fileToken))
      {
        // TODO: update UI
      }
    }
  }
}