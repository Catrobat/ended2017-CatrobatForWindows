using System;
using System.Windows;
using System.Windows.Controls;
using Catrobat.IDECommon.Resources.Main;
using Catrobat.IDEWindowsPhone.Misc;
using Catrobat.IDEWindowsPhone.ViewModel;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

namespace Catrobat.IDEWindowsPhone.Views.Service
{
  public partial class UploadProjectPage : PhoneApplicationPage
  {
    private UploadProjectViewModel _viewModel;
    private ApplicationBarIconButton _uploadButton;

    public UploadProjectPage()
    {
      InitializeComponent();
      _viewModel = DataContext as UploadProjectViewModel;
    }

    private void LocalizeApplicationBar()
    {
      _uploadButton = (ApplicationBarIconButton)ApplicationBar.Buttons[0];
      _uploadButton.Text = MainResources.ButtonUpload;
      ((ApplicationBarIconButton)ApplicationBar.Buttons[1]).Text = MainResources.ButtonCancel;
    }

    private void PhoneApplicationPage_Loaded(object sender, RoutedEventArgs e)
    {
      LocalizeApplicationBar();
      _uploadButton.IsEnabled = true;
    }

    private void Upload_Click(object sender, EventArgs e)
    {
      _viewModel.UploadCommand.Execute(sender);
      _uploadButton.IsEnabled = false;
      Navigation.NavigateBack();
    }

    private void Cancel_Click(object sender, EventArgs e)
    {
      Navigation.NavigateBack();
    }

    private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
    {
      // Cannot be implemented using MVVM - the Appbar is not XAML compatible.
      _uploadButton.IsEnabled = (sender as TextBox).Text != "";

      // Hack for Windows Phone 7
      (sender as TextBox).GetBindingExpression(TextBox.TextProperty).UpdateSource();
    }
  }
}