using System;
using System.Windows;
using System.Windows.Controls;
using Catrobat.IDECommon.Resources.Main;
using Catrobat.IDEWindowsPhone.ViewModel;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

namespace Catrobat.IDEWindowsPhone.Views.Main
{
  public partial class AddNewProject : PhoneApplicationPage
  {
    ApplicationBarIconButton _saveButton;
    AddNewProjectViewModel _viewModel;

    public AddNewProject()
    {
      InitializeComponent();
    }

    private void ProjectNameTextBox_Click(object sender, TextChangedEventArgs e)
    {
      // Cannot be implemented using MVVM - the Appbar is not XAML compatible.
      _saveButton.IsEnabled = (sender as TextBox).Text != "";

      // Hack for Windows Phone 7
      (sender as TextBox).GetBindingExpression(TextBox.TextProperty).UpdateSource();
    }

    private void SaveButton_Click(object sender, EventArgs e)
    {
      _viewModel.SaveCommand.Execute(sender);

      NavigationService.GoBack();
    }

    private void CancelButton_Click(object sender, EventArgs e)
    {
      NavigationService.GoBack();
    }

    private void PhoneApplicationPage_Loaded(object sender, RoutedEventArgs e)
    {
      // Get ViewModel
      _viewModel = DataContext as AddNewProjectViewModel;

      ApplicationBarIconButton button = ApplicationBar.Buttons[0] as ApplicationBarIconButton;
      if (button != null)
      {
        _saveButton = button;
        _saveButton.IsEnabled = false;
        button.Text = MainResources.ButtonSave;
      }
      
      button = ApplicationBar.Buttons[1] as ApplicationBarIconButton;
      if (button != null)
      {
        button.Text = MainResources.ButtonCancel;
      }
    }
    
  }
}