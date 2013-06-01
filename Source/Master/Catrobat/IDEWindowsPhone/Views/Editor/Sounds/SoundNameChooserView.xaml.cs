using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Catrobat.IDECommon.Resources.Editor;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

namespace Catrobat.IDEWindowsPhone.Views.Editor.Sounds
{
  public partial class SoundNameChooserView : PhoneApplicationPage
  {
    public SoundNameChooserView()
    {
      InitializeComponent();
    }

    private void BuildApplicationBar()
    {
      ApplicationBar = new ApplicationBar();

      var buttonSave = new ApplicationBarIconButton(new Uri("/Content/Images/ApplicationBar/dark/appbar.check.rest.png", UriKind.Relative));
      buttonSave.Text = EditorResources.ButtonSave;
      //buttonSave.Click += (sender, args) => AddTile();
      ApplicationBar.Buttons.Add(buttonSave);

      var buttonCancel = new ApplicationBarIconButton(new Uri("/Content/Images/ApplicationBar/dark/appbar.cancel.rest.png", UriKind.Relative));
      buttonCancel.Text = EditorResources.ButtonCancel;
      buttonCancel.Click += (sender, args) => NavigationService.GoBack();
      ApplicationBar.Buttons.Add(buttonCancel);
    }
  }
}