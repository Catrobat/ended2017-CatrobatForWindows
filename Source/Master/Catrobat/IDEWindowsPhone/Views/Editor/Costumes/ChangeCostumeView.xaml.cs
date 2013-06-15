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

namespace Catrobat.IDEWindowsPhone.Views.Editor.Costumes
{
    public partial class ChangeCostumeView : PhoneApplicationPage
    {
        private readonly ChangeCostumeViewModel _changeCostumeViewModel = ServiceLocator.Current.GetInstance<ChangeCostumeViewModel>();

        ApplicationBarIconButton _btnSave;

        public ChangeCostumeView()
        {
            InitializeComponent();

            BuildApplicationBar();
            (App.Current.Resources["LocalizedStrings"] as LocalizedStrings).PropertyChanged += LanguageChanged;
            _changeCostumeViewModel.PropertyChanged += AddNewCostumeViewModel_OnPropertyChanged;
        }

        protected override void OnBackKeyPress(System.ComponentModel.CancelEventArgs e)
        {
            _changeCostumeViewModel.ResetViewModel();
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            Dispatcher.BeginInvoke(() =>
            {
                TextBoxCostumeName.Focus();
                TextBoxCostumeName.SelectAll();
            });

            _btnSave.IsEnabled = _changeCostumeViewModel.IsCostumeNameValid;
            base.OnNavigatedTo(e);
        }

        private void AddNewCostumeViewModel_OnPropertyChanged(object sender, PropertyChangedEventArgs propertyChangedEventArgs)
        {
            if (propertyChangedEventArgs.PropertyName == "IsCostumeNameValid" && _btnSave != null)
            {
                _btnSave.IsEnabled = _changeCostumeViewModel.IsCostumeNameValid;
            }
        }

        private void TextBoxCostumeName_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            _changeCostumeViewModel.CostumeName = TextBoxCostumeName.Text;
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
            _changeCostumeViewModel.SaveCommand.Execute(null);
        }

        private void ButtonCancel_Click(object sender, EventArgs e)
        {
            _changeCostumeViewModel.CancelCommand.Execute(null);
        }

        private void LanguageChanged(object sender, PropertyChangedEventArgs e)
        {
            BuildApplicationBar();
        }

        #endregion
    }
}
