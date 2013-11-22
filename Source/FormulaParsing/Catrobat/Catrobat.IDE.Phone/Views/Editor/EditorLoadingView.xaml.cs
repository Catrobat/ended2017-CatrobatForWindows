using System.ComponentModel;
using System.Windows;
using System.Windows.Navigation;
using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.ViewModel;
using Catrobat.IDE.Core.ViewModel.Editor;
using Catrobat.IDE.Core.ViewModel.Editor.Sprites;
using Microsoft.Phone.Controls;

namespace Catrobat.IDE.Phone.Views.Editor
{
    public partial class EditorLoadingView : PhoneApplicationPage
    {
        private readonly EditorLoadingViewModel _viewModel =
            ((ViewModelLocator)ServiceLocator.ViewModelLocator).EditorLoadingViewModel;

        public EditorLoadingView()
        {
            InitializeComponent();
            Loaded += OnLoaded;
        }

        private void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
            ServiceLocator.NavigationService.NavigateTo<SpritesViewModel>();
            ServiceLocator.NavigationService.RemoveBackEntry();
        }

        protected override void OnBackKeyPress(CancelEventArgs e)
        {
            _viewModel.GoBackCommand.Execute(null);
            e.Cancel = true;
            base.OnBackKeyPress(e);
        }
    }
}