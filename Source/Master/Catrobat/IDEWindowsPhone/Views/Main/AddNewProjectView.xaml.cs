using System;
using System.Windows;
using System.Windows.Controls;
using Catrobat.IDECommon.Resources.Main;
using Catrobat.IDEWindowsPhone.Misc;
using Catrobat.IDEWindowsPhone.ViewModel;
using Catrobat.IDEWindowsPhone.ViewModel.Main;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Microsoft.Practices.ServiceLocation;
using IDEWindowsPhone;
using Catrobat.IDECommon.Resources;
using System.ComponentModel;
using Catrobat.IDECommon.Resources.Editor;
using System.Windows.Navigation;

namespace Catrobat.IDEWindowsPhone.Views.Main
{
    public partial class AddNewProjectView : PhoneApplicationPage
    {
        private ApplicationBarIconButton _btnSave;
        private readonly AddNewProjectViewModel _viewModel = ServiceLocator.Current.GetInstance<AddNewProjectViewModel>();

        public AddNewProjectView()
        {
            InitializeComponent();

            BuildApplicationBar();
            (App.Current.Resources["LocalizedStrings"] as LocalizedStrings).PropertyChanged += LanguageChanged;
            _viewModel.PropertyChanged += AddNewProjectViewModel_OnPropertyChanged;
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            _viewModel.ResetViewModel();
            base.OnNavigatedFrom(e);
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            Dispatcher.BeginInvoke(() =>
            {
                TextBoxProjectName.Focus();
                TextBoxProjectName.SelectAll();
            });

            _btnSave.IsEnabled = _viewModel.IsProjectNameValid;
            base.OnNavigatedTo(e);
        }

        private void AddNewProjectViewModel_OnPropertyChanged(object sender, PropertyChangedEventArgs propertyChangedEventArgs)
        {
            if (propertyChangedEventArgs.PropertyName == "IsProjectNameValid" && _btnSave != null)
            {
                _btnSave.IsEnabled = _viewModel.IsProjectNameValid;
            }
        }

        private void TextBoxProjectName_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            _viewModel.ProjectName = TextBoxProjectName.Text;
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
            _viewModel.SaveCommand.Execute(null);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            _viewModel.CancelCommand.Execute(null);
        }

        #endregion
    }
}