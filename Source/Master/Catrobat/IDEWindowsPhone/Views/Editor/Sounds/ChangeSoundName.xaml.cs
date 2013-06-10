using System;
using System.Windows.Controls;
using Catrobat.Core.Objects.Sounds;
using Catrobat.IDECommon.Resources;
using Catrobat.IDECommon.Resources.Editor;
using Catrobat.IDEWindowsPhone.Misc;
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
    private readonly EditorViewModel _editorViewModel = ServiceLocator.Current.GetInstance<EditorViewModel>();
    private ApplicationBarIconButton _buttonSave;
    
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

      _buttonSave = new ApplicationBarIconButton(new Uri("/Content/Images/ApplicationBar/dark/appbar.check.rest.png", UriKind.Relative));
      _buttonSave.Text = EditorResources.ButtonSave;
      _buttonSave.Click += btnSave_Click;
      ApplicationBar.Buttons.Add(_buttonSave);

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
      Navigation.NavigateBack();
    }

    private void btnCancel_Click(object sender, EventArgs e)
    {
      _editorViewModel.SelectedSound.Name = null;
      Navigation.NavigateBack();
    }

    private void txtName_TextChanged(object sender, TextChangedEventArgs e)
    {
      if (txtName.Text != "")
        _buttonSave.IsEnabled = true;
      else
        _buttonSave.IsEnabled = false;
    }
  }
}
