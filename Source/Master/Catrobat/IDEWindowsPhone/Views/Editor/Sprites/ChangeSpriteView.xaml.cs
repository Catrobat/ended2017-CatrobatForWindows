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
      private readonly ChangeSpriteViewModel _viewModel = ServiceLocator.Current.GetInstance<ChangeSpriteViewModel>();

    public ChangeSpriteView()
    {
      InitializeComponent();

      Dispatcher.BeginInvoke(() =>
      {
          TextBoxSpriteName.Focus();
          TextBoxSpriteName.SelectAll();
      });
    }

    protected override void OnNavigatedFrom(System.Windows.Navigation.NavigationEventArgs e)
    {
        _viewModel.ResetViewModelCommand.Execute(null);
        base.OnNavigatedFrom(e);
    }

    private void TextBoxSpriteName_OnTextChanged(object sender, TextChangedEventArgs e)
    {
        _viewModel.SpriteName = TextBoxSpriteName.Text;
    }
  }
}