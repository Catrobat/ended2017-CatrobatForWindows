using System;
using System.Windows.Controls;
using Catrobat.Core.Objects.Costumes;
using Catrobat.IDECommon.Resources;
using Catrobat.IDECommon.Resources.Editor;
using IDEWindowsPhone;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System.ComponentModel;
using Microsoft.Practices.ServiceLocation;
using Catrobat.IDEWindowsPhone.ViewModel;

namespace Catrobat.IDEWindowsPhone.Views.Editor.Costumes
{
  public partial class CostumeNameChooserView : PhoneApplicationPage
  {
    private readonly AddNewCostumeViewModel _addNewCostumeViewModel = ServiceLocator.Current.GetInstance<AddNewCostumeViewModel>();

    ApplicationBarIconButton _btnSave;

    public CostumeNameChooserView()
    {
      InitializeComponent();
      
      BuildApplicationBar();
      (App.Current.Resources["LocalizedStrings"] as LocalizedStrings).PropertyChanged += LanguageChanged;
      _addNewCostumeViewModel.PropertyChanged += AddNewCostumeViewModel_OnPropertyChanged;

      Dispatcher.BeginInvoke(() =>
      {
        TextBoxCostumeName.Focus();
        TextBoxCostumeName.SelectAll();
      });
    }

    private void AddNewCostumeViewModel_OnPropertyChanged(object sender, PropertyChangedEventArgs propertyChangedEventArgs)
    {
      if (propertyChangedEventArgs.PropertyName == "IsCostumeNameValid" && _btnSave != null)
      {
        _btnSave.IsEnabled = _addNewCostumeViewModel.IsCostumeNameValid;
      }
    }

    private void TextBoxCostumeName_OnTextChanged(object sender, TextChangedEventArgs e)
    {
      _addNewCostumeViewModel.CostumeName = TextBoxCostumeName.Text;
    }

    private void BuildApplicationBar()
    {
      ApplicationBar = new ApplicationBar();

      _btnSave = new ApplicationBarIconButton(new Uri("/Content/Images/ApplicationBar/dark/appbar.check.rest.png", UriKind.Relative));
      _btnSave.Text = EditorResources.ButtonSave;
      _btnSave.Click += (sender, args) => _addNewCostumeViewModel.SaveEvent();
      ApplicationBar.Buttons.Add(_btnSave);

      ApplicationBarIconButton btnCancel = new ApplicationBarIconButton(new Uri("/Content/Images/ApplicationBar/dark/appbar.cancel.rest.png", UriKind.Relative));
      btnCancel.Text = EditorResources.ButtonCancel;
      btnCancel.Click += (sender, args) => _addNewCostumeViewModel.CancelEvent();
      ApplicationBar.Buttons.Add(btnCancel);
    }

    private void LanguageChanged(object sender, PropertyChangedEventArgs e)
    {
      BuildApplicationBar();
    }
  }
}
