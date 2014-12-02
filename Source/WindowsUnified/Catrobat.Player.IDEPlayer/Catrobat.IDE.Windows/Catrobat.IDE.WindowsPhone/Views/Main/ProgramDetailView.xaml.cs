using System;
using System.ComponentModel;
using Windows.UI.Xaml;
using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.Utilities.Helpers;
using Catrobat.IDE.Core.ViewModels.Main;

namespace Catrobat.IDE.WindowsPhone.Views.Main
{
    public partial class ProgramDetailView
    {
        private readonly ProgramDetailViewModel _viewModel =
            ServiceLocator.ViewModelLocator.ProgramDetailViewModel;

        public ProgramDetailView()
        {
            InitializeComponent();

            //base._viewModel.PropertyChanged += ViewModelOnPropertyChanged;
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