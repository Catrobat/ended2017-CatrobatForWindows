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
using Catrobat.IDEWindowsPhone.Misc;
using Catrobat.Core;
using Catrobat.Core.Objects;
using Catrobat.IDEWindowsPhone.ViewModel.Editor.Costumes;
using System.Windows.Navigation;

namespace Catrobat.IDEWindowsPhone.Views.Editor.Costumes
{
    public partial class ChangeCostumeView : PhoneApplicationPage
    {
        private readonly ChangeCostumeViewModel _viewModel = ServiceLocator.Current.GetInstance<ChangeCostumeViewModel>();

        ApplicationBarIconButton _btnSave;

        public ChangeCostumeView()
        {
            InitializeComponent();

            BuildApplicationBar();
            (App.Current.Resources["LocalizedStrings"] as LocalizedStrings).PropertyChanged += LanguageChanged;
            _viewModel.PropertyChanged += AddNewCostumeViewModel_OnPropertyChanged;
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            _viewModel.ResetViewModel();
            base.OnNavigatedFrom(e);
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            Dispatcher.BeginInvoke(() =>
            {
                TextBoxCostumeName.Focus();
                TextBoxCostumeName.SelectAll();
            });

            _btnSave.IsEnabled = _viewModel.IsCostumeNameValid;
            base.OnNavigatedTo(e);
        }

        private void AddNewCostumeViewModel_OnPropertyChanged(object sender, PropertyChangedEventArgs propertyChangedEventArgs)
        {
            if (propertyChangedEventArgs.PropertyName == "IsCostumeNameValid" && _btnSave != null)
            {
                _btnSave.IsEnabled = _viewModel.IsCostumeNameValid;
            }
        }

        private void TextBoxCostumeName_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            _viewModel.CostumeName = TextBoxCostumeName.Text;
        }

        #region AppBar

        private void BuildApplicationBar()
        {
            ApplicationBar = new ApplicationBar();

            _btnSave = new ApplicationBarIconButton(new Uri("/Content/Images/ApplicationBar/dark/appbar.save.rest.png", UriKind.Relative));
            _btnSave.Text = EditorResources.ButtonSave;
            _btnSave.Click += ButtonSave_Click;
            ApplicationBar.Buttons.Add(_btnSave);

            ApplicationBarIconButton btnCancel = new ApplicationBarIconButton(new Uri("/Content/Images/ApplicationBar/dark/appbar.cancel.rest.png", UriKind.Relative));
            btnCancel.Text = EditorResources.ButtonCancel;
            btnCancel.Click += ButtonCancel_Click;
            ApplicationBar.Buttons.Add(btnCancel);
        }

        private void ButtonSave_Click(object sender, EventArgs e)
        {
            _viewModel.SaveCommand.Execute(null);
        }

        private void ButtonCancel_Click(object sender, EventArgs e)
        {
            _viewModel.CancelCommand.Execute(null);
        }

        private void LanguageChanged(object sender, PropertyChangedEventArgs e)
        {
            BuildApplicationBar();
        }

        #endregion
    }
}
