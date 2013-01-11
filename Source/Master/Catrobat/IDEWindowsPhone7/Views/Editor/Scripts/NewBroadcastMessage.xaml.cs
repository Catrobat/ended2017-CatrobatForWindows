using System;
using System.Windows.Controls;
using Catrobat.Core.Objects;
using Catrobat.Core.Objects.Bricks;
using Catrobat.IDECommon.Resources;
using Catrobat.IDECommon.Resources.Editor;
using Catrobat.IDEWindowsPhone7.ViewModel;
using IDEWindowsPhone7;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System.ComponentModel;

namespace Catrobat.IDEWindowsPhone7.Views.Editor.Scripts
{
  public partial class NewBroadcastMessage : PhoneApplicationPage
  {
    ApplicationBarIconButton btnSave;

    public NewBroadcastMessage()
    {
      InitializeComponent();
      BuildApplicationBar();
      (App.Current.Resources["LocalizedStrings"] as LocalizedStrings).PropertyChanged += LanguageChanged;

      Dispatcher.BeginInvoke(() =>
      {
        ApplicationBar.IsVisible = true;
        btnSave.IsEnabled = false;
      });
    }

    private void BuildApplicationBar()
    {
      ApplicationBar = new ApplicationBar();

      btnSave = new ApplicationBarIconButton(new Uri("/Content/Images/ApplicationBar/dark/appbar.check.rest.png", UriKind.Relative));
      btnSave.Text = EditorResources.ButtonSave;
      btnSave.Click += btnSave_Click;
      ApplicationBar.Buttons.Add(btnSave);

      ApplicationBarIconButton btnCancel = new ApplicationBarIconButton(new Uri("/Content/Images/ApplicationBar/dark/appbar.cancel.rest.png", UriKind.Relative));
      btnCancel.Text = EditorResources.ButtonCancel;
      btnCancel.Click += btnCancel_Click;
      ApplicationBar.Buttons.Add(btnCancel);
    }

    private void LanguageChanged(object sender, PropertyChangedEventArgs e)
    {
      BuildApplicationBar();
    }

    private void btnSave_Click(object sender, EventArgs e)
    {
      ((EditorViewModel)DataContext).AddBroadcastMessageAction(txtName.Text);
      var broadcastObject = ((EditorViewModel)DataContext).BroadcastObject;

      if (broadcastObject is BroadcastScript)
        (broadcastObject as BroadcastScript).ReceivedMessage = txtName.Text;
      if (broadcastObject is BroadcastBrick)
        (broadcastObject as BroadcastBrick).BroadcastMessage = txtName.Text;
      if (broadcastObject is BroadcastWaitBrick)
        (broadcastObject as BroadcastWaitBrick).BroadcastMessage = txtName.Text;

      NavigationService.GoBack();
    }

    private void btnCancel_Click(object sender, EventArgs e)
    {
      NavigationService.GoBack();
    }

    private void txtName_TextChanged(object sender, TextChangedEventArgs e)
    {
      if (txtName.Text != "")
        btnSave.IsEnabled = true;
      else
        btnSave.IsEnabled = false;
    }
  }
}