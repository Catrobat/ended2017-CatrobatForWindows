using System;
using System.Windows;
using System.Windows.Controls;
using Catrobat.IDECommon.Resources.Main;
using Catrobat.IDEWindowsPhone.ViewModel;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

namespace Catrobat.IDEWindowsPhone.Views.Main
{
  public partial class UploadProjectLoginPage : PhoneApplicationPage
  {
    private UploadProjectLoginViewModel _viewModel;

    public UploadProjectLoginPage()
    {
      InitializeComponent();

      // Get ViewModel
      _viewModel = DataContext as UploadProjectLoginViewModel;
      _viewModel.NavigationCallback = navigationCallback;
    }

    private void LocalizeApplicationBar()
    {
      ((ApplicationBarIconButton)ApplicationBar.Buttons[0]).Text = MainResources.ButtonLoginRegister;
      ((ApplicationBarIconButton)ApplicationBar.Buttons[1]).Text = MainResources.ButtonPasswordForgotten;
    }

    private void PhoneApplicationPage_Loaded(object sender, RoutedEventArgs e)
    {
      LocalizeApplicationBar();
    }

    private void LoginRegister_Click(object sender, EventArgs e)
    {
      NavigationService.RemoveBackEntry();
      _viewModel.LoginCommand.Execute(sender);
    }

    private void Forgotten_Click(object sender, EventArgs e)
    {
      _viewModel.ForgottenCommand.Execute(sender);
    }

    private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
    {
      // Hack for Windows Phone 7
      (sender as TextBox).GetBindingExpression(TextBox.TextProperty).UpdateSource();
    }

    private void navigationCallback()
    {
      Action action = () => NavigationService.Navigate(new Uri("/MetroCatIDE;component/Views/Main/UploadProjectPage.xaml", UriKind.Relative));
      Dispatcher.BeginInvoke(action);
    }

    private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
    {
      // Hack for Windows Phone 7
      (sender as PasswordBox).GetBindingExpression(PasswordBox.PasswordProperty).UpdateSource();
    }
  }
}