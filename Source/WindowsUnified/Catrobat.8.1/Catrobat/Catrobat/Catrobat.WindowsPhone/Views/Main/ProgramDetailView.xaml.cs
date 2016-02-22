using System;
using System.ComponentModel;
using Windows.UI.Xaml;
using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.Utilities.Helpers;
using Catrobat.IDE.Core.ViewModels.Main;
using Catrobat.IDE.Core.Resources.Localization;
using Catrobat.Core.Resources.Localization;

namespace Catrobat.IDE.WindowsPhone.Views.Main
{
    public partial class ProgramDetailView
    {
        private readonly ProgramDetailViewModel _viewModel =
            ServiceLocator.ViewModelLocator.ProgramDetailViewModel;

        public ProgramDetailView()
        {
            InitializeComponent();
            CheckSensors();
            //base._viewModel.PropertyChanged += ViewModelOnPropertyChanged;
        }

        private void CheckSensors()
        {
            bool allSensorsWorking = ServiceLocator.SensorService.CheckSensors();

            if (!allSensorsWorking)
            {
                ServiceLocator.NotifictionService.ShowMessageBox(AppResourcesHelper.Get(AppResources.Main_MessageBoxSensorsMissing),
                    AppResourcesHelper.Get(AppResources.Main_NotAllFeaturesSupported), delegate { /* no action */ }, MessageBoxOptions.Ok);
            }
        }

        //private void ViewModelOnPropertyChanged(object sender, 
        //    PropertyChangedEventArgs e)
        //{
        //    if (e.PropertyName == PropertyHelper.GetPropertyName(() =>
        //        _viewModel.IsActivatingLocalProject))
        //    {

        //    }
        //}

        //private bool _isLoaded
        //private void Image_OnLoaded(object sender, RoutedEventArgs e)
        //{
        //    GridProgress.Visibility = Visibility.Collapsed;
        //}

        //void CheckPrograss()
        //{
        //    if (_viewModel.IsActivatingLocalProject && ImageScreenshot)
        //        GridProgress.Visibility = Visibility.Collapsed;
        //}
    }
}
