using System;
using System.Windows.Controls;
using Catrobat.Core.Objects;
using Catrobat.IDECommon.Resources;
using Catrobat.IDECommon.Resources.Editor;
using Catrobat.IDEWindowsPhone.Misc;
using IDEWindowsPhone;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System.ComponentModel;
using Microsoft.Practices.ServiceLocation;
using Catrobat.IDEWindowsPhone.ViewModel.Editor.Sprites;

namespace Catrobat.IDEWindowsPhone.Views.Editor.Sprites
{
  public partial class ChangeSpriteView : PhoneApplicationPage
  {
      private readonly ChangeSpriteViewModel _changeSpriteViewModel = ServiceLocator.Current.GetInstance<ChangeSpriteViewModel>();

    ApplicationBarIconButton _btnSave;

    public ChangeSpriteView()
    {
      InitializeComponent();
      
      BuildApplicationBar();
      (App.Current.Resources["LocalizedStrings"] as LocalizedStrings).PropertyChanged += LanguageChanged;
      _changeSpriteViewModel.PropertyChanged += AddNewSpriteViewModel_OnPropertyChanged;
    }

    protected override void OnBackKeyPress(System.ComponentModel.CancelEventArgs e)
    {
        _changeSpriteViewModel.ResetViewModel();
    }

    protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
    {
        Dispatcher.BeginInvoke(() =>
        {
            TextBoxSpriteName.Focus();
            TextBoxSpriteName.SelectAll();
        });

        _btnSave.IsEnabled = _changeSpriteViewModel.IsSpriteNameValid;
        base.OnNavigatedTo(e);
    }

    private void AddNewSpriteViewModel_OnPropertyChanged(object sender, PropertyChangedEventArgs propertyChangedEventArgs)
    {
        if (propertyChangedEventArgs.PropertyName == "IsSpriteNameValid" && _btnSave != null)
        {
            _btnSave.IsEnabled = _changeSpriteViewModel.IsSpriteNameValid;
        }
    }

    private void TextBoxSpriteName_OnTextChanged(object sender, TextChangedEventArgs e)
    {
        _changeSpriteViewModel.SpriteName = TextBoxSpriteName.Text;
    }

    #region Appbar

    private void BuildApplicationBar()
    {
      ApplicationBar = new ApplicationBar();

      _btnSave = new ApplicationBarIconButton(new Uri("/Content/Images/ApplicationBar/dark/appbar.save.rest.png", UriKind.Relative));
      _btnSave.Text = EditorResources.ButtonSave;
      _btnSave.Click += btnSave_Click;
      ApplicationBar.Buttons.Add(_btnSave);

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
        _changeSpriteViewModel.SaveCommand.Execute(null);
    }

    private void btnCancel_Click(object sender, EventArgs e)
    {
        _changeSpriteViewModel.CancelCommand.Execute(null);
    }

    #endregion
  }
}