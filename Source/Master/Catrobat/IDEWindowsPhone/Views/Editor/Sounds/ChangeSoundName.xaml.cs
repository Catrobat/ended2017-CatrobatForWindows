using System;
using System.Windows.Controls;
using Catrobat.Core.Objects.Sounds;
using Catrobat.IDECommon.Resources;
using Catrobat.IDECommon.Resources.Editor;
using IDEWindowsPhone;
using Microsoft.Phone.Controls;
using System.ComponentModel;
using Microsoft.Phone.Shell;
using Catrobat.IDEWindowsPhone.ViewModel;
using Microsoft.Practices.ServiceLocation;

namespace Catrobat.IDEWindowsPhone.Views.Editor.Sounds
{
  public partial class ChangeSoundName : PhoneApplicationPage
  {
    private EditorViewModel _editorViewModel = ServiceLocator.Current.GetInstance<EditorViewModel>();

    ApplicationBarIconButton btnSave;
    
    public ChangeSoundName()
    {
      InitializeComponent();
      
      BuildApplicationBar();
      (App.Current.Resources["LocalizedStrings"] as LocalizedStrings).PropertyChanged += LanguageChanged;

      Dispatcher.BeginInvoke(() =>
      {
        txtName.Text = _editorViewModel.SelectedSound.Name;
        txtName.Focus();
        txtName.SelectAll();
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
      _editorViewModel.SelectedSound.Name = txtName.Text;
      NavigationService.GoBack();
    }

    private void btnCancel_Click(object sender, EventArgs e)
    {
      _editorViewModel.SelectedSound.Name = null;
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
